using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectIP.Data;
using System.Threading.Tasks;

namespace ProiectIP.Controllers
{
    public class RoomsController : Controller
    {
        private readonly AppDbContext _context;

        public RoomsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allRooms = await _context.Rooms.ToListAsync();

            return View(allRooms);
        }
    }
}
