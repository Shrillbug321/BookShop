using BookShop.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public decimal Price { get; set; } = 0.00m;
	}
	public enum Genre
	{
		Fantastyka, Kryminał, Akcja, Historyczny, Futurystyczny
	}
}
