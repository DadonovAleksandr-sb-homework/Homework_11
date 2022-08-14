using System;
using NLog;

namespace Homework_11.Models.Common;
/// <summary>
/// Паспортные данные
/// </summary>
public class PassportData
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    public const int MinSeriesValue = 1;
    public const int MaxSeriesValue = 9999;

    public const int MinNumberValue = 1;
    public const int MaxNumberValue = 999999;

    private int _series;
    /// <summary>
    /// Серия
    /// </summary>
    public int Series { get { return _series; } }

    private int _number;
    /// <summary>
    /// Номер
    /// </summary>
    public int Number { get { return _number; } }

    /// <summary>
    /// Проверка, являются ли вводимые данные серией паспорта
    /// </summary>
    /// <param name="series">Серия</param>
    /// <returns></returns>
    public static bool IsSeries(int series)
    {
        if (series < MinSeriesValue || series > MaxSeriesValue)
        {
            logger.Debug($"Число {series} не является корректным для серии паспорта");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверка, являются ли вводимые данные номером паспорта
    /// </summary>
    /// <param name="number">Номер</param>
    /// <returns></returns>
    public static bool IsNumber(int number)
    {
        if (number < MinNumberValue || number > MaxNumberValue)
        {
            logger.Debug($"Число {number} не является корректным для номера паспорта");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Создаем пасспорт с серией и номером
    /// </summary>
    /// <param name="series">Серия</param>
    /// <param name="number">Номер</param>
    public PassportData(int series, int number)
    {
        logger.Debug($"Вызов конструктора {GetType().Name} c параметрами: серия {series}, номер {number}");
        SetData(series, number);
    }

    /// <summary>
    /// Корректировка пасспортных данных
    /// </summary>
    /// <param name="series">Серия</param>
    /// <param name="number">Номер</param>
    public void CorrectData(int series, int number)
    {
        logger.Debug($"Изменение паспортных данных: серия {series}, номер {number}");
        SetData(series, number);
    }

    /// <summary>
    /// Установка паспортных данных
    /// </summary>
    /// <param name="series">Серия</param>
    /// <param name="number">Номер</param>
    private void SetData(int series, int number)
    {
        CheckData(series, number);
        _series = series;
        _number = number;
    }

    /// <summary>
    /// Проверка валидности паспортных данных
    /// </summary>
    /// <param name="series">Серия</param>
    /// <param name="number">Номер</param>
    private void CheckData(int series, int number)
    {
        // проверка серии
        if (!IsSeries(series))
        {
            throw new ArgumentException($"{nameof(series)} не может быть меньше {MinSeriesValue} и больше {MaxSeriesValue}");
        }

        // проверка номера
        if (!IsNumber(number))
        {
            throw new ArgumentException($"{nameof(number)} не может быть меньше {MinNumberValue} и больше {MaxNumberValue}");
        }

    }

    public override string ToString()
    {
        return $"{Series}-{Number}";
    }
}