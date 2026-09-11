using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bbbb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitizinRequiermentContents_Employees_EmployeeId",
                table: "CitizinRequiermentContents");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizinRequiermentEmployees_Employees_EmployeeId",
                table: "CitizinRequiermentEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeOrganizations_Employees_EmployeeId",
                table: "EmployeeOrganizations");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizinRequiermentContents_Employees_EmployeeId",
                table: "CitizinRequiermentContents",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizinRequiermentEmployees_Employees_EmployeeId",
                table: "CitizinRequiermentEmployees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOrganizations_Employees_EmployeeId",
                table: "EmployeeOrganizations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitizinRequiermentContents_Employees_EmployeeId",
                table: "CitizinRequiermentContents");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizinRequiermentEmployees_Employees_EmployeeId",
                table: "CitizinRequiermentEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeOrganizations_Employees_EmployeeId",
                table: "EmployeeOrganizations");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizinRequiermentContents_Employees_EmployeeId",
                table: "CitizinRequiermentContents",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CitizinRequiermentEmployees_Employees_EmployeeId",
                table: "CitizinRequiermentEmployees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOrganizations_Employees_EmployeeId",
                table: "EmployeeOrganizations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
