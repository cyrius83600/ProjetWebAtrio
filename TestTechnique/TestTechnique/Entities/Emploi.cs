using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTechnique.Entities
{
    public class Emploi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmploiID { get; set; }
        public string Entreprise { get; set; }
        public string Poste { get; set; }
        public List<PersonneEmploi> Personnes { get; set; } = new List<PersonneEmploi>();

    }
}
