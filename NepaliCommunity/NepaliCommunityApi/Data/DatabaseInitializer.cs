using Microsoft.EntityFrameworkCore;
using NepaliCommunityApi.Models;
using BCrypt.Net;

namespace NepaliCommunityApi.Data;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(NepaliCommunityContext context)
    {
        Console.WriteLine("Starting database initialization...");
        
        try
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();
            Console.WriteLine("Database schema created/updated successfully.");

            // Check if we need to seed data (only if no users exist)
            if (!await context.Users.AnyAsync())
            {
                Console.WriteLine("No users found. Seeding initial data...");
                await SeedDataAsync(context);
                Console.WriteLine("Seed data added successfully!");
            }
            else
            {
                Console.WriteLine("Users already exist. Skipping seed data.");
            }
            
            Console.WriteLine("Database initialization completed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during database initialization: {ex.Message}");
            // Continue with the application even if seeding fails
        }
    }

    private static async Task SeedDataAsync(NepaliCommunityContext context)
    {
        // Seed Users
        var users = new List<User>
        {
            new User
            {
                FirstName = "Ram",
                LastName = "Sharma",
                Email = "ram.sharma@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                PhoneNumber = "+977-9841234567",
                Address = "Thamel, Kathmandu",
                City = "Kathmandu",
                Country = "Nepal",
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                IsActive = true
            },
            new User
            {
                FirstName = "Sita",
                LastName = "Gurung",
                Email = "sita.gurung@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                PhoneNumber = "+977-9852345678",
                Address = "Pokhara, Nepal",
                City = "Pokhara",
                Country = "Nepal",
                CreatedAt = DateTime.UtcNow.AddDays(-25),
                IsActive = true
            },
            new User
            {
                FirstName = "Krishna",
                LastName = "Thapa",
                Email = "krishna.thapa@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                PhoneNumber = "+977-9863456789",
                Address = "Lalitpur, Nepal",
                City = "Lalitpur",
                Country = "Nepal",
                CreatedAt = DateTime.UtcNow.AddDays(-20),
                IsActive = true
            }
        };

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();
        Console.WriteLine($"Created {users.Count} users");

        // Get the created users for seeding other data
        var ramSharma = await context.Users.FirstAsync(u => u.Email == "ram.sharma@example.com");
        var sitaGurung = await context.Users.FirstAsync(u => u.Email == "sita.gurung@example.com");
        var krishnaThapa = await context.Users.FirstAsync(u => u.Email == "krishna.thapa@example.com");

        // Seed Posts
        var posts = new List<Post>
        {
            new Post
            {
                Title = "Dashain Celebration 2024",
                Content = "Join us for the biggest Dashain celebration in the city! We'll have traditional food, music, dance performances, and cultural activities. This is a great opportunity to connect with fellow Nepalis and celebrate our rich culture together. All are welcome!",
                AuthorId = ramSharma.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                IsPublished = true,
                ViewCount = 45,
                LikeCount = 12
            },
            new Post
            {
                Title = "Nepali Language Classes for Children",
                Content = "Starting new Nepali language classes for children aged 5-12. Help your kids stay connected to our roots and learn our beautiful language. Classes will be held every Saturday from 10 AM to 12 PM. Contact me for more details and registration.",
                AuthorId = sitaGurung.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                IsPublished = true,
                ViewCount = 32,
                LikeCount = 8
            },
            new Post
            {
                Title = "Cultural Exchange Program with South Asian Communities",
                Content = "Exciting opportunity for cultural exchange with other South Asian communities. We're organizing a cultural festival that will showcase Nepali traditions, food, music, and dance. This is a great way to promote understanding and friendship between different communities.",
                AuthorId = krishnaThapa.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                IsPublished = true,
                ViewCount = 28,
                LikeCount = 15
            }
        };

        await context.Posts.AddRangeAsync(posts);
        await context.SaveChangesAsync();
        Console.WriteLine($"Created {posts.Count} posts");

        // Get the created posts for seeding comments
        var dashainPost = await context.Posts.FirstAsync(p => p.Title == "Dashain Celebration 2024");
        var languagePost = await context.Posts.FirstAsync(p => p.Title == "Nepali Language Classes for Children");

        // Seed Comments
        var comments = new List<Comment>
        {
            new Comment
            {
                Content = "This sounds amazing! I'll definitely be there with my family. Looking forward to celebrating Dashain together!",
                PostId = dashainPost.Id,
                AuthorId = sitaGurung.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-14),
                IsActive = true
            },
            new Comment
            {
                Content = "Great initiative! Will there be traditional games like Deusi-Bhailo?",
                PostId = dashainPost.Id,
                AuthorId = krishnaThapa.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-13),
                IsActive = true
            },
            new Comment
            {
                Content = "Yes, we'll have Deusi-Bhailo performances and many other traditional activities!",
                PostId = dashainPost.Id,
                AuthorId = ramSharma.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-12),
                IsActive = true
            },
            new Comment
            {
                Content = "This is exactly what I was looking for! My kids need to learn Nepali. How do I register?",
                PostId = languagePost.Id,
                AuthorId = ramSharma.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-9),
                IsActive = true
            },
            new Comment
            {
                Content = "Please send me a private message with your contact details, and I'll send you the registration form.",
                PostId = languagePost.Id,
                AuthorId = sitaGurung.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-8),
                IsActive = true
            }
        };

        await context.Comments.AddRangeAsync(comments);
        await context.SaveChangesAsync();
        Console.WriteLine($"Created {comments.Count} comments");

        // Seed Events
        var events = new List<Event>
        {
            new Event
            {
                Title = "Dashain Festival 2024",
                Description = "Join us for a grand Dashain celebration featuring traditional Nepali food, music, dance performances, and cultural activities. This is a family-friendly event where we'll celebrate our rich Nepali culture together.",
                EventDate = DateTime.UtcNow.AddDays(30),
                Location = "Community Center",
                Address = "123 Main Street, Kathmandu, Nepal",
                OrganizerId = ramSharma.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-20),
                IsActive = true,
                MaxAttendees = 100,
                EventType = "Cultural"
            },
            new Event
            {
                Title = "Nepali New Year Celebration",
                Description = "Celebrate Nepali New Year (Bikram Sambat) with traditional rituals, cultural performances, and a grand feast. Let's welcome the new year together with joy and hope.",
                EventDate = DateTime.UtcNow.AddDays(60),
                Location = "City Park",
                Address = "Central Park, Kathmandu, Nepal",
                OrganizerId = sitaGurung.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                IsActive = true,
                MaxAttendees = 50,
                EventType = "Cultural"
            },
            new Event
            {
                Title = "Nepali Cooking Workshop",
                Description = "Learn to cook authentic Nepali dishes like Dal Bhat, Momo, and other traditional recipes. This hands-on workshop will teach you the secrets of Nepali cuisine.",
                EventDate = DateTime.UtcNow.AddDays(15),
                Location = "Cooking Studio",
                Address = "456 Food Street, Kathmandu, Nepal",
                OrganizerId = krishnaThapa.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                IsActive = true,
                MaxAttendees = 20,
                EventType = "Educational"
            }
        };

        await context.Events.AddRangeAsync(events);
        await context.SaveChangesAsync();
        Console.WriteLine($"Created {events.Count} events");

        // Get the created events for seeding attendees
        var dashainEvent = await context.Events.FirstAsync(e => e.Title == "Dashain Festival 2024");
        var cookingEvent = await context.Events.FirstAsync(e => e.Title == "Nepali Cooking Workshop");

        // Seed Event Attendees
        var eventAttendees = new List<EventAttendee>
        {
            new EventAttendee
            {
                EventId = dashainEvent.Id,
                UserId = sitaGurung.Id,
                RegisteredAt = DateTime.UtcNow.AddDays(-18),
                IsConfirmed = true
            },
            new EventAttendee
            {
                EventId = dashainEvent.Id,
                UserId = krishnaThapa.Id,
                RegisteredAt = DateTime.UtcNow.AddDays(-17),
                IsConfirmed = true
            },
            new EventAttendee
            {
                EventId = cookingEvent.Id,
                UserId = ramSharma.Id,
                RegisteredAt = DateTime.UtcNow.AddDays(-8),
                IsConfirmed = true
            },
            new EventAttendee
            {
                EventId = cookingEvent.Id,
                UserId = sitaGurung.Id,
                RegisteredAt = DateTime.UtcNow.AddDays(-7),
                IsConfirmed = false
            }
        };

        await context.EventAttendees.AddRangeAsync(eventAttendees);
        await context.SaveChangesAsync();
        Console.WriteLine($"Created {eventAttendees.Count} event attendees");

        // Update event attendee counts
        dashainEvent.CurrentAttendees = 2;
        cookingEvent.CurrentAttendees = 2;
        await context.SaveChangesAsync();
        
        Console.WriteLine("Seed data completed successfully!");
    }
} 