using NLog;

namespace Homework_11.Models.Common;

/// <summary>
/// Пользователь системы
/// </summary>
public class Person
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// Отчество
    /// </summary>
    public string MiddleName { get; set; }

    public Person()
    {
        logger.Debug($"Вызов конструктора {GetType().Name} по умолчанию");
    }
    
    public Person(string firstName, string lastName, string middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        
        logger.Debug($"Вызов конструктора {GetType().Name} c параметрами: имя {firstName}, фамилия {lastName}, отчество {middleName}");
    }
}