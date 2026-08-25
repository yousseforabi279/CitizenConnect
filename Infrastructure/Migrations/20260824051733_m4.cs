using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class m4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintAssignments_Complaintes_CitizenNationalId",
                table: "ComplaintAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintAssignments_Departments_DepartmentId",
                table: "ComplaintAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintAssignments_Employees_EmployeeId",
                table: "ComplaintAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintAssignments_EmployeeId",
                table: "ComplaintAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Complaintes",
                table: "Complaintes");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "ComplaintAssignments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ComplaintAssignments");

            migrationBuilder.RenameTable(
                name: "Complaintes",
                newName: "Citizen");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "ComplaintAssignments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Citizen",
                table: "Citizen",
                column: "NationalId");

            migrationBuilder.CreateTable(
                name: "CitizinRequiermentEmployee",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizinRequiermentId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizinRequiermentEmployee", x => x.id);
                    table.ForeignKey(
                        name: "FK_CitizinRequiermentEmployee_ComplaintAssignments_CitizinRequiermentId",
                        column: x => x.CitizinRequiermentId,
                        principalTable: "ComplaintAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CitizinRequiermentEmployee_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeOrganizations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeOrganizations", x => x.id);
                    table.ForeignKey(
                        name: "FK_EmployeeOrganizations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeOrganizations_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CitizinRequiermentEmployee_CitizinRequiermentId",
                table: "CitizinRequiermentEmployee",
                column: "CitizinRequiermentId");

            migrationBuilder.CreateIndex(
                name: "IX_CitizinRequiermentEmployee_EmployeeId",
                table: "CitizinRequiermentEmployee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOrganizations_EmployeeId",
                table: "EmployeeOrganizations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOrganizations_OrganizationId",
                table: "EmployeeOrganizations",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintAssignments_Citizen_CitizenNationalId",
                table: "ComplaintAssignments",
                column: "CitizenNationalId",
                principalTable: "Citizen",
                principalColumn: "NationalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintAssignments_Departments_DepartmentId",
                table: "ComplaintAssignments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintAssignments_Citizen_CitizenNationalId",
                table: "ComplaintAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintAssignments_Departments_DepartmentId",
                table: "ComplaintAssignments");

            migrationBuilder.DropTable(
                name: "CitizinRequiermentEmployee");

            migrationBuilder.DropTable(
                name: "EmployeeOrganizations");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Citizen",
                table: "Citizen");

            migrationBuilder.RenameTable(
                name: "Citizen",
                newName: "Complaintes");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "ComplaintAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "ComplaintAssignments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ComplaintAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Complaintes",
                table: "Complaintes",
                column: "NationalId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintAssignments_EmployeeId",
                table: "ComplaintAssignments",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintAssignments_Complaintes_CitizenNationalId",
                table: "ComplaintAssignments",
                column: "CitizenNationalId",
                principalTable: "Complaintes",
                principalColumn: "NationalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintAssignments_Departments_DepartmentId",
                table: "ComplaintAssignments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintAssignments_Employees_EmployeeId",
                table: "ComplaintAssignments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
