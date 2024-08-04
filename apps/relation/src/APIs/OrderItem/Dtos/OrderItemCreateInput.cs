namespace Relation.APIs.Dtos;

public class OrderItemCreateInput
{
    public DateTime CreatedAt { get; set; }

    public DateTime? Date { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public Order? Order { get; set; }

    public DateTime UpdatedAt { get; set; }
}
