using DotNetWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

namespace DotNetWebAPI.Controllers
{
	[ApiController]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;


		public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClient, IConfiguration Configuration)
		{
			_logger = logger;
			_httpClientFactory= httpClient;
			_configuration = Configuration;
		}

		[HttpGet]
		[Route("/Launch/{id}")]
		[ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, NoStore = true)]
		public Task<HttpResponseMessage> Launch(int id) {
			if (id <= 0)
			{
				throw new Exception("Id must be greater then 0!");
			}

			return _httpClientFactory.CreateClient().GetAsync($"{_configuration.GetSection("EndpointUrl").Value}launches/{id}");
		}
	}
}