using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Common
{
    /// <summary>Конвертер преобразующий ключ в значение по словарю.</summary>
    public class DictionaryKeyToValueConverter : Dictionary<object, object>,  IMultiValueConverter, IValueConverter
	{
		// Первым элементом в параметром values приходит ключ, вторым - словарь.
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			// Если пришёл только ключ, то используется словарь конвертера.
			if (values.Length == 1)
			{

				if (TryGetValue(values[0], out object val))
					return val;

			}
			// Если пришло больше одного элемента, то из первого извлекается ключ, из второго - словарь.
			else if (values.Length > 1 && (values[1] is IDictionary dictionary) && (dictionary.Contains(values[0]) == true))
			{
				return dictionary[values[0]];
			}
			return null;
		}

		// В параметре value должен прийти клю словаря.
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (TryGetValue(value, out object val))
				return val;

			return null;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
