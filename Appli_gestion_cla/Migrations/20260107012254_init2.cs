using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appli_gestion_cla.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nbr_heure",
                table: "Matieres",
                newName: "Nombre_heures");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre_heures",
                table: "Matieres",
                newName: "Nbr_heure");
        }
    }
}
