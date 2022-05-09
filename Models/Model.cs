using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BookStore.Models
{
    public partial class Model : DbContext
    {
        public Model()
            : base("name=Book")
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryGroup> CategoryGroups { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Reader> Readers { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .Property(e => e.AuthorID)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.BookID)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.BookDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.AuthorID)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.ProducerID)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Categories)
                .WithMany(e => e.Books)
                .Map(m => m.ToTable("Book_Category").MapLeftKey("BookID").MapRightKey("CategoryID"));

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Readers)
                .WithMany(e => e.Books)
                .Map(m => m.ToTable("Book_Reader").MapLeftKey("BookID").MapRightKey("ReaderID"));

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Customers)
                .WithMany(e => e.Books)
                .Map(m => m.ToTable("Wishlist").MapLeftKey("BookID").MapRightKey("CustomerID"));

            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryID)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.CateGroupID)
                .IsUnicode(false);

            modelBuilder.Entity<CategoryGroup>()
                .Property(e => e.GroupID)
                .IsUnicode(false);

            modelBuilder.Entity<CategoryGroup>()
                .HasMany(e => e.Categories)
                .WithRequired(e => e.CategoryGroup)
                .HasForeignKey(e => e.CateGroupID);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerID)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.NewsID)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderID)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.CustomerID)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.PaymentID)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.OrderID)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.BookID)
                .IsUnicode(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.PaymentID)
                .IsUnicode(false);

            modelBuilder.Entity<Producer>()
                .Property(e => e.ProducerID)
                .IsUnicode(false);

            modelBuilder.Entity<Producer>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Producer>()
                .Property(e => e.FAX)
                .IsUnicode(false);

            modelBuilder.Entity<Producer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Producer>()
                .Property(e => e.Website)
                .IsUnicode(false);

            modelBuilder.Entity<Producer>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Reader>()
                .Property(e => e.ReaderID)
                .IsUnicode(false);

            modelBuilder.Entity<Reader>()
                .Property(e => e.ReaderDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Reader>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.ReviewID)
                .IsUnicode(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.BookID)
                .IsUnicode(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.CustomerID)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.RoleID)
                .IsUnicode(false);
        }
    }
}
