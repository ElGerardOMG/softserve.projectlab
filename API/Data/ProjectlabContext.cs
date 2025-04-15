using System;
using System.Collections.Generic;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public partial class ProjectlabContext : DbContext
{
    public ProjectlabContext()
    {
    }

    public ProjectlabContext(DbContextOptions<ProjectlabContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Models.Attribute> Attributes { get; set; }

    public virtual DbSet<AttributeCategory> AttributeCategories { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<FinancePack> FinancePacks { get; set; }

    public virtual DbSet<FinancePackInterval> FinancePackIntervals { get; set; }

    public virtual DbSet<FinancedOrder> FinancedOrders { get; set; }

    public virtual DbSet<FinancedOrderPay> FinancedOrderPays { get; set; }

    public virtual DbSet<IntervalType> IntervalTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<Shipment> Shipments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserCardPayment> UserCardPayments { get; set; }

    public virtual DbSet<UserPaypalPayment> UserPaypalPayments { get; set; }

    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

    public virtual DbSet<UserWallet> UserWallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PC-BELICA; Initial Catalog=projectlab ;User=root ;Password=root123 ;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Attribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__attribut__3214EC0794CB92C7");

            entity.ToTable("attribute", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.Field).HasMaxLength(255);
            entity.Property(e => e.IdAttributeCategory).HasColumnName("Id_attribute_category");
            entity.Property(e => e.IdProduct).HasColumnName("Id_product");
            entity.Property(e => e.IdProductVariant).HasColumnName("Id_product_variant");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");
            entity.Property(e => e.Value).HasMaxLength(255);

            entity.HasOne(d => d.IdAttributeCategoryNavigation).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.IdAttributeCategory)
                .HasConstraintName("FK__attribute__Id_at__693CA210");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK__attribute__Id_pr__6754599E");

            entity.HasOne(d => d.IdProductVariantNavigation).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.IdProductVariant)
                .HasConstraintName("FK__attribute__Id_pr__68487DD7");
        });

        modelBuilder.Entity<AttributeCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__attribut__3214EC07CDF769EF");

            entity.ToTable("attribute_category", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cart__3214EC07251E5CCA");

            entity.ToTable("cart", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IdUser).HasColumnName("Id_user");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__cart__Id_user__628FA481");
        });

        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("cart_detail", "dbo");

            entity.Property(e => e.IdCart).HasColumnName("Id_cart");
            entity.Property(e => e.IdProduct).HasColumnName("Id_product");

            entity.HasOne(d => d.IdCartNavigation).WithMany()
                .HasForeignKey(d => d.IdCart)
                .HasConstraintName("FK__cart_deta__Id_ca__6383C8BA");

            entity.HasOne(d => d.IdProductNavigation).WithMany()
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK__cart_deta__Id_pr__6477ECF3");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__discount__3214EC07FE6D31A0");

            entity.ToTable("discount", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.IsCashback).HasColumnName("Is_cashback");
            entity.Property(e => e.IsConstant).HasColumnName("Is_constant");
            entity.Property(e => e.IsPercentual).HasColumnName("Is_percentual");
            entity.Property(e => e.IsPrimeOnly).HasColumnName("Is_prime_only");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");
        });

        modelBuilder.Entity<FinancePack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__finance___3214EC071F355AD9");

            entity.ToTable("finance_pack", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IntervalType).HasColumnName("Interval_type");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.IntervalTypeNavigation).WithMany(p => p.FinancePacks)
                .HasForeignKey(d => d.IntervalType)
                .HasConstraintName("FK__finance_p__Inter__6EF57B66");
        });

        modelBuilder.Entity<FinancePackInterval>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__finance___3214EC07727C84B4");

            entity.ToTable("finance_pack_interval", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IdFinancePack).HasColumnName("Id_finance_pack");
            entity.Property(e => e.IntervalCount).HasColumnName("Interval_count");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.IdFinancePackNavigation).WithMany(p => p.FinancePackIntervals)
                .HasForeignKey(d => d.IdFinancePack)
                .HasConstraintName("FK__finance_p__Id_fi__6FE99F9F");
        });

        modelBuilder.Entity<FinancedOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__financed__3214EC0742B6D9DC");

            entity.ToTable("financed_order", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IdFinancePack).HasColumnName("Id_finance_pack");
            entity.Property(e => e.IdOrder).HasColumnName("Id_order");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.IsPayed).HasColumnName("Is_payed");
            entity.Property(e => e.TotalInterest).HasColumnName("Total_Interest");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.IdFinancePackNavigation).WithMany(p => p.FinancedOrders)
                .HasForeignKey(d => d.IdFinancePack)
                .HasConstraintName("FK__financed___Id_fi__71D1E811");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.FinancedOrders)
                .HasForeignKey(d => d.IdOrder)
                .HasConstraintName("FK__financed___Id_or__70DDC3D8");
        });

        modelBuilder.Entity<FinancedOrderPay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__financed__3214EC07C39B570C");

            entity.ToTable("financed_order_pay", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IdOrder).HasColumnName("Id_order");
            entity.Property(e => e.IdPaymentNumber).HasColumnName("Id_payment_number");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.LimitPayDate)
                .HasColumnType("datetime")
                .HasColumnName("Limit_pay_date");
            entity.Property(e => e.PayDate)
                .HasColumnType("datetime")
                .HasColumnName("Pay_date");
            entity.Property(e => e.PaymentAmount).HasColumnName("Payment_amount");
            entity.Property(e => e.TotalPayed).HasColumnName("Total_payed");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.FinancedOrderPays)
                .HasForeignKey(d => d.IdOrder)
                .HasConstraintName("FK__financed___Id_or__72C60C4A");
        });

        modelBuilder.Entity<IntervalType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__interval__3214EC079E181475");

            entity.ToTable("interval_type", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.TimeInDays).HasColumnName("Time_in_days");
            entity.Property(e => e.Type).HasMaxLength(255);
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order__3214EC074DEBFA63");

            entity.ToTable("order", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IdUser).HasColumnName("Id_user");
            entity.Property(e => e.IdUserAddress).HasColumnName("Id_user_address");
            entity.Property(e => e.IdUserPaymentCard).HasColumnName("Id_user_payment_card");
            entity.Property(e => e.IdUserPaymentPaypal).HasColumnName("Id_user_payment_paypal");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.IsFinanced).HasColumnName("Is_financed");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(255)
                .HasColumnName("Payment_type");
            entity.Property(e => e.ReferralUser).HasColumnName("Referral_user");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.OrderIdUserNavigations)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__order__Id_user__6A30C649");

            entity.HasOne(d => d.IdUserAddressNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUserAddress)
                .HasConstraintName("FK__order__Id_user_a__6B24EA82");

            entity.HasOne(d => d.IdUserPaymentCardNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUserPaymentCard)
                .HasConstraintName("FK__order__Id_user_p__6C190EBB");

            entity.HasOne(d => d.IdUserPaymentPaypalNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUserPaymentPaypal)
                .HasConstraintName("FK__order__Id_user_p__6D0D32F4");

            entity.HasOne(d => d.ReferralUserNavigation).WithMany(p => p.OrderReferralUserNavigations)
                .HasForeignKey(d => d.ReferralUser)
                .HasConstraintName("FK__order__Referral___6E01572D");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("order_detail", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IdDiscount).HasColumnName("Id_discount");
            entity.Property(e => e.IdOrder).HasColumnName("Id_order");
            entity.Property(e => e.IdProduct).HasColumnName("Id_product");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.IdDiscountNavigation).WithMany()
                .HasForeignKey(d => d.IdDiscount)
                .HasConstraintName("FK__order_det__Id_di__75A278F5");

            entity.HasOne(d => d.IdOrderNavigation).WithMany()
                .HasForeignKey(d => d.IdOrder)
                .HasConstraintName("FK__order_det__Id_or__73BA3083");

            entity.HasOne(d => d.IdProductNavigation).WithMany()
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK__order_det__Id_pr__74AE54BC");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product__3214EC076B276C47");

            entity.ToTable("product", "dbo");

            entity.Property(e => e.Brand).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.Family).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.ProductCategory).HasColumnName("Product_category");
            entity.Property(e => e.ProductType)
                .HasMaxLength(255)
                .HasColumnName("Product_type");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.ProductCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategory)
                .HasConstraintName("FK__product__Product__656C112C");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3214EC071A45DB2C");

            entity.ToTable("product_category", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3214EC076C19EE49");

            entity.ToTable("product_variant", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IdProduct).HasColumnName("Id_product");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.Sku)
                .HasMaxLength(255)
                .HasColumnName("SKU");
            entity.Property(e => e.SubFamily)
                .HasMaxLength(255)
                .HasColumnName("Sub_family");
            entity.Property(e => e.SubName)
                .HasMaxLength(255)
                .HasColumnName("Sub_name");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK__product_v__Id_pr__66603565");
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__shipment__3214EC0736C2DF0B");

            entity.ToTable("shipment", "dbo");

            entity.Property(e => e.Arrival).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.EstimatedArrival)
                .HasColumnType("datetime")
                .HasColumnName("Estimated_arrival");
            entity.Property(e => e.GuideNumber)
                .HasMaxLength(255)
                .HasColumnName("Guide_number");
            entity.Property(e => e.IdOrder).HasColumnName("Id_order");
            entity.Property(e => e.IdUser).HasColumnName("Id_user");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.ShipmentCompany)
                .HasMaxLength(255)
                .HasColumnName("Shipment_company");
            entity.Property(e => e.ShipmentDate)
                .HasColumnType("datetime")
                .HasColumnName("Shipment_date");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.Shipments)
                .HasForeignKey(d => d.IdOrder)
                .HasConstraintName("FK__shipment__Id_ord__76969D2E");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Shipments)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__shipment__Id_use__778AC167");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3214EC0769193E66");

            entity.ToTable("user", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.LastUsedPayment).HasColumnName("Last_used_payment");
            entity.Property(e => e.LastUsedPaymentType)
                .HasMaxLength(255)
                .HasColumnName("Last_used_payment_type");
            entity.Property(e => e.Lastname).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.ReferralCode)
                .HasMaxLength(255)
                .HasColumnName("Referral_code");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");
            entity.Property(e => e.User1)
                .HasMaxLength(255)
                .HasColumnName("User");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_add__3214EC0709B7624A");

            entity.ToTable("user_address", "dbo");

            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .HasColumnName("Address_1");
            entity.Property(e => e.Address2)
                .HasMaxLength(255)
                .HasColumnName("Address_2");
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IdUser).HasColumnName("Id_user");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(255)
                .HasColumnName("Postal_code");
            entity.Property(e => e.State).HasMaxLength(255);
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__user_addr__Id_us__5FB337D6");
        });

        modelBuilder.Entity<UserCardPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_car__3214EC0790C6E840");

            entity.ToTable("user_card_payment", "dbo");

            entity.Property(e => e.CardExpirationMonth).HasColumnName("Card_expiration_month");
            entity.Property(e => e.CardExpirationYear).HasColumnName("Card_expiration_year");
            entity.Property(e => e.CardName)
                .HasMaxLength(255)
                .HasColumnName("Card_name");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(255)
                .HasColumnName("Card_number");
            entity.Property(e => e.CardType)
                .HasMaxLength(255)
                .HasColumnName("Card_type");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IdUserPayment).HasColumnName("Id_user_payment");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.LastUsed).HasColumnName("Last_used");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.IdUserPaymentNavigation).WithMany(p => p.UserCardPayments)
                .HasForeignKey(d => d.IdUserPayment)
                .HasConstraintName("FK__user_card__Id_us__60A75C0F");
        });

        modelBuilder.Entity<UserPaypalPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_pay__3214EC0794A36723");

            entity.ToTable("user_paypal_payment", "dbo");

            entity.Property(e => e.CardExpirationMonth).HasColumnName("Card_expiration_month");
            entity.Property(e => e.CardExpirationYear).HasColumnName("Card_expiration_year");
            entity.Property(e => e.CardName)
                .HasMaxLength(255)
                .HasColumnName("Card_name");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(255)
                .HasColumnName("Card_number");
            entity.Property(e => e.CardType)
                .HasMaxLength(255)
                .HasColumnName("Card_type");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.LastUsed).HasColumnName("Last_used");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserPaypalPayments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_payp__User___619B8048");
        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_sub__3214EC0769E9EF74");

            entity.ToTable("user_subscription", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("datetime")
                .HasColumnName("Expiration_date");
            entity.Property(e => e.IdUser).HasColumnName("Id_user");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__user_subs__Id_us__5EBF139D");
        });

        modelBuilder.Entity<UserWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_wal__3214EC077176ED95");

            entity.ToTable("user_wallet", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.Currency).HasMaxLength(255);
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("datetime")
                .HasColumnName("Expiration_date");
            entity.Property(e => e.IdUser).HasColumnName("Id_user");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserWallets)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__user_wall__Id_us__5DCAEF64");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
