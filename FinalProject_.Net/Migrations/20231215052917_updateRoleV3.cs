using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject_.Net.Migrations
{
    public partial class updateRoleV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salers_Roles_RolesId",
                table: "Salers");

            migrationBuilder.AlterColumn<int>(
                name: "RolesId",
                table: "Salers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Salers_Roles_RolesId",
                table: "Salers",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salers_Roles_RolesId",
                table: "Salers");

            migrationBuilder.AlterColumn<int>(
                name: "RolesId",
                table: "Salers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Salers_Roles_RolesId",
                table: "Salers",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
