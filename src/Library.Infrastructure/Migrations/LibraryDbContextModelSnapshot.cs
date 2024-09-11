﻿// <auto-generated />
using System;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    partial class LibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Library.Domain.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("author");

                    b.Property<DateTime>("CreateddAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdd_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("deleted_at");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("varchar(13)")
                        .HasColumnName("isbn");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_at");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("title");

                    b.Property<int>("YearOfPublication")
                        .HasColumnType("int")
                        .HasColumnName("year_of_publication");

                    b.HasKey("Id")
                        .HasName("pk_books");

                    b.HasIndex("IsDeleted");

                    b.ToTable("books", (string)null);
                });

            modelBuilder.Entity("Library.Domain.Models.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<DateTime>("CreateddAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdd_at");

                    b.Property<DateTime>("DeadlineReturnDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("deadline_return_date");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("loan_date");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_at");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("return_date");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_loans");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("loans", (string)null);
                });

            modelBuilder.Entity("Library.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateddAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdd_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("email");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("name");

                    b.Property<int>("UserTypeEnum")
                        .HasColumnType("int")
                        .HasColumnName("user_type_enum");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Library.Domain.Models.Loan", b =>
                {
                    b.HasOne("Library.Domain.Models.Book", "Book")
                        .WithMany("Loans")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_loans_books_book_id");

                    b.HasOne("Library.Domain.Models.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_loans_users_user_id");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Library.Domain.Models.Book", b =>
                {
                    b.Navigation("Loans");
                });

            modelBuilder.Entity("Library.Domain.Models.User", b =>
                {
                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
