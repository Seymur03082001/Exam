using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheTown.DAL;
using TheTown.Models;

namespace TheTown.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PositionController : Controller
    {
        public readonly AppDbContext _context;
        public PositionController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var position = _context.Positions.ToList();
            return View(position);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Position position)
        {
            if(position==null) return NotFound();
            _context.Positions.Add(position);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            Position position=_context.Positions.Find(id);
            if(position==null) return NotFound();
            _context.Positions.Remove(position);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id) 
        { 
            if(id==null || id==0) return BadRequest();
            Position newposition = _context.Positions.Find(id);
            if(newposition is null) return NotFound();
            return View(newposition);
        }
        [HttpPost]
        public IActionResult Update(int? id, Position position)
        {
            if (id == null) return BadRequest();
            var position1 = _context.Positions.Find(id);
            if (position1 == null) return NotFound();
            position.Name= position1.Name;
            if (!ModelState.IsValid) return View();
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
