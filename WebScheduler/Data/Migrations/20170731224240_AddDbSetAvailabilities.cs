using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebScheduler.Data.Migrations
{
    public partial class AddDbSetAvailabilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availability_AspNetUsers_ApplicationUserId",
                table: "Availability");

            migrationBuilder.DropForeignKey(
                name: "FK_Availability_AvailabilitySets_AvailabilitySetID",
                table: "Availability");

            migrationBuilder.DropTable(
                name: "AvailabilitySets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Availability",
                table: "Availability");

            migrationBuilder.DropIndex(
                name: "IX_Availability_AvailabilitySetID",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "AvailabilitySetID",
                table: "Availability");

            migrationBuilder.RenameTable(
                name: "Availability",
                newName: "AvailabilitySet");

            migrationBuilder.RenameIndex(
                name: "IX_Availability_ApplicationUserId",
                table: "AvailabilitySet",
                newName: "IX_AvailabilitySet_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailabilitySet",
                table: "AvailabilitySet",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailabilitySet_AspNetUsers_ApplicationUserId",
                table: "AvailabilitySet",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailabilitySet_AspNetUsers_ApplicationUserId",
                table: "AvailabilitySet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailabilitySet",
                table: "AvailabilitySet");

            migrationBuilder.RenameTable(
                name: "AvailabilitySet",
                newName: "Availability");

            migrationBuilder.RenameIndex(
                name: "IX_AvailabilitySet_ApplicationUserId",
                table: "Availability",
                newName: "IX_Availability_ApplicationUserId");

            migrationBuilder.AddColumn<int>(
                name: "AvailabilitySetID",
                table: "Availability",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Availability",
                table: "Availability",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "AvailabilitySets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilitySets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AvailabilitySets_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availability_AvailabilitySetID",
                table: "Availability",
                column: "AvailabilitySetID");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilitySets_ApplicationUserId",
                table: "AvailabilitySets",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_AspNetUsers_ApplicationUserId",
                table: "Availability",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_AvailabilitySets_AvailabilitySetID",
                table: "Availability",
                column: "AvailabilitySetID",
                principalTable: "AvailabilitySets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
