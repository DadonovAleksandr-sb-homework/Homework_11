using System;
using System.Globalization;
using System.Windows.Data;
using Homework_11.Models;

namespace Homework_11.Infrastructure.Convertors;

public class AppViewModeToString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is AppViewMode viewMode)) return null;
        return viewMode switch
        {
            AppViewMode.Consultant => "консультант",
            AppViewMode.Manager => "менеджер",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}