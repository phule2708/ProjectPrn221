using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPrn221.Models;

namespace ProjectPrn221.Pages.Admin.Orders
{
    public class ListModel : PageModel
    {
        private readonly PRN221DBContext _dbContext;

        public ListModel(PRN221DBContext db)
        {
            _dbContext = db;
        }
        [BindProperty]
        public List<Customer> Customer { get; set; }
        [BindProperty]
        public List<Order> Order { get; set; }
        [BindProperty]

        public Order Orders { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)

        {
            
                ViewData["id"] = id;
                if (id == null)
                {
                    Order = _dbContext.Orders.ToList();
                }
                else
                {
                    Order = _dbContext.Orders.Where(d => d.CustomerId == id).ToList();
                }
                Customer = _dbContext.Customers.ToList();
            
            return Page();
        }

        public async Task<IActionResult> OnPost(string id, string start, string end)
        {
            ViewData["id"] = id;
            ViewData["start"] = start; ViewData["end"] = end;
            if (id == null)
            {
                if (start == null && end == null)
                {
                    Order = _dbContext.Orders.ToList();
                }
                else if (start != null && end == null)
                {

                    Order = _dbContext.Orders.Where(d => d.OrderDate >= DateTime.Parse(start) && d.OrderDate <= DateTime.Now).ToList();
                }
                else if (end != null && start == null)
                {
                    Order = _dbContext.Orders.Where(d => d.OrderDate <= DateTime.Parse(end)).ToList();
                }
                else
                {
                    Order = _dbContext.Orders.Where(d => d.OrderDate >= DateTime.Parse(start) && d.OrderDate <= DateTime.Parse(end)).ToList();
                }
            }
            else
            {
                if (start == null && end == null)
                {
                    Order = _dbContext.Orders.Where(d => d.CustomerId == id).ToList();
                }
                else if (start != null && end == null)
                {

                    Order = _dbContext.Orders.Where(d => d.CustomerId == id).Where(d => d.OrderDate >= DateTime.Parse(start) && d.OrderDate <= DateTime.Now).ToList();
                }
                else if (end != null && start == null)
                {
                    Order = _dbContext.Orders.Where(d => d.CustomerId == id).Where(d => d.OrderDate <= DateTime.Parse(end)).ToList();
                }
                else
                {
                    Order = _dbContext.Orders.Where(d => d.CustomerId == id).Where(d => d.OrderDate >= DateTime.Parse(start) && d.OrderDate <= DateTime.Parse(end)).ToList();
                }
            }

            Customer = _dbContext.Customers.ToList();
            return Page();
        }
    }
}
