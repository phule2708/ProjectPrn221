using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPrn221.Models;
using ProjectPrn221.Pages.Common;
namespace ProjectPrn221.Pages.Admin.Employees
{
    public class IndexModel : PageModel
    {
        private readonly PRN221DBContext _context;

        public IndexModel(PRN221DBContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get; set; } = default!;

        private int pageSize = 10;
        public decimal totalPage = 0;

        public async Task OnGetAsync(int pageNum)
        {

            if (_context.Employees != null)
            {
        
                IQueryable<Employee> query = _context.Employees;
                totalPage = Math.Ceiling((decimal)query.Count() / pageSize);
                (query, totalPage) = Utils.Page(query, pageSize, pageNum);
                Employee = await query.Include(e => e.Department).ToListAsync();
            }
        }
    }
}
