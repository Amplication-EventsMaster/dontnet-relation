namespace Relation.APIs.Dtos;

public class OrderUpdateInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Customer { get; set; }

    public DateTime? Date { get; set; }

    public List<string>? OrderItems { get; set; }
}
