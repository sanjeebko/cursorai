# Nepali Community Application

A full-stack application for the Nepali community featuring an Angular 20 frontend and .NET 9 Web API backend.

## Quick Start

### Start the API:
```bash
cd NepaliCommunityApi
dotnet run --urls "http://localhost:5106;https://localhost:7250"
```

### Start the Frontend (in a new terminal):
```bash
cd NepaliCommunityUi
npm start
```

## Access URLs

### API Endpoints
- **HTTP API**: http://localhost:5106
- **HTTPS API**: https://localhost:7250
- **Swagger Documentation**: https://localhost:7250/swagger

### Frontend
- **Angular App**: http://localhost:4200

## Database Configuration

The application is configured to connect to your SQL Server database:
- **Server**: 192.168.0.214
- **Database**: nepalicommunity
- **Username**: ncuser
- **Password**: Scorpions18

## Automatic Data Seeding

When the application starts, it automatically:
1. **Creates missing tables** if they don't exist
2. **Adds sample data** if the Users table is empty
3. **Preserves existing data** if tables already have content

### Sample Data Created (if tables are empty):

**👥 Users (3 accounts):**
- Ram Sharma (ram.sharma@example.com) - password: `password123`
- Sita Gurung (sita.gurung@example.com) - password: `password123`
- Krishna Thapa (krishna.thapa@example.com) - password: `password123`

**📝 Posts (3 sample posts):**
- Dashain Celebration 2024
- Nepali Language Classes for Children
- Cultural Exchange Program with South Asian Communities

**🎉 Events (3 sample events):**
- Dashain Festival 2024
- Nepali New Year Celebration
- Nepali Cooking Workshop

**💬 Comments and Event Attendees:**
- Realistic comments on posts
- Event registrations with relationships

## Features

### Backend API (.NET 9)
- ✅ User authentication with JWT tokens
- ✅ Community posts and discussions
- ✅ Events management and registration
- ✅ Nested comments system
- ✅ Entity Framework with SQL Server
- ✅ Swagger API documentation
- ✅ CORS configuration for Angular
- ✅ Automatic data seeding

### Frontend (Angular 20)
- ✅ Responsive landing page
- ✅ User registration and login
- ✅ Modern UI with animations
- ✅ Navigation with authentication state
- ✅ Services for API communication

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - User login
- `GET /api/auth/me` - Get current user

### Posts
- `GET /api/posts` - Get all posts (with pagination)
- `GET /api/posts/{id}` - Get specific post with comments
- `POST /api/posts` - Create new post
- `PUT /api/posts/{id}` - Update post
- `DELETE /api/posts/{id}` - Delete post

### Events
- `GET /api/events` - Get all events (with pagination)
- `GET /api/events/{id}` - Get specific event with attendees
- `POST /api/events` - Create new event
- `PUT /api/events/{id}` - Update event
- `DELETE /api/events/{id}` - Delete event
- `POST /api/events/{id}/register` - Register for event
- `DELETE /api/events/{id}/register` - Unregister from event

### Comments
- `GET /api/comments/post/{postId}` - Get comments for a post
- `POST /api/comments` - Create new comment
- `PUT /api/comments/{id}` - Update comment
- `DELETE /api/comments/{id}` - Delete comment

## Testing the Application

1. **Start the API** - It will automatically create tables and add sample data if needed
2. **Start the Frontend** - Angular app will be available at http://localhost:4200
3. **Login with sample data** - Use any of the sample user accounts
4. **Test API endpoints** - Use Swagger at https://localhost:7250/swagger

## Troubleshooting

### Database Issues
- The application automatically handles table creation and data seeding
- If you get schema errors, the application will continue running
- Check console output for any database initialization messages

### Port Conflicts
If ports are already in use:
1. Change the ports in `launchSettings.json`
2. Update the frontend service URLs accordingly

## Development

### Prerequisites
- .NET 9 SDK
- Node.js (Latest LTS)
- Angular CLI
- SQL Server

### Project Structure
```
NepaliCommunity/
├── NepaliCommunityApi/          # .NET 9 Web API
│   ├── Controllers/             # API Controllers
│   ├── Models/                  # Database Models
│   ├── Services/                # Business Logic
│   ├── DTOs/                    # Data Transfer Objects
│   ├── Data/                    # Entity Framework Context
│   └── Program.cs               # Application Entry Point
├── NepaliCommunityUi/           # Angular 20 Frontend
│   ├── src/app/
│   │   ├── components/          # Angular Components
│   │   ├── services/            # API Services
│   │   └── app.*               # Main App Files
│   └── package.json
└── README.md                    # This file
```

## Next Steps

To enhance the application:
1. Add more pages (Posts listing, Events listing, User profile)
2. Implement real-time features with WebSocket
3. Add file upload functionality
4. Enhance security with role-based access control
5. Add search and filtering capabilities
6. Implement notifications system 