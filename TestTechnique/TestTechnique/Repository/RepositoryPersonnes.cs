using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
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

        public List<PersonneEmploiDto> GetAllPersonne()
        {
            var personnes = dataContext.Personnes.Include(p => p.PersonnesEmplois).OrderBy(p => p.Nom).ThenBy(p => p.Prenom).ToList();
            var personnesDto = new List<PersonneEmploiDto>();
            foreach (var personne in personnes)
            {
                personne.PersonnesEmplois = personne.PersonnesEmplois.Where(p => p.dateFin == null || p.dateFin == new DateTime() || p.dateFin >= DateTime.Now).ToList();
                var personneDto = GetPersonneEmploi(personne);
                personneDto.Age = personne.GetAge();
                personnesDto.Add(personneDto);

            }
            return personnesDto;
        }

        public PersonneEmploiDto GetEmploisPerPersonne(int personneID, DateTime dateDebut, DateTime dateFin)
        {
            var personne = dataContext.Personnes.Include(p => p.PersonnesEmplois.Where(p => p.dateDebut >= dateDebut && p.dateFin <= dateFin)).FirstOrDefault(p => p.PersonneId == personneID);
            PersonneEmploiDto personneDto = GetPersonneEmploi(personne); //new PersonneEmploiDto();
            return personneDto;
        }

        private PersonneEmploiDto GetPersonneEmploi(Personne? personne)
        {
            var personneDto = new PersonneEmploiDto();
            personneDto.Personne = new PersonneDto()
            {
                DateNaissance = personne.DateNaissance,
                Nom = personne.Nom,
                Prenom = personne.Prenom,
                PersonneID = personne.PersonneId
            };
            personneDto.Emplois = personne.PersonnesEmplois.Join(dataContext.Emplois,
                p => p.EmploiID,
                e => e.EmploiID,
                (p, e) => new EmploiDto()
                {
                    EmploiID = p.EmploiID,
                    Poste = e.Poste,
                    Entreprise = e.Entreprise,
                    dateDebut = p.dateDebut,
                    dateFin = p.dateFin
                }).ToList();
            return personneDto;
        }
        public List<Emploi> GetPersonesPerEntreprises(string entreprise)
        {
            var emplois = dataContext.Emplois.Include(p => p.PersonnesEmplois).ToList();
            foreach(var emploi in emplois)
            {
                emploi.PersonnesEmplois = emploi.PersonnesEmplois
                    .Join(dataContext.Personnes,
                          p => p.PersonneId,
                          e => e.PersonneId,
                          (p, e) => new PersonneEmploi()
                          {
                              dateDebut = p.dateDebut,
                              dateFin = p.dateFin,

                              personne = new Personne()
                              {
                                  Nom = e.Nom,
                                  Prenom = e.Prenom,
                                  DateNaissance = e.DateNaissance,
                                  PersonneId = e.PersonneId
                              }
                          }
                          ).ToList();
            }


            return emplois.ToList();
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
                dateDebut = debut,
                dateFin = fin,
                EmploiID = emploi.EmploiID,
                PersonneId = personne.PersonneId
            };
            dataContext.PersonneEmploi.Add(personneEmploi);

            dataContext.SaveChanges();
        }
    }
}
