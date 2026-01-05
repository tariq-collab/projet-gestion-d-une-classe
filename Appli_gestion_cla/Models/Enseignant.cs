namespace Appli_gestion_cla.Models
{
    public class Enseignant
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }

        public string? Email { get; set; }

        public int? MatiereId { get; set; }
        public Matiere? Matiere { get; set; }


        public ICollection<Classe> Classes { get; set; } = new List<Classe>();
        public ICollection<Matiere> Matieres { get; set; } = new List<Matiere>();
        public ICollection<Affectation> Affectations { get; set; } = new List<Affectation>();
    }
}
