using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshelf.Web.Data.Migrations
{
    public partial class SeededUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 0, "dd478bd2-e60a-45b2-8922-8cb0c52f2fa2", "ApplicationUser", "guest@gmail.com", false, "Ivan", "Georgiev", false, null, "GUEST@GMAIL.COM", "IVAN", "AQAAAAEAACcQAAAAEOt4GWxf9xPg2MJRAhpoTf64QjT9dUNKVgVfBPWZCzmeN1F2ojWfqq8guU0qqPQCgQ==", null, false, "f9a1e6e4-8f0b-499a-9dfc-6f97ab4eabec", false, "ivan" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dea12856-c198-4129-b3f3-b893d8395082", 0, "f0bb4cad-3677-43f1-a02f-188f9aa2ac53", "ApplicationUser", "admin@gmail.com", false, "Spiridon", "Stamatov", false, null, "ADMIN@GMAIL.COM", "SPIRIDON", "AQAAAAEAACcQAAAAEOaZP1ylDGILJ7ovmO8rhQLEAG9gtntG1fYx7Q/k2WDL7R/cEtQGQiqa8WQnQg6iwQ==", null, false, "c792ea86-c146-4e01-b5a9-e999245b98c8", false, "Spiridon" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");
        }
    }
}
