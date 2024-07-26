using Relation.Infrastructure;

namespace Relation.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(RelationDbContext context)
        : base(context) { }
}
