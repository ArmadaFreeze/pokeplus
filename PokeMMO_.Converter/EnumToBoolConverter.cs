using System;
using System.Globalization;
using System.Windows.Data;

namespace PokeMMO_.Converter;

public class EnumToBoolConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value?.Equals(parameter);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (!(value is bool) || !(bool)value) ? Binding.DoNothing : parameter;
	}
}
