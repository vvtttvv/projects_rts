using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlogApp.Domain.Entities;

namespace BlogApp.Postgres.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
	public const string TableName = "posts";

	public void Configure(EntityTypeBuilder<Post> builder)
	{
		builder.ToTable(TableName);
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Title).IsRequired().HasMaxLength(150);
		builder.HasIndex(x => new { x.UserId, x.Title }).IsUnique();

		builder.HasOne(x => x.User)
			.WithMany(x=> x.Posts)
			.HasForeignKey(x => x.UserId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}