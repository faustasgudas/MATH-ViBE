using Microsoft.EntityFrameworkCore;
using QuizApi.Data;
using QuizApi.Repositories;
using QuizApi.Application.Interfaces.Persistence;
using QuizApi.Application.Services;
using QuizApi.Application.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Database
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository Dependency Injection
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
    
builder.Services.AddScoped<IQuizService, QuizService>(); 

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

var app = builder.Build();

// Development tools
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS before authorization
app.UseCors("FlutterApp");

app.UseAuthorization();

app.MapControllers();

app.Run();