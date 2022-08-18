using System;
using System.Text.RegularExpressions;
using NLog;

namespace Homework_11.Models.Common;
/// <summary>
/// Номер телефона
/// </summary>
public class PhoneNumber
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    private string _number;
    /// <summary>
    /// Текстовое представление телефонного номера
    /// </summary>
    public string Number
    {
        get { return _number; }
        set { _number = value; }
    }

    public PhoneNumber() {}

    /// <summary>
    /// Создаем номер телефона из текстовой строки
    /// </summary>
    /// <param name="number"></param>
    public PhoneNumber(string number)
    {
        SetNumber(number);
        logger.Debug($"Вызов конструктора {GetType().Name} c параметрами: номер телефлна {number}");
    }

    /// <summary>
    /// Проверяем, является ли вводимая строка номером телефона
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsPhoneNumber(string number)
    {
        var result = Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        logger.Debug($"Проверка строки {number} на соответствие телефонному номеру: {result}");
        return result;
    }

    /// <summary>
    /// Установка номера телефона
    /// </summary>
    /// <param name="number"></param>
    private void SetNumber(string number)
    {
        CheckNumber(number);
        _number = number;
    }

    /// <summary>
    /// Проверка валидности входных данных
    /// </summary>
    /// <param name="number"></param>
    private void CheckNumber(string number)
    {
        if (string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number))
        {
            logger.Error($"Номер \"{nameof(number)}\" не может быть пустым или пробелом");
            throw new ArgumentException($"Номер \"{nameof(number)}\" не может быть пустым или пробелом");
        }

        if (!IsPhoneNumber(number))
        {
            logger.Error($"Строка \"{nameof(number)}\" не является номером телефона");
            throw new ArgumentException($"Строка \"{nameof(number)}\" не является номером телефона");
        }
    }


    public override string ToString()
    {
        return $"{Number}";
    }
}