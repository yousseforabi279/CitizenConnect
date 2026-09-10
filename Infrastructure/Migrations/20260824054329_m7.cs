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
                name: "FK_CitizenRequirementEmployee_ComplaintAssignments_CitizenRequirementId",
                table: "CitizenRequirementEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizenRequirementEmployee_Employees_EmployeeId",
                table: "CitizenRequirementEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintAssignments_Citizen_CitizenNationalId",
                table: "ComplaintAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintAssignments_Departments_DepartmentId",
                table: "ComplaintAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_complaintCategories_ComplaintAssignments_CitizenRequirementId",
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
                name: "PK_CitizenRequirementEmployee",
                table: "CitizenRequirementEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Citizen",
                table: "Citizen");

            migrationBuilder.RenameTable(
                name: "Organization",
                newName: "Organizations");

            migrationBuilder.RenameTable(
                name: "complaintCategories",
                newName: "CitizenRequirementContents");

            migrationBuilder.RenameTable(
                name: "ComplaintAssignments",
                newName: "CitizenRequirements");

            migrationBuilder.RenameTable(
                name: "CitizenRequirementEmployee",
                newName: "CitizenRequirementEmployees");

            migrationBuilder.RenameTable(
                name: "Citizen",
                newName: "Citizens");

            migrationBuilder.RenameIndex(
                name: "IX_complaintCategories_EmployeeId",
                table: "CitizenRequirementContents",
                newName: "IX_CitizenRequirementContents_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_complaintCategories_CitizenRequirementId",
                table: "CitizenRequirementContents",
                newName: "IX_CitizenRequirementContents_CitizenRequirementId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintAssignments_DepartmentId",
                table: "CitizenRequirements",
                newName: "IX_CitizenRequirements_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintAssignments_CitizenNationalId",
                table: "CitizenRequirements",
                newName: "IX_CitizenRequirements_CitizenNationalId");

            migrationBuilder.RenameIndex(
                name: "IX_CitizenRequirementEmployee_EmployeeId",
                table: "CitizenRequirementEmployees",
                newName: "IX_CitizenRequirementEmployees_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_CitizenRequirementEmployee_CitizenRequirementId",
                table: "CitizenRequirementEmployees",
                newName: "IX_CitizenRequirementEmployees_CitizenRequirementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizenRequirementContents",
                table: "CitizenRequirementContents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizenRequirements",
                table: "CitizenRequirements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizenRequirementEmployees",
                table: "CitizenRequirementEmployees",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Citizens",
                table: "Citizens",
                column: "NationalId");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenRequirementContents_CitizenRequirements_CitizenRequirementId",
                table: "CitizenRequirementContents",
                column: "CitizenRequirementId",
                principalTable: "CitizenRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenRequirementContents_Employees_EmployeeId",
                table: "CitizenRequirementContents",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenRequirementEmployees_CitizenRequirements_CitizenRequirementId",
                table: "CitizenRequirementEmployees",
                column: "CitizenRequirementId",
                principalTable: "CitizenRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenRequirementEmployees_Employees_EmployeeId",
                table: "CitizenRequirementEmployees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenRequirements_Citizens_CitizenNationalId",
                table: "CitizenRequirements",
                column: "CitizenNationalId",
                principalTable: "Citizens",
                principalColumn: "NationalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenRequirements_Departments_DepartmentId",
                table: "CitizenRequirements",
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
                name: "FK_CitizenRequirementContents_CitizenRequirements_CitizenRequirementId",
                table: "CitizenRequirementContents");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizenRequirementContents_Employees_EmployeeId",
                table: "CitizenRequirementContents");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizenRequirementEmployees_CitizenRequirements_CitizenRequirementId",
                table: "CitizenRequirementEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizenRequirementEmployees_Employees_EmployeeId",
                table: "CitizenRequirementEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizenRequirements_Citizens_CitizenNationalId",
                table: "CitizenRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizenRequirements_Departments_DepartmentId",
                table: "CitizenRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeOrganizations_Organizations_OrganizationId",
                table: "EmployeeOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CitizenRequirements",
                table: "CitizenRequirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CitizenRequirementEmployees",
                table: "CitizenRequirementEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CitizenRequirementContents",
                table: "CitizenRequirementContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Citizens",
                table: "Citizens");

            migrationBuilder.RenameTable(
                name: "Organizations",
                newName: "Organization");

            migrationBuilder.RenameTable(
                name: "CitizenRequirements",
                newName: "ComplaintAssignments");

            migrationBuilder.RenameTable(
                name: "CitizenRequirementEmployees",
                newName: "CitizenRequirementEmployee");

            migrationBuilder.RenameTable(
                name: "CitizenRequirementContents",
                newName: "complaintCategories");

            migrationBuilder.RenameTable(
                name: "Citizens",
                newName: "Citizen");

            migrationBuilder.RenameIndex(
                name: "IX_CitizenRequirements_DepartmentId",
                table: "ComplaintAssignments",
                newName: "IX_ComplaintAssignments_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_CitizenRequirements_CitizenNationalId",
                table: "ComplaintAssignments",
                newName: "IX_ComplaintAssignments_CitizenNationalId");

            migrationBuilder.RenameIndex(
                name: "IX_CitizenRequirementEmployees_EmployeeId",
                table: "CitizenRequirementEmployee",
                newName: "IX_CitizenRequirementEmployee_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_CitizenRequirementEmployees_CitizenRequirementId",
                table: "CitizenRequirementEmployee",
                newName: "IX_CitizenRequirementEmployee_CitizenRequirementId");

            migrationBuilder.RenameIndex(
                name: "IX_CitizenRequirementContents_EmployeeId",
                table: "complaintCategories",
                newName: "IX_complaintCategories_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_CitizenRequirementContents_CitizenRequirementId",
                table: "complaintCategories",
                newName: "IX_complaintCategories_CitizenRequirementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organization",
                table: "Organization",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComplaintAssignments",
                table: "ComplaintAssignments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizenRequirementEmployee",
                table: "CitizenRequirementEmployee",
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
                name: "FK_CitizenRequirementEmployee_ComplaintAssignments_CitizenRequirementId",
                table: "CitizenRequirementEmployee",
                column: "CitizenRequirementId",
                principalTable: "ComplaintAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenRequirementEmployee_Employees_EmployeeId",
                table: "CitizenRequirementEmployee",
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
                name: "FK_complaintCategories_ComplaintAssignments_CitizenRequirementId",
                table: "complaintCategories",
                column: "CitizenRequirementId",
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
