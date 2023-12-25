using FinalProject_.Net.Data;
using FinalProject_.Net.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject_.Net.Pages.customer
{
    public class UserProfileModel : PageModel
    {
        readonly private MyDbContext myDbContext;
        [BindProperty] public string Password { get; set; }
        [BindProperty] public string Id { get; set; }
        [BindProperty] public Model.Saler saler { get; set; }
        public UserProfileModel(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;

        }
        public void OnGet(int id)
        {
            Id = id.ToString();
            saler = myDbContext.Salers.Find(id);
        }
        public IActionResult OnPost(IFormFile imageFile)
        {
            if (imageFile != null)
            {
                saler.Avatar = ImageFormatHelper.ConvertToBitmapByte(imageFile);
                if (saler.Avatar == null)
                {
                    TempData["Error"] = "The image could not be uploaded. Please try again.";
                }
            }
            try
            {
                myDbContext.Salers.Update(saler);
                myDbContext.SaveChanges();
                TempData["Success"] = "The profile has been updated successfully";
            }
            catch (System.Exception ex)
            {
                TempData["Error"] = ex.Message.ToString();
            }

            return Page();
        }

        public IActionResult OnPostPassword()
        {
            PasswordService passwordService = new PasswordService();
            var IdInt= int.Parse(Id);
            var saler = myDbContext.Salers.FirstOrDefault(x => x.Id == IdInt);
            if(Password != null)
            {
                saler.Password = passwordService.HashPassword(Password);
                myDbContext.Salers.Update(saler);
                myDbContext.SaveChanges();
                TempData["Success"] = "The password has been updated successfully";
            }
            return Redirect("/customer");
        }

        public IActionResult OnGetImage(int id)
        {
            var saler = myDbContext.Salers.Find(id);

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
