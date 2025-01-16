using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tasksef.Models;

public class Task
{
  [Key]
  public Guid Id { get; set; }

  [ForeignKey("CategoryId")]
  public Guid CategoryId { get; set; }

  [Required]
  [MaxLength(80)]
  public string Title { get; set; }

  public string Description { get; set; }

  public bool IsComplete { get; set; }

  public Priority Priority { get; set; }

  public DateTime CreatedAt { get; set; }

  public virtual Category Category { get; set; }

  // con esta anotación se indica que la propiedad Resume no se mapea a la base de datos
  [NotMapped]
  public string Resume { get; set; }
}

public enum Priority
{
  Low,
  Medium,
  High
}