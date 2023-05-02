using BookShop.Models;
using BookShop.Validators;
using BookShop.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BookShop
{
	public partial class BookShopViewModel
	{
		private void enterValueToForm()
		{
			UIElementCollection sp = BookForm.Children;
			string[] values = { Book.Title, Book.Author, Book.Publisher, Book.PublishDate.ToString()[..10], Book.Genre.ToString(), Book.Language, Book.Price.ToString(), Book.Description };
			for (int i = 0; i < sp.Count - 2; i++)
			{
				if (sp[i] is Button) break;
				var a = ((StackPanel)sp[i]).Children[1];
				if (a is TextBox box)
				{
					box.Text = values[i];
				}
				else if (a is ComboBox)
				{
					getGenres();
				}
			}
		}

		private void getGenres()
		{
			ComboBox cb = (ComboBox)((StackPanel)BookForm.Children[4]).Children[1];
			foreach (string genre in Enum.GetNames(typeof(Genre)))
			{
				ComboBoxItem item = new ComboBoxItem { Content = genre };
				if (Book.Genre.ToString() == genre)
				{
					item.IsSelected = true;
				}
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
			{
				stackPanels[i] = (StackPanel)a[i];
			}
			BookFormValidator validator = new(stackPanels);
			if (validator.Validate())
			{
				return true;
			}
			else
			{
				validationOverlay.Children.Clear();
				Dictionary<string, string> errors = validator.NotValidElements;
				if (errors.ContainsKey("Title"))
				{
					validationOverlay.Children.Add(Controls.CreateErrorControl(errors["Title"], 0));
				}
				if (errors.ContainsKey("Author"))
				{
					validationOverlay.Children.Add(Controls.CreateErrorControl(errors["Author"], 50));
				}
				if (errors.ContainsKey("Publisher"))
				{
					validationOverlay.Children.Add(Controls.CreateErrorControl(errors["Publisher"], 100));
				}
				if (errors.ContainsKey("Data"))
				{
					validationOverlay.Children.Add(Controls.CreateErrorControl(errors["Data"], 150));
				}
				if (errors.ContainsKey("Language"))
				{
					validationOverlay.Children.Add(Controls.CreateErrorControl(errors["Language"], 250));
				}
				if (errors.ContainsKey("Price"))
				{
					validationOverlay.Children.Add(Controls.CreateErrorControl(errors["Price"], 300));
				}
				return false;
			}
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
