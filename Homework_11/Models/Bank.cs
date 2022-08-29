using System.Collections;
using System.Collections.Generic;
using Homework_11.Models.Clients;
using Homework_11.ViewModels;
using NLog;

namespace Homework_11.Models;

public class Bank
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Наименование Банка.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// База клиентов
    /// </summary>
    public IClientsRepository ClientsRepository { get; set; }

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
        logger.Info($"{_worker}. Добавление клиента: ID={client.Id}, Имя={client.FirstName}, Фамилия={client.LastName}, " +
                     $"Отчество={client.MiddleName}, Пасспортные данные: серия={client.PassportData.Serie}, номер={client.PassportData.Number}, " +
                     $"Телефон={client.PhoneNumber}");

        ClientsRepository.InsertClient(client);
    }
    
    public void EditClient(Client client)
    {
        logger.Info($"{_worker}. Редактирование клиента: ID={client.Id}, Имя={client.FirstName}, Фамилия={client.LastName}, " +
                    $"Отчество={client.MiddleName}, Пасспортные данные: серия={client.PassportData.Serie}, номер={client.PassportData.Number}, " +
                    $"Телефон={client.PhoneNumber}");

        ClientsRepository.UpdateClient(client);
    }
    
    public void DeleteClient(Client client)
    {
        logger.Info($"{_worker}. Удаление клиента: ID={client.Id}, Имя={client.FirstName}, Фамилия={client.LastName}, " +
                    $"Отчество={client.MiddleName}, Пасспортные данные: серия={client.PassportData.Serie}, номер={client.PassportData.Number}, " +
                    $"Телефон={client.PhoneNumber}");
        
        ClientsRepository.DeleteClient(client.Id);
    }
    
    
    
}
