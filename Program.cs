using MinimalApiCrud.Models.Data;
using MinimalApiCrud.Students;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// DATA BASE
builder.Services.AddScoped<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Minimal Api CRUD")
            .WithTheme(ScalarTheme.Default)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

// ROUTES
// app.MapGet("HelloWorld", () => "HelloWorld");
// StudentRoutes.AddStudentController(app);
app.AddStudentController();

app.Run();
