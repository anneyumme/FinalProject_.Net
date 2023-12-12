using FinalProject_.Net.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject_.Net.Pages.api
{
    public class productModel : PageModel
    {
        public MyDbContext MyDbContext { get; set; }
        public productModel(MyDbContext myDbContext)
        {
            MyDbContext = myDbContext;
        }
        public void OnGet()
        {
        }
        public IActionResult OnGetJsonData()
        {
            var data = MyDbContext.Products.ToList();

            var jsonData = System.Text.Json.JsonSerializer.Serialize(data);
            return new ContentResult
            {
                Content = jsonData,
                ContentType = "application/json",
                StatusCode = 200
            };
        }
    }
}
