using FinalProject_.Net.Data;
using FinalProject_.Net.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_.Net.Pages.admin.ranking
{
    public class IndexModel : PageModel
    {
        public DateTime CurrentDay { get; set; }
        readonly MyDbContext myDbContext;
        public List<Customer> ListBestCustomersOnDay { get; set; }
        public List<decimal> ListTotalMoneyOfEachCustomerOnDay { get; set; }

        public List<Customer> ListBestCustomersOnAllDay { get; set; }
        public List<decimal> ListTotalMoneyOfEachCustomerOnAllDay { get; set; }
        public IndexModel(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
            CurrentDay = DateTime.Now;
        }
        public void OnGet()
        {
            FilterObjectOnDay(CurrentDay);
            FilterObjectOnAllDay();
        }




        public void FilterObjectOnDay(DateTime date)
        {
            // Load all customer on day 
            var listCustomerOnDay = myDbContext.Customers
                .Where(c => c.Orders.Any(o => o.orderDate.Date == date.Date))
                .Select(c => new
                {
                    Customer = c, // or select specific fields of the customer
                    Orders = c.Orders.Where(o => o.orderDate.Date == date.Date).ToList()
                })
                .ToList()
                .Select(x =>
                {
                    x.Customer.Orders = x.Orders;
                    return x.Customer;
                })
                .ToList();


            // Top 5 best customers on day with highest total money
            ListBestCustomersOnDay = listCustomerOnDay.
                OrderByDescending(c => c.Orders.Sum(o => o.total)).Take(5).ToList();
            // List Total money of each customer
            ListTotalMoneyOfEachCustomerOnDay =
                ListBestCustomersOnDay.Select(c => Math.Round(c.Orders.Sum(o => o.total), 2)).ToList();
        }
        public void FilterObjectOnAllDay()
        {
            List<Customer> listCustomerOnAllDay= myDbContext.Customers
                .Include(c => c.Orders)
                .ToList();
            // Top 5 best customers on day with highest total money
            ListBestCustomersOnAllDay = listCustomerOnAllDay.OrderByDescending(c => c.Orders.Sum(o => o.total)).Take(5).ToList();
            // List Total money of each customer
            ListTotalMoneyOfEachCustomerOnAllDay =
                ListBestCustomersOnAllDay.Select(c => c.Orders.Sum(o => o.total)).ToList();
        }
    }
}
