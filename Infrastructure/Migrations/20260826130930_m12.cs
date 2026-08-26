using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class m12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "About",
                table: "Deputies",
                newName: "OfficeLocation");

            migrationBuilder.AddColumn<string>(
                name: "AboutPart1",
                table: "Deputies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutPart2",
                table: "Deputies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Deputies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutPart1",
                table: "Deputies");

            migrationBuilder.DropColumn(
                name: "AboutPart2",
                table: "Deputies");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Deputies");

            migrationBuilder.RenameColumn(
                name: "OfficeLocation",
                table: "Deputies",
                newName: "About");
        }
    }
}
