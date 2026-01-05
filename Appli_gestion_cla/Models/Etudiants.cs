namespace Appli_gestion_cla.Models
{
    public class Etudiant
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Age { get; set; }

        public int ClasseId { get; set; }
        public Classe? Classe { get; set; }


        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
