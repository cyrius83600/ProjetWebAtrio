namespace TestTechnique.Entities
{
    public class Jointure
    {
        public int PersonneId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public int EmploiID { get; set; }
        public string Poste { get; set; }
        public string Entreprise { get; set; }
        public DateTime? dateDebut { get; set; }
        public DateTime? dateFin { get; set; }
    }
}
