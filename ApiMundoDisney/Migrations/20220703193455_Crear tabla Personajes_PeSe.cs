using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiMundoDisney.Migrations
{
    public partial class CreartablaPersonajes_PeSe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonajesPeliculasSeries");

            migrationBuilder.CreateTable(
                name: "Personajes_PeSe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonajeId = table.Column<int>(type: "int", nullable: false),
                    PeliculaSerieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personajes_PeSe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personajes_PeSe_PeliculasSeries_PeliculaSerieId",
                        column: x => x.PeliculaSerieId,
                        principalTable: "PeliculasSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personajes_PeSe_Personajes_PersonajeId",
                        column: x => x.PersonajeId,
                        principalTable: "Personajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personajes_PeSe_PeliculaSerieId",
                table: "Personajes_PeSe",
                column: "PeliculaSerieId");

            migrationBuilder.CreateIndex(
                name: "IX_Personajes_PeSe_PersonajeId",
                table: "Personajes_PeSe",
                column: "PersonajeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personajes_PeSe");

            migrationBuilder.CreateTable(
                name: "PersonajesPeliculasSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeliculaSerieId = table.Column<int>(type: "int", nullable: false),
                    PersonajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajesPeliculasSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonajesPeliculasSeries_PeliculasSeries_PeliculaSerieId",
                        column: x => x.PeliculaSerieId,
                        principalTable: "PeliculasSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajesPeliculasSeries_Personajes_PersonajeId",
                        column: x => x.PersonajeId,
                        principalTable: "Personajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonajesPeliculasSeries_PeliculaSerieId",
                table: "PersonajesPeliculasSeries",
                column: "PeliculaSerieId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajesPeliculasSeries_PersonajeId",
                table: "PersonajesPeliculasSeries",
                column: "PersonajeId");
        }
    }
}
