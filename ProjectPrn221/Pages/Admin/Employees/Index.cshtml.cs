using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPrn221.Models;

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
                pageNum -= 1;
                if (pageNum <= 0)
                {
                    pageNum = 0;
                }
                totalPage = Math.Ceiling((decimal)_context.Employees.Count() / pageSize);
                Employee = await _context.Employees.Skip(pageNum*pageSize).Take(pageSize).Include(e => e.Department).ToListAsync();
            }
        }
    }
}
