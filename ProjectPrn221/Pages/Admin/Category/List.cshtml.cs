using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPrn221.Models;

namespace ProjectPrn221.Pages.Admin.Category
{
    public class ListModel : PageModel
    {
        private readonly PRN221DBContext _db;
        public ListModel(PRN221DBContext db)
        {
            _db = db;

        }
        [BindProperty]
        public List<Models.Category> ListCategory { get; set; }
    
        public async Task<IActionResult> OnGetAsync()
        {
            ListCategory = await _db.Categories.ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnGetDelete(int id)
        {
            var count = _db.Products.Where(od => od.CategoryId == id).Count();
            if (count > 0)
            {
                TempData["msg"] = "This Category existed in Product.";
                return RedirectToPage("./List");
            }
            var product = await _db.Products.FindAsync(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
            TempData["msg"] = "Delete success.";
            return RedirectToPage("./List");
        }
    }
}
