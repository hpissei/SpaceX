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

		//ILogger<HomeController> logger,
		public HomeController( IHttpClientFactory httpClient, IConfiguration Configuration)
		{
			//_logger = logger;
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
// To add test methods.
// To add reactjs app to display all past & upcoming launches

// Add a feature to your React application that allows the user to click on a launch and
// view the details of that specific launch.

// Add a button or link to your React application that, when clicked, calls your API
// endpoint and displays the details of the selected launch.

// To see if we can optimize code.
// To commit the changes to git 
// To see if we can optimize code.
// To add readme for building and running the projects.
// To check if we can add docker support.



