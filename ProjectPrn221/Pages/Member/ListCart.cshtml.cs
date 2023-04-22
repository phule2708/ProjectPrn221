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
        public List<Product> list { get; set; }

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
                list = db.Products.ToList();
            }
        }
        public void OnPostPay()
        {
            //string[] idArray = id.Split(',');
            string quantity = Request.Form["quantity"];
            string price = Request.Form["price"];
            string[] ArrayQuan = quantity.Split(',');
            string[] StringPrice = price.Split(',');
            addOrder();
            for(int i = 0; i < ArrayQuan.Length; i++)
            {
                if (int.Parse(ArrayQuan[i]) > 0)
                {
                    addOrderDetail(listProductId[i],decimal.Parse(StringPrice[i]),short.Parse(ArrayQuan[i]));
                }else
                {

                }
            }
           
            
        }
        public void addOrderDetail(int productid , decimal price, short quantity)
        {
            var db = new PRN221DBContext();
            var firstEntity = db.Orders.OrderByDescending(e => e.OrderId).FirstOrDefault();
            int id = firstEntity.OrderId;
            OrderDetail detail = new OrderDetail()
            {
                OrderId = id,
                ProductId = productid,
                UnitPrice = price,
                Quantity = quantity,
                Discount = 1,
            };

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
