using Microsoft.EntityFrameworkCore;
using UniversityContoso.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// ➜ REGISTRAZIONE DEL DbContext
builder.Services.AddDbContext<UniversityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityContext")));

// ➜ REGISTRAZIONE DEL CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirection
app.UseHttpsRedirection();

// *** CORS deve venire PRIMA dei controller ***
app.UseCors("AllowAngular");

app.UseAuthorization();

// Mapping dei controller API
app.MapControllers();

// Avvia l'app
app.Run();
