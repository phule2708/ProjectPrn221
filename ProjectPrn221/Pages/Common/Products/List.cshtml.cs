using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        [BindProperty]
        int cateId { get; set; }
        public async Task<IActionResult> OnGet(int cateId)
        {
            string role = HttpContext.Session.GetString("account");
            ViewData["role"] = role;
            ViewData["SelectedId"] = cateId;
            if (cateId == 0)
            {
                Products = _db.Products.Include(c => c.Category).ToList();
            }
            else
            {
                Products =  _db.Products.Include(c=>c.Category).Where(c=>c.CategoryId == cateId).ToList();
            }
            Categories = _db.Categories.ToList();
            return Page();
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
            ViewData["SelectedId"] = cateId;
            Categories = _db.Categories.ToList();
            TempData["msg"] = "Delete success.";
            return RedirectToPage("./List");
        }
        public void OnGetAddCart(int catId, int id)
        {
            Categories = _db.Categories.ToList();
            List<int> numbers = new List<int>();
            if (HttpContext.Session.GetString("listCart") != null)
            {
                string numbersJson = HttpContext.Session.GetString("listCart");
                numbers = JsonConvert.DeserializeObject<List<int>>(numbersJson);
            }
            numbers.Add(id);
            string numbersJson1 = JsonConvert.SerializeObject(numbers);
            HttpContext.Session.SetString("listCart", numbersJson1);
            ViewData["SelectedId"] = cateId;
            ViewData["productId"] = id;
            OnGet(catId);
            
        }
    }
}
