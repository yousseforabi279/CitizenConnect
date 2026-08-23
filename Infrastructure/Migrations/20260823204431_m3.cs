using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintAssignments_Complaintes_ComplaintId",
                table: "ComplaintAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaintes_complaintCategories_CategoryId",
                table: "Complaintes");

            migrationBuilder.DropTable(
                name: "ComplaintComments");

            migrationBuilder.DropTable(
                name: "complaintResolutions");

            migrationBuilder.DropTable(
                name: "ComplaintStatusHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Complaintes",
                table: "Complaintes");

            migrationBuilder.DropIndex(
                name: "IX_Complaintes_CategoryId",
                table: "Complaintes");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintAssignments_ComplaintId",
                table: "ComplaintAssignments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Complaintes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Complaintes");

            migrationBuilder.DropColumn(
                name: "CitizenName",
                table: "Complaintes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Complaintes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Complaintes");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Complaintes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Complaintes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Complaintes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "complaintCategories");

            migrationBuilder.DropColumn(
                name: "UnassignedAt",
                table: "ComplaintAssignments");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Complaintes",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Complaintes",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "complaintCategories",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "ComplaintAssignments",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "ComplaintId",
                table: "ComplaintAssignments",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "AssignedAt",
                table: "ComplaintAssignments",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "Complaintes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "Complaintes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "CitizinRequiermentId",
                table: "complaintCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "complaintCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "complaintCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CitizenNationalId",
                table: "ComplaintAssignments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ComplaintAssignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ComplaintAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ComplaintAssignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Complaintes",
                table: "Complaintes",
                column: "NationalId");

            migrationBuilder.CreateIndex(
                name: "IX_complaintCategories_CitizinRequiermentId",
                table: "complaintCategories",
                column: "CitizinRequiermentId");

            migrationBuilder.CreateIndex(
                name: "IX_complaintCategories_EmployeeId",
                table: "complaintCategories",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintAssignments_CitizenNationalId",
                table: "ComplaintAssignments",
                column: "CitizenNationalId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintAssignments_Complaintes_CitizenNationalId",
                table: "ComplaintAssignments",
                column: "CitizenNationalId",
                principalTable: "Complaintes",
                principalColumn: "NationalId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_complaintCategories_ComplaintAssignments_CitizinRequiermentId",
                table: "complaintCategories",
                column: "CitizinRequiermentId",
                principalTable: "ComplaintAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_complaintCategories_Employees_EmployeeId",
                table: "complaintCategories",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintAssignments_Complaintes_CitizenNationalId",
                table: "ComplaintAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_complaintCategories_ComplaintAssignments_CitizinRequiermentId",
                table: "complaintCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_complaintCategories_Employees_EmployeeId",
                table: "complaintCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Complaintes",
                table: "Complaintes");

            migrationBuilder.DropIndex(
                name: "IX_complaintCategories_CitizinRequiermentId",
                table: "complaintCategories");

            migrationBuilder.DropIndex(
                name: "IX_complaintCategories_EmployeeId",
                table: "complaintCategories");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintAssignments_CitizenNationalId",
                table: "ComplaintAssignments");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Complaintes");

            migrationBuilder.DropColumn(
                name: "CitizinRequiermentId",
                table: "complaintCategories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "complaintCategories");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "complaintCategories");

            migrationBuilder.DropColumn(
                name: "CitizenNationalId",
                table: "ComplaintAssignments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ComplaintAssignments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ComplaintAssignments");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ComplaintAssignments");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Complaintes",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Complaintes",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "complaintCategories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ComplaintAssignments",
                newName: "ComplaintId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ComplaintAssignments",
                newName: "AssignedAt");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "ComplaintAssignments",
                newName: "Note");

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "Complaintes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Complaintes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Complaintes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CitizenName",
                table: "Complaintes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Complaintes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Complaintes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Complaintes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Complaintes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Complaintes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "complaintCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UnassignedAt",
                table: "ComplaintAssignments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Complaintes",
                table: "Complaintes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ComplaintComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplaintComments_Complaintes_ComplaintId",
                        column: x => x.ComplaintId,
                        principalTable: "Complaintes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ComplaintComments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "complaintResolutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintId = table.Column<int>(type: "int", nullable: false),
                    ResolvedByEmployeeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_complaintResolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_complaintResolutions_Complaintes_ComplaintId",
                        column: x => x.ComplaintId,
                        principalTable: "Complaintes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_complaintResolutions_Employees_ResolvedByEmployeeId",
                        column: x => x.ResolvedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintStatusHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangedByEmployeeId = table.Column<int>(type: "int", nullable: false),
                    ComplaintId = table.Column<int>(type: "int", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NewStatus = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplaintStatusHistories_Complaintes_ComplaintId",
                        column: x => x.ComplaintId,
                        principalTable: "Complaintes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ComplaintStatusHistories_Employees_ChangedByEmployeeId",
                        column: x => x.ChangedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaintes_CategoryId",
                table: "Complaintes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintAssignments_ComplaintId",
                table: "ComplaintAssignments",
                column: "ComplaintId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintComments_ComplaintId",
                table: "ComplaintComments",
                column: "ComplaintId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintComments_EmployeeId",
                table: "ComplaintComments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_complaintResolutions_ComplaintId",
                table: "complaintResolutions",
                column: "ComplaintId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_complaintResolutions_ResolvedByEmployeeId",
                table: "complaintResolutions",
                column: "ResolvedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintStatusHistories_ChangedByEmployeeId",
                table: "ComplaintStatusHistories",
                column: "ChangedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintStatusHistories_ComplaintId",
                table: "ComplaintStatusHistories",
                column: "ComplaintId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintAssignments_Complaintes_ComplaintId",
                table: "ComplaintAssignments",
                column: "ComplaintId",
                principalTable: "Complaintes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaintes_complaintCategories_CategoryId",
                table: "Complaintes",
                column: "CategoryId",
                principalTable: "complaintCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
