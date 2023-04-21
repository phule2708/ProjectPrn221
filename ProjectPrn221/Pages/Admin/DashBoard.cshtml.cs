using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;
using ProjectPrn221.Models;

namespace ProjectPrn221.Pages.Admin
{
    public class DashBoardModel : PageModel
    {
        private readonly PRN221DBContext dBContext;
        public DashBoardModel(PRN221DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public float weeklySale = 0;
        public float totalOrders = 0;
        public int totalCustomerHasAccount = 0;
        public int totalGuest = 0;
        public int totalCustomer = 0;

        public Dictionary<int, int> orders12months;
        public List<int> orderyearsList = new List<int>();

        public IActionResult OnGet(int orderyear)
        {
            if (HttpContext.Session.GetString("account")!= "Employees")
            {
                return Redirect("/errorpage?code=401");
            }
            DateTime currentDateTime = DateTime.Now;
            if(orderyear == 0)
            {
                orderyear = currentDateTime.Year;
            }

            @ViewData["year"] = orderyear;
            DateTime monday, sunday;
            monday = currentDateTime.AddDays(-(int)currentDateTime.DayOfWeek + 1);
            sunday = currentDateTime.AddDays(7 - (int)currentDateTime.DayOfWeek);



            weeklySale = (from o in dBContext.Orders
                          where o.OrderDate >= monday && o.OrderDate <= sunday
                          select (float)o.Freight).Sum();

            totalOrders = (from o in dBContext.Orders
                           select (float)o.Freight).Sum();

            totalCustomerHasAccount = (from customer in dBContext.Customers
                                       join account in dBContext.Accounts on customer.CustomerId equals account.CustomerId
                                       select customer).Count();

            totalGuest = (from customer in dBContext.Customers
                          join account in dBContext.Accounts on customer.CustomerId equals account.CustomerId into ac
                          from subaccount in ac.DefaultIfEmpty()
                          where subaccount == null
                          select customer).Count();

            totalCustomer = (from customer in dBContext.Customers
                             select customer).Count();

          

            orders12months = (from order in dBContext.Orders
                                  where order.OrderDate.Value.Year == orderyear
                                    group order by order.OrderDate.Value.Month into orderMonth
                                  orderby orderMonth.Key ascending
                                  select new {Month = orderMonth.Key, Orders = orderMonth.Select(o => o.OrderId).Count() }

                     ).ToDictionary(e => e.Month, e=> e.Orders);

            // Get all year has orders
            var orderDate =  dBContext.Orders.GroupBy(order => order.OrderDate.Value.Year)
                .Select(group => group.First()).ToList();
            orderDate.ForEach(e => orderyearsList.Add(e.OrderDate.Value.Year));

            for (var i = 1; i<=12; i++){
                if (!orders12months.Keys.Contains(i))
                {
                    orders12months.Add(i, 0);
                }
            }

            return Page();
        }


    }
}
