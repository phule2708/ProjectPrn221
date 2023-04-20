using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPrn221.Models;

namespace ProjectPrn221.Pages.Admin.Orders
{
    public class DetailModel : PageModel
    {
        private readonly PRN221DBContext _dbContext;

        public DetailModel(PRN221DBContext db)
        {
            _dbContext = db;
        }
        [BindProperty]
        public List<OrderDetail> OrderDetail { get; set; }
        [BindProperty]
        public int OrderId { get; set; }
        public async Task<IActionResult> OnGet(int id)

        {
            OrderId = id;

            OrderDetail = _dbContext.OrderDetails.Include(d => d.Product).Where(d => d.OrderId == id).ToList();
            return Page();
        }
    }
}
