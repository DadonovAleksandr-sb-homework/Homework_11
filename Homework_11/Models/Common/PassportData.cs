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

    private int _serie;

    /// <summary>
    /// Серия
    /// </summary>
    public int Serie { get; set; }
    

    private int _number;

    /// <summary>
    /// Номер
    /// </summary>
    public int Number { get; set; }
    
    /// <summary>
    /// Проверка, являются ли вводимые данные серией паспорта
    /// </summary>
    /// <param name="series">Серия</param>
    /// <returns></returns>
    public static bool IsSeries(string value)
    {
        int serie;
        
        if (!int.TryParse(value, out serie))
            return false;

        if (serie < MinSeriesValue || serie > MaxSeriesValue)
        {
            logger.Debug($"Число {serie} не является корректным для серии паспорта");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверка, являются ли вводимые данные номером паспорта
    /// </summary>
    /// <param name="number">Номер</param>
    /// <returns></returns>
    public static bool IsNumber(string value)
    {
        int number;
        
        if (!int.TryParse(value, out number))
            return false;
        
        if (number < MinNumberValue || number > MaxNumberValue)
        {
            logger.Debug($"Число {number} не является корректным для номера паспорта");
            return false;
        }
        return true;
    }

    public PassportData() {}
    
    /// <summary>
    /// Создаем пасспорт с серией и номером
    /// </summary>
    /// <param name="series">Серия</param>
    /// <param name="number">Номер</param>
    public PassportData(int serie, int number)
    {
        logger.Debug($"Вызов конструктора {GetType().Name} c параметрами: серия {serie}, номер {number}");
        Serie = serie;
        Number = number;
        //SetData(serie, number);
    }

    public override string ToString()
    {
        return $"{Serie}-{Number}";
    }
}