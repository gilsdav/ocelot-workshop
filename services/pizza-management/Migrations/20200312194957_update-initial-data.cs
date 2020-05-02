using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaGraphQL.Migrations
{
    public partial class updateinitialdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "bacon");

            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "pepperoni");

            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "tomato");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "gorgonzola");

            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "salami");

            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "thon");
        }
    }
}
