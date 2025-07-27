# NepaliCommunity Frontend Documentation

## Overview

The NepaliCommunity frontend is built using Angular 20 with modern UI components and responsive design.

## Technology Stack

- **Framework**: Angular 20
- **Styling**: SCSS
- **Build Tool**: Angular CLI
- **Package Manager**: npm

## Project Structure

```
nc-ui/nepali-community-ui/
├── src/
│   ├── app/                 # Main application module
│   │   ├── app.ts          # Root component
│   │   ├── app.html        # Root template
│   │   ├── app.scss        # Root styles
│   │   ├── app.config.ts   # App configuration
│   │   └── app.routes.ts   # Routing configuration
│   ├── assets/             # Static assets
│   ├── styles.scss         # Global styles
│   ├── main.ts            # Application bootstrap
│   └── index.html         # Main HTML file
├── public/                 # Public assets
├── angular.json           # Angular CLI configuration
├── package.json           # Dependencies
└── tsconfig.json         # TypeScript configuration
```

## Development Commands

### Start Development Server
```bash
cd nc-ui/nepali-community-ui
ng serve
```

### Build for Production
```bash
ng build --configuration production
```

### Run Tests
```bash
ng test
```

### Run E2E Tests
```bash
ng e2e
```

### Generate Components
```bash
ng generate component component-name
ng generate service service-name
ng generate module module-name
```

## Features to Implement

### Core Features
- User authentication and registration
- Community posts and discussions
- Event management
- User profiles
- News and announcements

### UI Components
- Navigation header
- Sidebar menu
- Post cards
- Event calendar
- User profile cards
- Comment sections

### Pages
- Home/Dashboard
- Community Posts
- Events
- User Profile
- Settings
- About

## Styling Guidelines

### SCSS Structure
```scss
// Variables
$primary-color: #007bff;
$secondary-color: #6c757d;

// Mixins
@mixin button-style {
  padding: 0.5rem 1rem;
  border-radius: 0.25rem;
  border: none;
  cursor: pointer;
}

// Components
.btn-primary {
  @include button-style;
  background-color: $primary-color;
  color: white;
}
```

### Responsive Design
- Mobile-first approach
- Bootstrap-like grid system
- Breakpoints: 576px, 768px, 992px, 1200px

## State Management

For complex state management, consider implementing:
- NgRx for large applications
- Services with BehaviorSubject for simpler cases

## API Integration

### HTTP Client Setup
```typescript
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'https://localhost:7071/api';
  
  constructor(private http: HttpClient) {}
  
  getPosts() {
    return this.http.get(`${this.baseUrl}/posts`);
  }
}
```

## Deployment

### Production Build
```bash
ng build --configuration production
```

### Environment Configuration
Create environment files for different deployments:
- `src/environments/environment.ts` (development)
- `src/environments/environment.prod.ts` (production)