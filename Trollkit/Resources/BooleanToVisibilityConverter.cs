﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Trollkit.Resources
{
	public class BooleanToVisibilityConverter : IValueConverter
	{
		private object GetVisibility(object value)
		{
			if (!(value is bool))
				return Visibility.Hidden;
			bool objValue = (bool)value;
			if (!objValue)
			{
				return Visibility.Visible;
			}
			return Visibility.Hidden;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return GetVisibility(value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
