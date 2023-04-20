using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectPrn221.Models;

namespace ProjectPrn221.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly PRN221DBContext _db;
        public CreateModel(PRN221DBContext db)
        {
            _db = db;

        }

        [BindProperty]
        public Product Product { get; set; }
        public async Task<IActionResult> OnGet()
        {
            ViewData["Category"] = new SelectList(_db.Categories, "CategoryId", "CategoryName");
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            ViewData["Category"] = new SelectList(_db.Categories, "CategoryId", "CategoryName");
            if (!ModelState.IsValid)
                return Page();
            await _db.Products.AddAsync(Product);
            await _db.SaveChangesAsync();
            return RedirectToPage("List");
        }
    }
}
