namespace tasksef.Models
{
  public class Category
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    // de esta forma se establece la relación con la clase Task y si queremos incluir una colección de tareas en la clase Category en una consulta, se puede hacer
    public virtual ICollection<Task> Tasks { get; set; }
  }
}