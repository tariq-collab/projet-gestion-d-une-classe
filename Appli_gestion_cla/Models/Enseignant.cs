namespace Appli_gestion_cla.Models
{
    public class Enseignant
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Matiere { get; set; }

        public ICollection<Classe> Classe { get; set; }
        public ICollection<Matiere> Matieres { get; set; }
        public ICollection<Affectation> Affectations { get; set; }
    }
}
