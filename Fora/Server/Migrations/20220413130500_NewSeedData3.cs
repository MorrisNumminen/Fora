using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fora.Server.Migrations
{
    public partial class NewSeedData3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Threads",
                columns: new[] { "Id", "InterestId", "Name", "UserId" },
                values: new object[,]
                {
                    { 4, null, "Why is my game lagging???", null },
                    { 5, null, "How to git gud", null },
                    { 6, null, "New Lego City Speedrun Record!", null },
                    { 7, null, "GTA hydra abuse", null },
                    { 8, null, "Tetris laggy. What is my bottleneck??? help", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
