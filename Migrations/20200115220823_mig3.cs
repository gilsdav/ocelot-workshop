using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaGraphQL.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PizzaTopping",
                columns: new[] { "PizzaId", "ToppingId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PizzaTopping",
                keyColumns: new[] { "PizzaId", "ToppingId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PizzaTopping",
                keyColumns: new[] { "PizzaId", "ToppingId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "PizzaTopping",
                keyColumns: new[] { "PizzaId", "ToppingId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "PizzaTopping",
                keyColumns: new[] { "PizzaId", "ToppingId" },
                keyValues: new object[] { 3, 3 });
        }
    }
}
