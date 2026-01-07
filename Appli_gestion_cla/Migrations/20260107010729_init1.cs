using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appli_gestion_cla.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nom_sale",
                table: "Classes",
                newName: "Nom_salle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nom_salle",
                table: "Classes",
                newName: "Nom_sale");
        }
    }
}
