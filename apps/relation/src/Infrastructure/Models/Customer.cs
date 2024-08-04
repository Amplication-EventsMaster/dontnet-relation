using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relation.Infrastructure.Models;

[Table("Customers")]
public class CustomerDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public List<OrderDbModel>? Orders { get; set; } = new List<OrderDbModel>();

    [StringLength(1000)]
    public string? Phone { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
