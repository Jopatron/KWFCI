using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KWFCI.Migrations
{
    public partial class NullableDatesOnTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDue",
                table: "KWTasks",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "KWTasks",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AlertDate",
                table: "KWTasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDue",
                table: "KWTasks",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "KWTasks",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AlertDate",
                table: "KWTasks",
                nullable: false);
        }
    }
}
