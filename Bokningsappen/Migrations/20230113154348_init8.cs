using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bokningsappen.Migrations
{
    public partial class init8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "Bokningar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rum = table.Column<int>(type: "int", nullable: false),
                    Veckodag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Veckonummer = table.Column<int>(type: "int", nullable: false),
                    Tillgänglig = table.Column<bool>(type: "bit", nullable: false),
                    SällskapId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bokningar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bokningar_Sällskaper_SällskapId",
                        column: x => x.SällskapId,
                        principalTable: "Sällskaper",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bokningar_SällskapId",
                table: "Bokningar",
                column: "SällskapId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bokningar");

            migrationBuilder.DropTable(
                name: "Sällskaper");
        }
    }
}
