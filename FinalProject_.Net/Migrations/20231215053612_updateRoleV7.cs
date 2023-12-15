using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject_.Net.Migrations
{
    public partial class updateRoleV7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        

            // Step 3: Alter the column to be non-nullable
            migrationBuilder.AlterColumn<int>(
                name: "RolesId",
                table: "Salers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);


            // Step 4: Add the foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_Salers_Roles_RolesId",
                table: "Salers",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            // Step 1: Add a default role (if necessary)

            // Step 2: Update existing rows in the Salers table
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
