using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatBug.Persistence.Migrations
{
    public partial class UpdateIcons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WebName",
                table: "Icons",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 1,
                column: "WebName",
                value: "arrow-up");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 2,
                column: "WebName",
                value: "arrow-down");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 3,
                column: "WebName",
                value: "arrow-left");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 4,
                column: "WebName",
                value: "arrow-right");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 5,
                column: "WebName",
                value: "arrow-circle-up");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 6,
                column: "WebName",
                value: "arrow-circle-down");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 7,
                column: "WebName",
                value: "arrow-circle-left");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 8,
                column: "WebName",
                value: "arrow-circle-right");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 9,
                column: "WebName",
                value: "chevron-up");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 10,
                column: "WebName",
                value: "chevron-down");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 11,
                column: "WebName",
                value: "chevron-left");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 12,
                column: "WebName",
                value: "chevron-right");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 13,
                column: "WebName",
                value: "angle-up");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 14,
                column: "WebName",
                value: "angle-down");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 15,
                column: "WebName",
                value: "angle-left");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 16,
                column: "WebName",
                value: "angle-right");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 17,
                column: "WebName",
                value: "angles-up");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 18,
                column: "WebName",
                value: "angles-down");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 19,
                column: "WebName",
                value: "angles-left");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 20,
                column: "WebName",
                value: "angles-right");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Name", "WebName" },
                values: new object[] { "Exclaimaton", "exclamation" });

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Name", "WebName" },
                values: new object[] { "Circle Exclamation", "exclamation-circle" });

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Name", "WebName" },
                values: new object[] { "Triangle Exclamation", "exclamation-triangle" });

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Name", "WebName" },
                values: new object[] { "X Mark", "x-mark" });

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 25,
                column: "WebName",
                value: "ban");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 26,
                column: "WebName",
                value: "equals");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 27,
                column: "WebName",
                value: "bug");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Name", "WebName" },
                values: new object[] { "Square Plus", "plus-square" });

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Name", "WebName" },
                values: new object[] { "Square Check", "check-square" });

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Name", "WebName" },
                values: new object[] { "Square Caret Up", "caret-up-square" });

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Name", "WebName" },
                values: new object[] { "Square Caret Down", "caret-down-square" });

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Name", "WebName" },
                values: new object[] { "Square Caret Left", "caret-left-square" });

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Name", "WebName" },
                values: new object[] { "Square Caret Right", "caret-right-square" });

            migrationBuilder.InsertData(
                table: "Icons",
                columns: new[] { "Id", "Name", "WebName" },
                values: new object[,]
                {
                    { 36, "Information", "information" },
                    { 35, "Plus", "plus" },
                    { 34, "Pen", "pen" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DropColumn(
                name: "WebName",
                table: "Icons");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 21,
                column: "Name",
                value: "Exclaimation");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 22,
                column: "Name",
                value: "CircleExclaimation");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 23,
                column: "Name",
                value: "TriangleExclaimation");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 24,
                column: "Name",
                value: "XMark");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 28,
                column: "Name",
                value: "PlusSquare");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 29,
                column: "Name",
                value: "CheckSquare");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 30,
                column: "Name",
                value: "CaretSquareUp");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 31,
                column: "Name",
                value: "CaretSquareDown");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 32,
                column: "Name",
                value: "CaretSquareLeft");

            migrationBuilder.UpdateData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 33,
                column: "Name",
                value: "CaretSquareRight");
        }
    }
}
