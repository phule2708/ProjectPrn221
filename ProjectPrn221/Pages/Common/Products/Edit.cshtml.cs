using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectPrn221.Models;

namespace ProjectPrn221.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        private readonly PRN221DBContext _db;
        public EditModel(PRN221DBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Product Product { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            ViewData["Category"] = new SelectList(_db.Categories, "CategoryId", "CategoryName");
            var product = await _db.Products.FindAsync(id);
            if (product != null)
                Product = product;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Category"] = new SelectList(_db.Categories, "CategoryId", "CategoryName");
                return Page();
            }
            if (_db.Products.Find(Product.ProductId) == null)
            {
                HttpContext.Session.SetString("msg", "Không tồn tại Product");

                return RedirectToPage("/Common/Products/List");
            }
            _db.Attach(Product).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            TempData["msg"] = "Update success.";

            return RedirectToPage("./List");
        }
    }

}
