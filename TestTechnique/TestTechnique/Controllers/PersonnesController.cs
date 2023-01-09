using Microsoft.AspNetCore.Mvc;
using TestTechnique.Context;

namespace TestTechnique.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonnesController : Controller
    {
        public DataContext context { get; set; }

        public PersonnesController(DataContext context)
        {
            this.context = context;
        }


    }
}
