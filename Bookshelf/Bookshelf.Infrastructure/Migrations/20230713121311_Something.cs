using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshelf.Infrastructure.Migrations
{
    public partial class Something : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "784a1c74-c080-49ea-87a2-eb484893edfb", "AQAAAAEAACcQAAAAEPntmrUpwMcg4b938P4XhnmOsRQwVQYvKrtFC0SZ5Ddd5oveojh8Wor9LJHO9fzB1Q==", "eab66b59-c764-462e-8177-574d9922ad71" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6e7b3ae-a59a-4216-9a0f-225761c88800", "AQAAAAEAACcQAAAAEEOpehEpp/9ms3S2B/msoK83eeys24hoQV3nZmVkFrXbejN1I6XS7n9N502FoXqQug==", "b008e4ca-678f-4fed-8144-e5e3ba144b31" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "684904be-8e10-4456-9de3-293a0af9007c", "AQAAAAEAACcQAAAAEJFkYjlT61zz8Ge/BJUYnZoKQxGKoaKo3mgtZy+hasFZIvVNMgEoy1HYVBSPhopGIA==", "29f60d06-c31d-4d4d-a9cd-ae0fc049a26b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cda5f7d6-8d55-49c4-9fea-065af7aee8aa", "AQAAAAEAACcQAAAAEC8Rx79PvnsC5/DHV12AvIObBxZDQe51GdB0wKJpKOrUDHS02r12oPFAkRIRvs2pyA==", "89d44388-f622-43fe-aaa3-9f7e4c84657a" });
        }
    }
}
