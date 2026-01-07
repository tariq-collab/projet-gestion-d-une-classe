namespace Appli_gestion_cla.Models
{
    public class Matiere
    {
        public int Id { get; set; }
        public string? Nom { get; set; }

        public int Nombre_heures { get; set; }

        public ICollection<Enseignant> Enseignants { get; set; } = new List<Enseignant>();
        public ICollection<Classe> Classes { get; set; } = new List<Classe>();
        public ICollection<Affectation> Affectations { get; set; } = new List<Affectation>();
    }
}
