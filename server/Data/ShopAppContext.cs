using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShopAppApi.Data;

public partial class ShopAppContext : DbContext
{
    public ShopAppContext()
    {
    }

    public ShopAppContext(DbContextOptions<ShopAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Migration> Migrations { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<OptionValue> OptionValues { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductComment> ProductComments { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductStatistic> ProductStatistics { get; set; }

    public virtual DbSet<ProductWarehouse> ProductWarehouses { get; set; }

    public virtual DbSet<Sku> Skus { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Tax> Taxes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Variant> Variants { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer("Data Source=localhost,1434;Initial Catalog=shop_app;User ID=sa;Password=Anhem123@;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__brands__3213E83F0ECF6D00");

            entity.ToTable("brands");

            entity.HasIndex(e => e.Code, "brands_code_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NotUse)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("not_use");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__carts__3213E83FDD3A0109");

            entity.ToTable("carts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("('1')")
                .HasColumnName("quantity");
            entity.Property(e => e.SkuId).HasColumnName("sku_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("carts_customer_id_foreign");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("carts_product_id_foreign");

            entity.HasOne(d => d.Sku).WithMany(p => p.Carts)
                .HasForeignKey(d => d.SkuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("carts_sku_id_foreign");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83FAE859EA3");

            entity.ToTable("categories");

            entity.HasIndex(e => new { e.Lft, e.Rgt, e.ParentId }, "categories__lft__rgt_parent_id_index");

            entity.HasIndex(e => e.Code, "categories_code_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(24)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.HidenMenu)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("hiden_menu");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.IsPopular)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("is_popular");
            entity.Property(e => e.Lft)
                .HasDefaultValueSql("('0')")
                .HasColumnName("_lft");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NotUse)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("not_use");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Rgt)
                .HasDefaultValueSql("('0')")
                .HasColumnName("_rgt");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__customer__3213E83F01C57C68");

            entity.ToTable("customers");

            entity.HasIndex(e => e.Email, "customers_email_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.EmailVerifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("email_verified_at");
            entity.Property(e => e.Gender)
                .HasDefaultValueSql("('0')")
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(24)
                .HasColumnName("phone");
            entity.Property(e => e.RememberToken)
                .HasMaxLength(100)
                .HasColumnName("remember_token");
            entity.Property(e => e.Salt).HasColumnName("salt");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("('0')")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__discount__3213E83FC7957436");

            entity.ToTable("discounts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DiscountPercent)
                .HasDefaultValueSql("('0')")
                .HasColumnName("discount_percent");
            entity.Property(e => e.DiscountValue)
                .HasDefaultValueSql("('0')")
                .HasColumnName("discount_value");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.SkuId).HasColumnName("sku_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("('0')")
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Product).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("discounts_product_id_foreign");

            entity.HasOne(d => d.Sku).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.SkuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("discounts_sku_id_foreign");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__menus__3213E83F2031568A");

            entity.ToTable("menus");

            entity.HasIndex(e => new { e.Lft, e.Rgt, e.ParentId }, "menus__lft__rgt_parent_id_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.GroupMenu)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("group_menu");
            entity.Property(e => e.Hidden)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("hidden");
            entity.Property(e => e.Icon)
                .HasMaxLength(255)
                .HasColumnName("icon");
            entity.Property(e => e.Lft)
                .HasDefaultValueSql("('0')")
                .HasColumnName("_lft");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Position)
                .HasDefaultValueSql("('1')")
                .HasColumnName("position");
            entity.Property(e => e.Rgt)
                .HasDefaultValueSql("('0')")
                .HasColumnName("_rgt");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");
        });

        modelBuilder.Entity<Migration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__migratio__3213E83FDE44151A");

            entity.ToTable("migrations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Batch).HasColumnName("batch");
            entity.Property(e => e.Migration1)
                .HasMaxLength(255)
                .HasColumnName("migration");
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__options__3213E83F3C3BE608");

            entity.ToTable("options");

            entity.HasIndex(e => e.Code, "options_code_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(64)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Order)
                .HasDefaultValueSql("('0')")
                .HasColumnName("order");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Visual)
                .HasDefaultValueSql("('0')")
                .HasColumnName("visual");

            entity.HasOne(d => d.Product).WithMany(p => p.Options)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("options_product_id_foreign");
        });

        modelBuilder.Entity<OptionValue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__option_v__3213E83F43F28935");

            entity.ToTable("option_values");

            entity.HasIndex(e => e.Code, "option_values_code_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(64)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Label)
                .HasMaxLength(100)
                .HasColumnName("label");
            entity.Property(e => e.OptionId).HasColumnName("option_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("value");

            entity.HasOne(d => d.Option).WithMany(p => p.OptionValues)
                .HasForeignKey(d => d.OptionId)
                .HasConstraintName("option_values_option_id_foreign");

            entity.HasOne(d => d.Product).WithMany(p => p.OptionValues)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("option_values_product_id_foreign");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3213E83FBE9429A5");

            entity.ToTable("orders");

            entity.HasIndex(e => e.Code, "orders_code_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(24)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
            entity.Property(e => e.DiscountAmount)
                .HasDefaultValueSql("('0')")
                .HasColumnName("discount_amount");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("('0')")
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount)
                .HasDefaultValueSql("('0')")
                .HasColumnName("total_amount");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("orders_customer_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("orders_user_id_foreign");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order_de__3213E83FB7EC6192");

            entity.ToTable("order_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DiscountAmount)
                .HasDefaultValueSql("('0')")
                .HasColumnName("discount_amount");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("('0')")
                .HasColumnName("quantity");
            entity.Property(e => e.SkuId).HasColumnName("sku_id");
            entity.Property(e => e.TotalAmount)
                .HasDefaultValueSql("('0')")
                .HasColumnName("total_amount");
            entity.Property(e => e.UnitPrice)
                .HasDefaultValueSql("('0')")
                .HasColumnName("unit_price");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("order_details_product_id_foreign");

            entity.HasOne(d => d.Sku).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.SkuId)
                .HasConstraintName("order_details_sku_id_foreign");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3213E83FC919C14C");

            entity.ToTable("products");

            entity.HasIndex(e => e.Alias, "products_alias_unique").IsUnique();

            entity.HasIndex(e => e.Barcode, "products_barcode_unique").IsUnique();

            entity.HasIndex(e => e.Code, "products_code_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alias)
                .HasMaxLength(500)
                .HasColumnName("alias");
            entity.Property(e => e.Barcode)
                .HasMaxLength(24)
                .HasColumnName("barcode");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CanDelete)
                .IsRequired()
                .HasDefaultValueSql("('1')")
                .HasColumnName("can_delete");
            entity.Property(e => e.CanEdit)
                .IsRequired()
                .HasDefaultValueSql("('1')")
                .HasColumnName("can_edit");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Code)
                .HasMaxLength(24)
                .HasColumnName("code");
            entity.Property(e => e.ConversionUnit)
                .HasDefaultValueSql("('1')")
                .HasColumnName("conversion_unit");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.HasVariants)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("has_variants");
            entity.Property(e => e.ImageThumb)
                .HasMaxLength(255)
                .HasColumnName("image_thumb");
            entity.Property(e => e.IsFeatured)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("is_featured");
            entity.Property(e => e.IsNew)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("is_new");
            entity.Property(e => e.IsSale)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("is_sale");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.NumberWarning)
                .HasDefaultValueSql("('0')")
                .HasColumnName("number_warning");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("('0')")
                .HasColumnName("price");
            entity.Property(e => e.SoldOut)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("sold_out");
            entity.Property(e => e.TaxId).HasColumnName("tax_id");
            entity.Property(e => e.UnitBuy)
                .HasMaxLength(150)
                .HasColumnName("unit_buy");
            entity.Property(e => e.UnitSell)
                .HasMaxLength(150)
                .HasColumnName("unit_sell");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_brand_id_foreign");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_category_id_foreign");

            entity.HasOne(d => d.Tax).WithMany(p => p.Products)
                .HasForeignKey(d => d.TaxId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_tax_id_foreign");
        });

        modelBuilder.Entity<ProductComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3213E83F171932A5");

            entity.ToTable("product_comments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Rate)
                .HasDefaultValueSql("('5')")
                .HasColumnName("rate");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductComments)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("product_comments_product_id_foreign");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3213E83F2ED8E99E");

            entity.ToTable("product_images");

            entity.HasIndex(e => e.Code, "product_images_code_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(64)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Driver)
                .HasMaxLength(50)
                .HasColumnName("driver");
            entity.Property(e => e.Extension)
                .HasMaxLength(50)
                .HasColumnName("extension");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .HasColumnName("file_name");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Path)
                .HasMaxLength(255)
                .HasColumnName("path");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("('0')")
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("product_images_product_id_foreign");
        });

        modelBuilder.Entity<ProductStatistic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3213E83F0367AE98");

            entity.ToTable("product_statistics");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommentCount)
                .HasDefaultValueSql("('0')")
                .HasColumnName("comment_count");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.RateCount)
                .HasDefaultValueSql("('0')")
                .HasColumnName("rate_count");
            entity.Property(e => e.SoldCount)
                .HasDefaultValueSql("('0')")
                .HasColumnName("sold_count");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.ViewCount)
                .HasDefaultValueSql("('0')")
                .HasColumnName("view_count");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductStatistics)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("product_statistics_product_id_foreign");
        });

        modelBuilder.Entity<ProductWarehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3213E83FB5F56489");

            entity.ToTable("product_warehouses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.SkuId).HasColumnName("sku_id");
            entity.Property(e => e.Stock)
                .HasDefaultValueSql("('0')")
                .HasColumnName("stock");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

            entity.HasOne(d => d.Sku).WithMany(p => p.ProductWarehouses)
                .HasForeignKey(d => d.SkuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_warehouses_sku_id_foreign");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.ProductWarehouses)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_warehouses_warehouse_id_foreign");
        });

        modelBuilder.Entity<Sku>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__skus__3213E83FCF1FE709");

            entity.ToTable("skus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Barcode)
                .HasMaxLength(24)
                .HasColumnName("barcode");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ImageCode)
                .HasMaxLength(255)
                .HasColumnName("image_code");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .HasColumnName("image_path");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("('0')")
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Stock)
                .HasDefaultValueSql("('0')")
                .HasColumnName("stock");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Product).WithMany(p => p.Skus)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("skus_product_id_foreign");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__supplier__3213E83F00832D3C");

            entity.ToTable("suppliers");

            entity.HasIndex(e => e.Code, "suppliers_code_unique").IsUnique();

            entity.HasIndex(e => e.Email, "suppliers_email_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .HasColumnName("address");
            entity.Property(e => e.Code)
                .HasMaxLength(24)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Logo)
                .HasMaxLength(500)
                .HasColumnName("logo");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NotUse)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("not_use");
            entity.Property(e => e.Phone)
                .HasMaxLength(18)
                .HasColumnName("phone");
            entity.Property(e => e.TaxCode)
                .HasMaxLength(24)
                .HasColumnName("tax_code");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Tax>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__taxes__3213E83F3691C305");

            entity.ToTable("taxes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NotUse)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("not_use");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FDCD5DCFB");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.EmailVerifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("email_verified_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RememberToken)
                .HasMaxLength(100)
                .HasColumnName("remember_token");
            entity.Property(e => e.Salt).HasColumnName("salt");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Variant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__variants__3213E83F01DB3207");

            entity.ToTable("variants");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.OptionId).HasColumnName("option_id");
            entity.Property(e => e.OptionValueId).HasColumnName("option_value_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.SkuId).HasColumnName("sku_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Option).WithMany(p => p.Variants)
                .HasForeignKey(d => d.OptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("variants_option_id_foreign");

            entity.HasOne(d => d.OptionValue).WithMany(p => p.Variants)
                .HasForeignKey(d => d.OptionValueId)
                .HasConstraintName("variants_option_value_id_foreign");

            entity.HasOne(d => d.Product).WithMany(p => p.Variants)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("variants_product_id_foreign");

            entity.HasOne(d => d.Sku).WithMany(p => p.Variants)
                .HasForeignKey(d => d.SkuId)
                .HasConstraintName("variants_sku_id_foreign");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__warehous__3213E83FA8DC2E2D");

            entity.ToTable("warehouses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(225)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(24)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
