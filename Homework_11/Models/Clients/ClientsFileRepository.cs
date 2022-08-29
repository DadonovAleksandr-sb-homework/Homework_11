using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using NLog;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Homework_11.Models.Clients;

public class ClientsFileRepository: IClientsRepository
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    
    private List<Client> _clients;
    /// <summary>
    /// Список клиентов
    /// </summary>
    public List<Client> Clients => _clients;
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

        _clients = new List<Client>();
        
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
    /// Кол-во клиентов
    /// </summary>
    public int Count => Clients.Count();

    /// <summary>
    /// Получение списка клиентов
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Client?> GetAllClients() => Clients;

    /// <summary>
    /// Добавление нового клиента
    /// </summary>
    /// <param name="client">клиент</param>
    public void InsertClient(Client client)
    {
        if(client is null)
            return;
        int id = 0;
        if (_clients.Any())
            id = _clients.Max(c => c.Id);
        client.Id = ++id;
        logger.Debug($"Добавление клиента: ID={client.Id}, Имя={client.FirstName}, Фамилия={client.LastName}");
        _clients.Add(client);   
        Save();
    }

    /// <summary>
    /// Обновление данных о клиенте
    /// </summary>
    /// <param name="client"></param>
    public void UpdateClient(Client client)
    {
        if (!_clients.Any(c=>c.Id == client.Id))
        {
            logger.Error($"Клиент c ID={client.Id}, Имя={client.FirstName}, Фамилия={client.LastName} отсутствует в базе");
            throw new ArgumentOutOfRangeException(nameof(client), "Такого объекта нет в списке");
        }
        
        _clients[_clients.IndexOf(_clients.First(c=>c.Id == client.Id))] = client;
        logger.Debug($"Клиент c ID={client.Id}, Имя={client.FirstName}, Фамилия={client.LastName} обновлен");
        Save();
    }

    /// <summary>
    /// Очистка репозитория
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Clear() => Clients.Clear();
    
    
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
        Save();
    }

    /// <summary>
    /// Сохранение списка клиентов в файл
    /// </summary>
    void Save()
    {
        string dirPath = Path.GetFileName(Path.GetDirectoryName(_path));
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        string json = JsonSerializer.Serialize(_clients);
        File.WriteAllText(_path, json);
        logger.Debug($"Сохранение {_clients.Count()} клиентов в файл {_path}");
    }

    /// <summary>
    /// Загрузка списка клинтов
    /// </summary>
    void Load()
    {
        string data = File.ReadAllText(_path);
        _clients = JsonSerializer.Deserialize<List<Client>>(data, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
        logger.Debug($"Загрузка {_clients.Count()} клиентов из файла {_path}");
    }

    public IEnumerator<Client> GetEnumerator()
    {
        for (int i = 0; i < Clients.Count(); i++)
        {
            yield return Clients[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}