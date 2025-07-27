using System.ComponentModel.DataAnnotations;

namespace NepaliCommunityApi.Models;

public class Post
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    public int AuthorId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsPublished { get; set; } = true;

    public int ViewCount { get; set; } = 0;

    public int LikeCount { get; set; } = 0;

    // Navigation properties
    public virtual User Author { get; set; } = null!;
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}