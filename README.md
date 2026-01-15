# StudentManagementSystem API

## Overview

A simple RESTful API for managing students and classes. Built with ASP.NET Core (.NET 8) and using JWT for authentication. The solution is organized into layered projects: API, Application, Domain, and Infrastructure.

## Features

- JWT-based authentication
- CRUD for students and classes
- Clean, layered architecture

## Tech

- .NET 8
- C# 12
- ASP.NET Core Web API

## Prerequisites

- .NET 8 SDK
- Visual Studio 2022/2026 or VS Code

Verify SDK:

```
dotnet --version
```

## Setup

1. Clone the repository:

```
git clone https://github.com/abhihpatel98/StudentManagementSystem_AbhiPatel_API.git
cd StudentManagementSystem/StudentManagementSystem.API
```

2. Configure JWT settings in `appsettings.json` (or use environment variables):

```json
{
  "Jwt": {
    "Key": "YourSuperSecretKeyHere",
    "Issuer": "YourIssuer",
    "Audience": "YourAudience",
    "ExpiryMinutes": "60"
  }
}
```

3. (Optional) If using Visual Studio, open the solution file located in the `StudentManagementSystem` folder.

## Configuration

Configure JWT values in `appsettings.json` or environment variables under the `Jwt` section:

- `Jwt:Key` — secret key (store securely)
- `Jwt:Issuer`
- `Jwt:Audience`
- `Jwt:ExpiryMinutes` — token lifetime in minutes

For production, use user secrets or environment variables for `Jwt:Key`.

## Run locally

Using Visual Studio:
1. Open the solution located in the `StudentManagementSystem` folder.
2. Set `StudentManagementSystem.API` as the startup project.
3. Restore NuGet packages and run (F5 or Start Without Debugging).

Using dotnet CLI:

```
cd StudentManagementSystem.API
dotnet restore
dotnet run
```

The API base URL for this project is `https://localhost:7291`.

Swagger (API documentation) will be available at:

- `https://localhost:7291/swagger`
- or `https://localhost:7291/swagger/index.html`

When running in Visual Studio the exact port may be controlled by `Properties/launchSettings.json` for the API project; ensure the launch URL matches the base URL above or use the one shown in the console output.

## Authentication

Use POST `/api/auth/login` with the hardcoded credentials (for demo/testing):

- Username: `admin`
- Password: `admin123`

The endpoint returns a JWT to include in `Authorization: Bearer <token>` for protected endpoints.

To try endpoints interactively use the Swagger UI at `https://localhost:7291/swagger` and use the returned JWT in the Authorize button.

## Author
Abhi Patel