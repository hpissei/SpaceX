using DotNetWebAPI.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

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
			services.Configure<Startup>(_configuration);
			services.AddHttpClient();

			var provider = services.BuildServiceProvider();
			httpClient = provider.GetRequiredService<IHttpClientFactory>();

			
			_homeController = new HomeController(httpClient, _configuration);
		}

		[TestMethod]
		public void LaunchTest()
		{
			int flight_number = 69;
			var result = _homeController.Launch(flight_number).GetAwaiter().GetResult();
			dynamic data = JsonConvert.DeserializeObject(result);
			Assert.IsNotNull(result);
			Assert.IsTrue(data?.flight_number == flight_number);
		}

		[TestMethod]
		public void LaunchExceptionTest()
		{
			Assert.ThrowsException<Exception>(() => _homeController.Launch(0));
		}

		[TestMethod]
		public void LaunchNotFoundTest()
		{
			var result = _homeController.Launch(190).GetAwaiter().GetResult();
			Assert.IsTrue(string.Compare(result, "{\"error\":\"Not Found\"}") == 0);
		}
	}
}
