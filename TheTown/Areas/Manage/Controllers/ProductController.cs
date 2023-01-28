using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheTown.DAL;
using TheTown.Models;
using TheTown.ViewModels.Product;

namespace TheTown.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        public readonly AppDbContext _context;
        [HttpGet]
        public async Task<IActionResult> Index() => View();

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Positions=new SelectList(_context.Positions,nameof(Position.Id),nameof(Position.Name));
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {
            if (!ModelState.IsValid) return NotFound();
            if (productVM is null) return View();
            Product product = new Product();
            product.Title = productVM.Title;
            product.Icon= productVM.Icon;
            product.About= productVM.About;

            if(!ModelState.IsValid) await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),"Product");

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id is null || id==0) return BadRequest();
            var product = await _context.Products.FindAsync(id);

            if(product is null) return NotFound();

            if (!ModelState.IsValid) _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Product");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if(id is null || id == 0) return BadRequest();
            if (!ModelState.IsValid) return View();
            var product = await _context.Products.FindAsync(id);
            if(product is null) return NotFound();
            return RedirectToAction(nameof(Index),"Product");
        }
        [HttpPost]
        public async Task<IActionResult> Update(Product product,int? id)
        {
            if(id is null || id == 0) return BadRequest();
            if(!ModelState.IsValid) return View();

            var newProduct = await _context.Products.FindAsync(id);

            newProduct.Title=product.Title;
            newProduct.About=product.About;
            newProduct.Icon=product.Icon;

            _context.Products.Update(newProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index),"Product");
        }
    }
}
