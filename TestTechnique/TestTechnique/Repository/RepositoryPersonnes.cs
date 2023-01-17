using Microsoft.EntityFrameworkCore;
using TestTechnique.Context;
using TestTechnique.Entities;

namespace TestTechnique.Repository
{
    public class RepositoryPersonnes : IRepositoryPersonnes
    {
        DataContext dataContext = null;

        public RepositoryPersonnes(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public List<Personne> GetAllPersonne()
        {
            var personnes = dataContext.Personnes.Include(p => p.personneEmplois).ToList();

            foreach (var personne in personnes)
            {
                personne.Age = personne.GetAge();
                personne.personneEmplois = personne.personneEmplois.Where(p => p.dateFin > DateTime.Now).ToList();
            }

            return personnes.OrderBy(p => p.Nom).ThenBy(p => p.Prenom).ToList();
        }

        public List<Emploi> GetEmploisPerPersonne(Personne personne, DateTime dateDebut, DateTime dateFin)
        {
            return personne.personneEmplois.Where(p => p.dateDebut >= dateDebut && p.dateFin <= dateFin).Select(p => p.Emploi).ToList();
        }

        public List<Personne> GetPersonesPerEntreprises(string entreprise)
        {
            var personnesResultat = new List<Personne>();
            var emplois = dataContext.Emplois.Where(p => p.Entreprise == entreprise).ToList();
            var personnesEmplois = dataContext.PersonneEmploi.ToList();
            var personnes = dataContext.Personnes.ToList();

            var pesonnesResultat = (from emploi in emplois
                        join personneEmploi in personnesEmplois 
                        on emploi.EmploiID equals personneEmploi.EmploiID
                        join personne in personnes
                        on personneEmploi.PersonneID equals personne.PersonneId
                        select personne).ToList();

            return personnesResultat;
        }

        public void SauvegarderPersonne(Personne personne)
        {
            
            //Valide en fonction de l'age (150 ans)
            if (personne.IsValide())
            {
                dataContext.Personnes.Add(personne);
                dataContext.SaveChanges();
            }
            else
            {
                throw new Exception("La personne a plus de 150 ans");
            }
        }

        public void UpdatePersonne(Personne personne, Emploi emploi/*, DateTime debut, DateTime fin*/)
        {
            var personneContext = dataContext.Personnes.Where(p => p.Nom == personne.Nom && p.Prenom == personne.Prenom && p.DateNaissance == personne.DateNaissance).FirstOrDefault();
            var emploiContext = dataContext.Emplois.Where(p => p.Entreprise == emploi.Entreprise).FirstOrDefault();

            if(personneContext == null)
            {
                dataContext.Personnes.Add(personneContext);
            }
            if(emploiContext == null)
            {
                dataContext.Emplois.Add(emploiContext);
            }

            var personneEmploi = new PersonneEmploi()
            {
                Emploi = emploiContext
                //dateDebut = debut,
                //dateFin = fin
            };
            personneContext.personneEmplois.Add(personneEmploi);

            dataContext.SaveChanges();
        }
    }
}
