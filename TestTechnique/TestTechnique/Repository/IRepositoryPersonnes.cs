using Microsoft.EntityFrameworkCore;
using TestTechnique.Entities;

namespace TestTechnique.Repository
{
    public interface IRepositoryPersonnes
    {
        public void SauvegarderPersonne(Personne personne);
        public void UpdatePersonne(int personneID, int emploiID, DateTime? debut, DateTime? fin);
        public List<PersonneEmploiDto> GetAllPersonne();
        public List<Emploi> GetPersonesPerEntreprises(string entreprise);
        public PersonneEmploiDto GetEmploisPerPersonne(int personneID, DateTime dateDebut, DateTime dateFin);
    }
}
