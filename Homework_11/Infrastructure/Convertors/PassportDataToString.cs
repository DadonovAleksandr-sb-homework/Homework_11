using System;
using System.Globalization;
using System.Windows.Data;
using Homework_11.Models.Common;
using Homework_11.Models.Worker;

namespace Homework_11.Infrastructure.Convertors;

public class PassportDataToString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is PassportData data)) return null;
        if (parameter is bool flag)
        {
            return flag ? data.ToString() : "****-******";
        }
        return data.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}