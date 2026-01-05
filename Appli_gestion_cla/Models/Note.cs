using System;

namespace Appli_gestion_cla.Models
{
    public class Note
    {
        public int Id { get; set; }

        public double Valeur { get; set; }

        public DateTime DateEvaluation { get; set; }

        public string? TypeEvaluation { get; set; }


        public int EtudiantId { get; set; }
        public Etudiant? Etudiant { get; set; }

        public int AffectationId { get; set; }

        public Affectation? Affectation { get; set; }

    }
}
