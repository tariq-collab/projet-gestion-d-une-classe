namespace Appli_gestion_cla.Models
{
    public class Affectation
    {
        public int Id { get; set; }

        public int EnseignantId { get; set; }
        public Enseignant Enseignant { get; set; }

        public int MatiereId { get; set; }
        public Matiere Matiere { get; set; }

        public int ClasseId { get; set; }
        public Classe Classe { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
