using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Homework_11.Infrastructure.Convertors;

public class SelectedItemToForegroundColor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is Boolean flag)) return null;
        if (flag) return new SolidColorBrush(Colors.White);
        return new SolidColorBrush(Colors.Silver) ;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}