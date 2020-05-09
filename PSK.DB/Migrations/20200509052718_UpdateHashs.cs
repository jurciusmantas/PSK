using Microsoft.EntityFrameworkCore.Migrations;

namespace PSK.DB.Migrations
{
    public partial class UpdateHashs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "80/UimlzH7DOAw2PjY2UKOTdOLdpWZoWZWZfypQlRf7CDg1+");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "Tj/pU/YsOmLlmhKWo1Sbcb7Tb2lTUKMGo7G/UqDFud/PQPTQ");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "cwRgMnxMI6AU8ntGvYS3aq2tQ/rZtAnYoqHowGVUlBhNKWsh");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "MPhtBwdZpog8fLWUq9SZFWrwgOQr54spCr39caSxqgcfKyUX");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "OxvoDPKzWkeqwtxsmADnn1bk/C/NU3laWW65tlIyEn0PAESK");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "pr/2XcIIo91BH++8mtF//QiErYTm9CJUwoghSUWvbWglkSmk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "admin");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "vladas");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "ona");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "ema");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "matas");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "zita");
        }
    }
}
