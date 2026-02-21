using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(g => g.Id);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Description).HasMaxLength(300);
        builder.Property(a=>a.CreatedAt).HasDefaultValueSql("NOW()");
    }
}