﻿using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace FinalProject_.Net.Model;

public class Product
{
	[Key]
	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public double Price { get; set; }
	public int Stock { get; set; }
	public byte[]? ImageData { get; set; }
	public ICollection<OrderDetail> orderDetails { get; set; }

}