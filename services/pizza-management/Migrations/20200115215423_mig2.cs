using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaGraphQL.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toppings_Pizzas_PizzaId",
                table: "Toppings");

            migrationBuilder.DropIndex(
                name: "IX_Toppings_PizzaId",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "Toppings");

            migrationBuilder.CreateTable(
                name: "PizzaTopping",
                columns: table => new
                {
                    PizzaId = table.Column<int>(nullable: false),
                    ToppingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaTopping", x => new { x.PizzaId, x.ToppingId });
                    table.ForeignKey(
                        name: "FK_PizzaTopping_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaTopping_Toppings_ToppingId",
                        column: x => x.ToppingId,
                        principalTable: "Toppings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PizzaTopping_ToppingId",
                table: "PizzaTopping",
                column: "ToppingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaTopping");

            migrationBuilder.AddColumn<int>(
                name: "PizzaId",
                table: "Toppings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Toppings_PizzaId",
                table: "Toppings",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Toppings_Pizzas_PizzaId",
                table: "Toppings",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
