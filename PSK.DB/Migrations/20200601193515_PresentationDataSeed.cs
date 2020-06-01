using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSK.DB.Migrations
{
    public partial class PresentationDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RowVersion",
                table: "Topics",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(6)",
                oldNullable: true)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.UpdateData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CompletedOn", "TopicId" },
                values: new object[] { new DateTime(2020, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 14 });

            migrationBuilder.UpdateData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CompletedOn", "TopicId" },
                values: new object[] { new DateTime(2020, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 13 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RowVersion",
                table: "Topics",
                type: "timestamp(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.UpdateData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CompletedOn", "TopicId" },
                values: new object[] { new DateTime(2020, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.UpdateData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CompletedOn", "TopicId" },
                values: new object[] { new DateTime(2020, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.InsertData(
                table: "TopicCompletions",
                columns: new[] { "Id", "CompletedOn", "EmployeeId", "TopicId" },
                values: new object[,]
                {
                    { 13, new DateTime(2020, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3 },
                    { 14, new DateTime(2020, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 20 },
                    { 15, new DateTime(2020, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 14 },
                    { 16, new DateTime(2020, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 13 }
                });
        }
    }
}
