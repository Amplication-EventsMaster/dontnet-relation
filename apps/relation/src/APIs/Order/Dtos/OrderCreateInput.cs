namespace Relation.APIs.Dtos;

public class OrderCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Customer? Customer { get; set; }

    public DateTime? Date { get; set; }
}
