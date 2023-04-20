using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectPrn221.Models;

namespace ProjectPrn221.Pages.Admin.Category
{
    public class CreateModel : PageModel
    {
        private readonly PRN221DBContext _db;
        public CreateModel(PRN221DBContext db)
        {
            _db = db;

        }

        [BindProperty]
        public Models.Category Category { get; set; }
        public async Task<IActionResult> OnGet()
        {
            
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            
            if (!ModelState.IsValid)
                return Page();
            await _db.Categories.AddAsync(Category);
            await _db.SaveChangesAsync();
            return RedirectToPage("List");
        }
    }
}
