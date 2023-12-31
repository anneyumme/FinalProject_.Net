﻿using FinalProject_.Net.Model;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_.Net.Data;

public class MyDbContext : DbContext
{
	public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
	{
	}

	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderDetail> OrderDetails { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Saler> Salers { get; set; }
	public DbSet<Role> Roles { get; set; }
	
}