using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BookShop.Views
{
	public class Controls
	{
		delegate void MouseEvent(object sender, RoutedEventArgs e);
		public static Button GetAddButton(RoutedEventHandler add_Click, 
			MouseButtonEventHandler addBook_MouseLeftButtonDown, MouseButtonEventHandler addBook_MouseLeftButtonUp)
		{
			Button button = new Button
			{
				Name = "Add",
				Content = "Dodaj"
			};
			button.Click += add_Click;
			StackPanel buttonContent = new StackPanel();
			Image image = new();
			BitmapImage bitmap = new BitmapImage();
			bitmap.BeginInit();
			bitmap.UriSource = new Uri("E:\\Dokumenty\\Studia\\Semestr VI\\ŚPAWiM\\Projekt\\img\\plus.png");
			bitmap.EndInit();
			image.Stretch = Stretch.Fill;
			image.Source = bitmap;
			buttonContent.Children.Add(image);
			button.Width = 40;
			button.Height = 40;
			button.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
			button.Content = buttonContent;
			button.PreviewMouseLeftButtonDown += addBook_MouseLeftButtonDown;
			button.PreviewMouseLeftButtonUp += addBook_MouseLeftButtonUp;
			return button;
		}

		public static Button GetEditButton(RoutedEventHandler edit_Click,
			MouseButtonEventHandler editBook_MouseLeftButtonDown, MouseButtonEventHandler editBook_MouseLeftButtonUp)
		{
			Button button = new()
			{
				Name = "Edit",
				Content = "Edytuj"
			};
			button.Click += edit_Click;
			StackPanel buttonContent = new StackPanel();
			Image image = new();
			BitmapImage bitmap = new BitmapImage();
			bitmap.BeginInit();
			bitmap.UriSource = new Uri("E:\\Dokumenty\\Studia\\Semestr VI\\ŚPAWiM\\Projekt\\img\\edit.png");
			bitmap.EndInit();
			image.Stretch = Stretch.Fill;
			image.Source = bitmap;
			buttonContent.Children.Add(image);
			button.Width = 40;
			button.Height = 40;
			button.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
			button.Content = buttonContent;
			button.PreviewMouseLeftButtonDown += editBook_MouseLeftButtonDown;
			button.PreviewMouseLeftButtonUp += editBook_MouseLeftButtonUp;
			return button;
		}

		public static StackPanel CreateErrorControl(string text, float margin)
		{
			StackPanel errorControl = new StackPanel();
			errorControl.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
			TextBlock textBlock = new TextBlock { Text = text };
			textBlock.Margin = new Thickness(10);
			textBlock.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
			errorControl.Children.Add(textBlock);
			errorControl.Margin = new Thickness(130, 10 + margin, 0, 0);
			return errorControl;
		}
	}
}
