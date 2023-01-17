using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestTechnique.Entities
{
    public class Personne
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonneId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public List<PersonneEmploi> personneEmplois { get; set; } = new List<PersonneEmploi>();

        [NotMapped]
        public int Age { get; set; }

        public int GetAge()
        {
            return (int)((DateTime.Now - DateNaissance).TotalDays / 365);
        }
        public bool IsValide()
        {
            if(GetAge() >= 150 )
            {
                return false;
            }
            return true;
        }
    }
}
