using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bokningsappen.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminKonton",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lösen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminKonton", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TV = table.Column<int>(type: "int", nullable: false),
                    Bord = table.Column<int>(type: "int", nullable: false),
                    Stolar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

        

            migrationBuilder.CreateTable(
                name: "Bokningar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RumId = table.Column<int>(type: "int", nullable: false),
                    Veckodag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Veckonummer = table.Column<int>(type: "int", nullable: false),
                    Tillgänglig = table.Column<bool>(type: "bit", nullable: false),
                    SällskapId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bokningar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bokningar_Rooms_RumId",
                        column: x => x.RumId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bokningar_Sällskaper_SällskapId",
                        column: x => x.SällskapId,
                        principalTable: "Sällskaper",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bokningar_RumId",
                table: "Bokningar",
                column: "RumId");

            migrationBuilder.CreateIndex(
                name: "IX_Bokningar_SällskapId",
                table: "Bokningar",
                column: "SällskapId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminKonton");

            migrationBuilder.DropTable(
                name: "Bokningar");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Sällskaper");
        }
    }
}
