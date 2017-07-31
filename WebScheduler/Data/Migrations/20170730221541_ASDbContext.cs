using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebScheduler.Data.Migrations
{
    public partial class ASDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availability_AvailabilitySet_AvailabilitySetID",
                table: "Availability");

            migrationBuilder.DropForeignKey(
                name: "FK_AvailabilitySet_AspNetUsers_ApplicationUserId",
                table: "AvailabilitySet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailabilitySet",
                table: "AvailabilitySet");

            migrationBuilder.RenameTable(
                name: "AvailabilitySet",
                newName: "AvailabilitySets");

            migrationBuilder.RenameIndex(
                name: "IX_AvailabilitySet_ApplicationUserId",
                table: "AvailabilitySets",
                newName: "IX_AvailabilitySets_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailabilitySets",
                table: "AvailabilitySets",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_AvailabilitySets_AvailabilitySetID",
                table: "Availability",
                column: "AvailabilitySetID",
                principalTable: "AvailabilitySets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AvailabilitySets_AspNetUsers_ApplicationUserId",
                table: "AvailabilitySets",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availability_AvailabilitySets_AvailabilitySetID",
                table: "Availability");

            migrationBuilder.DropForeignKey(
                name: "FK_AvailabilitySets_AspNetUsers_ApplicationUserId",
                table: "AvailabilitySets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailabilitySets",
                table: "AvailabilitySets");

            migrationBuilder.RenameTable(
                name: "AvailabilitySets",
                newName: "AvailabilitySet");

            migrationBuilder.RenameIndex(
                name: "IX_AvailabilitySets_ApplicationUserId",
                table: "AvailabilitySet",
                newName: "IX_AvailabilitySet_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailabilitySet",
                table: "AvailabilitySet",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_AvailabilitySet_AvailabilitySetID",
                table: "Availability",
                column: "AvailabilitySetID",
                principalTable: "AvailabilitySet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AvailabilitySet_AspNetUsers_ApplicationUserId",
                table: "AvailabilitySet",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
