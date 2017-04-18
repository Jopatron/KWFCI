using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KWFCI.Migrations
{
    public partial class interactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interaction_Brokers_BrokerID",
                table: "Interaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interaction",
                table: "Interaction");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Interaction",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NextStep",
                table: "Interaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Interaction",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffProfileID",
                table: "Interaction",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interactions",
                table: "Interaction",
                column: "InteractionID");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_StaffProfileID",
                table: "Interaction",
                column: "StaffProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_Brokers_BrokerID",
                table: "Interaction",
                column: "BrokerID",
                principalTable: "Brokers",
                principalColumn: "BrokerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_StaffProfiles_StaffProfileID",
                table: "Interaction",
                column: "StaffProfileID",
                principalTable: "StaffProfiles",
                principalColumn: "StaffProfileID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Interaction_BrokerID",
                table: "Interaction",
                newName: "IX_Interactions_BrokerID");

            migrationBuilder.RenameTable(
                name: "Interaction",
                newName: "Interactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_Brokers_BrokerID",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_StaffProfiles_StaffProfileID",
                table: "Interactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interactions",
                table: "Interactions");

            migrationBuilder.DropIndex(
                name: "IX_Interactions_StaffProfileID",
                table: "Interactions");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Interactions");

            migrationBuilder.DropColumn(
                name: "NextStep",
                table: "Interactions");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Interactions");

            migrationBuilder.DropColumn(
                name: "StaffProfileID",
                table: "Interactions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interaction",
                table: "Interactions",
                column: "InteractionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Interaction_Brokers_BrokerID",
                table: "Interactions",
                column: "BrokerID",
                principalTable: "Brokers",
                principalColumn: "BrokerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Interactions_BrokerID",
                table: "Interactions",
                newName: "IX_Interaction_BrokerID");

            migrationBuilder.RenameTable(
                name: "Interactions",
                newName: "Interaction");
        }
    }
}
