using Homework_11.Models.Common;

namespace Homework_11.Models.Clients;

public class Client : Person
{
    private int _id;
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }


    private PhoneNumber _phoneNumber;
    /// <summary>
    /// Номер телефона
    /// </summary>
    public PhoneNumber PhoneNumber { get => _phoneNumber; }

    private PassportData _passportData;
    /// <summary>
    /// Паспортные данные
    /// </summary>
    public PassportData PassportData { get => _passportData; }

    /// <summary>
    /// Создаем клиента
    /// </summary>
    /// <param name="phoneNumber">Номер тедефона</param>
    /// <param name="passportData">Паспортный данные</param>
    /// <param name="firstName">Имя</param>
    /// <param name="lastName">Фамилия</param>
    /// <param name="middleName">отчество</param>
    public Client(PhoneNumber phoneNumber, PassportData passportData, string firstName, string lastName, string middleName = "")
        : base(firstName, lastName, middleName)
    {
        _phoneNumber = phoneNumber;
        _passportData = passportData;
    }
}