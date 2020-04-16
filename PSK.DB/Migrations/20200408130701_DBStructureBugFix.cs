using Microsoft.EntityFrameworkCore.Migrations;

namespace PSK.DB.Migrations
{
    public partial class DBStructureBugFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_Employees_CreatedById",
                table: "Recommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_Employees_RecommendedToId",
                table: "Recommendations");

            migrationBuilder.DropIndex(
                name: "IX_Recommendations_CreatedById",
                table: "Recommendations");

            migrationBuilder.DropIndex(
                name: "IX_Recommendations_RecommendedToId",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "RecommendedToId",
                table: "Recommendations");

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParentTopicId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 3,
                column: "ParentTopicId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_CreatorId",
                table: "Recommendations",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_ReceiverId",
                table: "Recommendations",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_Employees_CreatorId",
                table: "Recommendations",
                column: "CreatorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_Employees_ReceiverId",
                table: "Recommendations",
                column: "ReceiverId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_Employees_CreatorId",
                table: "Recommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_Employees_ReceiverId",
                table: "Recommendations");

            migrationBuilder.DropIndex(
                name: "IX_Recommendations_CreatorId",
                table: "Recommendations");

            migrationBuilder.DropIndex(
                name: "IX_Recommendations_ReceiverId",
                table: "Recommendations");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Topics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Recommendations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedToId",
                table: "Recommendations",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ParentId", "ParentTopicId" },
                values: new object[] { 1, null });

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ParentId", "ParentTopicId" },
                values: new object[] { 1, null });

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_CreatedById",
                table: "Recommendations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_RecommendedToId",
                table: "Recommendations",
                column: "RecommendedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_Employees_CreatedById",
                table: "Recommendations",
                column: "CreatedById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_Employees_RecommendedToId",
                table: "Recommendations",
                column: "RecommendedToId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
