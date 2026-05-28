# 📐 Project Subject: Math Calculator API
### ASP.NET Core Web API — Fundamentals Practice

---

## Overview

Build a **RESTful Math Calculator API** using ASP.NET Core.
The API accepts numbers via HTTP requests, performs calculations,
and returns results as JSON — all stored in-memory, no database needed.

This project is designed to cover **ASP.NET Core fundamentals** and
build directly on top of the C# features you already know from `cs00`.

---

## What You Will Build

A REST API that exposes math operations as HTTP endpoints:

```
POST /api/calculator/add
POST /api/calculator/subtract
POST /api/calculator/multiply
POST /api/calculator/divide
GET  /api/calculator/history
DELETE /api/calculator/history
GET  /api/calculator/history/{id}
```

---

## Project Structure

```
/MathCalculatorAPI
│
├── Controllers/
│   └── CalculatorController.cs   → API endpoints (HTTP methods)
│
├── Models/
│   ├── CalculationRequest.cs     → input model (what the user sends)
│   ├── CalculationResult.cs      → output model (what the API returns)
│   └── OperationType.cs          → enum: Add, Subtract, Multiply, Divide
│
├── Services/
│   ├── ICalculatorService.cs     → interface
│   └── CalculatorService.cs      → business logic + in-memory history
│
├── Middleware/
│   └── ErrorHandlingMiddleware.cs → global error handler
│
├── DTOs/
│   └── CalculationDto.cs         → Data Transfer Object
│
└── Program.cs                    → DI setup, middleware pipeline
```

---

## New Concepts to Learn & Use

### 🔷 ASP.NET Core Fundamentals

| Concept | Where | Description |
|---|---|---|
| `[ApiController]` | CalculatorController | marks class as API controller |
| `[Route("api/[controller]")]` | CalculatorController | sets base route |
| `[HttpPost]` `[HttpGet]` `[HttpDelete]` | each endpoint method | defines HTTP method |
| `[FromBody]` | method parameter | reads JSON from request body |
| `[FromRoute]` | method parameter | reads value from URL |
| `IActionResult` | return type | flexible HTTP response wrapper |
| `Ok()` `BadRequest()` `NotFound()` | inside methods | HTTP status code responses |
| Middleware pipeline | Program.cs | request/response processing chain |
| `builder.Services.AddScoped<>()` | Program.cs | DI container registration |
| Swagger / OpenAPI | Program.cs | auto-generated API documentation |

---

### 🔶 C# Features to Use (from cs00 + new ones)

| Feature | Where |
|---|---|
| `enum` | OperationType — Add, Subtract, Multiply, Divide |
| `record` | CalculationResult — immutable result snapshot |
| `interface` | ICalculatorService |
| `List<T>` | in-memory history storage |
| `Guid` | unique ID for each calculation result |
| `DateTime` | timestamp for each calculation |
| `switch expression` | map OperationType to actual operation |
| `IEnumerable<T>` | return history list |
| `async / await` | all service methods |
| `where` (generic constraint) | generic calculator method |
| Dependency Injection | inject ICalculatorService into controller |

---

## Models Detail

### `CalculationRequest.cs`
```csharp
public class CalculationRequest
{
    public double   OperandA    { get; set; }
    public double   OperandB    { get; set; }
    public string   Operation   { get; set; } = string.Empty;
}
```

### `CalculationResult.cs`
```csharp
public record CalculationResult(
    Guid        Id,
    double      OperandA,
    double      OperandB,
    string      Operation,
    double      Result,
    DateTime    CreatedAt
);
```

### `OperationType.cs`
```csharp
public enum OperationType
{
    Add,
    Subtract,
    Multiply,
    Divide
}
```

---

## Service Layer

### `ICalculatorService.cs`
```csharp
public interface ICalculatorService
{
    Task<CalculationResult>             CalculateAsync(CalculationRequest request);
    Task<IEnumerable<CalculationResult>> GetHistoryAsync();
    Task<CalculationResult?>            GetByIdAsync(Guid id);
    Task                                ClearHistoryAsync();
}
```

### `CalculatorService.cs` — key points
```csharp
public class CalculatorService : ICalculatorService
{
    // in-memory database
    private readonly List<CalculationResult> _history = new();

    public async Task<CalculationResult> CalculateAsync(CalculationRequest request)
    {
        // parse the operation string to enum
        // use switch expression to calculate
        // save to _history
        // return the result
    }
}
```

---

## Controller Detail

