namespace Relation.APIs.Dtos;

public class CustomerUpdateInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public List<string>? Orders { get; set; }
}
