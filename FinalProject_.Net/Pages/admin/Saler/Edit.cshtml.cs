using FinalProject_.Net.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject_.Net.Pages.admin.Saler
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Model.Saler Saler { get; set; }
        readonly MyDbContext dbContext;
        public EditModel(MyDbContext db)
        {
            dbContext = db;
        }
        public void OnGet(int id)
        {
            Saler = dbContext.Salers.Find(id);
        }
        public IActionResult OnPost()
        {
            try
            {
                dbContext.Salers.Update(Saler);
                dbContext.SaveChanges();
                TempData["Success"] = "The saler has been updated successfully";
            }
            catch (System.Exception ex)
            {
                ex.Message.ToString();
            }
            return RedirectToPage("./Index");
        }
    }
}
