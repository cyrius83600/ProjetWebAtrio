using TestTechnique.Entities;

namespace TestTechnique.Repository
{
    public class RepositoryPersonnes : IRepositoryPersonnes
    {
        public List<Personne> GetAllPersonne(List<Personne> personnes)
        {

            foreach (var personne in personnes)
            {
                personne.Age = personne.GetAge();
                personne.Emplois = personne.Emplois.Where(p => p.DateFin > DateTime.Now || p.DateFin == new DateTime()).ToList();
            }

            return personnes.OrderBy(p => p.Nom).ThenBy(p => p.Prenom).ToList();
        }

        public List<Emploi> GetEmploisPerPersonne(Personne personne, DateTime dateDebut, DateTime dateFin)
        {
            var emplois = new List<Emploi>();
            foreach (var emploi in personne.Emplois)
            {
                if (emploi.DateDebut >= dateDebut && emploi.DateFin <= dateFin)
                {
                    emplois.Add(emploi);
                }
            }
            return emplois;
        }

        public List<Personne> GetPersonesPerEntreprises(string entreprise, List<Personne> personnes)
        {
            var personnesEntreprise = new List<Personne>();

            foreach (var personne in personnes)
            {
                foreach (var emploi in personne.Emplois)
                {
                    if (emploi.Entreprise == entreprise)
                    {
                        personnesEntreprise.Add(personne);
                    }
                }
            }

            return personnesEntreprise;
        }

        public void SauvegarderPersonne(Personne personne, List<Personne> personnes)
        {
            //Valide en fonction de l'age (150 ans)
            if (personne.IsValide())
            {
                personnes.ToList().Add(personne);
                
            }
            else
            {
                throw new Exception("La personne a plus de 150 ans");
            }
        }

        public void UpdatePersonne(Personne personne, Emploi emploi)
        {
            if (emploi.DateDebut != null && emploi.DateDebut != new DateTime())
            {
                    personne.Emplois.Add(emploi);
            }
            else
            {
                throw new Exception("La date de début de l'emploi n'est pas correcte");
            }
        }
    }
}
