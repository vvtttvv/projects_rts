using Entity_Framework_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity_Framework_Core.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasIndex(a => a.Name).IsUnique();
        
        builder.Property(a => a.Country)
            .IsRequired()
            .HasMaxLength(40);

        builder.HasData(
            new Author { Id = 1,  Name = "Marksist Leninist", Country = "Moldova" }
        );
    }
}
