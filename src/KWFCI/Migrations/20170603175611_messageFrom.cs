using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KWFCI.Migrations
{
    public partial class messageFrom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_StaffProfiles_FromStaffProfileID",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_FromStaffProfileID",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "FromStaffProfileID",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "Messages",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NextStep",
                table: "Interactions",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "FromStaffProfileID",
                table: "Messages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromStaffProfileID",
                table: "Messages",
                column: "FromStaffProfileID");

            migrationBuilder.AlterColumn<string>(
                name: "NextStep",
                table: "Interactions",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_StaffProfiles_FromStaffProfileID",
                table: "Messages",
                column: "FromStaffProfileID",
                principalTable: "StaffProfiles",
                principalColumn: "StaffProfileID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
