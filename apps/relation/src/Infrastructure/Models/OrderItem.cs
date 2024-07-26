using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relation.Infrastructure.Models;

[Table("OrderItems")]
public class OrderItemDbModel
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public DateTime? Date { get; set; }

    public string? OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public OrderDbModel? Order { get; set; } = null;
}
