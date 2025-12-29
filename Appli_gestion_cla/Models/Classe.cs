namespace Appli_gestion_cla.Models
{
    public class Classe
    {
        public int Id { get; set; }
        public string Nom_sale { get; set; }
        public int Capacite_Max { get; set; }
        public ICollection<Etudiants> Etudiants { get; set; }
        public ICollection<Enseignant> Enseignants { get; set; }
        public ICollection<Matiere> Matieres { get; set; }
        public ICollection<Affectation> Affectations { get; set; }
    }
}
