namespace Appli_gestion_cla.Models
{
    public class Absence
    {
        public int Id { get; set; }
        public int? Heure_Absence { get; set; }

        public int EtudiantId { get; set; }
        public Etudiant? Etudiant { get; set; }

        public int ClasseId { get; set; }
        public Classe? Classe { get; set; }
    }
}
