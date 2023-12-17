using FinalProject_.Net.Data;
using FinalProject_.Net.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinalProject_.Net.Pages.customer
{
    public class ListOrderModel : PageModel
    {
        readonly MyDbContext dbContext;
        public List<Order> listOrderOfSaler { get; set; }

        private int salerId;

        public ListOrderModel(MyDbContext myDbContext)
        {
            dbContext = myDbContext;
        }
        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var salerId = Int32.Parse(User.FindFirstValue("Id"));
                var listOrder = dbContext.Orders.Include(o => o.Saler).Include(o => o.Customer).ToList();
                listOrderOfSaler = new List<Order>();
                foreach (var order in listOrder)
                {
                    if (order.Saler.Id == salerId)
                    {
                        listOrderOfSaler.Add(order);
                    }
                }
            }

         

        }
    }
}
