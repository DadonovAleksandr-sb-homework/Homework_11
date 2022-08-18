using System.Collections;
using System.Collections.Generic;
using Homework_11.Models.Clients;
using Homework_11.ViewModels;

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

    /// <summary>
    /// Получение сведений о клиентах
    /// представление зависит от работника
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ClientInfo> GetClientsInfo()
    {
        var clientsInfo = new List<ClientInfo>();
        foreach (var client in ClientsRepository)
        {
            clientsInfo.Add(_worker.GetClientInfo(client));
        }
        return clientsInfo;
    }

    public void AddClient(Client client)
    {
        ClientsRepository.InsertClient(client);
    }
    
    public void EditClient(Client client)
    {
        ClientsRepository.UpdateClient(client);
    }
    
    public void DeleteClient(Client client)
    {
        ClientsRepository.DeleteClient(client.Id);
    }
    
    
    
}
