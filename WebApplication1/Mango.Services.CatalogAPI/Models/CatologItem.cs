namespace Mango.Services.CatalogAPI.Models
{
	public class CatologItem
	{
        public int id { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string StockStatus { get; set; }
    }
}
