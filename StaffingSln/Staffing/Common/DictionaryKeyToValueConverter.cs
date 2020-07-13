using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Common
{
	/// <summary>Конвертер преобразующий ключ в значение по словарю</summary>
	public class DictionaryKeyToValueConverter : Dictionary<object, object>, /* OnPropertyChangedClass,*/ IMultiValueConverter, IValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Length == 1)
			{
				//if (Dictionary?.Contains(values[0]) == true)
				//    return Dictionary[values[0]];

				if (TryGetValue(values[0], out object val))
					return val;

			}
			else if (values.Length > 1 && (values[1] is IDictionary dictionary) && (dictionary.Contains(values[0]) == true))
			{
				return dictionary[values[0]];
			}
			return null;
		}


		//public IDictionary Dictionary { get; private set; }
		//public void SetDictionary(IDictionary dictionary)
		//{
		//    Dictionary = dictionary;
		//    OnPropertyChanged(nameof(Dictionary));
		//}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//if (value != null && Dictionary?.Contains(value) == true)
			//    return Dictionary[value];
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
