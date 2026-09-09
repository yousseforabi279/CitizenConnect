using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlobName",
                table: "Deputies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Deputies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSizeBytes",
                table: "Deputies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "MediaFileName",
                table: "Deputies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MediaType",
                table: "Deputies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "Deputies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedAt",
                table: "Deputies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BlobName",
                table: "CitizinRequierments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "CitizinRequierments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSizeBytes",
                table: "CitizinRequierments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "MediaFileName",
                table: "CitizinRequierments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MediaType",
                table: "CitizinRequierments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "CitizinRequierments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedAt",
                table: "CitizinRequierments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlobName",
                table: "Deputies");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Deputies");

            migrationBuilder.DropColumn(
                name: "FileSizeBytes",
                table: "Deputies");

            migrationBuilder.DropColumn(
                name: "MediaFileName",
                table: "Deputies");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "Deputies");

            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "Deputies");

            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "Deputies");

            migrationBuilder.DropColumn(
                name: "BlobName",
                table: "CitizinRequierments");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "CitizinRequierments");

            migrationBuilder.DropColumn(
                name: "FileSizeBytes",
                table: "CitizinRequierments");

            migrationBuilder.DropColumn(
                name: "MediaFileName",
                table: "CitizinRequierments");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "CitizinRequierments");

            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "CitizinRequierments");

            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "CitizinRequierments");
        }
    }
}
