using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityWithIntKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Referral_code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Last_used_payment_type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Last_used_payment = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });
            /*
            migrationBuilder.CreateTable(
                name: "attribute_category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__attribut__3214EC079648513B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "discount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Is_percentual = table.Column<bool>(type: "bit", nullable: true),
                    Is_constant = table.Column<bool>(type: "bit", nullable: true),
                    Is_cashback = table.Column<bool>(type: "bit", nullable: true),
                    Is_prime_only = table.Column<bool>(type: "bit", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__discount__3214EC0702ECDCCB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "interval_type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Time_in_days = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__interval__3214EC07A6200E28", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "product_category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__product___3214EC07E5C9FEB2", x => x.Id);
                });
            */

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            /*
            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_user = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cart__3214EC07DD3EBD57", x => x.Id);
                    table.ForeignKey(
                        name: "FK__cart__Id_user__628FA481",
                        column: x => x.Id_user,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "user_address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_user = table.Column<int>(type: "int", nullable: true),
                    Address_1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address_2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Postal_code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    State = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_add__3214EC071B191551", x => x.Id);
                    table.ForeignKey(
                        name: "FK__user_addr__Id_us__5FB337D6",
                        column: x => x.Id_user,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "user_card_payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_user_payment = table.Column<int>(type: "int", nullable: true),
                    Card_type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Card_number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Card_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Card_expiration_year = table.Column<int>(type: "int", nullable: true),
                    Card_expiration_month = table.Column<int>(type: "int", nullable: true),
                    Last_used = table.Column<bool>(type: "bit", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_car__3214EC07D60886DB", x => x.Id);
                    table.ForeignKey(
                        name: "FK__user_card__Id_us__60A75C0F",
                        column: x => x.Id_user_payment,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "user_paypal_payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_id = table.Column<int>(type: "int", nullable: true),
                    Card_type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Card_number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Card_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Card_expiration_year = table.Column<int>(type: "int", nullable: true),
                    Card_expiration_month = table.Column<int>(type: "int", nullable: true),
                    Last_used = table.Column<bool>(type: "bit", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_pay__3214EC071638E603", x => x.Id);
                    table.ForeignKey(
                        name: "FK__user_payp__User___619B8048",
                        column: x => x.User_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "user_subscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_user = table.Column<int>(type: "int", nullable: true),
                    Expiration_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_sub__3214EC076CD11E24", x => x.Id);
                    table.ForeignKey(
                        name: "FK__user_subs__Id_us__5EBF139D",
                        column: x => x.Id_user,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "user_wallet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_user = table.Column<int>(type: "int", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Expiration_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_wal__3214EC07308AF464", x => x.Id);
                    table.ForeignKey(
                        name: "FK__user_wall__Id_us__5DCAEF64",
                        column: x => x.Id_user,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "finance_pack",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Interval_type = table.Column<int>(type: "int", nullable: true),
                    Interest = table.Column<double>(type: "float", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__finance___3214EC07229E3F8D", x => x.Id);
                    table.ForeignKey(
                        name: "FK__finance_p__Inter__6EF57B66",
                        column: x => x.Interval_type,
                        principalTable: "interval_type",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Product_type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Product_category = table.Column<int>(type: "int", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Family = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__product__3214EC075A315265", x => x.Id);
                    table.ForeignKey(
                        name: "FK__product__Product__656C112C",
                        column: x => x.Product_category,
                        principalTable: "product_category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_user = table.Column<int>(type: "int", nullable: true),
                    Subtotal = table.Column<double>(type: "float", nullable: true),
                    Taxes = table.Column<double>(type: "float", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: true),
                    Id_user_address = table.Column<int>(type: "int", nullable: true),
                    Payment_type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Id_user_payment_card = table.Column<int>(type: "int", nullable: true),
                    Id_user_payment_paypal = table.Column<int>(type: "int", nullable: true),
                    Is_financed = table.Column<bool>(type: "bit", nullable: true),
                    Referral_user = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order__3214EC0789601794", x => x.Id);
                    table.ForeignKey(
                        name: "FK__order__Id_user__6A30C649",
                        column: x => x.Id_user,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__order__Id_user_a__6B24EA82",
                        column: x => x.Id_user_address,
                        principalTable: "user_address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__order__Id_user_p__6C190EBB",
                        column: x => x.Id_user_payment_card,
                        principalTable: "user_card_payment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__order__Id_user_p__6D0D32F4",
                        column: x => x.Id_user_payment_paypal,
                        principalTable: "user_paypal_payment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__order__Referral___6E01572D",
                        column: x => x.Referral_user,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "finance_pack_interval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_finance_pack = table.Column<int>(type: "int", nullable: true),
                    Interval_count = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__finance___3214EC07C844A178", x => x.Id);
                    table.ForeignKey(
                        name: "FK__finance_p__Id_fi__6FE99F9F",
                        column: x => x.Id_finance_pack,
                        principalTable: "finance_pack",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "cart_detail",
                columns: table => new
                {
                    Id_cart = table.Column<int>(type: "int", nullable: true),
                    Id_product = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__cart_deta__Id_ca__6383C8BA",
                        column: x => x.Id_cart,
                        principalTable: "cart",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__cart_deta__Id_pr__6477ECF3",
                        column: x => x.Id_product,
                        principalTable: "product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "product_variant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_product = table.Column<int>(type: "int", nullable: true),
                    Sub_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Sub_family = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__product___3214EC07B3469FC4", x => x.Id);
                    table.ForeignKey(
                        name: "FK__product_v__Id_pr__66603565",
                        column: x => x.Id_product,
                        principalTable: "product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "financed_order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_order = table.Column<int>(type: "int", nullable: true),
                    Id_finance_pack = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Total_Interest = table.Column<double>(type: "float", nullable: true),
                    Is_payed = table.Column<bool>(type: "bit", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__financed__3214EC07181120B4", x => x.Id);
                    table.ForeignKey(
                        name: "FK__financed___Id_fi__71D1E811",
                        column: x => x.Id_finance_pack,
                        principalTable: "finance_pack",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__financed___Id_or__70DDC3D8",
                        column: x => x.Id_order,
                        principalTable: "order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "order_detail",
                columns: table => new
                {
                    Id_order = table.Column<int>(type: "int", nullable: true),
                    Id_product = table.Column<int>(type: "int", nullable: true),
                    Id_discount = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__order_det__Id_di__75A278F5",
                        column: x => x.Id_discount,
                        principalTable: "discount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__order_det__Id_or__73BA3083",
                        column: x => x.Id_order,
                        principalTable: "order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__order_det__Id_pr__74AE54BC",
                        column: x => x.Id_product,
                        principalTable: "product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "shipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_order = table.Column<int>(type: "int", nullable: true),
                    Id_user = table.Column<int>(type: "int", nullable: true),
                    Shipment_company = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Guide_number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Shipment_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Estimated_arrival = table.Column<DateTime>(type: "datetime", nullable: true),
                    Arrival = table.Column<DateTime>(type: "datetime", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shipment__3214EC076B35AE75", x => x.Id);
                    table.ForeignKey(
                        name: "FK__shipment__Id_ord__76969D2E",
                        column: x => x.Id_order,
                        principalTable: "order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__shipment__Id_use__778AC167",
                        column: x => x.Id_user,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "attribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_product = table.Column<int>(type: "int", nullable: true),
                    Id_product_variant = table.Column<int>(type: "int", nullable: true),
                    Id_attribute_category = table.Column<int>(type: "int", nullable: true),
                    Field = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__attribut__3214EC079B66B196", x => x.Id);
                    table.ForeignKey(
                        name: "FK__attribute__Id_at__693CA210",
                        column: x => x.Id_attribute_category,
                        principalTable: "attribute_category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__attribute__Id_pr__6754599E",
                        column: x => x.Id_product,
                        principalTable: "product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__attribute__Id_pr__68487DD7",
                        column: x => x.Id_product_variant,
                        principalTable: "product_variant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "financed_order_pay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_order = table.Column<int>(type: "int", nullable: true),
                    Id_payment_number = table.Column<int>(type: "int", nullable: true),
                    Payment_amount = table.Column<double>(type: "float", nullable: true),
                    Total_payed = table.Column<double>(type: "float", nullable: true),
                    Limit_pay_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Pay_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__financed__3214EC07A556F8F3", x => x.Id);
                    table.ForeignKey(
                        name: "FK__financed___Id_or__72C60C4A",
                        column: x => x.Id_order,
                        principalTable: "financed_order",
                        principalColumn: "Id");
                });
            */
            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
            /*
            migrationBuilder.CreateIndex(
                name: "IX_attribute_Id_attribute_category",
                table: "attribute",
                column: "Id_attribute_category");

            migrationBuilder.CreateIndex(
                name: "IX_attribute_Id_product",
                table: "attribute",
                column: "Id_product");

            migrationBuilder.CreateIndex(
                name: "IX_attribute_Id_product_variant",
                table: "attribute",
                column: "Id_product_variant");

            migrationBuilder.CreateIndex(
                name: "IX_cart_Id_user",
                table: "cart",
                column: "Id_user");

            migrationBuilder.CreateIndex(
                name: "IX_cart_detail_Id_cart",
                table: "cart_detail",
                column: "Id_cart");

            migrationBuilder.CreateIndex(
                name: "IX_cart_detail_Id_product",
                table: "cart_detail",
                column: "Id_product");

            migrationBuilder.CreateIndex(
                name: "IX_finance_pack_Interval_type",
                table: "finance_pack",
                column: "Interval_type");

            migrationBuilder.CreateIndex(
                name: "IX_finance_pack_interval_Id_finance_pack",
                table: "finance_pack_interval",
                column: "Id_finance_pack");

            migrationBuilder.CreateIndex(
                name: "IX_financed_order_Id_finance_pack",
                table: "financed_order",
                column: "Id_finance_pack");

            migrationBuilder.CreateIndex(
                name: "IX_financed_order_Id_order",
                table: "financed_order",
                column: "Id_order");

            migrationBuilder.CreateIndex(
                name: "IX_financed_order_pay_Id_order",
                table: "financed_order_pay",
                column: "Id_order");

            migrationBuilder.CreateIndex(
                name: "IX_order_Id_user",
                table: "order",
                column: "Id_user");

            migrationBuilder.CreateIndex(
                name: "IX_order_Id_user_address",
                table: "order",
                column: "Id_user_address");

            migrationBuilder.CreateIndex(
                name: "IX_order_Id_user_payment_card",
                table: "order",
                column: "Id_user_payment_card");

            migrationBuilder.CreateIndex(
                name: "IX_order_Id_user_payment_paypal",
                table: "order",
                column: "Id_user_payment_paypal");

            migrationBuilder.CreateIndex(
                name: "IX_order_Referral_user",
                table: "order",
                column: "Referral_user");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_Id_discount",
                table: "order_detail",
                column: "Id_discount");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_Id_order",
                table: "order_detail",
                column: "Id_order");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_Id_product",
                table: "order_detail",
                column: "Id_product");

            migrationBuilder.CreateIndex(
                name: "IX_product_Product_category",
                table: "product",
                column: "Product_category");

            migrationBuilder.CreateIndex(
                name: "IX_product_variant_Id_product",
                table: "product_variant",
                column: "Id_product");

            migrationBuilder.CreateIndex(
                name: "IX_shipment_Id_order",
                table: "shipment",
                column: "Id_order");

            migrationBuilder.CreateIndex(
                name: "IX_shipment_Id_user",
                table: "shipment",
                column: "Id_user");

            migrationBuilder.CreateIndex(
                name: "IX_user_address_Id_user",
                table: "user_address",
                column: "Id_user");

            migrationBuilder.CreateIndex(
                name: "IX_user_card_payment_Id_user_payment",
                table: "user_card_payment",
                column: "Id_user_payment");

            migrationBuilder.CreateIndex(
                name: "IX_user_paypal_payment_User_id",
                table: "user_paypal_payment",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_subscription_Id_user",
                table: "user_subscription",
                column: "Id_user");

            migrationBuilder.CreateIndex(
                name: "IX_user_wallet_Id_user",
                table: "user_wallet",
                column: "Id_user");
            */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "attribute");

            migrationBuilder.DropTable(
                name: "cart_detail");

            migrationBuilder.DropTable(
                name: "finance_pack_interval");

            migrationBuilder.DropTable(
                name: "financed_order_pay");

            migrationBuilder.DropTable(
                name: "order_detail");

            migrationBuilder.DropTable(
                name: "shipment");

            migrationBuilder.DropTable(
                name: "user_subscription");

            migrationBuilder.DropTable(
                name: "user_wallet");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "attribute_category");

            migrationBuilder.DropTable(
                name: "product_variant");

            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "financed_order");

            migrationBuilder.DropTable(
                name: "discount");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "finance_pack");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product_category");

            migrationBuilder.DropTable(
                name: "interval_type");

            migrationBuilder.DropTable(
                name: "user_address");

            migrationBuilder.DropTable(
                name: "user_card_payment");

            migrationBuilder.DropTable(
                name: "user_paypal_payment");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
