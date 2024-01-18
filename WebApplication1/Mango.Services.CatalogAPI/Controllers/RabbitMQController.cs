using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CatalogAPI.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class RabbitMQController : ControllerBase
	{
		private readonly RabbitMQService _rabbitMQService;

		public RabbitMQController(RabbitMQService rabbitMQService)
		{
			_rabbitMQService = rabbitMQService;
		}

		[HttpGet]
		public IActionResult SendMessage()
		{
			_rabbitMQService.SendMessage("Hello, RabbitMQ from ASP.NET Core!");
			return Ok("Message sent to RabbitMQ");
		}
	}

}
