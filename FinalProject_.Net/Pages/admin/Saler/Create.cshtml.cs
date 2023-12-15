using FinalProject_.Net.Data;
using FinalProject_.Net.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_.Net.Pages.admin.Saler
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Model.Saler Saler { get; set; }
        public MyDbContext dbContext;
        [BindProperty] public string RoleId { get; set; }
        [BindProperty] public List<SelectListItem> Roles { get; set; }
        public CreateModel(MyDbContext db)
        {
            dbContext = db;
            Roles = dbContext.Roles.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {

            try
            {
                int Roleid = int.Parse(RoleId);
                var Role = dbContext.Roles.Find(Roleid);
                Saler.Roles = Role;

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
