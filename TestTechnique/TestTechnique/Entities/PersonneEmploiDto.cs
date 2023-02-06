namespace TestTechnique.Entities
{
    public class PersonneEmploiDto
    {
        public PersonneDto Personne { get; set; }
        public List<EmploiDto> Emplois { get; set; } 
        
        public int Age { get; set; }
        public int GetAge()
        {
            return (int)((DateTime.Now - this.Personne.DateNaissance).TotalDays / 365);
        }
        //public PersonneEmploi PersonneEmploi { get; set; }
    }
}
