using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bokningsappen.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bokningar",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Antal_personer = table.Column<int>(type: "int", nullable: true),
                    Rum = table.Column<int>(type: "int", nullable: false),
                    Veckodag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Veckonummer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bokningar", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bokningar");
        }
    }
}
