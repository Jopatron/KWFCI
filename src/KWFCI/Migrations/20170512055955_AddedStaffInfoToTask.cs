using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KWFCI.Migrations
{
    public partial class AddedStaffInfoToTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StaffEmail",
                table: "KWTasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StaffName",
                table: "KWTasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StaffEmail",
                table: "KWTasks");

            migrationBuilder.DropColumn(
                name: "StaffName",
                table: "KWTasks");
        }
    }
}
