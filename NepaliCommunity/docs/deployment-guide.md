# Deployment Guide - NepaliCommunity

## Overview

This guide covers the deployment process for both the Angular frontend and .NET API backend of the NepaliCommunity project.

## Prerequisites

### For Frontend Deployment
- Node.js (Latest LTS version)
- Angular CLI
- Web server (Apache, Nginx, or cloud hosting)

### For Backend Deployment
- .NET 9 Runtime
- Web server with reverse proxy support
- Database server (SQL Server, PostgreSQL, etc.)

## Frontend Deployment (Angular)

### Development Build
```bash
cd nc-ui/nepali-community-ui
npm install
ng serve
```

### Production Build
```bash
cd nc-ui/nepali-community-ui
ng build --configuration production
```

### Deployment Options

#### 1. Static Hosting (Netlify, Vercel, GitHub Pages)
- Build the project using `ng build --configuration production`
- Upload the `dist/` folder to your hosting service
- Configure routing for single-page application

#### 2. Traditional Web Server
```bash
# Copy build files to web server
cp -r dist/* /var/www/html/

# Configure Apache/Nginx for SPA routing
# Example Nginx configuration:
location / {
    try_files $uri $uri/ /index.html;
}
```

#### 3. Docker Deployment
```dockerfile
FROM nginx:alpine
COPY dist/ /usr/share/nginx/html/
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

## Backend Deployment (.NET API)

### Development
```bash
cd nc-api/NepaliCommunityApi
dotnet restore
dotnet run
```

### Production Build
```bash
cd nc-api/NepaliCommunityApi
dotnet publish -c Release -o ./publish
```

### Deployment Options

#### 1. IIS (Windows)
- Build and publish the application
- Copy files to IIS wwwroot
- Configure IIS application pool for .NET 9

#### 2. Linux with Systemd
```bash
# Create service file
sudo nano /etc/systemd/system/nepaliapi.service

[Unit]
Description=Nepali Community API
After=network.target

[Service]
Type=notify
WorkingDirectory=/var/www/nepaliapi
ExecStart=/usr/bin/dotnet NepaliCommunityApi.dll
Restart=on-failure
RestartSec=5
User=www-data

[Install]
WantedBy=multi-user.target

# Enable and start service
sudo systemctl enable nepaliapi.service
sudo systemctl start nepaliapi.service
```

#### 3. Docker Deployment
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY publish/ .
EXPOSE 80
ENTRYPOINT ["dotnet", "NepaliCommunityApi.dll"]
```

## Database Setup

### SQL Server
```sql
-- Create database
CREATE DATABASE NepaliCommunityDB;

-- Create connection string in appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=NepaliCommunityDB;Trusted_Connection=true;"
  }
}
```

### PostgreSQL
```bash
# Install PostgreSQL
sudo apt-get install postgresql

# Create database
sudo -u postgres createdb nepalicommunitydb

# Connection string
"Server=localhost;Database=nepalicommunitydb;Username=postgres;Password=yourpassword;"
```

## Environment Configuration

### Frontend Environment Files
Create environment-specific files:

**src/environments/environment.prod.ts**
```typescript
export const environment = {
  production: true,
  apiUrl: 'https://api.nepalicommunity.com/api'
};
```

### Backend Configuration
**appsettings.Production.json**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "your-production-connection-string"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key",
    "Issuer": "NepaliCommunityApi",
    "Audience": "NepaliCommunityApp"
  }
}
```

## SSL/HTTPS Configuration

### Frontend (Nginx)
```nginx
server {
    listen 443 ssl;
    server_name yourdomain.com;
    
    ssl_certificate /path/to/certificate.crt;
    ssl_certificate_key /path/to/private.key;
    
    location / {
        root /var/www/html;
        try_files $uri $uri/ /index.html;
    }
}
```

### Backend (.NET)
```csharp
// Program.cs
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseHsts();
}
```

## Monitoring and Logging

### Application Insights (Azure)
```csharp
// Add to Program.cs
builder.Services.AddApplicationInsightsTelemetry();
```

### Log Files
```csharp
// Configure logging in appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    },
    "File": {
      "Path": "/var/log/nepaliapi/app.log"
    }
  }
}
```

## Performance Optimization

### Frontend
- Enable gzip compression
- Use Angular's OnPush change detection
- Implement lazy loading for routes
- Optimize bundle size with tree shaking

### Backend
- Enable response compression
- Implement caching strategies
- Use connection pooling
- Configure rate limiting

## Security Considerations

### Frontend
- Implement Content Security Policy (CSP)
- Use HTTPS only
- Sanitize user inputs
- Implement proper error handling

### Backend
- Use HTTPS
- Implement JWT authentication
- Enable CORS properly
- Use parameterized queries
- Implement rate limiting
- Regular security updates

## Backup Strategy

### Database Backup
```bash
# PostgreSQL
pg_dump nepalicommunitydb > backup.sql

# SQL Server
BACKUP DATABASE NepaliCommunityDB TO DISK = 'C:\backup\database.bak'
```

### Application Backup
- Regular code repository backups
- Configuration file backups
- Static assets backup

## Troubleshooting

### Common Issues
1. CORS errors - Check API CORS configuration
2. 404 errors on refresh - Configure SPA routing
3. Database connection issues - Verify connection strings
4. SSL certificate problems - Check certificate validity

### Log Locations
- Frontend: Browser console and network tab
- Backend: Application logs and server logs
- Web Server: Access and error logs