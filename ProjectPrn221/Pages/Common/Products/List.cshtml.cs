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
        public async Task<IActionResult> OnGet(int cateId)
        {
            if (HttpContext.Session.GetString("account") == null)
            {
                return RedirectToPage("/SignIn");
            }
            else
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
                    Products = _db.Products.Include(c => c.Category).Where(c => c.CategoryId == cateId).ToList();
                }
                Categories = _db.Categories.ToList();
                return Page();
            }

        }
        public async Task<IActionResult> OnGetDelete(int cateId, int id)
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
            ViewData["SelectedId"] = catId;
            ViewData["productId"] = "Add product with id " + id + " to cart successfully";
            OnGet(catId);

        }
        public void OnPostBuy(int catId)
        {
            int id = int.Parse(Request.Form["buyProduct"]);
            decimal price = decimal.Parse(Request.Form["buyProduct"]);
            string name = Request.Form["productName"];
            addOrder();
            addOrderDetail(id, price);
            ViewData["productId"] = "buy product " + name + "to successfully";
            OnGet(catId);

        }
        public void addOrderDetail(int productid, decimal price)
        {
            var db = new PRN221DBContext();
            var firstEntity = db.Orders.OrderByDescending(e => e.OrderId).FirstOrDefault();
            int id = firstEntity.OrderId;
            OrderDetail detail = new OrderDetail()
            {
                OrderId = id,
                ProductId = productid,
                UnitPrice = price,
                Quantity = 1,
                Discount = 1,
            };
            db.OrderDetails.Add(detail);
            db.SaveChanges();
        }
        public void addOrder()
        {
            var db = new PRN221DBContext();
            string id = HttpContext.Session.GetString("id");
            Order o = new Order()
            {
                CustomerId = id,
                EmployeeId = null,
                OrderDate = DateTime.Now,
                RequiredDate = null,
                ShippedDate = null,
                Freight = 0,
                ShipName = null,
                ShipAddress = null,
                ShipCity = null,
                ShipRegion = null,
                ShipPostalCode = null,
                ShipCountry = null,
            };
            db.Orders.Add(o);
            db.SaveChanges();
        }
    }
}
