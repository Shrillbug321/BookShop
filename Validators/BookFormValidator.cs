using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace BookShop.Validators
{
	public class BookFormValidator
	{
		private StackPanel[] stackPanels;
		public Dictionary<string, string> NotValidElements { get; set; }

		public BookFormValidator(StackPanel[] stackPanels)
		{
			this.stackPanels = stackPanels;
			NotValidElements = new Dictionary<string, string>();
		}

		public bool Validate()
		{
			NotValidElements = new Dictionary<string, string>();
			Title(stackPanels[0]);
			Author(stackPanels[1]);
			Publisher(stackPanels[2]);
			Data(stackPanels[3]);
			Language(stackPanels[5]);
			Price(stackPanels[6]);
			return NotValidElements.Count == 0;
		}

		private void Title(StackPanel title)
		{
			if (string.IsNullOrEmpty(TextFromStackPanel(title)))
				NotValidElements.Add("Title", "Nie wpisano tytułu");
		}

		private void Author(StackPanel author)
		{
			string text = TextFromStackPanel(author);
			if (string.IsNullOrEmpty(text))
			{
				NotValidElements.Add("Author", "Nie wpisano autora");
				return;
			}

			if (!text.Contains(' '))
				NotValidElements.Add("Author", "Zapomniano wpisać imię lub nazwisko");
		}

		private void Publisher(StackPanel publisher)
		{
			if (string.IsNullOrEmpty(TextFromStackPanel(publisher)))
				NotValidElements.Add("Publisher", "Nie wpisano wydawcy");
		}

		private void Data(StackPanel data)
		{
			string pattern = @"(0[1-9]|[12][0-9]|3[01]).(0[1-9]|1[1-2]).\d{4}";
			Regex regex = new Regex(pattern);
			string text = TextFromStackPanel(data);
			if (string.IsNullOrEmpty(text))
			{
				NotValidElements.Add("Data", "Nie wpisano daty");
				return;
			}

			if (!regex.IsMatch(text))
				NotValidElements.Add("Data", "Proszę wpisać datę w formacie DD.MM.RRRR");
		}

		private void Language(StackPanel language)
		{
			string text = TextFromStackPanel(language);
			if (string.IsNullOrEmpty(text))
			{
				NotValidElements.Add("Language", "Nie wpisano języka");
				return;
			}

			if (text.Length != 2)
				NotValidElements.Add("Language", "Proszę wpisać dwuliterowy kod");
		}

		private void Price(StackPanel price)
		{
			const string pattern = @"^[+-]?[0-9]{1,3}(?:.?[0-9]{3})*(?:\,[0-9]{2})?$";
			Regex regex = new Regex(pattern);
			string text = TextFromStackPanel(price);
			if (string.IsNullOrEmpty(text))
			{
				NotValidElements.Add("Price", "Nie wpisano ceny");
				return;
			}

			if (!regex.IsMatch(text))
				NotValidElements.Add("Price", "Proszę wpisać cenę w formacie XXX lub XXX,XX");
		}

		private string TextFromStackPanel(StackPanel stackPanel)
		{
			return ((TextBox)stackPanel.Children[1]).Text;
		}
	}
}