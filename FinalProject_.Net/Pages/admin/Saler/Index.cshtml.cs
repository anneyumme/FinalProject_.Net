using FinalProject_.Net.Data;
using FinalProject_.Net.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject_.Net.Pages.admin.Saler
{
    public class IndexModel : PageModel
    {
        private readonly MyDbContext dbContext;
        private readonly EmailService emailService;
        private readonly TokenService tokenService;
        public List<Model.Saler> listSaler { get; set; }

        public IndexModel(MyDbContext db, EmailService emailService, TokenService tokenService)
        {
            dbContext = db;
            this.emailService = emailService;
            this.tokenService = tokenService;
        }
        public void OnGet(int id)
        {
            listSaler = dbContext.Salers.ToList();
        }

        public IActionResult OnGetEmail(int id)
        {
            var Saler = dbContext.Salers.Find(id);
            var MyToken = tokenService.GenerateToken(
                id, Saler.FName, Saler.LName, Saler.Username, Saler.Address, Saler.EmailAddress
                );
            string pageUrl = Url.Page("/account/register", null, new { token = MyToken }, Request.Scheme);

            emailService.sendEmail(Saler.EmailAddress, pageUrl);
            TempData["Success"] = "The email has been sent successfully";
            return RedirectToPage("/Admin/Saler/index");
        }
        public IActionResult OnGetDelete(int id)
        {
            var saler = dbContext.Salers.Find(id);

            try
            {
                dbContext.Salers.Remove(saler);
                dbContext.SaveChanges();
                TempData["Success"] = "The saler has been deleted successfully";
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
        public IActionResult OnGetImage(int id)
        {
            var saler = dbContext.Salers.Find(id);

            if (saler.Avatar != null)
            {
                return File(saler.Avatar, "image/bmp");
            }
            else
            {
                return NotFound();
            }
        }

    }
}
