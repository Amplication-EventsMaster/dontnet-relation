namespace Relation.APIs.Dtos;

public class OrderCreateInput
{
    public DateTime CreatedAt { get; set; }

    public Customer Customer { get; set; }

    public DateTime? Date { get; set; }

    public string? Id { get; set; }

    public List<OrderItem>? OrderItems { get; set; }

    public DateTime UpdatedAt { get; set; }
}
