using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject_.Net.Migrations
{
    public partial class updatecol1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.AddColumn<byte[]>(
		        name: "Avatar",
		        table: "Salers",
		        type: "varbinary(max)",
		        nullable: true);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
