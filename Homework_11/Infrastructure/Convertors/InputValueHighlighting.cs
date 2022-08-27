using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Homework_11.Views;

namespace Homework_11.Infrastructure.Convertors;

public class InputValueHighlightingConvertor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is InputValueHighlightingEnum mode)) return null;
        return mode switch
        {
            InputValueHighlightingEnum.Default => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#844eff")),
            InputValueHighlightingEnum.Disable => new SolidColorBrush(Colors.Silver),
            InputValueHighlightingEnum.Error => new SolidColorBrush(Colors.Red),
        };
            
                
            
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}