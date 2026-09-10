using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class m13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Deputies_DeputyId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivitiesAndVisits_Deputies_DeputyId",
                table: "ActivitiesAndVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_AreasOfWorkAndActivities_Deputies_DeputyId",
                table: "AreasOfWorkAndActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_DeputyWords_Deputies_DeputyId",
                table: "DeputyWords");

            migrationBuilder.DropForeignKey(
                name: "FK_MotionsForInformation_Deputies_DeputyId",
                table: "MotionsForInformation");

            migrationBuilder.DropIndex(
                name: "IX_MotionsForInformation_DeputyId",
                table: "MotionsForInformation");

            migrationBuilder.DropIndex(
                name: "IX_DeputyWords_DeputyId",
                table: "DeputyWords");

            migrationBuilder.DropIndex(
                name: "IX_AreasOfWorkAndActivities_DeputyId",
                table: "AreasOfWorkAndActivities");

            migrationBuilder.DropIndex(
                name: "IX_ActivitiesAndVisits_DeputyId",
                table: "ActivitiesAndVisits");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_DeputyId",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "DeputyId",
                table: "MotionsForInformation");

            migrationBuilder.DropColumn(
                name: "DeputyId",
                table: "DeputyWords");

            migrationBuilder.DropColumn(
                name: "DeputyId",
                table: "AreasOfWorkAndActivities");

            migrationBuilder.DropColumn(
                name: "DeputyId",
                table: "ActivitiesAndVisits");

            migrationBuilder.DropColumn(
                name: "DeputyId",
                table: "Achievements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeputyId",
                table: "MotionsForInformation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeputyId",
                table: "DeputyWords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeputyId",
                table: "AreasOfWorkAndActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeputyId",
                table: "ActivitiesAndVisits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeputyId",
                table: "Achievements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MotionsForInformation_DeputyId",
                table: "MotionsForInformation",
                column: "DeputyId");

            migrationBuilder.CreateIndex(
                name: "IX_DeputyWords_DeputyId",
                table: "DeputyWords",
                column: "DeputyId");

            migrationBuilder.CreateIndex(
                name: "IX_AreasOfWorkAndActivities_DeputyId",
                table: "AreasOfWorkAndActivities",
                column: "DeputyId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesAndVisits_DeputyId",
                table: "ActivitiesAndVisits",
                column: "DeputyId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_DeputyId",
                table: "Achievements",
                column: "DeputyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Deputies_DeputyId",
                table: "Achievements",
                column: "DeputyId",
                principalTable: "Deputies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivitiesAndVisits_Deputies_DeputyId",
                table: "ActivitiesAndVisits",
                column: "DeputyId",
                principalTable: "Deputies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AreasOfWorkAndActivities_Deputies_DeputyId",
                table: "AreasOfWorkAndActivities",
                column: "DeputyId",
                principalTable: "Deputies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeputyWords_Deputies_DeputyId",
                table: "DeputyWords",
                column: "DeputyId",
                principalTable: "Deputies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MotionsForInformation_Deputies_DeputyId",
                table: "MotionsForInformation",
                column: "DeputyId",
                principalTable: "Deputies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
