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
}