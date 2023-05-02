using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Transform = System.Windows.Media.Transform;

namespace BookShop.Views
{
	public class Animations
	{
		public static void MoveX(UIElement element, float start, float stop, float duration)
		{
			DoubleAnimation animation = new(start, stop, new Duration(TimeSpan.FromSeconds(duration)));
			Transform transform = new TranslateTransform();
			element.RenderTransform = transform;
			element.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
			transform.BeginAnimation(TranslateTransform.XProperty, animation);
			//transform.BeginAnimation(TranslateTransform.YProperty, animation);
		}
		public static void MoveY(UIElement element, float start, float stop, float duration)
		{
			DoubleAnimation animation = new(start, stop, new Duration(TimeSpan.FromSeconds(duration)));
			Transform transform = new TranslateTransform();
			element.RenderTransform = transform;
			element.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
			transform.BeginAnimation(TranslateTransform.YProperty, animation);
			//transform.BeginAnimation(TranslateTransform.YProperty, animation);
		}

		public static void DeleteButtonAnimation_Down(Button button)
		{
			ColorAnimation animation = new(Color.FromRgb(255,136,125), Color.FromRgb(181, 0, 71), new Duration(TimeSpan.FromSeconds(0.5)));
			button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
		}

		public static void DeleteButtonAnimation_Up(Button button)
		{
			ColorAnimation animation = new(Color.FromRgb(181, 0, 71), Color.FromRgb(255,136,125), new Duration(TimeSpan.FromSeconds(0.5)));
			button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
		}

		public static void EditButtonAnimation_Down(Button button)
		{
			ColorAnimation animation = new(Color.FromRgb(255,255,225), Color.FromRgb(241, 196, 15), new Duration(TimeSpan.FromSeconds(0.5)));
			button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
		}

		public static void EditButtonAnimation_Up(Button button)
		{
			ColorAnimation animation = new(Color.FromRgb(241, 196, 15), Color.FromRgb(255,255,225), new Duration(TimeSpan.FromSeconds(0.5)));
			button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
		}
		public static void AddButtonAnimation_Down(Button button)
		{
			ColorAnimation animation = new(Color.FromRgb(252,255,252), Color.FromRgb(229, 255, 231), new Duration(TimeSpan.FromSeconds(0.5)));
			button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
		}
		public static void AddButtonAnimation_Up(Button button)
		{
			ColorAnimation animation = new(Color.FromRgb(229, 255, 231), Color.FromRgb(252,255,252), new Duration(TimeSpan.FromSeconds(0.5)));
			button.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
		}
		public static void Opacity(UIElement element)
		{
			DoubleAnimation animation = new DoubleAnimation
			{
				From = 0,
				To = 1
			};
			Storyboard.SetTarget(animation, element);
			Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

			Storyboard storyboard = new();
			storyboard.Children.Add(animation);
			storyboard.Begin();
		}

		public static void Rotate(UIElement element)
		{
			DoubleAnimation animation = new(360, 0, new Duration(TimeSpan.FromSeconds(0.5)));
			RotateTransform rotateTransform = new RotateTransform();
			element.RenderTransform = rotateTransform;
			element.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
			rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
		}
	}
}
