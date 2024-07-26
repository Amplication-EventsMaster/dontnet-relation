using Microsoft.EntityFrameworkCore;
using Relation.Infrastructure.Models;

namespace Relation.Infrastructure;

public class RelationDbContext : DbContext
{
    public RelationDbContext(DbContextOptions<RelationDbContext> options)
        : base(options) { }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<OrderDbModel> Orders { get; set; }
}
