namespace Relation.APIs.Dtos;

public class CustomerCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public List<Order>? Orders { get; set; }

    public string? Phone { get; set; }

    public DateTime UpdatedAt { get; set; }
}
