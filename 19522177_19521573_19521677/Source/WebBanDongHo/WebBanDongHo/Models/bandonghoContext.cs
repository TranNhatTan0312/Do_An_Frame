using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebBanDongHo.ModelViews;

#nullable disable

namespace WebBanDongHo.Models
{
    public partial class bandonghoContext : DbContext
    {
        public bandonghoContext()
        {
        }

        public bandonghoContext(DbContextOptions<bandonghoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<Attributesprice> Attributesprices { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orderdetail> Orderdetails { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Quangcao> Quangcaos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Tindang> Tindangs { get; set; }
        public virtual DbSet<Transactstatus> Transactstatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost ;user id=root; password=; database=bandongho;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts");

                entity.HasIndex(e => e.RoleId, "FK_Accounts_Roles");

                entity.Property(e => e.AccountId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AccountID");

                entity.Property(e => e.Active).HasColumnType("bit(1)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LastLogin).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("RoleID")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Salt)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'NULL'")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Accounts_Roles");
            });

            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.ToTable("attributes");

                entity.Property(e => e.AttributeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AttributeID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Attributesprice>(entity =>
            {
                entity.ToTable("attributesprices");

                entity.HasIndex(e => e.AttributeId, "FK_AttributesPrices_Attributes");

                entity.HasIndex(e => e.ProductId, "FK_AttributesPrices_Products");

                entity.Property(e => e.AttributesPriceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AttributesPriceID");

                entity.Property(e => e.Active).HasColumnType("bit(1)");

                entity.Property(e => e.AttributeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AttributeID")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Price)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.Attributesprices)
                    .HasForeignKey(d => d.AttributeId)
                    .HasConstraintName("FK_AttributesPrices_Attributes");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Attributesprices)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_AttributesPrices_Products");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PRIMARY");

                entity.ToTable("categories");

                entity.Property(e => e.CatId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CatID");

                entity.Property(e => e.Alias)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CatName)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Cover)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Levels)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MetaDesc)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MetaKey)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Ordering)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ParentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ParentID")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Published).HasColumnType("bit(1)");

                entity.Property(e => e.SchemaMarkup)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Thumb)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.HasIndex(e => e.LocationId, "LocationID");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CustomerID");

                entity.Property(e => e.Active).HasColumnType("bit(1)");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Birthday).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreateDate).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.District)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'")
                    .IsFixedLength(true);

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LastLogin).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LocationId)
                    .HasColumnType("int(11)")
                    .HasColumnName("LocationID")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Salt)
                    .HasMaxLength(8)
                    .HasDefaultValueSql("'NULL'")
                    .IsFixedLength(true);

                entity.Property(e => e.Ward)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("customers_ibfk_1");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("locations");

                entity.Property(e => e.LocationId)
                    .HasColumnType("int(11)")
                    .HasColumnName("LocationID");

                entity.Property(e => e.Levels)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.NameWithType)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Parent)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Slug)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.CustomerId, "FK_Orders_Customers");

                entity.HasIndex(e => e.TransactStatusId, "FK_Orders_TransactStatus");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("OrderID");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CustomerID")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Deleted).HasColumnType("bit(1)");

                entity.Property(e => e.District)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LocationId)
                    .HasColumnType("int(11)")
                    .HasColumnName("LocationID")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Note)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.OrderDate).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Paid).HasColumnType("bit(1)");

                entity.Property(e => e.PaymentDate).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PaymentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("PaymentID")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ShipDate).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TotalMoney).HasColumnType("int(11)");

                entity.Property(e => e.TransactStatusId)
                    .HasColumnType("int(11)")
                    .HasColumnName("TransactStatusID");

                entity.Property(e => e.Ward)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.TransactStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TransactStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_TransactStatus");
            });

            modelBuilder.Entity<Orderdetail>(entity =>
            {
                entity.ToTable("orderdetails");

                entity.HasIndex(e => e.OrderId, "FK_OrderDetails_Orders");

                entity.HasIndex(e => e.ProductId, "FK_OrderDetails_Products");

                entity.Property(e => e.OrderDetailId)
                    .HasColumnType("int(11)")
                    .HasColumnName("OrderDetailID");

                entity.Property(e => e.Amount)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreateDate).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Discount)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("OrderID")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.OrderNumber)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Price)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TotalMoney)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_OrderDetails_Products");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("pages");

                entity.Property(e => e.PageId)
                    .HasColumnType("int(11)")
                    .HasColumnName("PageID");

                entity.Property(e => e.Alias)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Contents)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MetaDesc)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MetaKey)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Ordering)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PageName)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Published).HasColumnType("bit(1)");

                entity.Property(e => e.Thumb)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.CatId, "FK_Products_Categories");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID");

                entity.Property(e => e.Active).HasColumnType("bit(1)");

                entity.Property(e => e.Alias)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BestSellers).HasColumnType("bit(1)");

                entity.Property(e => e.CatId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CatID")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateModified).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Discount)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.HomeFlag).HasColumnType("bit(1)");

                entity.Property(e => e.MetaDesc)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MetaKey)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Price)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ShortDesc)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Tags)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Thumb)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UnitsInStock)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Video)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK_Products_Categories");
            });

            modelBuilder.Entity<Quangcao>(entity =>
            {
                entity.ToTable("quangcaos");

                entity.Property(e => e.QuangCaoId)
                    .HasColumnType("int(11)")
                    .HasColumnName("QuangCaoID");

                entity.Property(e => e.Active).HasColumnType("bit(1)");

                entity.Property(e => e.CreateDate).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ImageBg)
                    .HasMaxLength(250)
                    .HasColumnName("ImageBG")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ImageProduct)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SubTitle)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UrlLink)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("RoleID");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("shippers");

                entity.Property(e => e.ShipperId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ShipperID");

                entity.Property(e => e.Company)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'NULL'")
                    .IsFixedLength(true);

                entity.Property(e => e.ShipDate).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ShipperName)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Tindang>(entity =>
            {
                entity.HasKey(e => e.PostId)
                    .HasName("PRIMARY");

                entity.ToTable("tindangs");

                entity.Property(e => e.PostId)
                    .HasColumnType("int(11)")
                    .HasColumnName("PostID");

                entity.Property(e => e.AccountId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AccountID")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Alias)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Author)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CatId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CatID")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Contents)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsHot)
                    .HasColumnType("bit(1)")
                    .HasColumnName("isHot");

                entity.Property(e => e.IsNewfeed)
                    .HasColumnType("bit(1)")
                    .HasColumnName("isNewfeed");

                entity.Property(e => e.MetaDesc)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MetaKey)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Published).HasColumnType("bit(1)");

                entity.Property(e => e.Scontents)
                    .HasMaxLength(255)
                    .HasColumnName("SContents")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Tags)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Thumb)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Views)
                    .HasColumnType("int(11)")
                    .HasColumnName("_Views")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Transactstatus>(entity =>
            {
                entity.ToTable("transactstatus");

                entity.Property(e => e.TransactStatusId)
                    .HasColumnType("int(11)")
                    .HasColumnName("TransactStatusID");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<WebBanDongHo.ModelViews.RegisterVM> RegisterVM { get; set; }
    }
}
