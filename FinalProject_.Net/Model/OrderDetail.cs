using System.ComponentModel.DataAnnotations;

namespace FinalProject_.Net.Model;

public class OrderDetail
{
	[Key]
	public int Id { get; set; }
	public Order Order { get; set; }
	public Product Product { get; set; }
	public int Quantity { get; set; }

}