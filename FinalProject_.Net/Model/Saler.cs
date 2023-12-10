using System.ComponentModel.DataAnnotations;

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
        public string? Password { get; set; }
        
    }
}
