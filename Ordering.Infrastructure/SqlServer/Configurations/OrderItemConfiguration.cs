namespace Ordering.Infrastructure.SqlServer.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems", OrderingDbContext.DefaultSchema);
        
        builder.HasKey(oi => oi.Id);
        
        builder.Property(oi => oi.Id)
            .HasConversion(
                orderItemId => orderItemId.Value,
                dbId => (OrderItemId)dbId);
        
        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);

        builder.Property(oi => oi.Quantity).IsRequired();
        
        builder.Property(oi => oi.Price).IsRequired();
    }
}