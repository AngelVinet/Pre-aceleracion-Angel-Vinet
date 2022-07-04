using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiMundoDisney.Migrations
{
    public partial class CreartablaPersonajesPeliculasSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonajesPeliculasSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonajeId = table.Column<int>(type: "int", nullable: false),
                    PeliculaSerieId = table.Column<int>(type: "int", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonajesPeliculasSeries");
        }
    }
}
