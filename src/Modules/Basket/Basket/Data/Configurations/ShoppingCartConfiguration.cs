namespace Basket.Data.Configurations;

internal class ShoppingCartConfiguration 
    : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasIndex(c => c.Username)
            .IsUnique();

        builder.Property(c => c.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(c => c.Items)
            .WithOne()
            .HasForeignKey(i => i.ShoppingCartId);
    }
}
