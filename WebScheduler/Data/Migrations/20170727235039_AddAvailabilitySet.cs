using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebScheduler.Data.Migrations
{
    public partial class AddAvailabilitySet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailabilitySetID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AvailabilitySet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilitySet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Availability",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AvailabilitySetID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availability", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Availability_AvailabilitySet_AvailabilitySetID",
                        column: x => x.AvailabilitySetID,
                        principalTable: "AvailabilitySet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvailabilitySetID",
                table: "AspNetUsers",
                column: "AvailabilitySetID");

            migrationBuilder.CreateIndex(
                name: "IX_Availability_AvailabilitySetID",
                table: "Availability",
                column: "AvailabilitySetID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AvailabilitySet_AvailabilitySetID",
                table: "AspNetUsers",
                column: "AvailabilitySetID",
                principalTable: "AvailabilitySet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AvailabilitySet_AvailabilitySetID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Availability");

            migrationBuilder.DropTable(
                name: "AvailabilitySet");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AvailabilitySetID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvailabilitySetID",
                table: "AspNetUsers");
        }
    }
}