### `CalculatorController.cs`
```csharp
[ApiController]
[Route("api/[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ICalculatorService _calculatorService;

    public CalculatorController(ICalculatorService calculatorService)
    {
        _calculatorService = calculatorService;
    }

    [HttpPost("calculate")]
    public async Task<IActionResult> Calculate([FromBody] CalculationRequest request)
    {
        // call service → return Ok(result)
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHistory()
    {
        // call service → return Ok(history)
    }

    [HttpGet("history/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        // call service → return Ok or NotFound
    }

    [HttpDelete("history")]
    public async Task<IActionResult> ClearHistory()
    {
        // call service → return NoContent()
    }
}
```

---

## Middleware

### `ErrorHandlingMiddleware.cs`
```csharp
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);   // pass to next middleware
        }
        catch (Exception ex)
        {
            // catch any unhandled exception
            // return 500 with error message as JSON
        }
    }
}
```

---

## Program.cs — Full Pipeline

```csharp
var builder = WebApplication.CreateBuilder(args);

// DI registration
builder.Services.AddControllers();
builder.Services.AddScoped<ICalculatorService, CalculatorService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline — ORDER MATTERS
app.UseMiddleware<ErrorHandlingMiddleware>();  // 1. error handling
app.UseSwagger();                             // 2. swagger
app.UseSwaggerUI();                           // 3. swagger UI
app.UseRouting();                             // 4. routing
app.MapControllers();                         // 5. controllers

app.Run();
```

---

## Sample HTTP Requests

### Add two numbers
```http
POST /api/calculator/calculate
Content-Type: application/json

{
  "operandA": 10,
  "operandB": 5,
  "operation": "Add"
}
```

### Response
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "operandA": 10,
  "operandB": 5,
  "operation": "Add",
  "result": 15,
  "createdAt": "2025-01-01T12:00:00"
}
```

### Get history
```http
GET /api/calculator/history
```

### Get one result
```http
GET /api/calculator/history/3fa85f64-5717-4562-b3fc-2c963f66afa6
```

### Clear history
```http
DELETE /api/calculator/history
```

---

## HTTP Status Codes to Use

| Situation | Code | Method |
|---|---|---|
| Success with data | `200 OK` | `Ok(data)` |
| Success no content | `204 No Content` | `NoContent()` |
| Bad input | `400 Bad Request` | `BadRequest(message)` |
| Not found | `404 Not Found` | `NotFound(message)` |
| Server error | `500 Internal Server Error` | middleware handles |
| Division by zero | `400 Bad Request` | `BadRequest("Cannot divide by zero")` |

---

## New Things to Learn Before Starting

```
1. What is REST?                → 20 min
2. HTTP methods and status codes → 20 min
3. What is JSON request/response → 15 min (you already know JSON)
4. What is Middleware?          → 20 min
5. ASP.NET Core DI container    → 20 min (you already know DI concept)
6. What is Swagger?             → 10 min
7. How to test with Postman/curl → 15 min
```

> Total: ~2 hours of reading before touching code.

---

## Suggested Build Order

```
1. Program.cs skeleton          → get the app running (dotnet run)
2. OperationType enum           → simple, no dependencies
3. CalculationRequest model     → simple, no dependencies
4. CalculationResult record     → simple, no dependencies
5. ICalculatorService           → interface contract
6. CalculatorService            → business logic + in-memory list
7. CalculatorController         → wire HTTP endpoints
8. ErrorHandlingMiddleware      → global error handling
9. Test with Swagger / Postman  → verify everything works
```

---

## Deliverables

- [ ] Full ASP.NET Core Web API project
- [ ] All 4 HTTP endpoints working
- [ ] In-memory history with Guid IDs
- [ ] Global error handling middleware
- [ ] Swagger UI accessible at `/swagger`
- [ ] Division by zero handled gracefully
- [ ] Each concept commented with `// CONCEPT: ...`

---

## Grading Criteria

| Criterion | Points |
|---|---|
| All endpoints work correctly | 25 pts |
| Correct HTTP status codes | 15 pts |
| Middleware implemented | 15 pts |
| DI and interface used correctly | 15 pts |
| In-memory history works | 15 pts |
| Code quality and comments | 15 pts |
| **Total** | **100 pts** |

---

## Tips

- **Start with `dotnet new webapi`** — ASP.NET gives you a working template instantly.
- Run the app → open `http://localhost:5000/swagger` → you'll see the UI immediately.
- Use **Postman** or `curl` to test your endpoints before wiring everything.
- The middleware pipeline **order matters** — error handler always goes first.
- You already know DI, MVC, interfaces, async/await — **this project is the same concepts, just over HTTP.**

---

*The jump from console app to Web API is smaller than it looks —
you already know 70% of what you need.* 🚀