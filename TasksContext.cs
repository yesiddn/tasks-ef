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
    modelBuilder.Entity<Category>(category =>
    {
      category.ToTable("Category");
      category.HasKey(p => p.Id);

      category.Property(p => p.Name).IsRequired().HasMaxLength(80);
      category.Property(p => p.Description).HasMaxLength(150);
      category.Property(p => p.Difficulty);
    });

    modelBuilder.Entity<Models.Task>(task =>
    {
      task.ToTable("Task");
      task.HasKey(p => p.Id);

      // p.Tasks es el metodo virtual de la clase Category que tiene la ICollection de tareas
      task.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey(p => p.CategoryId);
      task.Property(p => p.Title).IsRequired().HasMaxLength(80);
      task.Property(p => p.Description).HasMaxLength(150);
      task.Property(p => p.IsComplete).HasDefaultValue(false);
      task.Property(p => p.Priority).HasConversion<string>();
      task.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
      // la propiedad Resume no se mapea a la base de datos
      task.Ignore(p => p.Resume);
    });
  }
}