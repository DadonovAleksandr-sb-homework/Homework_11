using Homework_11.Models.Clients;
using Homework_11.Models.Common;

namespace Homework_11.Models.Worker;

public class Consultant : Worker
{
    public override Client GetClientInfo(Client client)
    {
        //client.PassportData = new PassportData(0000, 000000);
        return client;
    }
}