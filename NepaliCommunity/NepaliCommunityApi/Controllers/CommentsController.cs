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
public class CommentsController : ControllerBase
{
    private readonly NepaliCommunityContext _context;

    public CommentsController(NepaliCommunityContext context)
    {
        _context = context;
    }

    [HttpGet("post/{postId}")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments(int postId)
    {
        var comments = await _context.Comments
            .Include(c => c.Author)
            .Include(c => c.Replies)
                .ThenInclude(r => r.Author)
            .Where(c => c.PostId == postId && c.ParentCommentId == null && c.IsActive)
            .OrderByDescending(c => c.CreatedAt)
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
                    .OrderBy(r => r.CreatedAt)
                    .ToList()
            })
            .ToListAsync();

        return Ok(comments);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CommentDto>> CreateComment(CreateCommentDto createCommentDto)
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

        // Verify post exists
        var post = await _context.Posts.FindAsync(createCommentDto.PostId);
        if (post == null)
        {
            return NotFound("Post not found");
        }

        // If this is a reply, verify parent comment exists
        if (createCommentDto.ParentCommentId.HasValue)
        {
            var parentComment = await _context.Comments.FindAsync(createCommentDto.ParentCommentId.Value);
            if (parentComment == null || parentComment.PostId != createCommentDto.PostId)
            {
                return BadRequest("Invalid parent comment");
            }
        }

        var comment = new Comment
        {
            Content = createCommentDto.Content,
            PostId = createCommentDto.PostId,
            AuthorId = userId,
            ParentCommentId = createCommentDto.ParentCommentId,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        var author = await _context.Users.FindAsync(userId);
        var commentDto = new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            PostId = comment.PostId,
            AuthorId = comment.AuthorId,
            AuthorName = $"{author!.FirstName} {author.LastName}",
            ParentCommentId = comment.ParentCommentId,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            Replies = new List<CommentDto>()
        };

        return CreatedAtAction(nameof(GetComments), new { postId = comment.PostId }, commentDto);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateComment(int id, UpdateCommentDto updateCommentDto)
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

        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        if (comment.AuthorId != userId)
        {
            return Forbid();
        }

        comment.Content = updateCommentDto.Content;
        comment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        if (comment.AuthorId != userId)
        {
            return Forbid();
        }

        // Soft delete - mark as inactive
        comment.IsActive = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }
} 