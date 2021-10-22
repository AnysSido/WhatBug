using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatBug.Persistence.Migrations
{
    public partial class AddDefaultPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Icons",
                columns: new[] { "Id", "Name", "WebName" },
                values: new object[,]
                {
                    { 37, "Circle", "circle" },
                    { 38, "Wave Square", "wave-square" }
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "Id", "ColorId", "Description", "IconId", "Name", "Order" },
                values: new object[] { 1, 63, "The default priority used by all issues without any other priority assigned.", 38, "Default", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Icons",
                keyColumn: "Id",
                keyValue: 38);
        }
    }
}
