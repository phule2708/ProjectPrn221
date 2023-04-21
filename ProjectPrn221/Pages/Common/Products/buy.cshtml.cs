using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ProjectPrn221.Pages.Admin.Products
{
    public class buyModel : PageModel
    {
        public void OnGet()
        {
            string numbersJson = HttpContext.Session.GetString("listCart");
            List<int> numbers = JsonConvert.DeserializeObject<List<int>>(numbersJson);
            Console.WriteLine(numbers);
        }
    }
}
