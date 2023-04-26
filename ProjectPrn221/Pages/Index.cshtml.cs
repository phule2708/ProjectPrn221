using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectPrn221.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("account") == null)
            {
                return RedirectToPage("/Common/Products/List");
            }
            return RedirectToPage("/Common/Products/List");
        }
    }
}
