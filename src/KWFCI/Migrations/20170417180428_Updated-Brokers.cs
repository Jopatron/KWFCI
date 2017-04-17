using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KWFCI.Migrations
{
    public partial class UpdatedBrokers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "KWTask",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Brokers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "KWTask");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Brokers");
        }
    }
}
