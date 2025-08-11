using Microsoft.EntityFrameworkCore;
using QuizApi.Data;
using QuizApi.Repositories;
using QuizApi.Application.Interfaces.Persistence;
using QuizApi.Application.Services;
using QuizApi.Application.Interfaces.Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using QuizApi.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Controllers + JSON options
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

// Database
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Identity configuration - ONLY ONE Identity setup
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options => 
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<QuizDbContext>()
    .AddDefaultTokenProviders();

// Repository Dependency Injection
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
    
// Service Dependency Injection
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();

// Razor Pages - MOVED BEFORE app.Build()
builder.Services.AddRazorPages();

// CORS for Flutter
builder.Services.AddCors(options =>
{
    options.AddPolicy("FlutterApp", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Swagger for testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the app AFTER all services are registered
var app = builder.Build();

// Development tools
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS must come before auth
app.UseCors("FlutterApp");

// Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapRazorPages();
app.MapControllers();

app.Run();