using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabLinkBackend.Migrations
{
    /// <inheritdoc />
    public partial class phase1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Physician",
                table: "Patient");

            migrationBuilder.RenameColumn(
                name: "PrimaryPhysicianId",
                table: "Patient",
                newName: "PrimaryPhysicianUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_PrimaryPhysicianId",
                table: "Patient",
                newName: "IX_Patient_PrimaryPhysicianUserId");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryPhysicianName",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_User_PrimaryPhysicianUserId",
                table: "Patient",
                column: "PrimaryPhysicianUserId",
                principalTable: "User",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_User_PrimaryPhysicianUserId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PrimaryPhysicianName",
                table: "Patient");

            migrationBuilder.RenameColumn(
                name: "PrimaryPhysicianUserId",
                table: "Patient",
                newName: "PrimaryPhysicianId");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_PrimaryPhysicianUserId",
                table: "Patient",
                newName: "IX_Patient_PrimaryPhysicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Physician",
                table: "Patient",
                column: "PrimaryPhysicianId",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}
