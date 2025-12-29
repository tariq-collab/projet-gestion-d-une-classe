using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appli_gestion_cla.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEnseignantMatiereClasse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matiere",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matiere", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClasseMatiere",
                columns: table => new
                {
                    ClassesId = table.Column<int>(type: "int", nullable: false),
                    MatieresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasseMatiere", x => new { x.ClassesId, x.MatieresId });
                    table.ForeignKey(
                        name: "FK_ClasseMatiere_Classe_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClasseMatiere_Matiere_MatieresId",
                        column: x => x.MatieresId,
                        principalTable: "Matiere",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnseignantMatiere",
                columns: table => new
                {
                    EnseignantsId = table.Column<int>(type: "int", nullable: false),
                    MatieresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnseignantMatiere", x => new { x.EnseignantsId, x.MatieresId });
                    table.ForeignKey(
                        name: "FK_EnseignantMatiere_Enseignants_EnseignantsId",
                        column: x => x.EnseignantsId,
                        principalTable: "Enseignants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnseignantMatiere_Matiere_MatieresId",
                        column: x => x.MatieresId,
                        principalTable: "Matiere",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClasseMatiere_MatieresId",
                table: "ClasseMatiere",
                column: "MatieresId");

            migrationBuilder.CreateIndex(
                name: "IX_EnseignantMatiere_MatieresId",
                table: "EnseignantMatiere",
                column: "MatieresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClasseMatiere");

            migrationBuilder.DropTable(
                name: "EnseignantMatiere");

            migrationBuilder.DropTable(
                name: "Matiere");
        }
    }
}
