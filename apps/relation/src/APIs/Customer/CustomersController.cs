using Microsoft.AspNetCore.Mvc;

namespace Relation.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
