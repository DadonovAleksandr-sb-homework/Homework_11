using System;
using System.Globalization;
using System.Windows.Data;

namespace Homework_11.Infrastructure.Convertors;

public class SelectedItemToFontSize : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is Boolean flag)) return null;
        if (flag) return 16;
        return 14;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}