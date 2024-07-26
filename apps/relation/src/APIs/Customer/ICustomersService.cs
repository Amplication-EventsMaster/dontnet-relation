using Relation.APIs.Common;
using Relation.APIs.Dtos;

namespace Relation.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<Customer> CreateCustomer(CustomerCreateInput customer);

    /// <summary>
    /// Connect multiple orders records to Customer
    /// </summary>
    public Task ConnectOrders(CustomerWhereUniqueInput uniqueId, OrderWhereUniqueInput[] ordersId);

    /// <summary>
    /// Disconnect multiple orders records from Customer
    /// </summary>
    public Task DisconnectOrders(
        CustomerWhereUniqueInput uniqueId,
        OrderWhereUniqueInput[] ordersId
    );

    /// <summary>
    /// Find multiple orders records for Customer
    /// </summary>
    public Task<List<Order>> FindOrders(
        CustomerWhereUniqueInput uniqueId,
        OrderFindManyArgs OrderFindManyArgs
    );

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple orders records for Customer
    /// </summary>
    public Task UpdateOrders(CustomerWhereUniqueInput uniqueId, OrderWhereUniqueInput[] ordersId);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<Customer> Customer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerWhereUniqueInput uniqueId, CustomerUpdateInput updateDto);
}
