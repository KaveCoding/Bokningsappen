using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bokningsappen.Migrations
{
    public partial class init9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aktivitet",
                table: "Sällskaper",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aktivitet",
                table: "Sällskaper");
        }
    }
}
