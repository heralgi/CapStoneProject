# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

Capstone project for an **Insurance Policy** application. Currently only the backend exists: an ASP.NET Core Web API targeting **.NET 10** (`net10.0`). The code is still the default Web API scaffold (the `WeatherForecast` template) — the insurance domain (controllers, models, persistence) has not been built yet.

## Layout

- `Backend/InsurancePolicyApi/InsurancePolicyApi.slnx` — solution file (uses the newer XML `.slnx` format).
- `Backend/InsurancePolicyApi/InsurancePolicyApi/` — the API project. All paths below are relative to here.
  - `Program.cs` — minimal hosting startup using the classic `Program` class with `Main` (not top-level statements). Registers controllers, OpenAPI, HTTPS redirection, and authorization middleware.
  - `Controllers/` — MVC controllers (`[ApiController]` + attribute routing).

## Commands

Run from `Backend/InsurancePolicyApi/` (where the `.slnx` lives), or pass the project/solution path explicitly.

```powershell
dotnet restore
dotnet build
dotnet run --project InsurancePolicyApi          # serves http://localhost:5135, https://localhost:7083
dotnet watch run --project InsurancePolicyApi    # hot reload during development
```

The default launch profile sets `ASPNETCORE_ENVIRONMENT=Development`. In Development, the OpenAPI document is exposed via `app.MapOpenApi()` at `/openapi/v1.json`.

There is no test project yet. When adding one, wire it into the solution with `dotnet sln InsurancePolicyApi.slnx add <path>` and run with `dotnet test`.

## Conventions

- `Nullable` and `ImplicitUsings` are **enabled** — respect nullable annotations and rely on implicit `using` rather than re-adding common namespaces.
- New code should live in the `InsurancePolicyApi` namespace.
- The single package dependency is `Microsoft.AspNetCore.OpenApi`; there is no Swagger UI package, ORM, or database configured yet.

## Building out the domain

The `WeatherForecast.cs` model and `WeatherForecastController.cs` are placeholder scaffold — replace them with insurance-policy controllers and models as the project grows.
