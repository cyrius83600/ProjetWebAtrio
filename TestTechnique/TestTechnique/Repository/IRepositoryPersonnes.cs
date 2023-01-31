using Microsoft.EntityFrameworkCore;
using TestTechnique.Entities;

namespace TestTechnique.Repository
{
    public interface IRepositoryPersonnes
    {
        public void SauvegarderPersonne(Personne personne);
        public void UpdatePersonne(int personneID, int emploiID, DateTime? debut, DateTime? fin);
        public List<PersonneDto> GetAllPersonne();
        public List<IEnumerable<PersonneEmploiDto>> GetPersonesPerEntreprises(string entreprise);
        public PersonneDto GetEmploisPerPersonne(int personneID, DateTime dateDebut, DateTime dateFin);
    }
}
