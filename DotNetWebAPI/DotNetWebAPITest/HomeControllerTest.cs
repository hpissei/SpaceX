using DotNetWebAPI.Controllers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace DotNetWebAPITest
{
	[TestClass]
	public class HomeControllerTest
	{
		private readonly HomeController _homeController;
		private readonly IHttpClientFactory httpClient;
		private readonly IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


		public HomeControllerTest() 
		{
			var services = new ServiceCollection();
			services.Configure<Startup>(_configuration);//.ConfigureServices();
			services.AddHttpClient();

			var provider = services.BuildServiceProvider();
			httpClient = provider.GetRequiredService<IHttpClientFactory>();


			//var webHost = WebHost.CreateDefaultBuilder()
			//	   .UseStartup<Startup>()
			//	   .Build();

			
			_homeController = new HomeController(httpClient, _configuration);
		}

		[TestMethod]
		public void LaunchTest()
		{
			var result = _homeController.Launch(69).GetAwaiter().GetResult();
			Assert.IsTrue(result.StatusCode.ToString() == "OK");
			Assert.IsNotNull(result.Content.ReadAsStringAsync().Result);
		}

		[TestMethod]
		public void LaunchExceptionTest()
		{
			Assert.ThrowsException<Exception>(() => _homeController.Launch(0));
		}
	}
}