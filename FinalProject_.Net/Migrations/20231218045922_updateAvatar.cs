using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject_.Net.Migrations
{
    public partial class updateAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "Salers");

            migrationBuilder.AddColumn<byte[]>(
	            name: "Avatar",
	            table: "Salers",
	            type: "varbinary(max)",
	            nullable: true);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "test",
                table: "Salers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
