using System;
using System.Globalization;
using FinalProject_.Net.Data;
using FinalProject_.Net.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject_.Net.Pages.admin
{
    public class IndexModel : PageModel
    {
        public string DateTemplate { get; set;}
        public string SelectDate { get; set;}
        public decimal TotalRevenue { get; set;}
        public int TotalOrder { get; set;}
        // Chart variable
        public string ChartLabelRevenue { get; set;}
        public string ChartDataRevenue { get; set;}
        public string LabelTipsRevenue { get; set;}

        public List<Order> Orderslist { get; set;}
        readonly private MyDbContext myDbContext;
        public IndexModel(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
            Orderslist = myDbContext.Orders.ToList();

        }
        public void OnGet()
        {
            // Get all orders
            // Get total revenue
            foreach (var item in Orderslist)
            {
                TotalRevenue += item.total;
            }
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.SignOutAsync("UserLoginCookie");
            return RedirectToPage("/Index");
        }
        public void OnGetDate(string selectedDate, string dateTemplate){
            // Setup date
            SelectDate = selectedDate;
            DateTemplate = dateTemplate;

            // Date Template to compare with template date selected
            DateTime yesterDay = (DateTime.Now.AddDays(-1));
            DateTime sevenDayAgo = (DateTime.Now.AddDays(-7));
            DateTime twentyEightDayAgo = (DateTime.Now.AddDays(-28));
            DateTime ninetyDayAgo = (DateTime.Now.AddDays(-90));
            DateTime yearsAgo = (DateTime.Now.AddYears(-1));
              

            //Convert string to int(dateTemplate) to know which date template is selected
            int value = int.Parse(DateTemplate);
            //Convert to date time

            // ===============================================================
            // Implement logic revenue
            // ===============================================================
            if (SelectDate != null)
            {
                DateTime dateSelect = DateTime.Parse(SelectDate);
                Orderslist = myDbContext.Orders.Where(o => o.orderDate >= dateSelect).ToList();

            }
            else
            {
                if (value == 1) // Today
                {
                    Orderslist = myDbContext.Orders.Where(o => o.orderDate.Year ==
                    DateTime.Now.Year && o.orderDate.Month == DateTime.Now.Month && o.orderDate.Day == DateTime.Now.Day).ToList();

                }
                else if (value == 2) // Yesterday
                {
                    Orderslist = myDbContext.Orders.Where(o => o.orderDate >= yesterDay).ToList();
                }
                else if (value == 3) // Last 7 days
                {
                    Orderslist = myDbContext.Orders.Where(o => o.orderDate >= sevenDayAgo).ToList();
                }
                else if (value == 4) // Last 28 days
                {
                    Orderslist = myDbContext.Orders.Where(o => o.orderDate >= twentyEightDayAgo).ToList();
                }
                else if (value == 5) // Last 90 days
                {
                    Orderslist = myDbContext.Orders.Where(o => o.orderDate >= ninetyDayAgo).ToList();
                }
                else if(value== 0) // All time
                {
                    Orderslist = myDbContext.Orders.ToList();
                }
            }

            foreach (var item in Orderslist)
            {
                TotalRevenue += item.total;
            }

            // ===============================================================
            // Implement logic chart revenue
            LabelTipsRevenue = "Revenue ($)";
            // ===============================================================
            if (value == 0) // All time
            {

                List<String> labelList = getDayNameYearAgo();
                ChartLabelRevenue = string.Join(", ", labelList.Select(day => $"'{day}'"));


                List<String> dataChart = new List<String>();
                List<DateTime> listDate = new List<DateTime>();
                foreach(var item in labelList)
                {
                    DateTime date = DateTime.ParseExact(item, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    listDate.Add(date);
                }

                decimal totalOrder = 0;
                foreach (var item in Orderslist)
                {
                        if(item.orderDate <= listDate[0])
                        {
                            totalOrder += item.total;
                        }                
                }
                dataChart.Add(totalOrder.ToString());

                for (int i = 1; i < listDate.Count; i++)
                { 
                    decimal total = 0;
                    foreach(var order in Orderslist)
                    {
                        if(order.orderDate <= listDate[i] && order.orderDate > listDate[i-1])
                        {
                            total += order.total;
                        }
                    }
                    dataChart.Add(total.ToString());
                }

                ChartDataRevenue = string.Join(", ", dataChart.Select(day => $"'{day}'"));

            }
        }


        public List<String> getDayNameYearAgo()
        {
            DateTime yearsAgo = DateTime.Now.AddYears(-1);
            DateTime currentDate = DateTime.Now;

            TimeSpan totalDuration = currentDate - yearsAgo;
            double intervalDays = totalDuration.TotalDays / 12;

            List<string> timePoints = new List<string>();

            for (int i = 0; i < 12; i++)
            {
                DateTime timePoint = yearsAgo.AddDays(intervalDays * i);
                timePoints.Add(timePoint.ToString("dd/MM/yyyy"));
            }
            timePoints.RemoveAt(0);
            timePoints.Add(DateTime.Now.ToString("dd/MM/yyyy"));
            timePoints = new HashSet<string>(timePoints).ToList();
            return timePoints;
        }
        
    }
}
