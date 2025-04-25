using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserCardPaymentUserIdProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__user_card__Id_us__628FA481",
                table: "user_card_payment");

            migrationBuilder.DropIndex(
                name: "IX_user_card_payment_Id_user_payment",
                table: "user_card_payment");

            migrationBuilder.AddColumn<int>(
                name: "IdUserPayment",
                table: "user_card_payment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_card_payment_IdUserPayment",
                table: "user_card_payment",
                column: "IdUserPayment");

            migrationBuilder.AddForeignKey(
                name: "FK__user_card__Id_us__628FA481",
                table: "user_card_payment",
                column: "IdUserPayment",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__user_card__Id_us__628FA481",
                table: "user_card_payment");

            migrationBuilder.DropIndex(
                name: "IX_user_card_payment_IdUserPayment",
                table: "user_card_payment");

            migrationBuilder.DropColumn(
                name: "IdUserPayment",
                table: "user_card_payment");

            migrationBuilder.CreateIndex(
                name: "IX_user_card_payment_Id_user_payment",
                table: "user_card_payment",
                column: "Id_user_payment");

            migrationBuilder.AddForeignKey(
                name: "FK__user_card__Id_us__628FA481",
                table: "user_card_payment",
                column: "Id_user_payment",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
