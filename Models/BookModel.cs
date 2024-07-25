using System;

namespace BookShop.Models
{
	public class BookModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string Publisher { get; set; }
		public DateTime PublishDate { get; set; } = DateTime.Now;
		public Genre Genre { get; set; }
		public string Language { get; set; } = "PL";
		public string Description { get; set; }
		public decimal Price { get; set; }
	}
	public enum Genre
	{
		Fantastyka, Kryminał, Akcja, Historyczny, Futurystyczny
	}
}