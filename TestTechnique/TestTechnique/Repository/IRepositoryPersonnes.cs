using TestTechnique.Entities;

namespace TestTechnique.Repository
{
    public interface IRepositoryPersonnes
    {
        public void SauvegarderPersonne(Personne personne, List<Personne> personnes);
        public void UpdatePersonne(Personne personne, Emploi emploi);
        public List<Personne> GetAllPersonne(List<Personne> personnes);
        public List<Personne> GetPersonesPerEntreprises(string entreprise, List<Personne> personnes);
        public List<Emploi> GetEmploisPerPersonne(Personne personne, DateTime dateDebut, DateTime dateFin);
    }
}
