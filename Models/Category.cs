using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace tasksef.Models
{
  public class Category
  {
    // Key es un atributo (data annotation) que indica que la propiedad Id es la clave primaria de la entidad
    // [Key]
    public Guid Id { get; set; }

    // [Required]
    // [MaxLength(80)]
    public string Name { get; set; }

    public string Description { get; set; }

    public Difficulty Difficulty { get; set; }

    // de esta forma se establece la relación con la clase Task y si queremos incluir una colección de tareas en la clase Category en una consulta, se puede hacer
    [JsonIgnore] // para evitar el bucle infinito
    public virtual ICollection<Task> Tasks { get; set; }
  }

  public enum Difficulty
  {
    Easy,
    Medium,
    Hard
  }
}