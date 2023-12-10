using FinalProject_.Net.Data;
using FinalProject_.Net.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_.Net.Pages.admin.Saler
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Model.Saler Saler { get; set; }
        public MyDbContext dbContext;
        public CreateModel(MyDbContext db)
        {
            dbContext = db;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {

            try
            {
                dbContext.Salers.Add(Saler);
                dbContext.SaveChanges();
                TempData["Success"] = "The saler has been added successfully";
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return RedirectToPage("./Index");
        }
    }
}
