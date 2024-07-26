using Relation.Infrastructure;

namespace Relation.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(RelationDbContext context)
        : base(context) { }
}
