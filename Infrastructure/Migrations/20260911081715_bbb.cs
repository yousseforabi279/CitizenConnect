using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bbb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeImage_Employees_EmployeeId",
                table: "EmployeeImage");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeImage_Employees_EmployeeId",
                table: "EmployeeImage",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeImage_Employees_EmployeeId",
                table: "EmployeeImage");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeImage_Employees_EmployeeId",
                table: "EmployeeImage",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
