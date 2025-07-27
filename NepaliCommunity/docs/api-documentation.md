# NepaliCommunity API Documentation

## Overview

The NepaliCommunity API is built using .NET 9 and provides RESTful endpoints for the frontend application.

## Base URL

- Development: `https://localhost:7071` or `http://localhost:5071`
- Production: TBD

## Authentication

The API uses JWT (JSON Web Tokens) for authentication. Include the token in the Authorization header:

```
Authorization: Bearer <your-jwt-token>
```

## API Endpoints

### Weather (Default Example)

#### GET /WeatherForecast
Returns weather forecast data.

**Response:**
```json
[
  {
    "date": "2023-01-01",
    "temperatureC": 25,
    "temperatureF": 77,
    "summary": "Sunny"
  }
]
```

### Future Endpoints

The following endpoints will be implemented:

#### Users
- `GET /api/users` - Get all users
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Create new user
- `PUT /api/users/{id}` - Update user
- `DELETE /api/users/{id}` - Delete user

#### Community Posts
- `GET /api/posts` - Get all community posts
- `GET /api/posts/{id}` - Get post by ID
- `POST /api/posts` - Create new post
- `PUT /api/posts/{id}` - Update post
- `DELETE /api/posts/{id}` - Delete post

#### Events
- `GET /api/events` - Get all events
- `GET /api/events/{id}` - Get event by ID
- `POST /api/events` - Create new event
- `PUT /api/events/{id}` - Update event
- `DELETE /api/events/{id}` - Delete event

## Error Handling

The API returns standard HTTP status codes:

- `200` - Success
- `201` - Created
- `400` - Bad Request
- `401` - Unauthorized
- `404` - Not Found
- `500` - Internal Server Error

## Development

### Running the API

```bash
cd nc-api/NepaliCommunityApi
dotnet run
```

### Building for Production

```bash
dotnet publish -c Release
```

### Testing

```bash
dotnet test
```