using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSK.DB.Migrations
{
    public partial class GlobalRestrictionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RowVersion",
                table: "Topics",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(6)",
                oldNullable: true)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.InsertData(
                table: "Restrictions",
                columns: new[] { "Id", "ConsecutiveDays", "CreationDate", "CreatorId", "Global", "MaxDaysPerMonth", "MaxDaysPerQuarter", "MaxDaysPerYear" },
                values: new object[] { 1, 3, new DateTime(2020, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, 3, 9, 36 });

            migrationBuilder.InsertData(
                table: "EmployeeRestriction",
                columns: new[] { "Id", "EmployeeId", "RestrictionId" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 7, 3, 1 },
                    { 8, 4, 1 },
                    { 9, 5, 1 },
                    { 10, 6, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Restrictions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RowVersion",
                table: "Topics",
                type: "timestamp(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);
        }
    }
}
