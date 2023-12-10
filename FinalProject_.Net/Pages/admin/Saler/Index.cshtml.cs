using FinalProject_.Net.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject_.Net.Pages.admin.Saler
{
    public class IndexModel : PageModel
    {
        readonly MyDbContext dbContext;
        public List<Model.Saler> listSaler { get; set; }
        public IndexModel(MyDbContext db)
        {
            dbContext = db;
        }
        public void OnGet()
        {
            listSaler = dbContext.Salers.ToList();
        }
        public IActionResult OnGetDelete(int id)
        {
            var saler = dbContext.Salers.Find(id);
            if (saler != null)
            {
                dbContext.Salers.Remove(saler);
                dbContext.SaveChanges();
                TempData["Success"] = "The saler has been deleted successfully";
            }
            return RedirectToPage("./Index");
        }
    }
}
