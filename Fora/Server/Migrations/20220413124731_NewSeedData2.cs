using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fora.Server.Migrations
{
    public partial class NewSeedData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Threads",
                columns: new[] { "Id", "InterestId", "Name", "UserId" },
                values: new object[] { 1, null, "Introduce yourself!", null });

            migrationBuilder.InsertData(
                table: "Threads",
                columns: new[] { "Id", "InterestId", "Name", "UserId" },
                values: new object[] { 2, null, "DS3 Cheat codes plz", null });

            migrationBuilder.InsertData(
                table: "Threads",
                columns: new[] { "Id", "InterestId", "Name", "UserId" },
                values: new object[] { 3, null, "How to get rich in sims 66", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
