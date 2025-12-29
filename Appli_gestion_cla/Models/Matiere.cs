namespace Appli_gestion_cla.Models
{
    public class Matiere
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public ICollection<Enseignant> Enseignants { get; set; }
        public ICollection<Classe> Classes { get; set; }
        public ICollection<Affectation> Affectations { get; set; }
    }
}
