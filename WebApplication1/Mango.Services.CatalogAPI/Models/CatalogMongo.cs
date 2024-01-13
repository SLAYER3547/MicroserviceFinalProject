using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace Mango.Services.CatalogAPI.Models
{
	public class CatalogMongo
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
		public string ProductId { get; set; }
		public string Product { get; set; }
		public string Category { get; set; }
		public string Price { get; set; }
		public string Description { get; set; }
		public bool StockStatus { get; set; }
	}
}
