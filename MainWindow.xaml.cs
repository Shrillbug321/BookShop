using BookShop.DataBase;
using BookShop.Models;
using BookShop.Views;
using Microsoft.Web.WebView2.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BookShop
{
	public partial class MainWindow
	{
		WebView2 WebView;
		ListBox BooksList;
		ListBox BookDetails;
		StackPanel BookForm;
		Button AddBookButton;
		private Canvas ValidationOverlay;

		public MainWindow(BooksContext context)
		{
			InitializeComponent();
			Window.Background = new LinearGradientBrush
			{
				StartPoint = new Point(0, 0),
				EndPoint = new Point(0, 1),
				GradientStops = new GradientStopCollection
				{
					new()
					{
						Color = Colors.White,
						Offset = 0,
					},
					new()
					{
						Color = Colors.SlateGray,
						Offset = 1.5,
					}
				}
			};
			List<BookModel> books = context.Books.ToList();

			BooksList = FindResource("BooksList") as ListBox;
			BooksList.Name = "BooksList";
			BooksList.ItemsSource = books;

			BookDetails = FindResource("BookDetails") as ListBox;
			BookDetails.Name = "BookDetails";

			BookForm = FindResource("BookForm") as StackPanel;
			BookForm.Name = "BookForm";

			WebView = (WebView2)FindResource("WebView");
			WebView.RenderTransform = new TranslateTransform(-50, -50);

			AddBookButton = (Button)FindResource("ShowBookForm_Add");
			ValidationOverlay = (Canvas)FindResource("ValidationOverlay");
			Animations.Opacity(BooksList);

			MainGrid.Children.Insert(0, ValidationOverlay);
			MainGrid.Children.Insert(1, BooksList);
			MainGrid.Children.Insert(2, BookDetails);
			MainGrid.Children.Insert(3, BookForm);
			MainGrid.Children.Insert(4, WebView);
			MainGrid.Children.Insert(5, AddBookButton);
			
			BooksList.Visibility = Visibility.Visible;
			BookDetails.Visibility = Visibility.Hidden;
			BookForm.Visibility = Visibility.Hidden;
			WebView.Visibility = Visibility.Hidden;
			AddBookButton.Visibility = Visibility.Hidden;
			ValidationOverlay.Visibility = Visibility.Hidden;
			Panel.SetZIndex(ValidationOverlay, 1);

			WebView.Margin = new Thickness(50, 300, 50, 50);
			BookShopViewModel bse = BookShopViewModel.INSTANCE;
			bse.Initialize(BooksList, BookDetails, context, BookForm, WebView, AddBookButton, ValidationOverlay);
		}
	}
}