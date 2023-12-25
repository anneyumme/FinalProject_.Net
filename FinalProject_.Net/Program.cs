using FinalProject_.Net.Data;
using FinalProject_.Net.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
			builder.Services.AddScoped<TokenService>();
			builder.Services.AddScoped<PasswordService>();
			builder.Services.AddScoped<EmailService>();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));
                options.AddPolicy("SalerAndAdmin", policy =>
                    policy.RequireRole("Saler", "Admin"));
            });

			builder.Services.Configure<RazorPagesOptions>(options =>
			{
				options.Conventions.AuthorizeFolder("/admin", "AdminOnly"); // Apply your policy here
			});

			builder.Services.Configure<RazorPagesOptions>(options =>
			{
				options.Conventions.AuthorizeFolder("/customer", "SalerAndAdmin"); // Apply your policy here

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

            app.UseExceptionHandler("/Error500");
            app.UseStatusCodePagesWithReExecute("/Error");

            app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

            app.MapRazorPages();
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/customer/");
                return Task.CompletedTask;
            });
           

			app.Run();
		}
	}
}