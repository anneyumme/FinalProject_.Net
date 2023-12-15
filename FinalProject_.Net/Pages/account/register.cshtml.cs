using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FinalProject_.Net.Data;
using FinalProject_.Net.Model;
using FinalProject_.Net.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject_.Net.Pages.account;

public class registerModel : PageModel
{
	[BindProperty] public string UserName { get; set; }
	[BindProperty] public string Id { get; set; }
	[BindProperty] public string Address { get; set; }
	[BindProperty] public string FirstName { get; set; }
	[BindProperty] public string LastName { get; set; }
	[BindProperty] public string password { get; set; }

	public string FullName { get; set; }
	[BindProperty] public string Email { get; set; }

	private readonly PasswordService passwordService;
	private readonly TokenService tokenService;
	private readonly MyDbContext db;
	private readonly EmailService emailService;

	public registerModel(TokenService tokenService, PasswordService passwordService, EmailService emailService ,MyDbContext db)
	{
		this.tokenService = tokenService;
		this.passwordService = passwordService;
		this.emailService = emailService;
		this.db = db;
	}

	public IActionResult OnGet(string token)
	{
		if (token == null) return RedirectToPage("/Error");
		if (tokenService.ValidateToken(token))
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenS = tokenHandler.ReadJwtToken(token);

			Id = tokenS.Claims.First(claim => claim.Type == "Id").Value;
			UserName = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
			Address = tokenS.Claims.First(claim => claim.Type == ClaimTypes.StreetAddress).Value;
			FirstName = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;
			LastName = tokenS.Claims.First(claim => claim.Type == "LastName").Value;
			FullName = FirstName + " " + LastName;
			Email = tokenS.Claims.First(claim => claim.Type == "email").Value;
			return Page();
		}

		return RedirectToPage("/ErrorToken");
	}

	public IActionResult OnPost()
	{
		int id = int.Parse(Id);
		var saler = db.Salers.Find(id);
		saler.FName = FirstName;
		saler.LName = LastName;
		saler.Username = UserName;
		saler.Address = Address;
		saler.EmailAddress = Email;
		saler.Password = passwordService.HashPassword(password);
		db.Salers.Update(saler);
		db.SaveChanges();
		return RedirectToPage("/Index");
	}
}