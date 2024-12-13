namespace Ordering.Infrastructure.SqlServer.Configurations;

public class CostumerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Costumers", OrderingDbContext.DefaultSchema);
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasConversion(
            costumerId => costumerId.Value,
            dbId => (CustomerId)dbId);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(c => c.Email)
            .IsUnique();
    }
}