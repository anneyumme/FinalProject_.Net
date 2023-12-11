using FinalProject_.Net.Data;
using FinalProject_.Net.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject_.Net.Pages.admin.product
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Model.Product product { get; set; }
        readonly MyDbContext dbContext;
        public EditModel(MyDbContext db)
        {
            dbContext = db;
        }
        public void OnGet(int id)
        {
            product = dbContext.Products.Find(id);
        }
        public IActionResult OnPost(IFormFile imageFile)
        {
            if(imageFile != null)
            {
                product.ImageData = ImageFormatHelper.ConvertToBitmapByte(imageFile);
                if(product.ImageData == null)
                {
                    TempData["Error"] = "The image could not be uploaded. Please try again.";
                }
            }
            try
            {
                dbContext.Products.Update(product);
                dbContext.SaveChanges();
                TempData["Success"] = "The product has been updated successfully";
            }
            catch (System.Exception ex)
            {
                ex.Message.ToString();
            }
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetImage(int id)
        {
            var myproduct = dbContext.Products.Find(id);

            if (myproduct.ImageData != null)
            {
                return File(myproduct.ImageData, "image/bmp");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
