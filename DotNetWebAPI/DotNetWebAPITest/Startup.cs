using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetWebAPITest
{
	internal class Startup
	{
		public void Configure() {
			var builder = WebApplication.CreateBuilder();

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddHttpClient();
			builder.Services.AddSwaggerGen();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapControllers();
			//app.UseSwaggerUI(options =>
			//{
			//	options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
			//	options.RoutePrefix = string.Empty;
			//});
			//app.UseSwagger(options =>
			//{
			//	options.SerializeAsV2 = true;
			//});


			app.Run();

		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddHttpClient();
			//everything else the application needs
		}
	}
}