using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTechnique.Entities
{
    public class Emploi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmploiId { get; set; }
        public string Entreprise { get; set; }
        public string Poste { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

    }
}
