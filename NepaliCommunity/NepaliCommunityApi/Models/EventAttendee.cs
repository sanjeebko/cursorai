using System.ComponentModel.DataAnnotations;

namespace NepaliCommunityApi.Models;

public class EventAttendee
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    public bool IsConfirmed { get; set; } = false;

    public DateTime? ConfirmedAt { get; set; }

    // Navigation properties
    public virtual Event Event { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}