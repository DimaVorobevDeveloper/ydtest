using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YDTest.Data.Abstractions;

public class EntityBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }

    public DateTime Created { get; set; }
    
    public DateTime Modified { get; set; }

    public DateTime? Deleted { get; set; }

    public bool? IsDeleted { get; set; }
}
