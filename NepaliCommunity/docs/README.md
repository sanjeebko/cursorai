# NepaliCommunity Project

A full-stack application for the Nepali community featuring an Angular 20 frontend and .NET 9 Web API backend.

## Project Structure

```
NepaliCommunity/
├── nc-ui/                    # Angular 20 Frontend Application
│   └── nepali-community-ui/  # Main Angular project
├── nc-api/                   # .NET 9 Web API Backend
│   └── NepaliCommunityApi/   # Main API project
└── docs/                     # Project Documentation
```

## Getting Started

### Prerequisites

- Node.js (Latest LTS version)
- .NET 9 SDK
- Angular CLI

### Frontend (Angular 20)

```bash
cd nc-ui/nepali-community-ui
npm install
ng serve
```

The application will be available at `http://localhost:4200`

### Backend (.NET 9 API)

```bash
cd nc-api/NepaliCommunityApi
dotnet restore
dotnet run
```

The API will be available at `https://localhost:7071` or `http://localhost:5071`

## Development

### Frontend Development

- Navigate to `nc-ui/nepali-community-ui`
- Run `ng serve` for development server
- Run `ng build` for production build
- Run `ng test` for unit tests

### Backend Development

- Navigate to `nc-api/NepaliCommunityApi`
- Run `dotnet run` for development server
- Run `dotnet build` for building the project
- Run `dotnet test` for unit tests

## Documentation

All project documentation is maintained in the `docs/` folder:

- [API Documentation](./api-documentation.md)
- [Frontend Documentation](./frontend-documentation.md)
- [Deployment Guide](./deployment-guide.md)

## Contributing

Please read the contributing guidelines before making any changes to the project.

## License

This project is licensed under the MIT License.