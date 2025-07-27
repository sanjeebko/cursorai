# Nepali Community Application

A full-stack application for the Nepali community featuring an Angular 20 frontend and .NET 9 Web API backend.

[![Build and Test](https://github.com/{owner}/{repo}/workflows/Build%20and%20Test/badge.svg)](https://github.com/{owner}/{repo}/actions/workflows/build-and-test.yml)
[![Deploy](https://github.com/{owner}/{repo}/workflows/Deploy/badge.svg)](https://github.com/{owner}/{repo}/actions/workflows/deploy.yml)
[![Database Test](https://github.com/{owner}/{repo}/workflows/Database%20Test/badge.svg)](https://github.com/{owner}/{repo}/actions/workflows/database-test.yml)
[![Dependency Check](https://github.com/{owner}/{repo}/workflows/Dependency%20Check/badge.svg)](https://github.com/{owner}/{repo}/actions/workflows/dependency-check.yml)

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

## CI/CD Pipeline

This project includes comprehensive GitHub Actions workflows:

### 🔄 **Build and Test** (`build-and-test.yml`)
- **Triggers**: Push to main/develop, Pull Requests
- **Actions**: Builds API and frontend, runs tests, security scans
- **Artifacts**: Uploads build outputs for deployment

### 🚀 **Deploy** (`deploy.yml`)
- **Triggers**: Release published, Manual dispatch
- **Actions**: Creates deployment packages for staging/production
- **Environments**: Supports multiple deployment targets

### 🗄️ **Database Test** (`database-test.yml`)
- **Triggers**: Changes to database files
- **Actions**: Tests schema creation and data seeding
- **Infrastructure**: Uses SQL Server container for testing

### 📦 **Dependency Check** (`dependency-check.yml`)
- **Triggers**: Weekly schedule, Manual dispatch
- **Actions**: Checks for outdated packages, security vulnerabilities
- **Automation**: Can auto-update dependencies

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
├── .github/workflows/           # GitHub Actions Workflows
│   ├── build-and-test.yml      # Main CI/CD pipeline
│   ├── deploy.yml              # Deployment workflow
│   ├── database-test.yml       # Database testing
│   ├── dependency-check.yml    # Dependency management
│   └── README.md               # Workflow documentation
└── README.md                    # This file
```

## Troubleshooting

### Database Issues
- The application automatically handles table creation and data seeding
- If you get schema errors, the application will continue running
- Check console output for any database initialization messages

### Port Conflicts
If ports are already in use:
1. Change the ports in `launchSettings.json`
2. Update the frontend service URLs accordingly

### CI/CD Issues
- Check the **Actions** tab in GitHub for workflow status
- Review workflow logs for detailed error information
- Ensure all dependencies are properly configured

## Next Steps

To enhance the application:
1. Add more pages (Posts listing, Events listing, User profile)
2. Implement real-time features with WebSocket
3. Add file upload functionality
4. Enhance security with role-based access control
5. Add search and filtering capabilities
6. Implement notifications system

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

The CI/CD pipeline will automatically test your changes and provide feedback. 