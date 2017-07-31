using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebScheduler.Data.Migrations
{
    public partial class EditAvailabilityProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Availability");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Availability",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Availability");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "Availability",
                nullable: false,
                defaultValue: 0);
        }
    }
}
