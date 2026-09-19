# AkkWebService — C# Backend API

A RESTful API built with ASP.NET Core 9, Entity Framework Core, and SQL Server. Serves as the backend for the "AK's Friendly Forest of Chaos" website.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server instance with the `akk_kercheval` database created (see `akk_kercheval CookieCutters table create.sql` in the parent folder)

## Running Locally

```bash
cd AkkWebService
dotnet run
```

The API starts on **http://localhost:5185** by default.

## Configuration

Connection string and auth settings are in `appsettings.json`:

- `ConnectionStrings.DefaultConnection` — SQL Server connection string
- `Jwt.Key` — signing key for JWT tokens
- `Jwt.ExpirationMinutes` — token lifetime (default: 480 minutes / 8 hours)
- `AdminUser.Username` — admin login username
- `AdminUser.PasswordHash` — BCrypt hash of the admin password

## API Endpoints

### Public (no authentication required)

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/cookiecutters` | List all cookie cutters (sorted by name) |
| GET | `/api/cookiecutters/{id}` | Get a single cookie cutter by ID |
| GET | `/api/cookiecutters/search?name=term` | Search cookie cutters by name (partial match) |

### Protected (requires Bearer token)

| Method | Route | Description |
|--------|-------|-------------|
| POST | `/api/cookiecutters` | Create a new cookie cutter |
| PUT | `/api/cookiecutters/{id}` | Update an existing cookie cutter |
| DELETE | `/api/cookiecutters/{id}` | Delete a cookie cutter |

### Authentication

| Method | Route | Description |
|--------|-------|-------------|
| POST | `/api/auth/login` | Login with username/password, returns JWT |

**Login request:**
```json
{
  "username": "akk",
  "password": "your-password"
}
```

**Login response:**
```json
{
  "token": "eyJhbG...",
  "expiresAt": "2026-07-28T11:04:53Z"
}
```

Use the token in subsequent requests:
```
Authorization: Bearer eyJhbG...
```

## Project Structure

```
AkkWebService/
├── Controllers/          — API endpoints (like Spring @RestController)
│   ├── AuthController.cs
│   └── CookieCuttersController.cs
├── Configuration/        — Settings classes bound from appsettings.json
│   ├── AdminUserSettings.cs
│   └── JwtSettings.cs
├── Data/
│   └── AkkDbContext.cs   — EF Core DbContext (like JPA configuration)
├── DTOs/                 — Request/response objects
│   ├── CookieCutterDto.cs
│   ├── CreateCookieCutterDto.cs
│   ├── UpdateCookieCutterDto.cs
│   ├── LoginRequestDto.cs
│   └── LoginResponseDto.cs
├── Models/
│   └── CookieCutter.cs   — Entity class (like JPA @Entity)
├── Services/             — Business logic (like Spring @Service)
│   ├── ICookieCutterService.cs
│   └── CookieCutterService.cs
├── Program.cs            — App startup and DI configuration
├── appsettings.json      — Configuration (like application.yaml)
└── AkkWebService.csproj  — Project file (like pom.xml)
```

## Technology Stack

- ASP.NET Core 9 Web API (controllers)
- Entity Framework Core 9 (SQL Server provider)
- JWT Bearer authentication
- BCrypt password hashing
- CORS configured for React frontend (localhost:3000, localhost:5173)
