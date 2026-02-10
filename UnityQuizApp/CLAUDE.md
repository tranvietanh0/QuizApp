# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

A Quiz App with two codebases in this monorepo:
- **Unity Client** (`UnityQuizApp/`) — Mobile quiz game built with Unity
- **ASP.NET Core Backend** (`../QuizAppBackend/QuizAppBackend/`) — REST API server

## Build & Run Commands

### Backend (.NET 10)
```bash
# Build
dotnet build ../QuizAppBackend/QuizAppBackend/QuizAppBackend.csproj

# Run (starts on localhost, Swagger at /swagger)
dotnet run --project ../QuizAppBackend/QuizAppBackend/QuizAppBackend.csproj
```
Requires MySQL on `localhost:3306`. Database auto-creates and seeds on first run via `DatabaseInitializer`.

### Unity Client
Open `UnityQuizApp/` in Unity Editor. No CLI build pipeline is configured. Target platform: Android (1920x1080).

## Architecture

### Unity Client — MVP + State Machine

**DI Framework:** VContainer (`jp.hadashikick.vcontainer`) with custom wrappers (`VContainerWrapper`, `VContainerAdapter`).

**Lifetime Scopes (DI entry points):**
- `GameLifetimeScope` — Root scope; registers GameFoundation and UITemplate frameworks
- `LoadingSceneScope` — Loading scene; binds `LoadingScreenPresenter`
- `MainSceneScope` — Main scene; registers `GameStateMachine` and auto-discovers all `IGameState` implementations

**MVP Pattern:**
- **View** — Inherits `BaseView`, implements `IScreenView`. Handles UI rendering and animations.
- **Presenter** — Inherits `BaseScreenPresenter<TView>` (or `<TView, TModel>`). Contains business logic, manages view lifecycle.
- **Screen Manager** (`IScreenManager`) — `OpenScreen<TPresenter>()`, `CloseCurrentScreen()`, manages screen stack with intro/outro animations.

**State Machine:**
- `GameStateMachine` manages app flow. States implement `IGameState`.
- Transitions: `TransitionTo<TState>()` or `TransitionTo<TState, TModel>(model)`
- Current flow: `WelcomeState` → (first launch?) → `CheerScreenPresenter` → `LoginState` → `GameHomeState`

**Key Libraries:**
- `UniTask` (Cysharp) — async/await throughout, replaces coroutines
- `MessagePipe` — Signal/event bus (`SignalBus.Fire<T>()`, `SignalBus.Subscribe<T>()`)
- `R3` — Reactive programming
- `DOTween/DOTweenPro` — Animation tweening
- `Odin Inspector` — Editor tooling and serialization
- `Newtonsoft.Json` — JSON serialization

**Submodules** (`Assets/Submodules/`): `GameFoundationCore`, `UITemplate`, `Extensions`, `Logging` — shared framework code providing base classes for views, presenters, state machines, DI wrappers, and data persistence.

**Data Persistence:**
- `AppLocalData` / `AppLocalDataController` — App-level settings (e.g., `IsFirstLaunch`)
- `UserLocalData` — User-specific data
- Uses `UITemplateLocalData<TController>` base + `HandleLocalUserDataServices` for serialization

**Scenes:**
- `0.LoadingScene.unity` — Loads user data, transitions to main scene
- `1.MainScene.unity` — Main game scene with state machine

### Backend — Layered Architecture

**Request flow:** Controller → Service → Repository → Dapper/SQL → MySQL

**Controllers (all under `/api`):**
| Endpoint | Auth | Purpose |
|---|---|---|
| `POST /api/auth/register` | No | Create account, returns JWT |
| `POST /api/auth/login` | No | Authenticate, returns JWT |
| `GET /api/quizzes` | Yes | List active quizzes |
| `GET /api/quizzes/{id}` | Yes | Quiz detail (answers WITHOUT `IsCorrect`) |
| `POST /api/quizzes/{id}/submit` | Yes | Submit answers, get score + correct answers |
| `GET /api/leaderboard` | Yes | Global top 50 (SUM of MAX scores per quiz) |
| `GET /api/leaderboard/quiz/{quizId}` | Yes | Per-quiz top 50 |

**Auth:** JWT Bearer with BCrypt password hashing. Token contains `NameIdentifier` (userId), `Name`, `displayName`. Extract userId via `ClaimsPrincipalExtensions.GetUserId()`.

**Anti-cheat:** `IsCorrect` on answers is hidden from GET responses; only revealed after submission.

**Database:** MySQL with Dapper. Tables: Users, Quizzes, Questions, Answers, QuizAttempts. `DatabaseInitializer` handles schema creation + seeding.

**Error handling:** `ExceptionHandlingMiddleware` maps `NotFoundException` → 404, `ConflictException` → 409, `UnauthorizedAccessException` → 401.

## Key Conventions

- All async code in Unity uses `UniTask`, not `Task` or coroutines
- New screens: create View (inheriting `BaseView`) + Presenter (inheriting `BaseScreenPresenter<TView>`) + register in the appropriate scope
- New game states: implement `IGameState`, auto-discovered by `MainSceneScope`
- Backend services/repositories: register as Scoped in `Program.cs`; `DbConnectionFactory` is Singleton
- Backend DTOs live in `DTOs/` organized by feature folder (Auth, Quiz, Leaderboard)

## .NET 10 / Swashbuckle v10 Notes

- Namespace: `Microsoft.OpenApi` (not `Microsoft.OpenApi.Models`)
- `AddSecurityRequirement()` takes `Func<OpenApiDocument, OpenApiSecurityRequirement>`
- Use `OpenApiSecuritySchemeReference("name", document)` instead of `OpenApiSecurityScheme { Reference = ... }`