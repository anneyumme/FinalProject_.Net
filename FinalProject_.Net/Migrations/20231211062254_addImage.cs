using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject_.Net.Migrations
{
    public partial class addImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarrantyInfo",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "Stock");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalerId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SalerId",
                table: "Orders",
                column: "SalerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Salers_SalerId",
                table: "Orders",
                column: "SalerId",
                principalTable: "Salers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Salers_SalerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SalerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalerId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Products",
                newName: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "WarrantyInfo",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
