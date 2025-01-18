using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tasksef;

var builder = WebApplication.CreateBuilder(args);

// de esta forma se puede configurar el contexto de la base de datos en memoria
// builder.Services.AddDbContext<TasksContext>(opt => opt.UseInMemoryDatabase("TasksDB"));

// al metodo AddSqlServer se le pasa el contexto y la cadena de conexion
// Data Source=nombre del servidor;Initial Catalog=nombre de la base de datos;User Id=usuario;Password=contraseña;TrustServerCertificate=True;
// TrustServerCertificate=True; es para que no se muestre el error de certificado no confiable
// builder.Services.AddSqlServer<TasksContext>("Data Source=YESID\\SQLEXPRESS;Initial Catalog=Tasks;user id=sa;password=root;TrustServerCertificate=True;");

// en este caso se esta utilizando la cadena de conexion de un archivo appsettings.json
builder.Services.AddSqlServer<TasksContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbConnection", ([FromServices] TasksContext dbContext) =>
  {
    // se asegura que la base de datos se haya creado, si no existe la crea
    dbContext.Database.EnsureCreated();

    // retorna true si la base de datos esta creada en memoria
    return Results.Ok($"Database created: {dbContext.Database.IsInMemory()}");
  }
);

app.Run();
