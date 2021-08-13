using Microsoft.EntityFrameworkCore.Migrations;

namespace TheGreatGrape.Data.Migrations
{
    public partial class RemoveVotesFromWinery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Wineries_WineryId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_WineryId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "WineryId",
                table: "Votes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WineryId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_WineryId",
                table: "Votes",
                column: "WineryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Wineries_WineryId",
                table: "Votes",
                column: "WineryId",
                principalTable: "Wineries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
