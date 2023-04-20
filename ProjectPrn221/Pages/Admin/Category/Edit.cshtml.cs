using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectPrn221.Models;

namespace ProjectPrn221.Pages.Admin.Category
{
    public class EditModel : PageModel
    {
        private readonly PRN221DBContext _db;
        public EditModel(PRN221DBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Models.Category Category { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            
            var cate = await _db.Categories.FindAsync(id);
            if (cate != null)
                Category = cate;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _db.Attach(Category).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            TempData["msg"] = "Update success.";

            return RedirectToPage("./List");
        }
    }
}
}
