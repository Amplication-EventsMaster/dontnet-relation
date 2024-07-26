namespace Relation.APIs.Dtos;

public class OrderItemUpdateInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Name { get; set; }

    public DateTime? Date { get; set; }

    public string? Order { get; set; }
}
