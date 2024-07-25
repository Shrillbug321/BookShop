using BookShop.DataBase;
using BookShop.Models;
using BookShop.Views;
using HtmlAgilityPack;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookShop
{
	public partial class BookShopViewModel
	{
		private ListBox BooksList;
		private ListBox BookDetails;
		private StackPanel BookForm;
		private static BookShopViewModel instance;
		private BooksContext context;
		private BookModel Book; 
		private WebView2 WebView;
		private Button AddBookButton;
		private Canvas validationOverlay;
		public static BookShopViewModel INSTANCE { get; set; }
		public BookShopViewModel()
		{
			if (INSTANCE != null)
				return;
			INSTANCE = this;
		}

		public void Initialize(ListBox booksList, ListBox bookDetails, BooksContext context,
			StackPanel bookForm, WebView2 webView, Button addBookButton, Canvas validationOverlay)
		{
			BooksList = booksList;
			BookDetails = bookDetails;
			BookForm = bookForm;
			this.context = context;
			WebView = webView;
			AddBookButton = addBookButton;
			Book = new BookModel();
			this.validationOverlay = validationOverlay;
		}
		#region BooksList
		private async void BooksList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Animations.MoveX(BooksList, 0, 600, 0.5f);
			await Utils.Utils.Wait(500);
			Book = (BookModel)BooksList.SelectedItem;
			BooksList.Visibility = Visibility.Hidden;
			List<BookModel> books = new List<BookModel> { Book };
			BookDetails.ItemsSource = books;
			Animations.MoveX(BookDetails, 480, 0, 0.5f);
			BookDetails.Visibility = Visibility.Visible;
			WebView.Visibility = Visibility.Visible;
		}

		private void BooksList_VisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			AddBookButton.Visibility = BooksList.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
		}

		private void ShowBookForm_Edit_Click(object sender, RoutedEventArgs e)
		{
			Book = (BookModel)((ListBoxItem)BooksList.ContainerFromElement((Button)sender)).Content;
			BookDetails.Visibility = Visibility.Hidden;
			BooksList.Visibility = Visibility.Hidden;
			BookForm.Visibility = Visibility.Visible;
			Animations.MoveY(BookForm, -480, 0, 0.5f);
			BookForm.Children.Add(Controls.GetEditButton(Edit_Click, EditBook_MouseLeftButtonDown, EditBook_MouseLeftButtonUp));
			enterValueToForm();
		}

		private void ShowBookForm_Add_Click(object sender, RoutedEventArgs e)
		{
			BookDetails.Visibility = Visibility.Hidden;
			BooksList.Visibility = Visibility.Hidden;
			BookForm.Visibility = Visibility.Visible;
			Animations.MoveY(BookForm, -480, 0, 0.5f);
			Book = new BookModel();
			BookForm.Children.Add(Controls.GetAddButton(Add_Click, AddBook_MouseLeftButtonDown, AddBook_MouseLeftButtonUp));
			clearForm(BookForm.Children);
			getGenres();
		}

		private void DeleteBook_Click(object sender, RoutedEventArgs e)
		{
			Book = (BookModel)((ListBoxItem)BooksList.ContainerFromElement((Button)sender)).Content;
			string areYouSure = $"Czy na pewno chcesz usunąć {Book.Title}?";
			if (MessageBox.Show(areYouSure, "Potwierdź usunięcie", MessageBoxButton.YesNo) ==
			    MessageBoxResult.No) return;
			context.Books.Remove(Book);
			context.SaveChanges();
			refreshBooksList();
		}
		#endregion
		#region BookDetails
		private async void WebView2_Loaded(object sender, RoutedEventArgs e)
		{
			await WebView.EnsureCoreWebView2Async();
			WebView.Source = new Uri("file:///E:/Dokumenty/Studia/Tymczasowy/BookShop/Web/book.html");
		}

		private async void WebView_VisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (WebView.Visibility == Visibility.Hidden) return;
			WebClient html = new();
			string url = string.Format($"https://www.bing.com/images/search?q={Book.Title}&first=1&tsc=ImageHoverTitle");
			byte[] b = html.DownloadData(url);
			string a = Encoding.UTF8.GetString(b);
			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(a);
			HtmlNodeCollection? images = doc.DocumentNode.SelectNodes("//img");
			string imagesUrls = "";
			for (int i = 8; i < 12; i++)
			{
				if (images[i].Attributes["src2"] == null)
				{
					imagesUrls = "https://tse2.mm.bing.net/th?q=no+image&amp;w=42&amp;h=42&amp;c=1&amp;p=0&amp;pid=InlineBlock&amp;mkt=pl-PL&amp;cc=PL&amp;setlang=pl&amp;adlt=moderate&amp;t=1|";
					break;
				}
				imagesUrls += images[i].Attributes["src2"].Value+"|";
			}
			await WebView.CoreWebView2.ExecuteScriptAsync($"showImage('{imagesUrls}')");
		}

		private void BookDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			BookDetails.SelectedItem = null;
		}

		private async void ReturnToList_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			if (button.Tag.ToString() == "FromDetails")
			{
				Animations.MoveX(BookDetails, 0, -480, 0.5f);
				Animations.Opacity(WebView);
				await Utils.Utils.Wait(500);
				BookDetails.Visibility = Visibility.Hidden;
				WebView.Visibility = Visibility.Hidden;
				Animations.MoveX(BooksList, 480, 0, 0.5f);
			}
			
			if (button.Tag.ToString() == "FromForm")
			{
				Animations.MoveY(BookForm, 0, 480, 0.5f);
				await Utils.Utils.Wait(500);
				BookForm.Visibility = Visibility.Hidden;
				Animations.MoveY(BooksList, 480, 0, 0.5f);
				BookForm.Children.Remove(BookForm.Children[^1]);
			}
			refreshBooksList();			
			BooksList.Visibility = Visibility.Visible;
			validationOverlay.Visibility = Visibility.Hidden;
		}

		private void ReturnToList_MouseEnter(object sender, RoutedEventArgs e)
		{
			Animations.Rotate((Button)sender);
		}

		private void BookDetails_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
		{
			Button button = new() {	Tag = "FromDetails"	};
			ReturnToList_Click(button, e);
		}
		#endregion
		#region BookForm
		private void Edit_Click(object sender, RoutedEventArgs e)
		{
			if (!validate())
			{
				validationOverlay.Visibility = Visibility.Visible;
				return;
			}
			fromFormToBook(BookForm.Children);			
			context.Books.Update(Book);
			context.SaveChanges();
			BookForm.Children.Remove(BookForm.Children[^1]);
			validationOverlay.Visibility = Visibility.Hidden;
			refreshBooksList();
			ReturnToDetails_Click(sender, e);
		}
		private void Add_Click(object sender, RoutedEventArgs e)
		{
			if (!validate())
			{
				validationOverlay.Visibility = Visibility.Visible;
				return;
			}
			fromFormToBook(BookForm.Children);
			context.Books.Add(Book);
			context.SaveChanges();
			BookForm.Children.Remove(BookForm.Children[^1]);
			validationOverlay.Visibility = Visibility.Hidden;
			ReturnToDetails_Click(sender, e);
		}

		private void ReturnToDetails_Click(object sender, RoutedEventArgs e)
		{
			BookForm.Visibility = Visibility.Hidden;
			Animations.MoveX(BookDetails, -480, 0, 0.5f);
			BookDetails.ItemsSource = new List<BookModel> { Book };
			BookDetails.Visibility = Visibility.Visible;
			WebView.Visibility = Visibility.Visible;
		}
		#endregion
	}
}