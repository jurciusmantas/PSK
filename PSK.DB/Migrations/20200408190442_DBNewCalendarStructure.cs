using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSK.DB.Migrations
{
    public partial class DBNewCalendarStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "AssignedTopics");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Restrictions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Restrictions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    TopicId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Days_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Days_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicCompletions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TopicId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    CompletedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicCompletions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicCompletions_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicCompletions_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "Id", "Date", "EmployeeId", "TopicId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 2, new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 3, new DateTime(2020, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 4, new DateTime(2020, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 5, new DateTime(2020, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2 },
                    { 6, new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2 },
                    { 7, new DateTime(2020, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2 },
                    { 8, new DateTime(2020, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4 },
                    { 9, new DateTime(2020, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4 }
                });

            migrationBuilder.InsertData(
                table: "TopicCompletions",
                columns: new[] { "Id", "CompletedOn", "EmployeeId", "TopicId" },
                values: new object[] { 1, new DateTime(2020, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Restrictions_CreatorId",
                table: "Restrictions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Days_EmployeeId",
                table: "Days",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Days_TopicId",
                table: "Days",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicCompletions_EmployeeId",
                table: "TopicCompletions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicCompletions_TopicId",
                table: "TopicCompletions",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restrictions_Employees_CreatorId",
                table: "Restrictions",
                column: "CreatorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restrictions_Employees_CreatorId",
                table: "Restrictions");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "TopicCompletions");

            migrationBuilder.DropIndex(
                name: "IX_Restrictions_CreatorId",
                table: "Restrictions");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Restrictions");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Restrictions");

            migrationBuilder.CreateTable(
                name: "AssignedTopics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompletedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EmploeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    IsCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedTopics_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignedTopics_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssignedTopicId = table.Column<int>(type: "int", nullable: false),
                    WorkDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plans_AssignedTopics_AssignedTopicId",
                        column: x => x.AssignedTopicId,
                        principalTable: "AssignedTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AssignedTopics",
                columns: new[] { "Id", "CompletedOn", "EmploeeId", "EmployeeId", "IsCompleted", "TopicId" },
                values: new object[,]
                {
                    { 1, null, 2, null, false, 1 },
                    { 2, null, 4, null, false, 2 },
                    { 3, new DateTime(2020, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, true, 3 },
                    { 4, null, 4, null, false, 4 }
                });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "AssignedTopicId", "WorkDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, new DateTime(2020, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, new DateTime(2020, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 2, new DateTime(2020, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 2, new DateTime(2020, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 4, new DateTime(2020, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 4, new DateTime(2020, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTopics_EmployeeId",
                table: "AssignedTopics",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTopics_TopicId",
                table: "AssignedTopics",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_AssignedTopicId",
                table: "Plans",
                column: "AssignedTopicId");
        }
    }
}
