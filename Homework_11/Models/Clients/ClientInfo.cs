using Homework_11.Models.Clients;
using Homework_11.Models.Common;

namespace Homework_11.ViewModels;

public class ClientInfo : Client
{
    public string StringPassportData { get; set; }
    public ClientInfo(Client client) 
        : base(client.PhoneNumber, client.PassportData, client.FirstName, client.LastName, client.MiddleName)
    {
    }
}