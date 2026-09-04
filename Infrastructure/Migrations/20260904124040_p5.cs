using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class p5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Deputies");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AreasOfWorkAndActivities");

            migrationBuilder.RenameColumn(
                name: "Image_Video",
                table: "MotionsForInformation",
                newName: "MediaFileName");

            migrationBuilder.RenameColumn(
                name: "Video_image",
                table: "DeputyWords",
                newName: "MediaFileName");

            migrationBuilder.RenameColumn(
                name: "Image_Video",
                table: "ActitvitiesAndVisits",
                newName: "MediaFileName");

            migrationBuilder.AddColumn<string>(
                name: "BlobName",
                table: "MotionsForInformation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "MotionsForInformation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSizeBytes",
                table: "MotionsForInformation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MediaType",
                table: "MotionsForInformation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedAt",
                table: "MotionsForInformation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlobName",
                table: "DeputyWords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "DeputyWords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSizeBytes",
                table: "DeputyWords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "MediaType",
                table: "DeputyWords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedAt",
                table: "DeputyWords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BlobName",
                table: "AreasOfWorkAndActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "AreasOfWorkAndActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSizeBytes",
                table: "AreasOfWorkAndActivities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaFileName",
                table: "AreasOfWorkAndActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MediaType",
                table: "AreasOfWorkAndActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedAt",
                table: "AreasOfWorkAndActivities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlobName",
                table: "ActitvitiesAndVisits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "ActitvitiesAndVisits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSizeBytes",
                table: "ActitvitiesAndVisits",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MediaType",
                table: "ActitvitiesAndVisits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedAt",
                table: "ActitvitiesAndVisits",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlobName",
                table: "MotionsForInformation");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "MotionsForInformation");

            migrationBuilder.DropColumn(
                name: "FileSizeBytes",
                table: "MotionsForInformation");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "MotionsForInformation");

            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "MotionsForInformation");

            migrationBuilder.DropColumn(
                name: "BlobName",
                table: "DeputyWords");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "DeputyWords");

            migrationBuilder.DropColumn(
                name: "FileSizeBytes",
                table: "DeputyWords");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "DeputyWords");

            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "DeputyWords");

            migrationBuilder.DropColumn(
                name: "BlobName",
                table: "AreasOfWorkAndActivities");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "AreasOfWorkAndActivities");

            migrationBuilder.DropColumn(
                name: "FileSizeBytes",
                table: "AreasOfWorkAndActivities");

            migrationBuilder.DropColumn(
                name: "MediaFileName",
                table: "AreasOfWorkAndActivities");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "AreasOfWorkAndActivities");

            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "AreasOfWorkAndActivities");

            migrationBuilder.DropColumn(
                name: "BlobName",
                table: "ActitvitiesAndVisits");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "ActitvitiesAndVisits");

            migrationBuilder.DropColumn(
                name: "FileSizeBytes",
                table: "ActitvitiesAndVisits");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "ActitvitiesAndVisits");

            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "ActitvitiesAndVisits");

            migrationBuilder.RenameColumn(
                name: "MediaFileName",
                table: "MotionsForInformation",
                newName: "Image_Video");

            migrationBuilder.RenameColumn(
                name: "MediaFileName",
                table: "DeputyWords",
                newName: "Video_image");

            migrationBuilder.RenameColumn(
                name: "MediaFileName",
                table: "ActitvitiesAndVisits",
                newName: "Image_Video");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Deputies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AreasOfWorkAndActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
