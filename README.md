# Team Ranker API

This solution provides a RESTful web API built with .NET 5 for managing teams and played matches while exposing a ranking table that updates whenever results are recorded.

## Projects

- `TeamRanker.Api` – ASP.NET Core Web API project exposing CRUD endpoints for teams and matches, ranking endpoints, Swagger UI, and centralized exception handling.
- `TeamRanker.Core` – Domain project containing entities, interfaces, DTOs, and the ranking strategy implementation (Strategy design pattern).
- `TeamRanker.Tests` – xUnit test project covering the ranking strategy logic.

## Prerequisites

- .NET 5 SDK
- SQL Server instance (update the connection string in `src/TeamRanker.Api/appsettings.json` if needed).

> **Note:** When no connection string is provided, the API falls back to an in-memory database, which is helpful for local testing or automated tests.

## Getting started

```bash
cd src/TeamRanker.Api
dotnet run
```

The API will be available at `http://localhost:5000` with Swagger UI enabled at `/swagger`.

## Running tests

```bash
dotnet test TeamRanker.sln
```

## Key features

- CRUD operations for teams and matches.
- Automatic recalculation of standings using the 3-1-0 scoring system.
- Strategy pattern encapsulating the ranking algorithm for easy substitution.
- Global exception handling middleware producing consistent error responses.
- Swagger/OpenAPI documentation enabled by default.

## Design patterns

- **Strategy pattern** – The `IRankingStrategy` abstraction and `StandardRankingStrategy` implementation encapsulate the ranking
  algorithm so that alternative strategies can be swapped without changing callers such as `RankingService`.
