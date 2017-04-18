using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KWFCI.Migrations
{
    public partial class AddedStatusToInteraction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Interactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Interactions");
        }
    }
}
