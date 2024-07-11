using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(_ => _.Id);

            builder
              .ToTable("users");

            builder
              .Property(_ => _.Name)
              .IsRequired()
              .HasColumnType("varchar(30)");

            builder
              .Property(_ => _.Email)
              .IsRequired()
              .HasColumnType("varchar(50)");

            builder
                .HasMany(_ => _.Loans)
                .WithOne(_ => _.User)
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
