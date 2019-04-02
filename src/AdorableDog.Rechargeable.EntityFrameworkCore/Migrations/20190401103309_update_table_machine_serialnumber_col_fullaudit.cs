using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdorableDog.Rechargeable.Migrations
{
    public partial class update_table_machine_serialnumber_col_fullaudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "SerialNumbers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "SerialNumbers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "SerialNumbers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "SerialNumbers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireTime",
                table: "SerialNumbers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SerialNumbers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "SerialNumbers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "SerialNumbers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Machines",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Machines",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Machines",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Machines",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Machines",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Machines",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "Machines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "SerialNumbers");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "SerialNumbers");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "SerialNumbers");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "SerialNumbers");

            migrationBuilder.DropColumn(
                name: "ExpireTime",
                table: "SerialNumbers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SerialNumbers");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "SerialNumbers");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "SerialNumbers");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "Machines");
        }
    }
}
