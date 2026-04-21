using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.PostgreSQL.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public const string TableName = "products";

	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.ToTable(TableName);
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
		builder.Property(x => x.Price).HasColumnType("numeric(18,2)");

		builder.HasOne(x => x.Category)
			.WithMany()
			.HasForeignKey(x => x.CategoryId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}