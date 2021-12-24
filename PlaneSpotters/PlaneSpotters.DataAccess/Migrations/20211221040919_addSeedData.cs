using Microsoft.EntityFrameworkCore.Migrations;

namespace PlaneSpotters.DataAccess.Migrations
{
    public partial class addSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c95da274-f07c-41ce-b84f-38e41d94f5a5", "5a6a4325-fa1a-4d1b-897f-bb10ada89e7a", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4ca8f2fb-e250-43ee-aeed-2bf460af0032", "06006331-a216-49c4-afd1-ff64181a7937", "Spotter", "Spotter" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ca8f2fb-e250-43ee-aeed-2bf460af0032");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c95da274-f07c-41ce-b84f-38e41d94f5a5");
        }
    }
}
