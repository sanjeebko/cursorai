using System.ComponentModel.DataAnnotations;

namespace NepaliCommunityApi.DTOs;

public class CreateEventDto
{
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

    public int MaxAttendees { get; set; } = 0;

    [StringLength(50)]
    public string? EventType { get; set; }
}

public class UpdateEventDto
{
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

    public int MaxAttendees { get; set; } = 0;

    [StringLength(50)]
    public string? EventType { get; set; }
}

public class EventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public string? Location { get; set; }
    public string? Address { get; set; }
    public int OrganizerId { get; set; }
    public string OrganizerName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public int MaxAttendees { get; set; }
    public int CurrentAttendees { get; set; }
    public string? EventType { get; set; }
    public bool IsUserRegistered { get; set; } = false;
}

public class EventDetailDto : EventDto
{
    public List<EventAttendeeDto> Attendees { get; set; } = new List<EventAttendeeDto>();
}

public class EventAttendeeDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; }
    public bool IsConfirmed { get; set; }
}