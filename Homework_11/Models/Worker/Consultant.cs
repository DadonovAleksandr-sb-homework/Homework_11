using Homework_11.Models.Clients;
using Homework_11.ViewModels;

namespace Homework_11.Models.Worker;

public class Consultant : Worker
{
    public Consultant()
    {
        DataAccess = new RoleDataAccess(
            new CommandsAccess
            {
                AddClient = false,
                EditClient = true,
                DelClient = false
            }, 
            new EditFieldsAccess()
            {
                FirstName = false,
                LastName = false,
                MidleName = false,
                PassortData = false,
                PhoneNumber = true
            });
    }
    
    public override ClientInfo GetClientInfo(Client client)
    {
        var clientInfo = new ClientInfo(client);
        clientInfo.PassportSerie = "****";
        clientInfo.PassportNumber = "******";
        return clientInfo;
    }

    public override string ToString()
    {
        return "Консультант";
    }
}