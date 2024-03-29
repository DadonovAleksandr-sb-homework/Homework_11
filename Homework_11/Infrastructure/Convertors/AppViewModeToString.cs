﻿using System;
using System.Globalization;
using System.Windows.Data;
using Homework_11.Models;
using Homework_11.Models.Worker;

namespace Homework_11.Infrastructure.Convertors;

public class AppViewModeToString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is Worker worker)) return null;
        return worker switch
        {
            Consultant _ => "консультант",
            Manager _ => "менеджер",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}