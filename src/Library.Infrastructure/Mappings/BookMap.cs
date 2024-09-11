using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Mappings
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
               .ToTable("books");

            builder
                .HasKey(_ => _.Id);

            builder
             .Property(_ => _.Title)
             .IsRequired()
             .HasColumnType("varchar(30)");

            builder
             .Property(_ => _.Author)
             .IsRequired()
             .HasColumnType("varchar(30)");

            builder
             .Property(_ => _.ISBN)
             .IsRequired()
             .HasColumnType("varchar(13)");

            builder
                .HasMany(_ => _.Loans)
                .WithOne(_ => _.Book)
                .HasForeignKey(_ => _.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.IsDeleted);

            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
