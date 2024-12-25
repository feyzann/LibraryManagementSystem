using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace IleriWebProject.Models;

public partial class LibraryManagementSystemContext : DbContext
{
    public LibraryManagementSystemContext()
    {
    }

    public LibraryManagementSystemContext(DbContextOptions<LibraryManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Bookstatus> Bookstatuses { get; set; }

    public virtual DbSet<Borrowtransaction> Borrowtransactions { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VBooksByCategory> VBooksByCategories { get; set; }

    public virtual DbSet<VBooksCountByStatus> VBooksCountByStatuses { get; set; }

    public virtual DbSet<VBooksStatus> VBooksStatuses { get; set; }

    public virtual DbSet<VUserBorrowedBook> VUserBorrowedBooks { get; set; }

    public virtual DbSet<VUsersBorrowedBooksStatus> VUsersBorrowedBooksStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=LibraryManagementSystem;user=root;password=1042", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.1.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PRIMARY");

            entity.ToTable("books");

            entity.HasIndex(e => e.CategoryId, "CategoryID");

            entity.HasIndex(e => e.StatusId, "StatusID");

            entity.HasIndex(e => e.BookName, "idx_books_name");

            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.BookName).HasMaxLength(100);
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("books_ibfk_1");

            entity.HasOne(d => d.Status).WithMany(p => p.Books)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("books_ibfk_2");
        });

        modelBuilder.Entity<Bookstatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PRIMARY");

            entity.ToTable("bookstatus");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StatusDescription).HasMaxLength(50);
        });

        modelBuilder.Entity<Borrowtransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PRIMARY");

            entity.ToTable("borrowtransactions");

            entity.HasIndex(e => e.BookId, "idx_borrowtransactions_bookid");

            entity.HasIndex(e => e.UserId, "idx_borrowtransactions_userid");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Book).WithMany(p => p.Borrowtransactions)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("borrowtransactions_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Borrowtransactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("borrowtransactions_ibfk_1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.RoleId, "RoleID");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("users_ibfk_1");
        });

        modelBuilder.Entity<VBooksByCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_books_by_category");

            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.BookName).HasMaxLength(100);
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<VBooksCountByStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_books_count_by_status");

            entity.Property(e => e.StatusDescription).HasMaxLength(50);
        });

        modelBuilder.Entity<VBooksStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_books_status");

            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.BookName).HasMaxLength(100);
            entity.Property(e => e.StatusDescription).HasMaxLength(50);
        });

        modelBuilder.Entity<VUserBorrowedBook>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_user_borrowed_books");

            entity.Property(e => e.BookName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.MaskedPhoneNumber).HasMaxLength(23);
        });

        modelBuilder.Entity<VUsersBorrowedBooksStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_users_borrowed_books_status");

            entity.Property(e => e.BookName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.StatusDescription).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
