using Microsoft.AspNetCore.Mvc;
using ProiectIP.Data;
using ProiectIP.Data.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectIP.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;
        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAll();
            return View(data);
        }
    }
}
