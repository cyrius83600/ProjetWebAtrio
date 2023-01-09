using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTechnique.Context;
using TestTechnique.Entities;

namespace TestTechnique.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonnesController : Controller
    {
        public DataContext context { get; set; }
        private readonly ILogger _logger;

        public PersonnesController(DataContext context)
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
            else
            {
                var tt = context.Personnes.ToList();
            }
        }

        [HttpPost]
        public async Task SavePersonne(Personne personne)
        {
            try
            {
                //Valide en fonction de l'age (150 ans)
                if (personne.IsValide())
                {
                    context.Personnes.Add(personne);
                }
                else
                {
                    throw new Exception("La personne a plus de 150 ans");
                }
            }
            catch(Exception ex)
            {
                if (ex.Message == "La personne a plus de 150 ans")
                    _logger.LogError("La personne a plus de 150 ans");
                else
                    _logger.LogError($"Erreur de l'ajout de la personne {personne.Nom}");
            }
        }

        [HttpPut("{personneID:int}")]
        public async Task UpdatePersonne(int personneID, Emploi emploi)
        {
            try
            {
                if (!(emploi.DateDebut == null || emploi.DateDebut == new DateTime()))
                {
                    var personneBase = context.Personnes.Where(p => p.PersonneID == personneID).FirstOrDefault();
                    if (personneBase != null)
                    {
                        personneBase.Emplois.Add(emploi);
                    }

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("La date de début de l'emploi n'est pas correcte");
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
            var personnes = context.Personnes;

            try
            {
                foreach (var personne in personnes)
                {
                    personne.Age = personne.GetAge();
                    //Emplois actuels si l'emploi commence avant maintenant et si la date de fin n'est pas précisée
                    //ou si elle est postérieur à maintenant
                    //personne.Emplois = personne.Emplois.Where(p => p.DateDebut < DateTime.Now && (p.DateFin > DateTime.Now || p.DateFin == null || p.DateFin == DateTime.Now)).ToList();
                }

                return personnes.Include(p => p.Emplois).OrderBy(p => p.Nom).ThenBy(p => p.Prenom);
            }
            catch(Exception ex)
            {
                _logger.LogError("Erreur dans la récupération des personnes");
                return personnes;
            }

        }

        [HttpGet("GetPersonnesEntreprise")]
        public async Task<IEnumerable<Personne>> GetPersonnesEntreprises(string entreprise)
        {
            var personnes = new List<Personne>();

            try
            {
                foreach (var personne in context.Personnes)
                {
                    foreach (var emploi in personne.Emplois)
                    {
                        if (emploi.Poste == entreprise)
                        {
                            personnes.Add(personne);
                        }
                    }
                }
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
                var personne = context.Personnes.Where(p => p.PersonneID == personneID).FirstOrDefault();
                if (personne != null)
                {
                    foreach (var emploi in personne.Emplois)
                    {
                        if (emploi.DateDebut >= dateDebut && emploi.DateFin <= dateFin)
                        {
                            emplois.Add(emploi);
                        }
                    }
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
