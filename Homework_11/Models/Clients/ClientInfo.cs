using Homework_11.Models.Clients;
using Homework_11.Models.Common;
using Microsoft.VisualBasic;

namespace Homework_11.ViewModels;
/// <summary>
/// Информация о клиенте для отображения
/// TODO: чет слишком кучеряво получилось. Прикольно было бы выяснить, может как-то по другому можно?
/// </summary>
public class ClientInfo : Client
{
    public string StringPassportData => PassportSerie + "-" + PassportNumber;
    public string FIO => LastName + " " + FirstName + " " + MiddleName;
    public string PassportSerie { get; set; }
    public string PassportNumber { get; set; }

    public ClientInfo() {}
    public ClientInfo(Client client) 
        : base(client.PhoneNumber, client.PassportData, client.FirstName, client.LastName, client.MiddleName)
    {
        Id = client.Id;
    }
}