using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPrn221.Models;

namespace ProjectPrn221.Pages.Member
{
    public class IndexModel : PageModel
    {
        public static string CustomerID;
        private readonly PRN221DBContext DB;        

        [ViewData]
        public List<Order> ListOrder { get; set; }

        [ViewData]
        public List<OrderDetail> ListOrderDetail { get; set; }
        public void OnGet()
        {
            string id = HttpContext.Session.GetString("id");
            CustomerID = id;
            ListOrder = getListOrderById(id);
            var data = DB.OrderDetails.Include("Product").ToList();
            ListOrderDetail = data;
            ViewData["sort"] = "asc";
        }

        public void OnPost()
        {
            var data = DB.OrderDetails.Include("Product").ToList();
            ListOrderDetail = data;
            string sort = Request.Form["sortProduct"];
            if (sort == "0")
            {
                ListOrder = getListOrderById(CustomerID);
            }
            else if (sort == "asc")
            {
                ListOrder = getListOrderSortASC(CustomerID);
            }
            else
            {
                ListOrder = getListOrderSortDESC(CustomerID);
            }
            ViewData["sort"] = sort.ToString();
        }

        public IndexModel(PRN221DBContext _db)
        {
            this.DB = _db;
        }
       

        public List<Order> getListOrderById(string id)
        {
            List<Order> order = new List<Order>();
            order = DB.Orders.Where(x => x.CustomerId == id).ToList();
            return order;
        }
        public List<Order> getListOrderSortASC(string id)
        {
            List<Order> order = new List<Order>();
            order = DB.Orders.Where(x => x.CustomerId == id).OrderBy(p => p.OrderDate).ToList();
            return order;
        }
        public List<Order> getListOrderSortDESC(string id)
        {
            List<Order> order = new List<Order>();
            order = DB.Orders.Where(x => x.CustomerId == id).OrderByDescending(p => p.OrderDate).ToList();
            return order;
        }

    }
}
