using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReserveDate",
                table: "deliveryOrderRepo");

            migrationBuilder.AddColumn<string>(
                name: "ReserveDate",
                table: "reserveOrderRepo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReserveDate",
                table: "reserveOrderRepo");

            migrationBuilder.AddColumn<string>(
                name: "ReserveDate",
                table: "deliveryOrderRepo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
