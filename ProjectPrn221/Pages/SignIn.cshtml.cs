using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPrn221.Models;

namespace ProjectPrn221.Pages
{
    public class SignInModel : PageModel
    {
        //private readonly PRN221DBContext db;
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            Account account = new Account();
            string Email = Request.Form["Email"];
            string password = Request.Form["password"];
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(password))
            {
                ViewData["Message"] = "Email hoặc Mật Khẩu không được để trống";
                return Page();
            }
            else
            {
                account = GetAccount(Email);
                if (account == null)
                {
                    ViewData["Message"] = "Email không Tồn Tại ";
                }
                else
                {
                    if (account.Password != password)
                    {
                        ViewData["Message"] = "Pass không đúng ";
                    }
                    else
                    {
                        if (account.Role == 1)
                        {

                            HttpContext.Session.SetString("account", "Employees");

                            return RedirectToPage("/Admin/Orders/List");
                        }
                        else
                        {
                            string id = account.CustomerId;
                            HttpContext.Session.SetString("account", "Customer");
                            HttpContext.Session.SetString("id", id);
                            return RedirectToPage("/Member/Index");
                        }
                    }
                }
            }
            return Page();
        }
        public Account GetAccount(string email)
        {
            using (var db = new PRN221DBContext())
            {
                Account account = new Account();
                account = db.Accounts.Where(x => x.Email == email).FirstOrDefault();
                return account;
            }

        }

    }
}
