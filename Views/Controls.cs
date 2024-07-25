using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BookShop.Views
{
	public class Controls
	{
		public static Button GetAddButton(RoutedEventHandler add_Click, 
			MouseButtonEventHandler addBook_MouseLeftButtonDown, MouseButtonEventHandler addBook_MouseLeftButtonUp)
		{
			BitmapImage bitmap = new BitmapImage();
			bitmap.BeginInit();
			bitmap.UriSource = new Uri(@"E:\Dokumenty\Studia\Tymczasowy\BookShop\img\plus.png");
			bitmap.EndInit();
			
			Image image = new()
			{
				Stretch = Stretch.Fill,
				Source = bitmap
			};
			
			StackPanel buttonContent = new StackPanel();
			buttonContent.Children.Add(image);
			
			Button button = new Button
			{
				Name = "Add",
				Content = "Dodaj",
				Width = 40,
				Height = 40,
				Background = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
			};
			button.Content = buttonContent;
			button.Click += add_Click;
			button.PreviewMouseLeftButtonDown += addBook_MouseLeftButtonDown;
			button.PreviewMouseLeftButtonUp += addBook_MouseLeftButtonUp;
			return button;
		}

		public static Button GetEditButton(RoutedEventHandler edit_Click,
			MouseButtonEventHandler editBook_MouseLeftButtonDown, MouseButtonEventHandler editBook_MouseLeftButtonUp)
		{
			BitmapImage bitmap = new BitmapImage();
			bitmap.BeginInit();
			bitmap.UriSource = new Uri(@"E:\Dokumenty\Studia\Tymczasowy\BookShop\img\edit.png");
			bitmap.EndInit();
			
			Image image = new()
			{
				Stretch = Stretch.Fill,
				Source = bitmap
			};
			
			StackPanel buttonContent = new StackPanel();
			buttonContent.Children.Add(image);
			
			Button button = new()
			{
				Name = "Edit",
				Content = "Edytuj",
				Width = 40,
				Height = 40,
				Background = new SolidColorBrush(Color.FromRgb(255, 255, 255))
			};
			button.Content = buttonContent;
			button.Click += edit_Click;
			button.PreviewMouseLeftButtonDown += editBook_MouseLeftButtonDown;
			button.PreviewMouseLeftButtonUp += editBook_MouseLeftButtonUp;
			return button;
		}

		public static StackPanel CreateErrorControl(string text, float margin)
		{
			TextBlock textBlock = new TextBlock
			{
				Text = text,
				Margin = new Thickness(10),
				Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
			};
			StackPanel errorControl = new StackPanel
			{
				Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)),
				Margin = new Thickness(130, 10 + margin, 0, 0),
				Children = { textBlock }
			};
			return errorControl;
		}
	}
}