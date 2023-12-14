using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace FinalProject_.Net.Model
{
	public class Customer
	{
		[Key]
		public int Id { get; set; }
		public string FName { get; set; }
		public string LName { get; set; }
		public string EmailAddress { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
        public ICollection<Order> Orders { get; set; }
	}
}
