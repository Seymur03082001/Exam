using Microsoft.AspNetCore.Mvc;
using TheTown.DAL;

namespace TheTown.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HomeController : Controller
    {
        public readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
