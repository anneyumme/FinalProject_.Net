using System.ComponentModel.DataAnnotations;

namespace FinalProject_.Net.Model;

public class Order
{
	[Key]
	public int Id { get; set; }
	public string status { get; set; }
	public string paymentMethod { get; set; }
	public string shippingMethod { get; set; }
	public decimal total { get; set; }
	public DateTime orderDate { get; set; }
	public ICollection<OrderDetail> OrderDetails { get; set; }

	
}