using FinalProject_.Net.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace FinalProject_.Net.Pages.Account
{
    public class loginModel : PageModel
    {
        [BindProperty]
        public Customer customer { get; set; }
        public void OnGet()
        {
			
        }
        
        public async Task<IActionResult> OnPost()
        {
	        if (customer.EmailAddress == "admin@admin.com" )
	        {
		        var listCalm = new List<Claim>()
		        {
			        new Claim(ClaimTypes.Name, customer.EmailAddress),
			        new Claim(ClaimTypes.Role, "Admin")
		        };
		        var claimIdentity = new ClaimsIdentity(listCalm, "UserLoginCookie");
		        var claimPrincipal = new ClaimsPrincipal(claimIdentity);
		        await HttpContext.SignInAsync("UserLoginCookie", claimPrincipal);
		        return RedirectToPage("/index");
	        }
	        return Page();
		}
    }
}
