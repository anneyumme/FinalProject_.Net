using System.Drawing;
using FinalProject_.Net.Data;
using FinalProject_.Net.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_.Net.Pages.admin.product;

public class CreateModel : PageModel
{
    [BindProperty] 
    public Model.Product product { get; set; }
    public MyDbContext dbContext;

    public CreateModel(MyDbContext db)
    {
        dbContext = db;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost(IFormFile imageFile)
    {
        product.ImageData = ImageFormatHelper.ConvertToBitmapByte(imageFile);
        if(product.ImageData == null)
        {
            TempData["Error"] = "The image could not be uploaded. Please try again.";
        }
        dbContext.Products.Add(product);
        dbContext.SaveChanges();
        TempData["Success"] = "Product added successfully";
        return RedirectToPage("./Index");
    }

}
