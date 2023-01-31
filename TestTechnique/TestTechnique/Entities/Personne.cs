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
        public List<PersonneEmploi> PersonnesEmplois { get; set; } = new List<PersonneEmploi>();
        public int GetAge()
        {
            return (int)((DateTime.Now - this.DateNaissance).TotalDays / 365);
        }
        public bool IsValide()
        {
            return GetAge() >= 150;
        }
    }
}
