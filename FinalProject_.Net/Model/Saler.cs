using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace FinalProject_.Net.Model
{
    public class Saler
    {
        [Key]
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string? AdditionalInfo { get; set; }
        public byte[]? Avatar { get; set; }
        public string? Password { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Role Roles { get; set; }


    }
}
