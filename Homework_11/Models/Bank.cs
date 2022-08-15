using System.Collections;
using System.Collections.Generic;
using Homework_11.Models.Clients;

namespace Homework_11.Models;

public class Bank
{
    /// <summary>
    /// Наименование Банка.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// База клиентов
    /// </summary>
    public IClientsRepository ClientsRepository { get; private set; }

    private Worker.Worker _worker;
    public Bank(string name, IClientsRepository clientsRepository, Worker.Worker worker)
    {
        Name = name;
        ClientsRepository = clientsRepository;
        _worker = worker;
    }

    public IEnumerable<Client> GetClientsInfo()
    {
        var clientsInfo = new List<Client>();
        foreach (var client in ClientsRepository.GetAllClients())
        {
            clientsInfo.Add(_worker.GetClientInfo(client));
        }

        return clientsInfo;
    }
    
    
}
