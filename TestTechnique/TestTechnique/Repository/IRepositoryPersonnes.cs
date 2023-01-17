using Microsoft.EntityFrameworkCore;
using TestTechnique.Entities;

namespace TestTechnique.Repository
{
    public interface IRepositoryPersonnes
    {
        public void SauvegarderPersonne(Personne personne);
        public void UpdatePersonne(Personne personne, Emploi emploi/*, DateTime debut, DateTime fin*/);
        public List<Personne> GetAllPersonne();
        public List<Personne> GetPersonesPerEntreprises(string entreprise);
        public List<Emploi> GetEmploisPerPersonne(Personne personne, DateTime dateDebut, DateTime dateFin);
    }
}
