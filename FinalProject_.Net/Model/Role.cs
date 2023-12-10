using System.ComponentModel.DataAnnotations;

namespace FinalProject_.Net.Model;

public class Role
{
	[Key]
	public int RoleId { get; set; }
	public string RoleName { get; set; }
}