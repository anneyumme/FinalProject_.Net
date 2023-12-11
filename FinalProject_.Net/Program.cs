using FinalProject_.Net.Data;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_.Net
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddDbContext<MyDbContext>(options =>
							options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddAuthentication("UserLoginCookie").AddCookie("UserLoginCookie", options =>
			{
				options.Cookie.Name = "UserLoginCookie";
				options.LoginPath = "/Account/Login";
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapRazorPages();
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/admin/product/create");
                return Task.CompletedTask;
            });

            app.Run();
		}
	}
}