using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        public List<Customer> Customers { get; set; }
        [BindProperty]
        public List<Order> Order { get; set; }
        [BindProperty]

        public Order Orders { get; set; }

        public decimal totalFreight = 0;
        public async Task<IActionResult> OnGetAsync(string id)
        {
            Customers = new List<Customer>();
            if (HttpContext.Session.GetString("account") == null)
            {
                return RedirectToPage("/SignIn");
            }
            else
            {
                int empId = int.Parse(HttpContext.Session.GetString("id"));


                ViewData["id"] = id;
                if (id == null)
                {
                    Order = _dbContext.Orders.Where(c=>c.EmployeeId==empId).ToList();
                }
                else
                {
                    Order = _dbContext.Orders.Where(d => d.CustomerId == id && d.EmployeeId==empId).ToList();
                }
                foreach(var item in Order)
                {
                    totalFreight += item.Freight.Value;
                }
                ViewData["totalFreight"] = totalFreight;
                List<String> customerid = _dbContext.Orders.Where(c => c.EmployeeId == empId).Select(c => c.CustomerId).Distinct().ToList();
                foreach(string item in customerid)
                {
                    Customers.Add((Customer)_dbContext.Customers.Find(item));
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPost(string id, string start, string end)
        {
            ViewData["id"] = id;
            int empId = int.Parse(HttpContext.Session.GetString("id"));

            ViewData["start"] = start; ViewData["end"] = end;
            if (id == null)
            {
                if (start == null && end == null)
                {
                    Order = _dbContext.Orders.Where(c => c.EmployeeId == empId).ToList();
                }
                else if (start != null && end == null)
                {

                    Order = _dbContext.Orders.Where(c => c.EmployeeId == empId).Where(d => d.OrderDate >= DateTime.Parse(start) && d.OrderDate <= DateTime.Now).ToList();
                }
                else if (end != null && start == null)
                {
                    Order = _dbContext.Orders.Where(c => c.EmployeeId == empId).Where(d => d.OrderDate <= DateTime.Parse(end)).ToList();
                }
                else
                {
                    Order = _dbContext.Orders.Where(c => c.EmployeeId == empId).Where(d => d.OrderDate >= DateTime.Parse(start) && d.OrderDate <= DateTime.Parse(end)).ToList();
                }
            }
            else
            {
                if (start == null && end == null)
                {
                    Order = _dbContext.Orders.Where(c => c.EmployeeId == empId).Where(d => d.CustomerId == id).ToList();
                }
                else if (start != null && end == null)
                {

                    Order = _dbContext.Orders.Where(c => c.EmployeeId == empId).Where(d => d.CustomerId == id).Where(d => d.OrderDate >= DateTime.Parse(start) && d.OrderDate <= DateTime.Now).ToList();
                }
                else if (end != null && start == null)
                {
                    Order = _dbContext.Orders.Where(c => c.EmployeeId == empId).Where(d => d.CustomerId == id).Where(d => d.OrderDate <= DateTime.Parse(end)).ToList();
                }
                else
                {
                    Order = _dbContext.Orders.Where(c => c.EmployeeId == empId).Where(d => d.CustomerId == id).Where(d => d.OrderDate >= DateTime.Parse(start) && d.OrderDate <= DateTime.Parse(end)).ToList();
                }
            }
            foreach (var item in Order)
            {
                totalFreight += item.Freight.Value;
            }
            ViewData["totalFreight"] = totalFreight;
            List<String> customerid = _dbContext.Orders.Where(c=>c.EmployeeId == empId).Select(c => c.CustomerId).Distinct().ToList();
            foreach (string item in customerid)
            {
                Customers.Add((Customer)_dbContext.Customers.Find(item));
            }
            return Page();
        }
    }
}
