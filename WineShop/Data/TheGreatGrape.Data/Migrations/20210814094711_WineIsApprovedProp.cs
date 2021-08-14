using Microsoft.EntityFrameworkCore.Migrations;

namespace TheGreatGrape.Data.Migrations
{
    public partial class WineIsApprovedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Wines",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Wines");
        }
    }
}
