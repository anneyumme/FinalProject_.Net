using System;
using System.Globalization;
using System.Xml.Serialization;
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

        public string ChartLabelOrder { get; set; }
        public string ChartDataOrder { get; set; }
        public string LabelTipsOrder  { get; set; }

        public List<Order> Orderslist { get; set;}
        readonly private MyDbContext myDbContext;
        public IndexModel(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
            Orderslist = myDbContext.Orders.ToList();

        }
        public void OnGet()
        {
            
            foreach (var item in Orderslist)
            {
                TotalRevenue += item.total;
                TotalOrder += 1;
            }
            ChartRevenue(0);
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.SignOutAsync("UserLoginCookie");
            return RedirectToPage("/Index");
        }
        public void OnGetDate(string selectedDate, string dateTemplate){

            int value = int.Parse(dateTemplate);
            // Setup date
            SelectDate = selectedDate;
            DateTemplate = dateTemplate;


            //================================================================
            // Implement logic Total revenue and Order
            TotalOrderAndRevenue(selectedDate, dateTemplate, value);

            // ===============================================================
            // Implement logic chart revenue
            if (!String.IsNullOrEmpty(selectedDate))
            {
                DateTime dateSelect = DateTime.ParseExact(selectedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                SpecificTimeFilter(dateSelect);
            }
            else
            {
                ChartRevenue(value);
            }
            // ===============================================================

        }

        public void ChartRevenue(int value)
        {
         
            if (value == 0) // All time
            {
                LabelTipsRevenue = "Revenue ($)";
                LabelTipsOrder = "Quantity Of Order";
                List<String> labelList = getDayNameYearAgo();
                ChartLabelRevenue = ConvertListToStringChartData(labelList);
                ChartLabelOrder = ConvertListToStringChartData(labelList);
                List<String> dataChart = new List<String>();
                List<String> dataChartOrder = new List<String>();

                List<DateTime> ListDateFromString = new List<DateTime>();

                foreach (var item in labelList)
                {
                    DateTime date = DateTime.ParseExact(item, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ListDateFromString.Add(date);
                }
                decimal totalOrder = 0;
                decimal totalQuantity1 = 0;
                foreach (var item in Orderslist)
                {
                    if (item.orderDate <= ListDateFromString[0])
                    {
                        totalOrder += item.total;
                        totalQuantity1++;
                    }
                }
                dataChart.Add(totalOrder.ToString());
                dataChartOrder.Add(totalQuantity1.ToString());

                for (int i = 1; i < ListDateFromString.Count; i++)
                {
                    decimal total = 0;
                    decimal totalQuantity = 0;
                    foreach (var order in Orderslist)
                    {
                        if (order.orderDate <= ListDateFromString[i] && order.orderDate > ListDateFromString[i - 1])
                        {
                            total += order.total;
                            totalQuantity++;
                        }
                    }
                    dataChart.Add(total.ToString());
                    dataChartOrder.Add(totalQuantity.ToString());
                }
                ChartDataOrder = ConvertListToStringChartData(dataChartOrder);
                ChartDataRevenue = ConvertListToStringChartData(dataChart);
            }
            else if(value == 1) // Today
            {
                SpecificTimeFilter(DateTime.Now);
            }
            else if(value == 2) // Yesterday
            {
                DateTime Today = DateTime.Now;
                SpecificTimeFilter(Today.AddDays(-1));
            }
            else if(value == 3) // Last 7 days
            {
                   DateTime Today = DateTime.Now;
                   LastDateTime(Today.AddDays(-7));
            }
            else if(value == 4) // Last 28 days
            {
                DateTime Today = DateTime.Now;
                LastDateTime(Today.AddDays(-28));
            }
            else if(value == 5) // Last 90 days
            {
                DateTime Today = DateTime.Now;
                LastDateTime(Today.AddDays(-90));
            }        
        }
        public void TotalOrderAndRevenue(string selectedDate, string dateTemplate, int value)
        {
            // Date Template to compare with template date selected
            DateTime yesterDay = (DateTime.Now.AddDays(-1));
            DateTime sevenDayAgo = (DateTime.Now.AddDays(-7));
            DateTime twentyEightDayAgo = (DateTime.Now.AddDays(-28));
            DateTime ninetyDayAgo = (DateTime.Now.AddDays(-90));
            DateTime yearsAgo = (DateTime.Now.AddYears(-1));
           
            //Convert to date time

            // ===============================================================
            // Implement logic revenue
            // ===============================================================
            if (SelectDate != null)
            {
                DateTime dateSelect = DateTime.ParseExact(selectedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Orderslist = myDbContext.Orders.Where(o => o.orderDate.Year == dateSelect.Year && o.orderDate.Month == dateSelect.Month &&
                                                           o.orderDate.Day == dateSelect.Day ).ToList();
       
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
                else if (value == 0) // All time
                {
                    Orderslist = myDbContext.Orders.ToList();
                }
            }

            foreach (var item in Orderslist)
            {
                TotalRevenue += item.total;
                TotalOrder += 1;
            }
        }
        public void SpecificTimeFilter(DateTime dateSelect)
        {
            LabelTipsRevenue = "Revenue ($)";
            LabelTipsOrder = "Quantity Of Order";
            ChartLabelRevenue = "'0h', '3h', '6h', '9h', '12h', '15h', '18h', '21h', '24h'";
            ChartLabelOrder = "'0h', '3h', '6h', '9h', '12h', '15h', '18h', '21h', '24h'";
            List<int> hours = ConvertStringToList(ChartLabelRevenue);
            List<String> chartData = new List<String>();
            List<String> chartDataOrder = new List<String>();



            // Add total order from 0h to 3h
            decimal totalOrder = 0;
            decimal totalQuantity1 = 0;

            foreach (var item in Orderslist)
            {
                if (item.orderDate.Year == dateSelect.Year && item.orderDate.Month == dateSelect.Month &&
                   item.orderDate.Day == dateSelect.Day)
                {
                    if (item.orderDate.Hour < 3 && item.orderDate.Hour >= 0)
                    {
                        totalOrder += item.total;
                        totalQuantity1 ++;
                    }
                }
            }

            chartData.Add(totalOrder.ToString());
            chartDataOrder.Add(totalQuantity1.ToString());
            // Add total order from 3h to 24h

            for (int i = 1; i < hours.Count ; i++)
            {
                decimal total = 0;
                decimal totalQuantity = 0;
                foreach (var item in Orderslist)
                {
                    if (item.orderDate.Year == dateSelect.Year && item.orderDate.Month == dateSelect.Month &&
                        item.orderDate.Day == dateSelect.Day)
                    {
                        if (item.orderDate.Hour >= hours[i] && item.orderDate.Hour < hours[i + 1])
                        {
                            total += item.total;
                            totalQuantity++;
                        }
                    }
                }
                chartData.Add(total.ToString());
                chartDataOrder.Add(totalQuantity.ToString());
            }
            ChartDataOrder = ConvertListToStringChartData(chartDataOrder);
            ChartDataRevenue = ConvertListToStringChartData(chartData);
        }
        public void LastDateTime(DateTime dateSelect)
        {
            LabelTipsRevenue = "Revenue ($)";
            LabelTipsOrder = "Quantity Of Order";
            List<String > ListLabel = DivideDaySuit(dateSelect);

            ChartLabelRevenue = ConvertListToStringChartData(ListLabel);
            ChartLabelOrder = ConvertListToStringChartData(ListLabel);

            List<DateTime> ListDateFromString = new List<DateTime>();
            foreach (var item in ListLabel)
            {
                DateTime date = DateTime.ParseExact(item, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ListDateFromString.Add(date);
            }

            List<String> chartData = new List<String>();
            List<String> chartDataOrder = new List<String>();
         

            for (int i = 1; i < ListDateFromString.Count; i++)
            {
                decimal total = 0;
                decimal totalQuantity = 0;
                foreach (var item in Orderslist)
                {                
                        if (item.orderDate.Date >= ListDateFromString[i-1].Date && item.orderDate.Date < ListDateFromString[i].Date)
                        {
                            total += item.total;
                            totalQuantity++;
                        }              
                }
                chartData.Add(total.ToString());
                chartDataOrder.Add(totalQuantity.ToString());
            }
            ChartDataOrder = ConvertListToStringChartData(chartDataOrder);
            ChartDataRevenue = ConvertListToStringChartData(chartData);
        }
        // Help me to divide  point on chart line rely on time
        public List<String> DivideDaySuit( DateTime lastTime )
        {
            DateTime currentDate = DateTime.Now;

            TimeSpan totalDuration = currentDate - lastTime;
            double intervalDays = totalDuration.TotalDays / 8;

            List<string> timePoints = new List<string>();

            for (int i = 0; i <= 8; i++)
            {
                DateTime timePoint = lastTime.AddDays(intervalDays * i);
                timePoints.Add(timePoint.ToString("dd/MM/yyyy"));
            }
            timePoints = new HashSet<string>(timePoints).ToList();
            return timePoints;
        }

        public List<String> getDayNameYearAgo()
        {
            DateTime yearsAgo = DateTime.Now.AddYears(-1);
            DateTime currentDate = DateTime.Now;

            TimeSpan totalDuration = currentDate - yearsAgo;
            double intervalDays = totalDuration.TotalDays / 11;

            List<string> timePoints = new List<string>();

            for (int i = 0; i <= 11; i++)
            {
                DateTime timePoint = yearsAgo.AddDays(intervalDays * i);
                timePoints.Add(timePoint.ToString("dd/MM/yyyy"));
            }
            //timePoints.RemoveAt(0);
            //timePoints.Add(DateTime.Now.ToString("dd/MM/yyyy"));
            timePoints = new HashSet<string>(timePoints).ToList();
            return timePoints;
        }

        public List<int> ConvertStringToList(String array)
        {
            string[] parts = array.Split(new[] { ", " }, StringSplitOptions.None);

            List<int> hours = new List<int>();

            foreach (var part in parts)
            {
                string cleanPart = part.Trim(new[] { '\'', 'h' });
                if (int.TryParse(cleanPart, out int hour))
                {
                    hours.Add(hour);
                }
            }
            return hours;

        }

        public String ConvertListToStringChartData(List<String> data)
        {
            ChartDataRevenue = string.Join(", ", data.Select(day => $"'{day}'"));
            return ChartDataRevenue;
        }

    }
}
