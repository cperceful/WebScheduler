using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebScheduler.Data.Migrations
{
    public partial class AvailabilitySet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailabilityId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "AvailabilitySet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilitySet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AvailabilitySet_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Availability",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    AvailabilitySetID = table.Column<int>(nullable: true),
                    Day = table.Column<int>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availability", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Availability_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Availability_AvailabilitySet_AvailabilitySetID",
                        column: x => x.AvailabilitySetID,
                        principalTable: "AvailabilitySet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availability_ApplicationUserId",
                table: "Availability",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Availability_AvailabilitySetID",
                table: "Availability",
                column: "AvailabilitySetID");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilitySet_ApplicationUserId",
                table: "AvailabilitySet",
                column: "ApplicationUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Availability");

            migrationBuilder.DropTable(
                name: "AvailabilitySet");

            migrationBuilder.AddColumn<int>(
                name: "AvailabilityId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
