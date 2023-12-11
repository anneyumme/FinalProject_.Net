using FinalProject_.Net.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject_.Net.Pages.admin.product
{
    public class IndexModel : PageModel
    {
        readonly MyDbContext dbContext;
        public List<Model.Product> listProducts { get; set; }
        public IndexModel(MyDbContext db)
        {
            dbContext = db;
        }
        public void OnGet()
        {
            listProducts = dbContext.Products.ToList();
        }

        public IActionResult OnGetDelete(int id)
        {

            var product = dbContext.Products.Find(id);
            if (product != null)
            {
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
                TempData["Success"] = "The product has been deleted successfully";
            }
            return RedirectToPage("./Index");
        }
    }
}
