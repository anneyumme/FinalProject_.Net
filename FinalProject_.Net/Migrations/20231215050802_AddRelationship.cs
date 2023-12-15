using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject_.Net.Migrations
{
    public partial class AddRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Salers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salers_RoleId",
                table: "Salers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salers_Roles_RoleId",
                table: "Salers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salers_Roles_RoleId",
                table: "Salers");

            migrationBuilder.DropIndex(
                name: "IX_Salers_RoleId",
                table: "Salers");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Salers");
        }
    }
}
