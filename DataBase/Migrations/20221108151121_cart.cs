using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    public partial class cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItemsRepo_itemRepo_itemId",
                table: "cartItemsRepo");

            migrationBuilder.DropIndex(
                name: "IX_cartItemsRepo_itemId",
                table: "cartItemsRepo");

            migrationBuilder.DropColumn(
                name: "count",
                table: "cartItemsRepo");

            migrationBuilder.RenameColumn(
                name: "itemId",
                table: "cartItemsRepo",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "Cartid",
                table: "cartItemsRepo",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "cartItemsRepo",
                newName: "itemId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "cartItemsRepo",
                newName: "Cartid");

            migrationBuilder.AddColumn<int>(
                name: "count",
                table: "cartItemsRepo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_cartItemsRepo_itemId",
                table: "cartItemsRepo",
                column: "itemId");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItemsRepo_itemRepo_itemId",
                table: "cartItemsRepo",
                column: "itemId",
                principalTable: "itemRepo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
