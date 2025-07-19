using Microsoft.EntityFrameworkCore;
using QuizApi.Data;

var builder = WebApplication.CreateBuilder(args);

// BŪTINA: kad veiktų [ApiController]
builder.Services.AddControllers();

// Duomenų bazės prijungimas
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

// BŪTINA: kad veiktų HTTP maršrutai
app.MapControllers();

app.Run();