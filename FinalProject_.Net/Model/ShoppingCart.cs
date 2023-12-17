using System.ComponentModel.DataAnnotations;

namespace FinalProject_.Net.Model;

public class ShoppingCart
{
    [Key]
    public int Id { get; set; }
    public Product Products { get; set; }

}