using Microsoft.EntityFrameworkCore;
using Relation.APIs;
using Relation.APIs.Common;
using Relation.APIs.Dtos;
using Relation.APIs.Errors;
using Relation.APIs.Extensions;
using Relation.Infrastructure;
using Relation.Infrastructure.Models;

namespace Relation.APIs;

public abstract class OrdersServiceBase : IOrdersService
{
    protected readonly RelationDbContext _context;

    public OrdersServiceBase(RelationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Order
    /// </summary>
    public async Task<Order> CreateOrder(OrderCreateInput createDto)
    {
        var order = new OrderDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Date = createDto.Date,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            order.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            order.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.OrderItems != null)
        {
            order.OrderItems = await _context
                .OrderItems.Where(orderItem =>
                    createDto.OrderItems.Select(t => t.Id).Contains(orderItem.Id)
                )
                .ToListAsync();
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<OrderDbModel>(order.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Order
    /// </summary>
    public async Task DeleteOrder(OrderWhereUniqueInput uniqueId)
    {
        var order = await _context.Orders.FindAsync(uniqueId.Id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many orders
    /// </summary>
    public async Task<List<Order>> Orders(OrderFindManyArgs findManyArgs)
    {
        var orders = await _context
            .Orders.Include(x => x.Customer)
            .Include(x => x.OrderItems)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return orders.ConvertAll(order => order.ToDto());
    }

    /// <summary>
    /// Meta data about Order records
    /// </summary>
    public async Task<MetadataDto> OrdersMeta(OrderFindManyArgs findManyArgs)
    {
        var count = await _context.Orders.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Order
    /// </summary>
    public async Task<Order> Order(OrderWhereUniqueInput uniqueId)
    {
        var orders = await this.Orders(
            new OrderFindManyArgs { Where = new OrderWhereInput { Id = uniqueId.Id } }
        );
        var order = orders.FirstOrDefault();
        if (order == null)
        {
            throw new NotFoundException();
        }

        return order;
    }

    /// <summary>
    /// Update one Order
    /// </summary>
    public async Task UpdateOrder(OrderWhereUniqueInput uniqueId, OrderUpdateInput updateDto)
    {
        var order = updateDto.ToModel(uniqueId);

        if (updateDto.OrderItems != null)
        {
            order.OrderItems = await _context
                .OrderItems.Where(orderItem =>
                    updateDto.OrderItems.Select(t => t).Contains(orderItem.Id)
                )
                .ToListAsync();
        }

        _context.Entry(order).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Orders.Any(e => e.Id == order.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a customer record for order
    /// </summary>
    public async Task<Customer> GetCustomer(OrderWhereUniqueInput uniqueId)
    {
        var order = await _context
            .Orders.Where(order => order.Id == uniqueId.Id)
            .Include(order => order.Customer)
            .FirstOrDefaultAsync();
        if (order == null)
        {
            throw new NotFoundException();
        }
        return order.Customer.ToDto();
    }

    /// <summary>
    /// Connect multiple OrderItems records to Order
    /// </summary>
    public async Task ConnectOrderItems(
        OrderWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] orderItemsId
    )
    {
        var parent = await _context
            .Orders.Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var orderItems = await _context
            .OrderItems.Where(t => orderItemsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (orderItems.Count == 0)
        {
            throw new NotFoundException();
        }

        var orderItemsToConnect = orderItems.Except(parent.OrderItems);

        foreach (var orderItem in orderItemsToConnect)
        {
            parent.OrderItems.Add(orderItem);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple OrderItems records from Order
    /// </summary>
    public async Task DisconnectOrderItems(
        OrderWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] orderItemsId
    )
    {
        var parent = await _context
            .Orders.Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var orderItems = await _context
            .OrderItems.Where(t => orderItemsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var orderItem in orderItems)
        {
            parent.OrderItems?.Remove(orderItem);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple OrderItems records for Order
    /// </summary>
    public async Task<List<OrderItem>> FindOrderItems(
        OrderWhereUniqueInput uniqueId,
        OrderItemFindManyArgs orderFindManyArgs
    )
    {
        var orderItems = await _context
            .OrderItems.Where(m => m.OrderId == uniqueId.Id)
            .ApplyWhere(orderFindManyArgs.Where)
            .ApplySkip(orderFindManyArgs.Skip)
            .ApplyTake(orderFindManyArgs.Take)
            .ApplyOrderBy(orderFindManyArgs.SortBy)
            .ToListAsync();

        return orderItems.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple OrderItems records for Order
    /// </summary>
    public async Task UpdateOrderItems(
        OrderWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] orderItemsId
    )
    {
        var order = await _context
            .Orders.Include(t => t.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        var orderItems = await _context
            .OrderItems.Where(a => orderItemsId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (orderItems.Count == 0)
        {
            throw new NotFoundException();
        }

        order.OrderItems = orderItems;
        await _context.SaveChangesAsync();
    }
}
