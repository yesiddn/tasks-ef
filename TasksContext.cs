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
    });
  }
}