using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectPrn221.Models;

namespace ProjectPrn221.Pages.Member
{
    public class ListCartModel : PageModel
    {
        //private readonly PRN221DBContext db;

        [ViewData]
        public List<OrderDetail> list { get; set; }

        [ViewData]
        public List<int> listProductId { get; set; }
        public void OnGet()
        {
            if (HttpContext.Session.GetString("listCart") != null)
            {
                var db = new PRN221DBContext();
                string numbersJson = HttpContext.Session.GetString("listCart");
                listProductId = JsonConvert.DeserializeObject<List<int>>(numbersJson);
                HashSet<int> myHashSet = new HashSet<int>(listProductId);
                listProductId = myHashSet.ToList();
                list = db.OrderDetails.Include("Product").ToList();
            }

            

        }
    }
}
