using Microsoft.AspNetCore.Mvc;
using Relation.APIs;
using Relation.APIs.Common;
using Relation.APIs.Dtos;
using Relation.APIs.Errors;

namespace Relation.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Connect multiple orders records to Customer
    /// </summary>
    [HttpPost("{Id}/orders")]
    public async Task<ActionResult> ConnectOrders(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] OrderWhereUniqueInput[] ordersId
    )
    {
        try
        {
            await _service.ConnectOrders(uniqueId, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple orders records from Customer
    /// </summary>
    [HttpDelete("{Id}/orders")]
    public async Task<ActionResult> DisconnectOrders(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] OrderWhereUniqueInput[] ordersId
    )
    {
        try
        {
            await _service.DisconnectOrders(uniqueId, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple orders records for Customer
    /// </summary>
    [HttpGet("{Id}/orders")]
    public async Task<ActionResult<List<Order>>> FindOrders(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] OrderFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindOrders(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CustomersMeta(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.CustomersMeta(filter));
    }

    /// <summary>
    /// Update multiple orders records for Customer
    /// </summary>
    [HttpPatch("{Id}/orders")]
    public async Task<ActionResult> UpdateOrders(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] OrderWhereUniqueInput[] ordersId
    )
    {
        try
        {
            await _service.UpdateOrders(uniqueId, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCustomer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Customer>>> Customers(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Customer>> Customer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Customer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(uniqueId, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
