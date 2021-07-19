using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddColorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "White" },
                    { 34, "Blue" },
                    { 35, "Cobalt" },
                    { 36, "Navy" },
                    { 37, "Mint" },
                    { 38, "Seafoam" },
                    { 39, "Lime" },
                    { 40, "Olive" },
                    { 41, "Pear" },
                    { 42, "Pickle" },
                    { 43, "Green" },
                    { 44, "Shamrock" },
                    { 45, "Emerald" },
                    { 46, "Basil" },
                    { 33, "Cerulean" },
                    { 47, "Tortilla" },
                    { 49, "Tawny" },
                    { 50, "Caramel" },
                    { 51, "Coffee" },
                    { 52, "Mocha" },
                    { 53, "Hickory" },
                    { 54, "Brown" },
                    { 55, "Cloud" },
                    { 56, "Silver" },
                    { 57, "Coin" },
                    { 58, "Ash" },
                    { 59, "Gray" },
                    { 60, "Pewter" },
                    { 61, "Smoke" },
                    { 48, "Peanut" },
                    { 62, "Slate" },
                    { 32, "Sapphire" },
                    { 30, "Violet" },
                    { 2, "Chiffon" },
                    { 3, "Frost" },
                    { 4, "Tan" },
                    { 5, "Buttermilk" },
                    { 6, "Beige" },
                    { 7, "Oat" },
                    { 8, "Cookie" },
                    { 9, "Latte" },
                    { 10, "Lemon" }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 11, "Yellow" },
                    { 12, "Honey" },
                    { 13, "Gold" },
                    { 14, "Apricot" },
                    { 31, "Sky" },
                    { 15, "Orange" },
                    { 17, "Red" },
                    { 18, "Rose" },
                    { 19, "Candy" },
                    { 20, "Apple" },
                    { 21, "Cherry" },
                    { 22, "Pink" },
                    { 23, "Fushcia" },
                    { 24, "Hotpink" },
                    { 25, "Magenta" },
                    { 26, "Lilac" },
                    { 27, "Iris" },
                    { 28, "Amethyst" },
                    { 29, "Purple" },
                    { 16, "Blush" },
                    { 63, "Black" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colors");
        }
    }
}
