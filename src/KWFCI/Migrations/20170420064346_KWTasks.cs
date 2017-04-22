using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KWFCI.Migrations
{
    public partial class KWTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KWTask_Brokers_BrokerID",
                table: "KWTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KWTask",
                table: "KWTask");

            migrationBuilder.AddColumn<int>(
                name: "AlertID",
                table: "KWTask",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffProfileID",
                table: "KWTask",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KWTasks",
                table: "KWTask",
                column: "KWTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_KWTasks_AlertID",
                table: "KWTask",
                column: "AlertID");

            migrationBuilder.CreateIndex(
                name: "IX_KWTasks_StaffProfileID",
                table: "KWTask",
                column: "StaffProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_KWTasks_Alerts_AlertID",
                table: "KWTask",
                column: "AlertID",
                principalTable: "Alerts",
                principalColumn: "AlertID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KWTasks_Brokers_BrokerID",
                table: "KWTask",
                column: "BrokerID",
                principalTable: "Brokers",
                principalColumn: "BrokerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KWTasks_StaffProfiles_StaffProfileID",
                table: "KWTask",
                column: "StaffProfileID",
                principalTable: "StaffProfiles",
                principalColumn: "StaffProfileID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_KWTask_BrokerID",
                table: "KWTask",
                newName: "IX_KWTasks_BrokerID");

            migrationBuilder.RenameTable(
                name: "KWTask",
                newName: "KWTasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KWTasks_Alerts_AlertID",
                table: "KWTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_KWTasks_Brokers_BrokerID",
                table: "KWTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_KWTasks_StaffProfiles_StaffProfileID",
                table: "KWTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KWTasks",
                table: "KWTasks");

            migrationBuilder.DropIndex(
                name: "IX_KWTasks_AlertID",
                table: "KWTasks");

            migrationBuilder.DropIndex(
                name: "IX_KWTasks_StaffProfileID",
                table: "KWTasks");

            migrationBuilder.DropColumn(
                name: "AlertID",
                table: "KWTasks");

            migrationBuilder.DropColumn(
                name: "StaffProfileID",
                table: "KWTasks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KWTask",
                table: "KWTasks",
                column: "KWTaskID");

            migrationBuilder.AddForeignKey(
                name: "FK_KWTask_Brokers_BrokerID",
                table: "KWTasks",
                column: "BrokerID",
                principalTable: "Brokers",
                principalColumn: "BrokerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_KWTasks_BrokerID",
                table: "KWTasks",
                newName: "IX_KWTask_BrokerID");

            migrationBuilder.RenameTable(
                name: "KWTasks",
                newName: "KWTask");
        }
    }
}
