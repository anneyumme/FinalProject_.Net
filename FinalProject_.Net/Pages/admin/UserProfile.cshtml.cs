using System;
using System.Globalization;
using System.Xml.Serialization;
using FinalProject_.Net.Data;
using FinalProject_.Net.Model;
using FinalProject_.Net.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_.Net.Pages.admin
{
    public class UserProfileModel : PageModel
    {
        readonly private MyDbContext myDbContext;
        [BindProperty] public Model.Saler saler { get; set; }
        public UserProfileModel(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;

        }
        public void OnGet(int id)
        {
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
