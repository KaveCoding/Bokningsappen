using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bokningsappen.Migrations
{
    public partial class test5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aktivitet",
                table: "Sällskaper");

            migrationBuilder.AddColumn<int>(
                name: "AktivitetId",
                table: "Sällskaper",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Aktiviteter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aktiviteter", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sällskaper_AktivitetId",
                table: "Sällskaper",
                column: "AktivitetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sällskaper_Aktiviteter_AktivitetId",
                table: "Sällskaper",
                column: "AktivitetId",
                principalTable: "Aktiviteter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sällskaper_Aktiviteter_AktivitetId",
                table: "Sällskaper");

            migrationBuilder.DropTable(
                name: "Aktiviteter");

            migrationBuilder.DropIndex(
                name: "IX_Sällskaper_AktivitetId",
                table: "Sällskaper");

            migrationBuilder.DropColumn(
                name: "AktivitetId",
                table: "Sällskaper");

            migrationBuilder.AddColumn<string>(
                name: "Aktivitet",
                table: "Sällskaper",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
