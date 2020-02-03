using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServerStudy.Core.Migrations
{
    public partial class charactercolor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ColorA",
                table: "Characters",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ColorB",
                table: "Characters",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ColorG",
                table: "Characters",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ColorR",
                table: "Characters",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorA",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ColorB",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ColorG",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ColorR",
                table: "Characters");
        }
    }
}
