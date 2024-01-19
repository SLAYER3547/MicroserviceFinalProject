using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
namespace Mango.Services.CatalogAPI.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class SampleController : ControllerBase
	{
		private readonly IConnectionMultiplexer _redisConnection;

		public SampleController(IConnectionMultiplexer redisConnection)
		{
			_redisConnection = redisConnection;
			
		}

		[HttpGet]
		public IActionResult Get()
		{
			// Redis bağlantısını kullanmak için _redisConnection'ı kullanabilirsiniz
			var database = _redisConnection.GetDatabase();
			var value = database.StringGet("sampleKey");
			database.StringSet("testKey", "Merhaba Serdar Hocam!");
			database.StringSet("testKey2", "Merhaba Hocam!");
			return Ok(value);
		}
	}
}
