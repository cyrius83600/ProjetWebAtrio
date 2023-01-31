namespace TestTechnique.Entities
{
    public class EmploiDto
    {
        public int ID { get; set; }
        public string Poste { get; set; }
        public string Entreprise { get; set; }
        public DateTime? dateDebut { get; set; }
        public DateTime? dateFin { get; set; }
    }
}
