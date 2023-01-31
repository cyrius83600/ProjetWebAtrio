namespace TestTechnique.Entities
{
    public class PersonneDto
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public int Age { get; set; }
        public List<EmploiDto> Emplois { get; set; }

    }
}
