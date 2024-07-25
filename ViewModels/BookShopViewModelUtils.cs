using BookShop.Models;
using BookShop.Validators;
using BookShop.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace BookShop
{
	public partial class BookShopViewModel
	{
		private Dictionary<string, int> errorsMargins = new()
		{
			{"Title", 0},
			{"Author",50},
			{"Publisher",100},
			{"Data",150},
			{"Language",250},
			{"Price",300}
		};
		private void enterValueToForm()
		{
			UIElementCollection sp = BookForm.Children;
			string[] values =
			{
				Book.Title, Book.Author, Book.Publisher, Book.PublishDate.ToString(CultureInfo.CurrentCulture)[..10],
				Book.Genre.ToString(), Book.Language, Book.Price.ToString(CultureInfo.CurrentCulture), Book.Description
			};
			for (int i = 0; i < sp.Count - 2; i++)
			{
				if (sp[i] is Button) break;
				UIElement? a = ((StackPanel)sp[i]).Children[1];
				if (a is TextBox box)
					box.Text = values[i];
				if (a is ComboBox)
					getGenres();
			}
		}

		private void getGenres()
		{
			ComboBox cb = (ComboBox)((StackPanel)BookForm.Children[4]).Children[1];
			foreach (string genre in Enum.GetNames(typeof(Genre)))
			{
				ComboBoxItem item = new ComboBoxItem { Content = genre };
				if (Book.Genre.ToString() == genre)
					item.IsSelected = true;

				cb.Items.Add(item);
			}
		}

		private void fromFormToBook(UIElementCollection form)
		{
			Book.Title = ((TextBox)((StackPanel)form[0]).Children[1]).Text;
			Book.Author = ((TextBox)((StackPanel)form[1]).Children[1]).Text;
			Book.Publisher = ((TextBox)((StackPanel)form[2]).Children[1]).Text;
			Book.PublishDate = DateTime.Parse(((TextBox)((StackPanel)form[3]).Children[1]).Text);
			Book.Genre = (Genre)Enum.Parse(typeof(Genre), ((ComboBox)((StackPanel)form[4]).Children[1]).Text);
			Book.Language = ((TextBox)((StackPanel)form[5]).Children[1]).Text;
			Book.Price = decimal.Parse(((TextBox)((StackPanel)form[6]).Children[1]).Text);
			Book.Description = ((TextBox)((StackPanel)form[7]).Children[1]).Text;
		}

		private void clearForm(UIElementCollection form)
		{
			((TextBox)((StackPanel)form[0]).Children[1]).Text = "";
			((TextBox)((StackPanel)form[1]).Children[1]).Text = "";
			((TextBox)((StackPanel)form[2]).Children[1]).Text = "";
			((TextBox)((StackPanel)form[3]).Children[1]).Text = DateTime.Now.ToShortDateString();
			//(Genre)Enum.Parse(typeof(Genre), ((ComboBox)((StackPanel)form[4]).Children[1]).Text) = Models.Genre.Fantastyka;
			((TextBox)((StackPanel)form[5]).Children[1]).Text = "PL";
			((TextBox)((StackPanel)form[6]).Children[1]).Text = "0,00";
			((TextBox)((StackPanel)form[7]).Children[1]).Text = "";
		}

		private void refreshBooksList()
		{
			BooksList.ItemsSource = context.Books.Local.ToObservableCollection();
		}

		private bool validate()
		{
			UIElementCollection a = BookForm.Children;
			StackPanel[] stackPanels = new StackPanel[8];
			for (int i = 0; i < 8; i++)
				stackPanels[i] = (StackPanel)a[i];

			BookFormValidator validator = new(stackPanels);
			if (validator.Validate())
				return true;
			
			validationOverlay.Children.Clear();
			Dictionary<string, string> errors = validator.NotValidElements;

			foreach (KeyValuePair<string,int> margin in errorsMargins)
			{
				if (errors.TryGetValue(margin.Key, out string? error))
					validationOverlay.Children.Add(Controls.CreateErrorControl(error, margin.Value));
			}

			return false;
		}

		private void DeleteBook_MouseLeftButtonDown(object sender, RoutedEventArgs e)
		{
			Animations.DeleteButtonAnimation_Down((Button)sender);
		}

		private void DeleteBook_MouseLeftButtonUp(object sender, RoutedEventArgs e)
		{
			Animations.DeleteButtonAnimation_Up((Button)sender);
		}

		private void EditBook_MouseLeftButtonDown(object sender, RoutedEventArgs e)
		{
			Animations.EditButtonAnimation_Down((Button)sender);
		}

		private void EditBook_MouseLeftButtonUp(object sender, RoutedEventArgs e)
		{
			Animations.EditButtonAnimation_Up((Button)sender);
		}

		private void AddBook_MouseLeftButtonDown(object sender, RoutedEventArgs e)
		{
			Animations.AddButtonAnimation_Down((Button)sender);
		}

		private void AddBook_MouseLeftButtonUp(object sender, RoutedEventArgs e)
		{
			Animations.AddButtonAnimation_Up((Button)sender);
		}
	}
}