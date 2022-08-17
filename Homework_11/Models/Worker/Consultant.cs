using Homework_11.Models.Clients;
using Homework_11.ViewModels;

namespace Homework_11.Models.Worker;

public class Consultant : Worker
{
    public Consultant()
    {
        DataAccess = new RoleDataAccess(false, false, true);
    }
    
    public override ClientInfo GetClientInfo(Client client)
    {
        var clientInfo = new ClientInfo(client);
        clientInfo.StringPassportData = "****-******";
        return clientInfo;
    }
}