namespace Appli_gestion_cla.Models
{
    public class Classe
    {
        public int Id { get; set; }
        public string? Nom_sale { get; set; }
        public int Capacite_Max { get; set; }

        public ICollection<Etudiant> Etudiants { get; set; } = new List<Etudiant>();
        public ICollection<Enseignant> Enseignants { get; set; } = new List<Enseignant>();
        public ICollection<Matiere> Matieres { get; set; } = new List<Matiere>();
        public ICollection<Affectation> Affectations { get; set; } = new List<Affectation>();
        public ICollection<Absence>? Absences { get; set; }
    }
}
