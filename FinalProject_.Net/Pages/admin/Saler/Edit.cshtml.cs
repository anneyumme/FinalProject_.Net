using FinalProject_.Net.Data;
using FinalProject_.Net.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_.Net.Pages.admin.Saler
{
    public class EditModel : PageModel
    {
        [BindProperty] public List<SelectListItem> RoleList { get; set; }

        [BindProperty] public Model.Saler Saler { get; set; }
        [BindProperty] public string RoleId { get; set; }
        readonly MyDbContext dbContext;
        public EditModel(MyDbContext db)
        {
            dbContext = db;
            RoleList = dbContext.Roles.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();
        }
        public void OnGet(int id)
        {
            Saler = dbContext.Salers.Include(r=> r.Roles).First(s=>s.Id == id);
            RoleId = Saler.Roles.Id.ToString();
        }


        public IActionResult OnPost()
        {
            try
            {
                Saler.Roles = dbContext.Roles.Find(int.Parse(RoleId));
                dbContext.Salers.Update(Saler);
                dbContext.SaveChanges();
                TempData["Success"] = "The saler has been updated successfully";
            }
            catch (System.Exception ex)
            {
                TempData["Error"] = ex.Message.ToString();
            }
            return RedirectToPage("./Index");
        }
    }
}
