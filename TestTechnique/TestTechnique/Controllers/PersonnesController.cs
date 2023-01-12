using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTechnique.Context;
using TestTechnique.Entities;
using TestTechnique.Repository;

namespace TestTechnique.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonnesController : Controller
    {
        public DataContext context { get; set; }
        private readonly ILogger _logger;
        private IRepositoryPersonnes repositoryPersonnes;

        public PersonnesController(DataContext contexte, IRepositoryPersonnes repositoryPersonnes)
        {
            this.context = context;
            if(context.Personnes.Count() == 0)
            {
                var p1 = new Personne()
                {
                    DateNaissance = new DateTime(1998, 2, 1),
                    Nom = "ATRIUM",
                    Prenom = "Web"
                };
                context.Personnes.Add(p1);

                var emploi1 = new Emploi()
                {
                    Entreprise = "Web atrio",
                    Poste = "Developpeur",
                    DateDebut = new DateTime(2018, 5, 2),
                    DateFin = new DateTime(2020, 3, 4)
                };
                //context.Emplois.Add(emploi1);
                
                var emploi2 = new Emploi()
                {
                    Entreprise = "EBP",
                    Poste = "Scrum Master",
                    DateDebut = new DateTime(2020, 7, 31),
                };
                //context.Emplois.Add(emploi2);
                
                

                
                var p2 = new Personne()
                {
                    DateNaissance = new DateTime(1985, 12, 21),
                    Nom = "WEBI",
                    Prenom = "Atrio"
                };
                p2.Emplois.Add(emploi1);
                p2.Emplois.Add(emploi2);
                context.Personnes.Add(p2);

                var emploi3 = new Emploi()
                {
                    Entreprise = "EBP",
                    Poste = "Scrum Master",
                    DateDebut = new DateTime(2022, 11, 2),
                    DateFin = new DateTime(2022, 12, 31)
                };
                context.Emplois.Add(emploi3);

                context.SaveChanges();
            }
        }

        [HttpPost]
        public async Task SavePersonne(Personne personne)
        {
            try
            {
                repositoryPersonnes.SauvegarderPersonne(personne, context.Personnes.ToList());
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Erreur de l'ajout de la personne {personne.Nom}");
            }
        }

        [HttpPut("UpdatePersonne/{personneID}/{emploiID}")]
        public async Task UpdatePersonne(int personneID, int emploiID)
        {
            try
            {
                var emploi = context.Emplois.Where(p => p.EmploiId == emploiID).FirstOrDefault();
                if (emploi != null)
                {
                    if (emploi.DateDebut != null && emploi.DateDebut != new DateTime())
                    {
                        var personne = context.Personnes.Where(p => p.PersonneID == personneID).FirstOrDefault();
                        repositoryPersonnes.UpdatePersonne(personne, emploi);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("La date de début de l'emploi n'est pas correcte");
                    }
                }
            }
            catch(Exception ex)
            {
                 _logger.LogError($"Erreur lors de la  MAJ de la personne ayant pour id {personneID}");
            }
        }

        [HttpGet("GetAllPersonnes")]
        public async Task<IEnumerable<Personne>> GetPersonnesEnregistrees()
        {
            var personnes = new List<Personne>();
            try
            {
                personnes = repositoryPersonnes.GetAllPersonne(context.Personnes.Include(p => p.Emplois).ToList());
            }
            catch(Exception ex)
            {
                _logger.LogError("Erreur dans la récupération des personnes");
            }

            return personnes;

        }

        [HttpGet("GetPersonnesEntreprise")]
        public async Task<IEnumerable<Personne>> GetPersonnesEntreprises(string entreprise)
        {
            var personnes = new List<Personne>();

            try
            {
                personnes = repositoryPersonnes.GetPersonesPerEntreprises(entreprise, context.Personnes.Include(p => p.Emplois).ToList());
            }
            catch(Exception ex)
            {
                _logger.LogError("Erreur lors de la récupération des salariés de l'entreprise");
            }

            return personnes;
        }

        [HttpGet("EmploisPersonne")]
        public async Task<IEnumerable<Emploi>> GetEmploisPersonnes(int personneID, DateTime dateDebut, DateTime dateFin)
        {
            var emplois = new List<Emploi>();

            try
            {
                var personne = context.Personnes.Include(p => p.Emplois).Where(p => p.PersonneID == personneID).FirstOrDefault();
                if (personne != null)
                {
                    emplois = repositoryPersonnes.GetEmploisPerPersonne(personne, dateDebut, dateFin);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Erreur lors de la récupération des emplois d'une personne");
            }

            return emplois;

        }
    }
}
