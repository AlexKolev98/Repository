using Microsoft.EntityFrameworkCore.Migrations;

namespace TheGreatGrape.Data.Migrations
{
    public partial class GrapeNameShouldBeUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Grapes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Grapes_Name",
                table: "Grapes",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Grapes_Name",
                table: "Grapes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Grapes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
