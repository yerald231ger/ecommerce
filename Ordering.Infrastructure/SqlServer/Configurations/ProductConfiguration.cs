namespace Ordering.Infrastructure.SqlServer.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", OrderingDbContext.DefaultSchema);
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasConversion(
                productId => productId.Value,
                dbId => (ProductId)dbId);

        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
    }
}