using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NepaliCommunityApi.Data;
using NepaliCommunityApi.DTOs;
using NepaliCommunityApi.Models;
using System.Security.Claims;

namespace NepaliCommunityApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly NepaliCommunityContext _context;

    public PostsController(NepaliCommunityContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = _context.Posts
            .Include(p => p.Author)
            .Where(p => p.IsPublished)
            .OrderByDescending(p => p.CreatedAt);

        var totalCount = await query.CountAsync();
        var posts = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                AuthorId = p.AuthorId,
                AuthorName = $"{p.Author.FirstName} {p.Author.LastName}",
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                ViewCount = p.ViewCount,
                LikeCount = p.LikeCount,
                CommentCount = p.Comments.Count,
                IsPublished = p.IsPublished
            })
            .ToListAsync();

        Response.Headers["X-Total-Count"] = totalCount.ToString();
        Response.Headers["X-Page"] = page.ToString();
        Response.Headers["X-Page-Size"] = pageSize.ToString();

        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDetailDto>> GetPost(int id)
    {
        var post = await _context.Posts
            .Include(p => p.Author)
            .Include(p => p.Comments)
                .ThenInclude(c => c.Author)
            .Include(p => p.Comments)
                .ThenInclude(c => c.Replies)
                    .ThenInclude(r => r.Author)
            .FirstOrDefaultAsync(p => p.Id == id && p.IsPublished);

        if (post == null)
        {
            return NotFound();
        }

        // Increment view count
        post.ViewCount++;
        await _context.SaveChangesAsync();

        var postDto = new PostDetailDto
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            AuthorId = post.AuthorId,
            AuthorName = $"{post.Author.FirstName} {post.Author.LastName}",
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            ViewCount = post.ViewCount,
            LikeCount = post.LikeCount,
            CommentCount = post.Comments.Count,
            IsPublished = post.IsPublished,
            Comments = post.Comments
                .Where(c => c.ParentCommentId == null && c.IsActive)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    PostId = c.PostId,
                    AuthorId = c.AuthorId,
                    AuthorName = $"{c.Author.FirstName} {c.Author.LastName}",
                    ParentCommentId = c.ParentCommentId,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    Replies = c.Replies
                        .Where(r => r.IsActive)
                        .Select(r => new CommentDto
                        {
                            Id = r.Id,
                            Content = r.Content,
                            PostId = r.PostId,
                            AuthorId = r.AuthorId,
                            AuthorName = $"{r.Author.FirstName} {r.Author.LastName}",
                            ParentCommentId = r.ParentCommentId,
                            CreatedAt = r.CreatedAt,
                            UpdatedAt = r.UpdatedAt,
                            Replies = new List<CommentDto>()
                        })
                        .ToList()
                })
                .ToList()
        };

        return Ok(postDto);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<PostDto>> CreatePost(CreatePostDto createPostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var post = new Post
        {
            Title = createPostDto.Title,
            Content = createPostDto.Content,
            AuthorId = userId,
            CreatedAt = DateTime.UtcNow,
            IsPublished = true
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        var author = await _context.Users.FindAsync(userId);
        var postDto = new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            AuthorId = post.AuthorId,
            AuthorName = $"{author!.FirstName} {author.LastName}",
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            ViewCount = post.ViewCount,
            LikeCount = post.LikeCount,
            CommentCount = 0,
            IsPublished = post.IsPublished
        };

        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, postDto);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdatePost(int id, UpdatePostDto updatePostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        if (post.AuthorId != userId)
        {
            return Forbid();
        }

        post.Title = updatePostDto.Title;
        post.Content = updatePostDto.Content;
        post.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeletePost(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        if (post.AuthorId != userId)
        {
            return Forbid();
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}