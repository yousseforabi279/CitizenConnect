using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class m7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitizinRequiermentEmployee_ComplaintAssignments_CitizinRequiermentId",
                table: "CitizinRequiermentEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizinRequiermentEmployee_Employees_EmployeeId",
                table: "CitizinRequiermentEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintAssignments_Citizen_CitizenNationalId",
                table: "ComplaintAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintAssignments_Departments_DepartmentId",
                table: "ComplaintAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_complaintCategories_ComplaintAssignments_CitizinRequiermentId",
                table: "complaintCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_complaintCategories_Employees_EmployeeId",
                table: "complaintCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeOrganizations_Organization_OrganizationId",
                table: "EmployeeOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organization",
                table: "Organization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_complaintCategories",
                table: "complaintCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComplaintAssignments",
                table: "ComplaintAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CitizinRequiermentEmployee",
                table: "CitizinRequiermentEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Citizen",
                table: "Citizen");

            migrationBuilder.RenameTable(
                name: "Organization",
                newName: "Organizations");

            migrationBuilder.RenameTable(
                name: "complaintCategories",
                newName: "CitizinRequiermentContents");

            migrationBuilder.RenameTable(
                name: "ComplaintAssignments",
                newName: "CitizinRequierments");

            migrationBuilder.RenameTable(
                name: "CitizinRequiermentEmployee",
                newName: "CitizinRequiermentEmployees");

            migrationBuilder.RenameTable(
                name: "Citizen",
                newName: "Citizens");

            migrationBuilder.RenameIndex(
                name: "IX_complaintCategories_EmployeeId",
                table: "CitizinRequiermentContents",
                newName: "IX_CitizinRequiermentContents_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_complaintCategories_CitizinRequiermentId",
                table: "CitizinRequiermentContents",
                newName: "IX_CitizinRequiermentContents_CitizinRequiermentId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintAssignments_DepartmentId",
                table: "CitizinRequierments",
                newName: "IX_CitizinRequierments_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintAssignments_CitizenNationalId",
                table: "CitizinRequierments",
                newName: "IX_CitizinRequierments_CitizenNationalId");

            migrationBuilder.RenameIndex(
                name: "IX_CitizinRequiermentEmployee_EmployeeId",
                table: "CitizinRequiermentEmployees",
                newName: "IX_CitizinRequiermentEmployees_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_CitizinRequiermentEmployee_CitizinRequiermentId",
                table: "CitizinRequiermentEmployees",
                newName: "IX_CitizinRequiermentEmployees_CitizinRequiermentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizinRequiermentContents",
                table: "CitizinRequiermentContents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizinRequierments",
                table: "CitizinRequierments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizinRequiermentEmployees",
                table: "CitizinRequiermentEmployees",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Citizens",
                table: "Citizens",
                column: "NationalId");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizinRequiermentContents_CitizinRequierments_CitizinRequiermentId",
                table: "CitizinRequiermentContents",
                column: "CitizinRequiermentId",
                principalTable: "CitizinRequierments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CitizinRequiermentContents_Employees_EmployeeId",
                table: "CitizinRequiermentContents",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CitizinRequiermentEmployees_CitizinRequierments_CitizinRequiermentId",
                table: "CitizinRequiermentEmployees",
                column: "CitizinRequiermentId",
                principalTable: "CitizinRequierments",
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
                name: "FK_CitizinRequierments_Citizens_CitizenNationalId",
                table: "CitizinRequierments",
                column: "CitizenNationalId",
                principalTable: "Citizens",
                principalColumn: "NationalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CitizinRequierments_Departments_DepartmentId",
                table: "CitizinRequierments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOrganizations_Organizations_OrganizationId",
                table: "EmployeeOrganizations",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitizinRequiermentContents_CitizinRequierments_CitizinRequiermentId",
                table: "CitizinRequiermentContents");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizinRequiermentContents_Employees_EmployeeId",
                table: "CitizinRequiermentContents");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizinRequiermentEmployees_CitizinRequierments_CitizinRequiermentId",
                table: "CitizinRequiermentEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizinRequiermentEmployees_Employees_EmployeeId",
                table: "CitizinRequiermentEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizinRequierments_Citizens_CitizenNationalId",
                table: "CitizinRequierments");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizinRequierments_Departments_DepartmentId",
                table: "CitizinRequierments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeOrganizations_Organizations_OrganizationId",
                table: "EmployeeOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CitizinRequierments",
                table: "CitizinRequierments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CitizinRequiermentEmployees",
                table: "CitizinRequiermentEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CitizinRequiermentContents",
                table: "CitizinRequiermentContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Citizens",
                table: "Citizens");

            migrationBuilder.RenameTable(
                name: "Organizations",
                newName: "Organization");

            migrationBuilder.RenameTable(
                name: "CitizinRequierments",
                newName: "ComplaintAssignments");

            migrationBuilder.RenameTable(
                name: "CitizinRequiermentEmployees",
                newName: "CitizinRequiermentEmployee");

            migrationBuilder.RenameTable(
                name: "CitizinRequiermentContents",
                newName: "complaintCategories");

            migrationBuilder.RenameTable(
                name: "Citizens",
                newName: "Citizen");

            migrationBuilder.RenameIndex(
                name: "IX_CitizinRequierments_DepartmentId",
                table: "ComplaintAssignments",
                newName: "IX_ComplaintAssignments_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_CitizinRequierments_CitizenNationalId",
                table: "ComplaintAssignments",
                newName: "IX_ComplaintAssignments_CitizenNationalId");

            migrationBuilder.RenameIndex(
                name: "IX_CitizinRequiermentEmployees_EmployeeId",
                table: "CitizinRequiermentEmployee",
                newName: "IX_CitizinRequiermentEmployee_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_CitizinRequiermentEmployees_CitizinRequiermentId",
                table: "CitizinRequiermentEmployee",
                newName: "IX_CitizinRequiermentEmployee_CitizinRequiermentId");

            migrationBuilder.RenameIndex(
                name: "IX_CitizinRequiermentContents_EmployeeId",
                table: "complaintCategories",
                newName: "IX_complaintCategories_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_CitizinRequiermentContents_CitizinRequiermentId",
                table: "complaintCategories",
                newName: "IX_complaintCategories_CitizinRequiermentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organization",
                table: "Organization",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComplaintAssignments",
                table: "ComplaintAssignments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizinRequiermentEmployee",
                table: "CitizinRequiermentEmployee",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_complaintCategories",
                table: "complaintCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Citizen",
                table: "Citizen",
                column: "NationalId");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizinRequiermentEmployee_ComplaintAssignments_CitizinRequiermentId",
                table: "CitizinRequiermentEmployee",
                column: "CitizinRequiermentId",
                principalTable: "ComplaintAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CitizinRequiermentEmployee_Employees_EmployeeId",
                table: "CitizinRequiermentEmployee",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_complaintCategories_ComplaintAssignments_CitizinRequiermentId",
                table: "complaintCategories",
                column: "CitizinRequiermentId",
                principalTable: "ComplaintAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_complaintCategories_Employees_EmployeeId",
                table: "complaintCategories",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOrganizations_Organization_OrganizationId",
                table: "EmployeeOrganizations",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
