using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tasksef;

var builder = WebApplication.CreateBuilder(args);

// de esta forma se puede configurar el contexto de la base de datos en memoria
builder.Services.AddDbContext<TasksContext>(opt => opt.UseInMemoryDatabase("TasksDB"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbConection", ([FromServices] TasksContext dbContext) =>
  {
    // se asegura que la base de datos se haya creado, si no existe la crea
    dbContext.Database.EnsureCreated();

    // retorna true si la base de datos esta creada en memoria
    return Results.Ok($"Database created: {dbContext.Database.IsInMemory()}");
  }
);

app.Run();
