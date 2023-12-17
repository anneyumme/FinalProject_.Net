using FinalProject_.Net.Data;
using FinalProject_.Net.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_.Net.Pages.customer
{
    public class OrderDetailModel : PageModel
    {
        readonly MyDbContext myDbContext;
        [BindProperty]
       public Model.Order Order { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
        public OrderDetailModel(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public void OnGet(int OrderId)
        {
            Order = myDbContext.Orders.Include(o => o.Customer).Include(o => o.OrderDetails).
                ThenInclude(od => od.Product).FirstOrDefault(o => o.Id == OrderId);
             orderDetails = Order.OrderDetails.ToList();
        }





        public IActionResult OnGetImage(int id)
        {
            var myproduct = myDbContext.Products.Find(id);

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
