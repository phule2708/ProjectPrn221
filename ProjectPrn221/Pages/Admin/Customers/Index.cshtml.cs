using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPrn221.Models;
using ProjectPrn221.Pages.Common;
namespace ProjectPrn221.Pages.Admin.Customers
{
    public class IndexModel : PageModel
    {
        private readonly PRN221DBContext _context;

        public IndexModel(PRN221DBContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; } = default!;

        private int pageSize = 10;
        [BindProperty]
        public decimal totalPage { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNum, string search)
        {
            if (HttpContext.Session.GetString("account")!="Employees")
            {
                return Redirect("/errorpage?code=401");
            }
            if (_context.Customers != null)
            {
                
                ViewData["search"] = search;
               
                IQueryable<Customer> query = _context.Customers.Where(e=> (search == null)? true : e.ContactName.Contains(search));
                totalPage = Math.Ceiling((decimal)query.Count() / pageSize);
                (query, totalPage) = Utils.Page(query, pageSize, pageNum);
                ViewData["page"] = pageNum;
                Customer = await query.ToListAsync();
            }
            return Page();
        }
    }
}
