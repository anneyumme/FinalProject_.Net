using FinalProject_.Net.Data;
using FinalProject_.Net.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace FinalProject_.Net.Pages.customer
{
    public class IndexModel : PageModel
    {
        public List<Product> listProduct { get; set; }
        public List<double> listPriceSummarize { get; set; }

        public List<Product> ListProductAddToCart { get; set; }


        readonly MyDbContext dbContext;

        [BindProperty]
        public Customer customer { get; set; }

        public IndexModel(MyDbContext db)
        {
            dbContext = db;


        }


        public void OnGet()
        {
            listProduct = dbContext.Products.ToList();
            var ListProductIds = GetCartFromCookie();
            ListProductAddToCart = dbContext.Products.Where(p => ListProductIds.Contains(p.Id)).ToList();
            customer = dbContext.Customers.Find(GetCustomerFromCookie());
            CaculatePrice();
        }
        public IActionResult OnPost()
        {
            var ListProductIds = GetCartFromCookie();
            ListProductAddToCart = dbContext.Products.Where(p => ListProductIds.Contains(p.Id)).ToList();
            CaculatePrice();
            Order order = new Order();

            Saler saler = dbContext.Salers.Where(s=>s.Id == GetSalerId()).FirstOrDefault();

            order.orderDate = DateTime.Now;
            order.paymentMethod = "Cash";
            order.shippingMethod = "Hand on";
            order.status = "Complete";
            order.total = (decimal)listPriceSummarize[3];
            if(GetCustomerFromCookie() != 0)
            {
                dbContext.Customers.Update(customer);
            }
            else
            {
                dbContext.Customers.Add(customer);
            }
            order.Saler = saler;
            order.Customer = customer;
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
            foreach (var item in ListProductAddToCart)
            {
                var orderDetail = new OrderDetail();
                orderDetail.Order = order;
                orderDetail.Product = item;
                orderDetail.Quantity = 1;
                dbContext.OrderDetails.Add(orderDetail);
                dbContext.SaveChanges();
            }

            // Clear cart
            ListProductIds.Clear();
            SetCartCookie(ListProductIds);
            // Clear customer
            setCustomerInformation(0);
            // Redirect to order detail
            //Response.Redirect("/customer/OrderDetail?id=" + order.Id);
            TempData["Success"] = "Order successfully";
            return RedirectToPage("/Index");

        }
                 

        // Get image
        public IActionResult OnGetImage(int id)
        {
            var myproduct = dbContext.Products.Find(id);

            if (myproduct.ImageData != null)
            {
                return File(myproduct.ImageData, "image/bmp");
            }
            else
            {
                return NotFound();
            }
        }

        // Get customer when click on button "Check"
        public IActionResult OnGetCustomer(string phoneNumber)
        {
            customer = dbContext.Customers.Where(c=>c.PhoneNumber == phoneNumber).FirstOrDefault();
            if (customer == null)
            {
                   TempData["Message"] = "This is new customer, please fill all customer information";
                TempData["PhoneNumber"] = phoneNumber;
                   setCustomerInformation(0);

            }
            else
            {
                setCustomerInformation(customer.Id);
            }
            return RedirectToPage("/Index");
        }


        //================================================================================================
        // Store product in cart cookie

        public IActionResult OnGetAddToCart(int productId)
        {
            var cart = GetCartFromCookie();
            if (!cart.Contains(productId))
            {
                cart.Add(productId);
                SetCartCookie(cart);
            }
            return RedirectToPage("/Index");
        }

        public IActionResult OnGetRemoveItemFromCart(int idProduct)
        {
            var cart = GetCartFromCookie();
            if (cart.Contains(idProduct))
            {
                cart.Remove(idProduct);
                SetCartCookie(cart);
            }
            return RedirectToPage("/Index");

        }

        private void SetCartCookie(List<int> cart)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true
            };
            var serializedCart = JsonSerializer.Serialize(cart);
            Response.Cookies.Append("cart", serializedCart, options);
        }

        // Get list product in cart from cookie
        private List<int> GetCartFromCookie()
        {
            var cartCookie = Request.Cookies["cart"];
            return string.IsNullOrEmpty(cartCookie)
                ? new List<int>()
                : JsonSerializer.Deserialize<List<int>>(cartCookie);
        }

        //================================================================================================
        // Customer Information

        public void setCustomerInformation(int CustomerId)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true
            };
            var serializedCustomer = JsonSerializer.Serialize(CustomerId);
            Response.Cookies.Append("customer", serializedCustomer, options);

        }

        private int GetCustomerFromCookie()
        {
            var customerCookie = Request.Cookies["customer"];
            return string.IsNullOrEmpty(customerCookie)
                ? 0
                : JsonSerializer.Deserialize<int>(customerCookie);
        }

        //================================================================================================
        // Calculate summarize price
        //  Subtotal
        // Discount
        // Tax
        // TOTAL
        public void CaculatePrice()
        {
            listPriceSummarize = new List<double>();
            double Subtotal = 0;
            double Discount = 0;
            double Tax = 0;
            double Total = 0;
            if (ListProductAddToCart != null)
            {
                foreach (var item in ListProductAddToCart)
                {
                    Subtotal += item.Price;
                }
                Discount = 0;
                Tax = Subtotal * 0.08;
                Total = Subtotal - Discount + Tax;
                listPriceSummarize.Add(Subtotal);
                listPriceSummarize.Add(Discount);
                listPriceSummarize.Add(Tax);
                listPriceSummarize.Add(Total);
            }
        }

        // Clear ORder 
        public IActionResult OnGetClearOrder()
        {
            SetCartCookie(new List<int>());
            setCustomerInformation(0);
            return RedirectToPage("/Index");
        }
        public int GetSalerId()
        {
            if (User.Identity.IsAuthenticated)
            {
                var salerId = Int32.Parse(User.FindFirstValue("Id"));
                return salerId;
            }
            return 0;

        }
    }
}
