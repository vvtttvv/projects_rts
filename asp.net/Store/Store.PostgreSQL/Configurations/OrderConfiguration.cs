using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.PostgreSQL.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public const string TableName = "orders";

	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.ToTable(TableName);
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Quantity).IsRequired();

		builder.HasOne(x => x.Product)
			.WithMany()
			.HasForeignKey(x => x.ProductId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}