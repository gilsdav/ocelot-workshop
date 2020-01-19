using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaGraphQL.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PizzaTopping_Pizzas_PizzaId",
                table: "PizzaTopping");

            migrationBuilder.DropForeignKey(
                name: "FK_PizzaTopping_Toppings_ToppingId",
                table: "PizzaTopping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PizzaTopping",
                table: "PizzaTopping");

            migrationBuilder.RenameTable(
                name: "PizzaTopping",
                newName: "PizzaToppings");

            migrationBuilder.RenameIndex(
                name: "IX_PizzaTopping_ToppingId",
                table: "PizzaToppings",
                newName: "IX_PizzaToppings_ToppingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PizzaToppings",
                table: "PizzaToppings",
                columns: new[] { "PizzaId", "ToppingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PizzaToppings_Pizzas_PizzaId",
                table: "PizzaToppings",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PizzaToppings_Toppings_ToppingId",
                table: "PizzaToppings",
                column: "ToppingId",
                principalTable: "Toppings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PizzaToppings_Pizzas_PizzaId",
                table: "PizzaToppings");

            migrationBuilder.DropForeignKey(
                name: "FK_PizzaToppings_Toppings_ToppingId",
                table: "PizzaToppings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PizzaToppings",
                table: "PizzaToppings");

            migrationBuilder.RenameTable(
                name: "PizzaToppings",
                newName: "PizzaTopping");

            migrationBuilder.RenameIndex(
                name: "IX_PizzaToppings_ToppingId",
                table: "PizzaTopping",
                newName: "IX_PizzaTopping_ToppingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PizzaTopping",
                table: "PizzaTopping",
                columns: new[] { "PizzaId", "ToppingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PizzaTopping_Pizzas_PizzaId",
                table: "PizzaTopping",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PizzaTopping_Toppings_ToppingId",
                table: "PizzaTopping",
                column: "ToppingId",
                principalTable: "Toppings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
