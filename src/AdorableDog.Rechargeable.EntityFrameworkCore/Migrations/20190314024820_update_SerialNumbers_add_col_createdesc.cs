using Microsoft.EntityFrameworkCore.Migrations;

namespace AdorableDog.Rechargeable.Migrations
{
    public partial class update_SerialNumbers_add_col_createdesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateDesc",
                table: "SerialNumbers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDesc",
                table: "SerialNumbers");
        }
    }
}
