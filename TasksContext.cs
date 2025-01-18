// a nivel de nombramiento, generalmente se usa el nombre de la app y al final 'Context'
using Microsoft.EntityFrameworkCore;
using tasksef.Models;

namespace tasksef;

// DbContext contiene todos los componentes que se necesitan para crear la configuración de la base de datos
public class TasksContext : DbContext {
  // DbSet es una colección de entidades que se pueden consultar y guardar en la base de datos
  public DbSet<Category> Categories { get; set; }
  public DbSet<Models.Task> Tasks { get; set; }

  // DbContextOptions es una clase que contiene la configuración de la base de datos
  public TasksContext(DbContextOptions<TasksContext> options) : base(options) { }

  // no es buena practica combinar data annotations con fluent api, aunque en dado caso de hacerlo, se le da prioridad a fluent api porque se ejecuta después de data annotations
  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    // Guid.NewGuid() genera un identificador, pero generara uno diferente cada vez que se ejecute la funcion OnModelCreating, por ello es recomendable usar un identificador fijo -> GUID generator
    List<Category> categoriesInit =
    [
      new Category { 
        Id = Guid.Parse("13d3c9ba-5d63-4397-84b1-58b2fcdd6202"), 
        Name = "Personal", 
        Description = "Tareas personales", 
        Difficulty = Difficulty.Easy 
      },
      new Category { 
        Id = Guid.Parse("f5950436-1c72-4d10-a211-9752045e2947"), 
        Name = "Work", 
        Description = "Tareas de trabajo", 
        Difficulty = Difficulty.Medium 
      },
    ];

    modelBuilder.Entity<Category>(category =>
    {
      category.ToTable("Category");
      category.HasKey(p => p.Id);

      category.Property(p => p.Name).IsRequired().HasMaxLength(80);
      // en el archivo .csproj la propiedad <Nullable>enable</Nullable> hace que las propiedades de las entidades no sean nulas, por ello no es necesario usar .IsRequired(false)
      category.Property(p => p.Description).HasMaxLength(150).IsRequired(false);
      category.Property(p => p.Difficulty);

      // se insertan los datos de la lista categoriesInit en la tabla Category
      category.HasData(categoriesInit);
    });

    List<Models.Task> tasksInit =
    [
      new Models.Task { 
        Id = Guid.Parse("3161ea46-b39e-434f-8893-bb8a1f8932dc"), 
        CategoryId = Guid.Parse("13d3c9ba-5d63-4397-84b1-58b2fcdd6202"), 
        Title = "Comprar leche",
        Description = "Ir al supermercado y comprar leche",
        Priority = Priority.Low,
        // CreatedAt = DateTime.Now
      },
      new Models.Task { 
        Id = Guid.Parse("98326563-8e88-4402-b12e-4bd824967291"), 
        CategoryId = Guid.Parse("13d3c9ba-5d63-4397-84b1-58b2fcdd6202"), 
        Title = "Sacar la basura",
        Priority = Priority.Low,
        // CreatedAt = DateTime.Now
      },
    ];

    modelBuilder.Entity<Models.Task>(task =>
    {
      task.ToTable("Task");
      task.HasKey(p => p.Id);

      // p.Tasks es el metodo virtual de la clase Category que tiene la ICollection de tareas
      task.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey(p => p.CategoryId);
      task.Property(p => p.Title).IsRequired().HasMaxLength(80);
      task.Property(p => p.Description).HasMaxLength(150).IsRequired(false);
      task.Property(p => p.IsComplete).HasDefaultValue(false);
      task.Property(p => p.Priority).HasConversion<string>();
      task.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
      // la propiedad Resume no se mapea a la base de datos
      task.Ignore(p => p.Resume);

      task.HasData(tasksInit);
    });
  }
}