using Homework_11.Models.Clients;
using Homework_11.ViewModels;

namespace Homework_11.Models.Worker;

public class Manager : Worker
{
    public Manager()
    {
        DataAccess = new RoleDataAccess(
            new CommandsAccess
            {
                AddClient = true,
                EditClient = true,
                DelClient = true
            }, 
            new EditFieldsAccess()
            {
                FirstName = true,
                LastName = true,
                MidleName = true,
                PassortData = true,
                PhoneNumber = true
            });
    }
    public override ClientInfo GetClientInfo(Client client)
    {
        var clientInfo = new ClientInfo(client);
        clientInfo.PassportSerie = client.PassportData.Serie.ToString();
        clientInfo.PassportNumber = client.PassportData.Number.ToString();
        return clientInfo;
    }

    public override string ToString()
    {
        return "Менеджер";
    }
}