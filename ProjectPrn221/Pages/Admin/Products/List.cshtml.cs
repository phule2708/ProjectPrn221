using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPrn221.Models;

namespace ProjectPrn221.Pages.Admin.Products
{
    public class ListModel : PageModel
    {
        private readonly PRN221DBContext _db;
        public ListModel(PRN221DBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public List<Product> Products { get; set; }

        [BindProperty]

        public List<Category> Categories { get; set; }
        public async Task<IActionResult> OnGetAsync(int cateId)
        {
            string role = HttpContext.Session.GetString("account");
            ViewData["role"] = role;
            ViewData["SelectedId"] = cateId;
            if (cateId == 0)
            {
                Products =await _db.Products.Include(c => c.Category).ToListAsync();
            }
            else
            {
                Products = await _db.Products.Include(c=>c.Category).Where(c=>c.CategoryId == cateId).ToListAsync();
            }
            Categories = _db.Categories.ToList();
            return Page();
        }
        public void MyFunction()
        {
            Console.WriteLine();
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            var count = _db.OrderDetails.Where(od => od.ProductId == id).Count();
            if (count > 0)
            {
                TempData["msg"] = "This product existed in Order details.";
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
