﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace BookShop.Utils
{
	internal class DateConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			
			DateTime date = (DateTime)value;
			return date.ToShortDateString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string strval = value.ToString();
			DateTime date = (DateTime)value;
			return date;
		}
	}
}
