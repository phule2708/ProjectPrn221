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
        
        public static List<int> listId;

        [ViewData]
        public List<Product> list { get; set; }

        [ViewData]
        public List<int> listProductId { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("account") == null)
            {
                return RedirectToPage("/SignIn");
            }
            else
            {
                if (HttpContext.Session.GetString("listCart") != null)
                {
                    var db = new PRN221DBContext();
                    string numbersJson = HttpContext.Session.GetString("listCart");
                    listProductId = JsonConvert.DeserializeObject<List<int>>(numbersJson);
                    HashSet<int> myHashSet = new HashSet<int>(listProductId);
                    listProductId = myHashSet.ToList();
                    listId = listProductId;
                    list = db.Products.ToList();
                    return Page();
                }
            }
            return Page();
        }
        public void OnPostDelete()
        {
            int deleteProduct = int.Parse(Request.Form["delete"]);
            for(int i = 0; i < listId.Count; i++)
            {
                listId.RemoveAll(item => item == deleteProduct);
            }
            string List = JsonConvert.SerializeObject(listId);
            HttpContext.Session.Remove("listCart");
            HttpContext.Session.SetString("listCart", List);
            ViewData["message"] = "Đã xóa succesfully";
            OnGet();
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
                    addOrderDetail(listId[i],decimal.Parse(StringPrice[i]),short.Parse(ArrayQuan[i]));
                }else
                {

                }
            }
            HttpContext.Session.Remove("listCart");

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
            db.OrderDetails.Add(detail);
            db.SaveChanges();
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
