using Relation.Infrastructure;

namespace Relation.APIs;

public class OrderItemsService : OrderItemsServiceBase
{
    public OrderItemsService(RelationDbContext context)
        : base(context) { }
}
