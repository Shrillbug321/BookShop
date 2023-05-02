using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace BookShop.Validators
{
	public class BookFormValidator
	{
		private StackPanel[] StackPanels;
		public Dictionary<string, string> NotValidElements { get; set; }
		public BookFormValidator(StackPanel[] stackPanels)
		{
			StackPanels = stackPanels;
			NotValidElements = new Dictionary<string, string>();
		}
		public bool Validate()
		{
			NotValidElements = new Dictionary<string, string>();
			Title(StackPanels[0]);
			Author(StackPanels[1]);
			Publisher(StackPanels[2]);
			Data(StackPanels[3]);
			Language(StackPanels[5]);
			Price(StackPanels[6]);
			if (NotValidElements.Count == 0)
				return true;
			return false;
		}
		public bool Title(StackPanel title)
		{
			string text = ((TextBox)title.Children[1]).Text;
			if (string.IsNullOrEmpty(text))
			{
				NotValidElements.Add("Title", "Nie wpisano tyułu");
				return false;
			}
			return true;
		}
		public bool Author(StackPanel author)
		{
			string text = ((TextBox)author.Children[1]).Text;
			if (string.IsNullOrEmpty(text))
			{
				NotValidElements.Add("Author","Nie wpisano autora");
				return false;
			}
			if (!text.Contains(' '))
			{
				NotValidElements.Add("Author","Zapomniano wpisać imię lub nazwisko");
				return false;
			}
			return true;
		}
		public bool Publisher(StackPanel publisher)
		{
			string text = ((TextBox)publisher.Children[1]).Text;
			if (string.IsNullOrEmpty(text))
			{
				NotValidElements.Add("Publisher", "Nie wpisano wydawcy");
				return false;
			}
			return true;
		}
		public bool Data(StackPanel data)
		{
			string pattern = @"(0[1-9]|[12][0-9]|3[01]).(0[1-9]|1[1-2]).\d{4}";
			Regex regex = new Regex(pattern);
			string text = ((TextBox)data.Children[1]).Text;
			if (string.IsNullOrEmpty(text))
			{
				NotValidElements.Add("Data","Nie wpisano daty");
				return false;
			}
			if (!regex.IsMatch(text))
			{
				NotValidElements.Add("Data","Proszę wpisać datę w formacie DD.MM.RRRR");
				return false;
			}
			return true;
		}
		public bool Language(StackPanel language)
		{
			string text = ((TextBox)language.Children[1]).Text;
			if (string.IsNullOrEmpty(text))
			{
				NotValidElements.Add("Language","Nie wpisano języka");
				return false;
			}
			if (text.Length != 2)
			{
				NotValidElements.Add("Language","Proszę wpisać dwuliterowy kod");
				return false;
			}
			return true;
		}
		public bool Price(StackPanel price)
		{
			string pattern = @"^[+-]?[0-9]{1,3}(?:.?[0-9]{3})*(?:\,[0-9]{2})?$";
			Regex regex = new Regex(pattern);
			string text = ((TextBox)price.Children[1]).Text;
			if (string.IsNullOrEmpty(text))
			{
				NotValidElements.Add("Price","Nie wpisano ceny");
				return false;
			}
			if (!regex.IsMatch(text))
			{
				NotValidElements.Add("Price","Proszę wpisać cenę w formacie XXX lub XXX,XX");
				return false;
			}
			return true;
		}
	}
}
