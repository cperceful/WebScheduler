using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebScheduler.Data.Migrations
{
    public partial class RemoveAvailabilitySet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AvailabilitySet_AvailabilitySetID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Availability_AvailabilitySet_AvailabilitySetID",
                table: "Availability");

            migrationBuilder.DropTable(
                name: "AvailabilitySet");

            migrationBuilder.DropIndex(
                name: "IX_Availability_AvailabilitySetID",
                table: "Availability");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AvailabilitySetID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvailabilitySetID",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "AvailabilitySetID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Availability",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Availability_ApplicationUserId",
                table: "Availability",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_AspNetUsers_ApplicationUserId",
                table: "Availability",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availability_AspNetUsers_ApplicationUserId",
                table: "Availability");

            migrationBuilder.DropIndex(
                name: "IX_Availability_ApplicationUserId",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Availability");

            migrationBuilder.AddColumn<int>(
                name: "AvailabilitySetID",
                table: "Availability",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Availability_AvailabilitySetID",
                table: "Availability",
                column: "AvailabilitySetID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvailabilitySetID",
                table: "AspNetUsers",
                column: "AvailabilitySetID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AvailabilitySet_AvailabilitySetID",
                table: "AspNetUsers",
                column: "AvailabilitySetID",
                principalTable: "AvailabilitySet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_AvailabilitySet_AvailabilitySetID",
                table: "Availability",
                column: "AvailabilitySetID",
                principalTable: "AvailabilitySet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
