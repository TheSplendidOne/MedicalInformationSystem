using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mis.Migrations
{
    public partial class Addedmeetingcell : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeetingCell",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MeetingDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingCell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingCell_DoctorInfo_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Identity",
                        principalTable: "DoctorInfo",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingCell_User_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingCell_DoctorId",
                schema: "Identity",
                table: "MeetingCell",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingCell_PatientId",
                schema: "Identity",
                table: "MeetingCell",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingCell",
                schema: "Identity");
        }
    }
}
