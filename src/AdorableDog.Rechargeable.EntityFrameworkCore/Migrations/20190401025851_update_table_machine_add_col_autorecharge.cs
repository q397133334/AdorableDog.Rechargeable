using Microsoft.EntityFrameworkCore.Migrations;

namespace AdorableDog.Rechargeable.Migrations
{
    public partial class update_table_machine_add_col_autorecharge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AutoRecharge",
                table: "Machines",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoRecharge",
                table: "Machines");
        }
    }
}
