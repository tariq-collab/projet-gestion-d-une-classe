using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appli_gestion_cla.Migrations
{
    /// <inheritdoc />
    public partial class heure_matiere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nbr_heure",
                table: "Matieres",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nbr_heure",
                table: "Matieres");
        }
    }
}
