using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibrary1.Migrations
{
    /// <inheritdoc />
    public partial class AddCartItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItemEntity_Carts_CartId1",
                table: "CartItemEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItemEntity_Products_ProductId1",
                table: "CartItemEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItemEntity",
                table: "CartItemEntity");

            migrationBuilder.RenameTable(
                name: "CartItemEntity",
                newName: "CartItems");

            migrationBuilder.RenameIndex(
                name: "IX_CartItemEntity_ProductId1",
                table: "CartItems",
                newName: "IX_CartItems_ProductId1");

            migrationBuilder.RenameIndex(
                name: "IX_CartItemEntity_CartId1",
                table: "CartItems",
                newName: "IX_CartItems_CartId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId1",
                table: "CartItems",
                column: "CartId1",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId1",
                table: "CartItems",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId1",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId1",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "CartItemEntity");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_ProductId1",
                table: "CartItemEntity",
                newName: "IX_CartItemEntity_ProductId1");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_CartId1",
                table: "CartItemEntity",
                newName: "IX_CartItemEntity_CartId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItemEntity",
                table: "CartItemEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemEntity_Carts_CartId1",
                table: "CartItemEntity",
                column: "CartId1",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemEntity_Products_ProductId1",
                table: "CartItemEntity",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
