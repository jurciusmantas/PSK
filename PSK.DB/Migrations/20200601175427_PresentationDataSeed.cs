using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSK.DB.Migrations
{
    public partial class PresentationDataSeed : Migration
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

            migrationBuilder.UpdateData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2020, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2020, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2020, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2020, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2020, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2020, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 7,
                column: "Date",
                value: new DateTime(2020, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "Id", "Date", "EmployeeId", "TopicId" },
                values: new object[,]
                {
                    { 29, new DateTime(2020, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3 },
                    { 28, new DateTime(2020, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 12, new DateTime(2020, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 25, new DateTime(2020, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4 },
                    { 17, new DateTime(2020, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4 },
                    { 15, new DateTime(2020, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 14, new DateTime(2020, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 20, new DateTime(2020, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 },
                    { 13, new DateTime(2020, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 18, new DateTime(2020, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4 },
                    { 27, new DateTime(2020, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 21, new DateTime(2020, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeRestriction",
                columns: new[] { "Id", "EmployeeId", "RestrictionId" },
                values: new object[,]
                {
                    { 2, 3, 1 },
                    { 5, 6, 1 },
                    { 3, 4, 1 },
                    { 4, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "LeaderId", "Name", "Password" },
                values: new object[,]
                {
                    { 10, "vytas@gmail.com", 1, "vytas", "WJhLLap3r2gIReQyuqplu/UpE8LCjg7gCquuKomeEnVNhhL8" },
                    { 9, "elena@gmail.com", 2, "elena", "CB4ltXA0mmqJy/8y5xq+cNqBJ95ykdJ17JT1amz1JuMHTpke" },
                    { 8, "jonas@gmail.com", 1, "jonas", "dwuCXS1FIDzjkkQ+9KlFKdwZf7zwMzwChWj5TaqrZklsu8NS" },
                    { 7, "gerda@gmail.com", 1, "gerda", "gNtzUNfmP4zx17KZqhZOVrRZq7LlEtjihxgifaD++coEGt4R" }
                });

            migrationBuilder.InsertData(
                table: "Restrictions",
                columns: new[] { "Id", "ConsecutiveDays", "CreationDate", "CreatorId", "Global", "MaxDaysPerMonth", "MaxDaysPerQuarter", "MaxDaysPerYear" },
                values: new object[,]
                {
                    { 3, 3, new DateTime(2020, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, 3, 16, 30 },
                    { 4, 5, new DateTime(2020, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false, 5, 15, 60 },
                    { 2, 3, new DateTime(2020, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, 4, 12, 48 }
                });

            migrationBuilder.InsertData(
                table: "TopicCompletions",
                columns: new[] { "Id", "CompletedOn", "EmployeeId", "TopicId" },
                values: new object[,]
                {
                    { 2, new DateTime(2020, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4 },
                    { 3, new DateTime(2020, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 4, new DateTime(2020, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 },
                    { 5, new DateTime(2020, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3 },
                    { 9, new DateTime(2020, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4 },
                    { 11, new DateTime(2020, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 12, new DateTime(2020, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 },
                    { 13, new DateTime(2020, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Description", "Name", "ParentTopicId" },
                values: new object[,]
                {
                    { 7, "Link https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/", "C# LINQ", 4 },
                    { 6, "Link http://www.thejavageek.com/2014/01/12/jpa-crud-example/", "Java EE JPA.CRUD Example", 2 },
                    { 5, "Link https://www.javaworld.com/article/3379043/what-is-jpa-introduction-to-the-java-persistence-api.html", "Java EE JPA.Introduction", 2 },
                    { 11, "Link https://www.w3schools.com/sql/", "SQL", null },
                    { 8, "Documentation: https://docs.microsoft.com/en-us/ef/", "C# Entity Framework", 4 }
                });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "Id", "Date", "EmployeeId", "TopicId" },
                values: new object[,]
                {
                    { 19, new DateTime(2020, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 3 },
                    { 10, new DateTime(2020, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 1 },
                    { 11, new DateTime(2020, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 1 },
                    { 26, new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5 }
                });

            migrationBuilder.UpdateData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 7,
                column: "EmployeeId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 8,
                column: "EmployeeId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 9,
                column: "EmployeeId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "EmployeeId", "RestrictionId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "EmployeeRestriction",
                columns: new[] { "Id", "EmployeeId", "RestrictionId" },
                values: new object[,]
                {
                    { 6, 7, 1 },
                    { 18, 9, 4 },
                    { 17, 5, 4 },
                    { 15, 10, 3 },
                    { 16, 4, 4 },
                    { 13, 10, 2 },
                    { 12, 7, 2 },
                    { 11, 3, 2 },
                    { 14, 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Recommendations",
                columns: new[] { "Id", "CreatorId", "ReceiverId", "TopicId" },
                values: new object[,]
                {
                    { 4, 1, 3, 5 },
                    { 5, 1, 4, 6 },
                    { 6, 2, 5, 7 },
                    { 7, 1, 9, 8 },
                    { 8, 1, 8, 8 },
                    { 9, 1, 2, 8 }
                });

            migrationBuilder.InsertData(
                table: "TopicCompletions",
                columns: new[] { "Id", "CompletedOn", "EmployeeId", "TopicId" },
                values: new object[] { 10, new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5 });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Description", "Name", "ParentTopicId" },
                values: new object[,]
                {
                    { 12, "Link https://www.mysql.com/", "MySQL", 11 },
                    { 9, "Link https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database", "C# EF Code first", 8 },
                    { 10, "Link https://docs.microsoft.com/en-us/ef/ef6/modeling/designer/workflows/database-first", "C# EF DB first", 8 },
                    { 13, "Link https://www.postgresql.org/", "PostgreSQL", 11 }
                });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "Id", "Date", "EmployeeId", "TopicId" },
                values: new object[] { 32, new DateTime(2020, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 13 });

            migrationBuilder.InsertData(
                table: "Recommendations",
                columns: new[] { "Id", "CreatorId", "ReceiverId", "TopicId" },
                values: new object[,]
                {
                    { 10, 1, 3, 9 },
                    { 11, 2, 5, 10 }
                });

            migrationBuilder.InsertData(
                table: "TopicCompletions",
                columns: new[] { "Id", "CompletedOn", "EmployeeId", "TopicId" },
                values: new object[] { 16, new DateTime(2020, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 13 });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Description", "Name", "ParentTopicId" },
                values: new object[,]
                {
                    { 14, "Link https://dev.mysql.com/doc/refman/8.0/en/creating-database.html", "MySQL DB creation", 12 },
                    { 15, "Link https://dev.mysql.com/doc/refman/8.0/en/trigger-syntax.html", "MySQL Triggers", 12 },
                    { 16, "Link https://dev.mysql.com/doc/mysql-tutorial-excerpt/8.0/en/examples.html", "MySQL Queries", 12 }
                });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "Id", "Date", "EmployeeId", "TopicId" },
                values: new object[,]
                {
                    { 31, new DateTime(2020, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 14 },
                    { 16, new DateTime(2020, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 16 }
                });

            migrationBuilder.InsertData(
                table: "Recommendations",
                columns: new[] { "Id", "CreatorId", "ReceiverId", "TopicId" },
                values: new object[] { 12, 2, 5, 14 });

            migrationBuilder.InsertData(
                table: "TopicCompletions",
                columns: new[] { "Id", "CompletedOn", "EmployeeId", "TopicId" },
                values: new object[] { 15, new DateTime(2020, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 14 });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Description", "Name", "ParentTopicId" },
                values: new object[,]
                {
                    { 17, "Link https://dev.mysql.com/doc/refman/8.0/en/select.html", "MySQL Select", 16 },
                    { 18, "Link https://www.w3schools.com/sql/sql_insert.asp", "MySQL Insert Into", 16 },
                    { 19, "Link https://www.w3schools.com/sql/sql_update.asp", "MySQL Update", 16 },
                    { 22, "Link https://dev.mysql.com/doc/refman/8.0/en/delete.html", "MySQL Delete", 16 }
                });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "Id", "Date", "EmployeeId", "TopicId" },
                values: new object[,]
                {
                    { 24, new DateTime(2020, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 18 },
                    { 23, new DateTime(2020, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 19 }
                });

            migrationBuilder.InsertData(
                table: "TopicCompletions",
                columns: new[] { "Id", "CompletedOn", "EmployeeId", "TopicId" },
                values: new object[,]
                {
                    { 8, new DateTime(2020, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 18 },
                    { 7, new DateTime(2020, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 19 }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Description", "Name", "ParentTopicId" },
                values: new object[,]
                {
                    { 20, "Link https://www.w3schools.com/sql/sql_join.asp", "MySQL Select with Join", 17 },
                    { 21, "Link https://www.w3schools.com/php/php_mysql_select_limit.asp", "MySQL Select with Limit", 17 }
                });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "Id", "Date", "EmployeeId", "TopicId" },
                values: new object[,]
                {
                    { 22, new DateTime(2020, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 20 },
                    { 30, new DateTime(2020, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 20 }
                });

            migrationBuilder.InsertData(
                table: "TopicCompletions",
                columns: new[] { "Id", "CompletedOn", "EmployeeId", "TopicId" },
                values: new object[,]
                {
                    { 6, new DateTime(2020, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 20 },
                    { 14, new DateTime(2020, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 20 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 6);

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
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "EmployeeRestriction",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TopicCompletions",
                keyColumn: "Id",
                keyValue: 12);

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

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Restrictions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Restrictions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Restrictions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 11);

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
                table: "Days",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2020, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2020, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2020, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2020, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 7,
                column: "Date",
                value: new DateTime(2020, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "EmployeeRestriction",
                columns: new[] { "Id", "EmployeeId", "RestrictionId" },
                values: new object[,]
                {
                    { 10, 6, 1 },
                    { 9, 5, 1 },
                    { 8, 4, 1 },
                    { 7, 3, 1 }
                });
        }
    }
}
