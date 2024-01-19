using RabbitMQ.Client;
using System.Text;

namespace Mango.Services.CatalogAPI
{

	public class RabbitMQService : IDisposable
	{
		private readonly IConnection _connection;
		private readonly IModel _channel;

		public RabbitMQService()
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.QueueDeclare(queue: "Ne mutlu Fenerbahçeli ve Serdar hocanın orencisi olanlara", durable: false, exclusive: false, autoDelete: false, arguments: null);
		}

		public void SendMessage(string message)
		{
			var body = Encoding.UTF8.GetBytes(message);
			_channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
			Console.WriteLine($" [x] Sent '{message}'");
		}

		public void Dispose()
		{
			_channel.Close();
			_connection.Close();
		}
	}

}
