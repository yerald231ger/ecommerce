using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Data;

public interface IOrderingUnitOfWork
{
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Product> Products { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}