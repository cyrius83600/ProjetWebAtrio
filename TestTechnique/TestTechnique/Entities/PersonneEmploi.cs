using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTechnique.Entities
{
    public class PersonneEmploi
    {
        [Key]
        public int PersonneEmploiID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Entreprise { get; set; }
        public string Poste { get; set; }
        public DateTime? dateDebut { get; set; }
        public DateTime? dateFin { get; set; }
        public Emploi Emploi { get; set; }
        public Personne Personne { get; set; }
        public int EmploiID { get; set; }
        public int PersonneId { get; set; }

        





    }
}
