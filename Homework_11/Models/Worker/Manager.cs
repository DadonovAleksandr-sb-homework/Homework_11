using Homework_11.Models.Clients;
using Homework_11.ViewModels;

namespace Homework_11.Models.Worker;

public class Manager : Worker
{
    public Manager()
    {
        DataAccess = new RoleDataAccess(true, true, true);
    }
    public override ClientInfo GetClientInfo(Client client)
    {
        var clientInfo = new ClientInfo(client);
        clientInfo.StringPassportData = client.PassportData.ToString();
        return clientInfo;
    }
}