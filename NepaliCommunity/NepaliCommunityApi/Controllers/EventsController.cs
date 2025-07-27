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
public class EventsController : ControllerBase
{
    private readonly NepaliCommunityContext _context;

    public EventsController(NepaliCommunityContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        var currentUserId = userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId) ? userId : 0;

        var query = _context.Events
            .Include(e => e.Organizer)
            .Where(e => e.IsActive && e.EventDate >= DateTime.UtcNow)
            .OrderBy(e => e.EventDate);

        var totalCount = await query.CountAsync();
        var events = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                EventDate = e.EventDate,
                Location = e.Location,
                Address = e.Address,
                OrganizerId = e.OrganizerId,
                OrganizerName = $"{e.Organizer.FirstName} {e.Organizer.LastName}",
                CreatedAt = e.CreatedAt,
                IsActive = e.IsActive,
                MaxAttendees = e.MaxAttendees,
                CurrentAttendees = e.Attendees.Count,
                EventType = e.EventType,
                IsUserRegistered = currentUserId > 0 && e.Attendees.Any(a => a.UserId == currentUserId)
            })
            .ToListAsync();

        Response.Headers["X-Total-Count"] = totalCount.ToString();
        Response.Headers["X-Page"] = page.ToString();
        Response.Headers["X-Page-Size"] = pageSize.ToString();

        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventDetailDto>> GetEvent(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        var currentUserId = userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId) ? userId : 0;

        var eventItem = await _context.Events
            .Include(e => e.Organizer)
            .Include(e => e.Attendees)
                .ThenInclude(a => a.User)
            .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

        if (eventItem == null)
        {
            return NotFound();
        }

        var eventDto = new EventDetailDto
        {
            Id = eventItem.Id,
            Title = eventItem.Title,
            Description = eventItem.Description,
            EventDate = eventItem.EventDate,
            Location = eventItem.Location,
            Address = eventItem.Address,
            OrganizerId = eventItem.OrganizerId,
            OrganizerName = $"{eventItem.Organizer.FirstName} {eventItem.Organizer.LastName}",
            CreatedAt = eventItem.CreatedAt,
            IsActive = eventItem.IsActive,
            MaxAttendees = eventItem.MaxAttendees,
            CurrentAttendees = eventItem.Attendees.Count,
            EventType = eventItem.EventType,
            IsUserRegistered = currentUserId > 0 && eventItem.Attendees.Any(a => a.UserId == currentUserId),
            Attendees = eventItem.Attendees
                .Select(a => new EventAttendeeDto
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    UserName = $"{a.User.FirstName} {a.User.LastName}",
                    RegisteredAt = a.RegisteredAt,
                    IsConfirmed = a.IsConfirmed
                })
                .ToList()
        };

        return Ok(eventDto);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<EventDto>> CreateEvent(CreateEventDto createEventDto)
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

        var eventItem = new Event
        {
            Title = createEventDto.Title,
            Description = createEventDto.Description,
            EventDate = createEventDto.EventDate,
            Location = createEventDto.Location,
            Address = createEventDto.Address,
            OrganizerId = userId,
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
            MaxAttendees = createEventDto.MaxAttendees,
            EventType = createEventDto.EventType
        };

        _context.Events.Add(eventItem);
        await _context.SaveChangesAsync();

        var organizer = await _context.Users.FindAsync(userId);
        var eventDto = new EventDto
        {
            Id = eventItem.Id,
            Title = eventItem.Title,
            Description = eventItem.Description,
            EventDate = eventItem.EventDate,
            Location = eventItem.Location,
            Address = eventItem.Address,
            OrganizerId = eventItem.OrganizerId,
            OrganizerName = $"{organizer!.FirstName} {organizer.LastName}",
            CreatedAt = eventItem.CreatedAt,
            IsActive = eventItem.IsActive,
            MaxAttendees = eventItem.MaxAttendees,
            CurrentAttendees = 0,
            EventType = eventItem.EventType,
            IsUserRegistered = false
        };

        return CreatedAtAction(nameof(GetEvent), new { id = eventItem.Id }, eventDto);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateEvent(int id, UpdateEventDto updateEventDto)
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

        var eventItem = await _context.Events.FindAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }

        if (eventItem.OrganizerId != userId)
        {
            return Forbid();
        }

        eventItem.Title = updateEventDto.Title;
        eventItem.Description = updateEventDto.Description;
        eventItem.EventDate = updateEventDto.EventDate;
        eventItem.Location = updateEventDto.Location;
        eventItem.Address = updateEventDto.Address;
        eventItem.MaxAttendees = updateEventDto.MaxAttendees;
        eventItem.EventType = updateEventDto.EventType;
        eventItem.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var eventItem = await _context.Events.FindAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }

        if (eventItem.OrganizerId != userId)
        {
            return Forbid();
        }

        _context.Events.Remove(eventItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{id}/register")]
    [Authorize]
    public async Task<IActionResult> RegisterForEvent(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var eventItem = await _context.Events
            .Include(e => e.Attendees)
            .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

        if (eventItem == null)
        {
            return NotFound();
        }

        // Check if user is already registered
        if (eventItem.Attendees.Any(a => a.UserId == userId))
        {
            return BadRequest(new { message = "Already registered for this event" });
        }

        // Check if event is full
        if (eventItem.MaxAttendees > 0 && eventItem.Attendees.Count >= eventItem.MaxAttendees)
        {
            return BadRequest(new { message = "Event is full" });
        }

        var attendee = new EventAttendee
        {
            EventId = id,
            UserId = userId,
            RegisteredAt = DateTime.UtcNow,
            IsConfirmed = false
        };

        _context.EventAttendees.Add(attendee);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Successfully registered for event" });
    }

    [HttpDelete("{id}/register")]
    [Authorize]
    public async Task<IActionResult> UnregisterFromEvent(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var attendee = await _context.EventAttendees
            .FirstOrDefaultAsync(a => a.EventId == id && a.UserId == userId);

        if (attendee == null)
        {
            return NotFound();
        }

        _context.EventAttendees.Remove(attendee);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Successfully unregistered from event" });
    }
} 