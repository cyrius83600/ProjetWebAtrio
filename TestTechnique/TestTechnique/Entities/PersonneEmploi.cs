using System.ComponentModel.DataAnnotations;

namespace TestTechnique.Entities
{
    public class PersonneEmploi
    {
        [Key]
        public int PersonneEmploiID { get; set; }
        public Personne Persone { get; set; }
        public int PersonneID { get; set; }
        public Emploi Emploi { get; set; }
        public int EmploiID { get; set; }
        public DateTime dateDebut { get; set; }
        public DateTime dateFin { get; set; }

    }
}
