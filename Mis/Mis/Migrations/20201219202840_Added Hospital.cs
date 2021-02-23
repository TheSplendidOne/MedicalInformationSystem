using Microsoft.EntityFrameworkCore.Migrations;

namespace Mis.Migrations
{
    public partial class AddedHospital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HospitalId",
                schema: "Identity",
                table: "DoctorInfo",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Hospital",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospital", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorInfo_HospitalId",
                schema: "Identity",
                table: "DoctorInfo",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorInfo_Hospital_HospitalId",
                schema: "Identity",
                table: "DoctorInfo",
                column: "HospitalId",
                principalSchema: "Identity",
                principalTable: "Hospital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorInfo_Hospital_HospitalId",
                schema: "Identity",
                table: "DoctorInfo");

            migrationBuilder.DropTable(
                name: "Hospital",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_DoctorInfo_HospitalId",
                schema: "Identity",
                table: "DoctorInfo");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                schema: "Identity",
                table: "DoctorInfo");
        }
    }
}
