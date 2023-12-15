using System.ComponentModel.DataAnnotations;

namespace FinalProject_.Net.Model;

public class Role
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Saler> Salers { get; set; }
}