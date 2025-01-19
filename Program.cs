using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tasksef;
using tasksef.Models;

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

app.MapGet("/api/tasks", ([FromServices] TasksContext dbContext) =>
{
  // retorna la lista de tareas
  // return Results.Ok(dbContext.Tasks.ToList());
  
  // retorna la lista de tareas con prioridad baja
  // return Results.Ok(dbContext.Tasks.Where(t => t.Priority == Priority.Low).ToList());

  // retorna la lista de tareas con su categoria
  // como sucede en java, esto genera un bucle infinito, ya que la tarea incluye la categoria y la categoria incluye la tarea, para solucionar esto se debe agregar un atributo JsonIgnore en la propiedad Tasks de la clase Category
  return Results.Ok(dbContext.Tasks.Include(t => t.Category));
});

app.MapPost("/api/tasks", async ([FromServices] TasksContext dbContext, [FromBody] tasksef.Models.Task task) =>
{
  task.Id = Guid.NewGuid();
  task.CreatedAt = DateTime.Now;

  // hay dos formas de agregar una tarea a la base de datos
  await dbContext.AddAsync(task); // estos metodos tambien se pueden usan sin el await asi -> dbContext.Tasks.Add(task);
  // await dbContext.Tasks.AddAsync(task);
  await dbContext.SaveChangesAsync(); // tambien tiene el metodo SaveChanges y es importante usarlo despues de agregar o modificar datos en la base de datos para que los cambios se guarden

  return Results.Ok(task);
});

app.MapPut("/api/tasks/{id}", async (
  [FromServices] TasksContext dbContext, 
  [FromBody] tasksef.Models.Task task, 
  [FromRoute] Guid id
) =>
{
  var taskToUpdate = await dbContext.Tasks.FindAsync(id);

  if (taskToUpdate == null)
  {
    return Results.NotFound();
  }

  taskToUpdate.CategoryId = task.CategoryId;
  taskToUpdate.Title = task.Title;
  taskToUpdate.Description = task.Description;
  taskToUpdate.IsComplete = task.IsComplete;
  taskToUpdate.Priority = task.Priority;

  // esto actualiza la tarea en la base de datos automaticamente ya que al obtener la tarea con el contexto, esta se convierte en un objeto rastreado por el contexto, por lo que cualquier cambio que se haga en el objeto se reflejara en la base de datos
  await dbContext.SaveChangesAsync();

  return Results.Ok(taskToUpdate);
});

app.Run();
