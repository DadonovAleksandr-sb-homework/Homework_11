using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NLog;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Homework_11.Models.Clients;

public class ClientsFileRepository: IClientsRepository
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// Список клиентов
    /// </summary>
    List<Client?> _clients;
    /// <summary>
    /// Файл репозитория
    /// </summary>
    string _path;

    /// <summary>
    /// Конструктор репозитория
    /// </summary>
    /// <param name="path">Путь к файлу-репозиторию</param>
    public ClientsFileRepository(string path)
    {
        logger.Debug($"Вызов конструктора {GetType().Name} c параметрами: база клиентов {path}");
        if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path))
        {
            logger.Error($"{path} не допустимое наименование файла");
            throw new ArgumentNullException(nameof(path));
        }
        _path = path;

        if (File.Exists(_path))
            Load();
    }

    /// <summary>
    /// Конструктор репозитория
    /// </summary>
    /// <param name="path">Путь к файлу-репозиторию</param>
    public ClientsFileRepository(string path, IEnumerable<Client?> clients) : this(path)
    {
        logger.Debug($"Вызов конструктора {GetType().Name} c параметрами: база клиентов {path}, клиенты в кол-ве {clients.Count()} шт");
        _path = path;
        _clients = clients.ToList();
    }
    
    /// <summary>
    /// Удаление клиента
    /// </summary>
    /// <param name="clientId">ИД клиента</param>
    public void DeleteClient(int clientId)
    {
        if(_clients.Any(c=>c.Id == clientId))
        {
            _clients.Remove(_clients.FirstOrDefault(c => c.Id == clientId));    
            logger.Debug($"Удаление клиента с ID =  {clientId}");
        }
        else
        {
            logger.Warn($"Удаление клиента с ID =  {clientId} не возможно. Заданный ID не найден");
        }
           
    }

    /// <summary>
    /// Получение данных о клиенте по ИД
    /// </summary>
    /// <param name="clientId">ИД клиента</param>
    /// <returns></returns>
    public Client? GetClientByID(int clientId) 
    {
        if(_clients.Any(c=>c.Id == clientId))
        {
            logger.Debug($"Получение клиента с ID =  {clientId}");
            return _clients.FirstOrDefault(c => c.Id == clientId);    
        }
        
        logger.Error($"Получение клиента с ID =  {clientId} не возможно. Заданный ID не найден");
        return null;
    }

    /// <summary>
    /// Получение списка клиентов
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Client?> GetAllClients() => _clients;

    // TODO: проверить передачу null/не заполненого ID
    /// <summary>
    /// Добавление нового клиента
    /// </summary>
    /// <param name="client">клиент</param>
    public void InsertClient(Client? client)
    {
        logger.Debug($"Добавление клиента: ID={client.Id}, Имя={client.FirstName}, Фамилия={client.LastName}");
        _clients.Add(client);   
    }

    /// <summary>
    /// Обновление данных о клиенте
    /// </summary>
    /// <param name="client"></param>
    public void UpdateClient(Client? client)
    {
        if (_clients.Contains(client))
        {
            logger.Error($"Клиент c ID={client.Id}, Имя={client.FirstName}, Фамилия={client.LastName} отсутствует в базе");
            throw new ArgumentOutOfRangeException(nameof(client), "Такого объекта нет в списке");
        }
        
        _clients[_clients.IndexOf(client)] = client;
        logger.Debug($"Клиент c ID={client.Id}, Имя={client.FirstName}, Фамилия={client.LastName} обновлен");
    }

    /// <summary>
    /// Сохранение списка клиентов в файл
    /// </summary>
    public void Save()
    {
        string dirPath = Path.GetFileName(Path.GetDirectoryName(_path));
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        string json = JsonSerializer.Serialize(_clients);
        File.WriteAllText(_path, json, Encoding.UTF8);
    }

    /// <summary>
    /// Загрузка списка клинтов
    /// </summary>
    void Load()
    {
        string data = File.ReadAllText(_path);
        _clients = JsonSerializer.Deserialize<List<Client>>(data);
    }

}