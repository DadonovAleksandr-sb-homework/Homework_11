using System;
using System.Globalization;
using System.Windows.Data;

namespace Homework_11.Infrastructure.Convertors;

public class SelectedItemToBorderThickness : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is Boolean flag)) return null;
        if (flag) return 4;
        return 2;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}