using System.ComponentModel.DataAnnotations;

namespace NepaliCommunityApi.Models;

public class Event
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public DateTime EventDate { get; set; }
    
    [StringLength(200)]
    public string? Location { get; set; }
    
    [StringLength(500)]
    public string? Address { get; set; }
    
    public int OrganizerId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public int MaxAttendees { get; set; } = 0; // 0 means unlimited
    
    public int CurrentAttendees { get; set; } = 0;
    
    [StringLength(50)]
    public string? EventType { get; set; } // Cultural, Social, Educational, etc.
    
    // Navigation properties
    public virtual User Organizer { get; set; } = null!;
    public virtual ICollection<EventAttendee> Attendees { get; set; } = new List<EventAttendee>();
} 