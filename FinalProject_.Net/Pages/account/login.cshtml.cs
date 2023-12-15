using FinalProject_.Net.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using FinalProject_.Net.Data;
using FinalProject_.Net.Service;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_.Net.Pages.Account
{
    public class loginModel : PageModel
    {
        [BindProperty]
        public string password { get; set; }
        [BindProperty]
        public string email { get; set; }   
		public MyDbContext myDbContext { get; set; }
		public PasswordService MyPasswordService { get; set; }
        public loginModel( MyDbContext myDbContext, PasswordService passwordService)
        {
            this.myDbContext = myDbContext;
            MyPasswordService = passwordService;
        }
        public void OnGet()
        {
			
        }
        
        public async Task<IActionResult> OnPost()
        {
			var SalerLogin = myDbContext.Salers.Include(s => s.Roles)
                .FirstOrDefault(s => s.EmailAddress == email);

	        if (SalerLogin != null && SalerLogin.Password != null)
	        {
                if (MyPasswordService.ValidatePassword(password, SalerLogin.Password))
                {
                    var listCalm = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email, SalerLogin.EmailAddress),
                        new Claim("Id", SalerLogin.Id.ToString()),
                        new Claim(ClaimTypes.Name, SalerLogin.Username),
                        new Claim(ClaimTypes.Role, SalerLogin.Roles.Name)
                    };
                    var claimIdentity = new ClaimsIdentity(listCalm, "UserLoginCookie");
                    var claimPrincipal = new ClaimsPrincipal(claimIdentity);
                    await HttpContext.SignInAsync("UserLoginCookie", claimPrincipal);
                    return RedirectToPage("/index");
                }
	        }
            TempData["Message"] = "Hmm, something wrong with your credential. Please try again.";
	        return Page();
		}
    }
}
