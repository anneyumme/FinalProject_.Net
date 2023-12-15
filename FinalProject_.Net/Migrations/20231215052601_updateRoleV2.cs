using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject_.Net.Migrations
{
    public partial class updateRoleV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salers_Roles_RoleId",
                table: "Salers");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Salers",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_Salers_RoleId",
                table: "Salers",
                newName: "IX_Salers_RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salers_Roles_RolesId",
                table: "Salers",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salers_Roles_RolesId",
                table: "Salers");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "Salers",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Salers_RolesId",
                table: "Salers",
                newName: "IX_Salers_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salers_Roles_RoleId",
                table: "Salers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
