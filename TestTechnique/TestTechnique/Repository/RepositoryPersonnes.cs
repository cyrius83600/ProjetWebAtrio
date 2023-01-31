using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

        public List<PersonneDto> GetAllPersonne()
        {
            return dataContext.Personnes.Include(p => p.PersonnesEmplois).OrderBy(p => p.Nom).ThenBy(p => p.Prenom).Select(p => new PersonneDto()
            {
               Age = p.GetAge(),
               Nom = p.Nom,
               DateNaissance = p.DateNaissance,
               Prenom = p.Prenom,
               Emplois = p.PersonnesEmplois.Select(p => new EmploiDto()
               {
                   dateDebut = p.dateDebut,
                   dateFin = p.dateFin,
                   Entreprise = p.Entreprise,
                   Poste = p.Poste,
                   ID = p.EmploiID
                   
               }).ToList()
            }).ToList();

            //foreach(var personne in personnes)
            //{
            //    personne.Age = personne.GetAge();
            //    personne.PersonnesEmplois = personne.PersonnesEmplois.Where(p => p.dateFin == null || p.dateFin >= DateTime.Now).ToList();
            //}

            //var test = dataContext.Personnes.Include(p => p.PersonnesEmplois).Where(p => p.PersonneId == 1).ToList();

            //return new List<Personne>();
        }

        public PersonneDto GetEmploisPerPersonne(int personneID, DateTime dateDebut, DateTime dateFin)
        {
            var personne = dataContext.Personnes.Include(p => p.PersonnesEmplois).Where(p => p.PersonneId == personneID).FirstOrDefault();
            return new PersonneDto()
            {
                Age = personne.GetAge(),
                Nom = personne.Nom,
                DateNaissance = personne.DateNaissance,
                Prenom = personne.Prenom,
                Emplois = personne.PersonnesEmplois.Where(p => p.dateDebut >= dateDebut && p.dateFin <= dateFin).Select(u => new EmploiDto()
                {
                    dateDebut = u.dateDebut,
                    dateFin = u.dateFin,
                    Entreprise = u.Entreprise,
                    Poste = u.Poste,
                    ID = u.EmploiID

                }).ToList()
            };
        }



        public List<IEnumerable<PersonneEmploiDto>> GetPersonesPerEntreprises(string entreprise)
        {
            //var personnes = new List<Personne>();


            return dataContext.Emplois.Include(p => p.Personnes).Select(p => p.Personnes.Select(u => new PersonneEmploiDto()
            {
                Personne = new PersonneDto()
                {
                    Prenom = u.Prenom,
                    Nom = u.Nom,
                    DateNaissance = u.DateNaissance,
                },
                Emplois = new EmploiDto()
                {
                    Poste = u.Poste,
                    Entreprise = u.Entreprise,
                    dateDebut = u.dateDebut,
                    dateFin = u.dateFin,
                    ID = u.EmploiID
                }
            })).ToList();

            //foreach (var personne in emplois)
            //{
            //    personnes.Add(new Personne()
            //    {
            //        DateNaissance = personne.DateNaissance,
            //        Nom = personne.Nom,
            //        Prenom = personne.Prenom
            //    });
            //}

            //return personnes;

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

        public void UpdatePersonne(int personneID, int emploiID, DateTime? debut, DateTime? fin)
        {
            var personne = dataContext.Personnes.Where(p => p.PersonneId == personneID).FirstOrDefault();
            var emploi = dataContext.Emplois.Where(p => p.EmploiID == emploiID).FirstOrDefault();

            var personneEmploi = new PersonneEmploi()
            {
                Nom = personne.Nom,
                Prenom = personne.Prenom,
                DateNaissance = personne.DateNaissance,
                Entreprise = emploi.Entreprise,
                Poste = emploi.Poste,
                dateDebut = debut,
                dateFin = fin,
                EmploiID = emploiID,
                PersonneId = personneID
            };
            dataContext.PersonneEmploi.Add(personneEmploi);

            dataContext.SaveChanges();
        }
    }
}
