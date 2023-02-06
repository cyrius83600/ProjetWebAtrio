using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestTechnique.Context;
using TestTechnique.Entities;
using TestTechnique.Repository;

namespace TestTechnique.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonnesController : Controller
    {
        private IRepositoryPersonnes repositoryPersonnes;
        
        public PersonnesController(IRepositoryPersonnes repositoryPersonnes)
        {
            this.repositoryPersonnes = repositoryPersonnes;            

            //if (context.Personnes.Count() == 0)
            //{
            //    var p1 = new Personne()
            //    {
            //        DateNaissance = new DateTime(1998, 2, 1),
            //        Nom = "ATRIUM",
            //        Prenom = "Web"
            //    };
            //    context.Personnes.Add(p1);

            //    var emploi1 = new Emploi()
            //    {
            //        Entreprise = "Web atrio",
            //        Poste = "Developpeur"
            //    };
            //    context.Emplois.Add(emploi1);

            //    var emploi2 = new Emploi()
            //    {
            //        Entreprise = "EBP",
            //        Poste = "Scrum Master"
            //    };
            //    context.Emplois.Add(emploi2);




            //    var p2 = new Personne()
            //    {
            //        DateNaissance = new DateTime(1985, 12, 21),
            //        Nom = "WEBI",
            //        Prenom = "Atrio"
            //    };
            //    context.Personnes.Add(p2);

            //    var emploi3 = new Emploi()
            //    {
            //        Entreprise = "EBP",
            //        Poste = "Scrum Master"
            //    };
            //    context.Emplois.Add(emploi3);

            //    var jointure1 = new PersonneEmploi()
            //    {
            //        EmploiID = 1,
            //        dateDebut = DateTime.Now,
            //        dateFin = DateTime.Now
            //    };

            //    context.SaveChanges();

            //    var pjoint = context.Personnes.Where(p => p.PersonneId == 1).First();
            //    pjoint.personneEmplois.Add(jointure1);

            //    context.SaveChanges();

            //    //context.SaveChanges();
            //}
        }

        [HttpPost]
        public async Task SavePersonne(Personne personne)
        {
            repositoryPersonnes.SauvegarderPersonne(personne);
        }

        [HttpPut]
        public async Task UpdatePersonne(PostEmploiDto personneEmploi)
        {

            repositoryPersonnes.UpdatePersonne(personneEmploi.PersonneID, personneEmploi.EmploiID, personneEmploi.dateDebut, personneEmploi.dateFin);
        }

        [HttpGet("GetAllPersonnes")]
        public IEnumerable<PersonneEmploiDto> GetPersonnesEnregistrees()
        {
            return repositoryPersonnes.GetAllPersonne();
        }

        [HttpGet("GetPersonnesEntreprise/{entreprise}")]
        public async Task<List<Emploi>> GetPersonnesEntreprises(string entreprise)
        {
            return repositoryPersonnes.GetPersonesPerEntreprises(entreprise);
        }

        [HttpGet("EmploisPersonne")]
        public PersonneEmploiDto GetEmploisPersonnes(int personneID, DateTime dateDebut, DateTime dateFin)
        {
            return repositoryPersonnes.GetEmploisPerPersonne(personneID, dateDebut, dateFin);
        }
    }
}
