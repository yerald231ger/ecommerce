namespace Ordering.Infrastructure.SqlServer.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", OrderingDbContext.DefaultSchema);

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasConversion(
                orderId => orderId.Value,
                dbId => (OrderId)dbId);

        builder.ComplexProperty(o => o.OrderName, propertyBuilder =>
        {
            propertyBuilder.Property(oi => oi.Value)
                .HasColumnName(nameof(Order.OrderName))
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.ComplexProperty(o => o.ShippingAddress, propertyBuilder =>
        {
            propertyBuilder.Property(sa => sa.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            propertyBuilder.Property(sa => sa.LastName)
                .HasMaxLength(50)
                .IsRequired();

            propertyBuilder.Property(sa => sa.EmailAddress)
                .HasMaxLength(50)
                .IsRequired();

            propertyBuilder.Property(sa => sa.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

            propertyBuilder.Property(sa => sa.Country)
                .HasMaxLength(50);

            propertyBuilder.Property(sa => sa.State)
                .HasMaxLength(50);

            propertyBuilder.Property(sa => sa.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.BillingAddress, propertyBuilder =>
        {
            propertyBuilder.Property(ba => ba.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            propertyBuilder.Property(ba => ba.LastName)
                .HasMaxLength(50)
                .IsRequired();

            propertyBuilder.Property(ba => ba.EmailAddress)
                .HasMaxLength(50)
                .IsRequired();

            propertyBuilder.Property(ba => ba.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

            propertyBuilder.Property(ba => ba.Country)
                .HasMaxLength(50);

            propertyBuilder.Property(ba => ba.State)
                .HasMaxLength(50);

            propertyBuilder.Property(ba => ba.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.Payment, propertyBuilder =>
        {
            propertyBuilder.Property(p => p.CardHolderName)
                .HasMaxLength(50);

            propertyBuilder.Property(p => p.CardNumber)
                .HasMaxLength(25)
                .IsRequired();

            propertyBuilder.Property(p => p.CardExpiration)
                .HasMaxLength(10);

            propertyBuilder.Property(p => p.CardCvv)
                .HasMaxLength(3);

            propertyBuilder.Property(p => p.PaymentMethod);
        });
        
        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                status => status.ToString(),
                dbStatus => Enum.Parse<OrderStatus>(dbStatus));

        builder.Property(o => o.TotalPrice);

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);
    }
}