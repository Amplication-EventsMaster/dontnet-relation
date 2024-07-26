using Microsoft.AspNetCore.Mvc;

namespace Relation.APIs;

[ApiController()]
public class OrdersController : OrdersControllerBase
{
    public OrdersController(IOrdersService service)
        : base(service) { }
}
