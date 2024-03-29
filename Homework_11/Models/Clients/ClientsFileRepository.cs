﻿using System;
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

    private static int Id;
    static ClientsFileRepository()
    {
        Id = 0;
    }
    /// <summary>
    /// Получение следующего свободного идентификатора клиента
    /// </summary>
    /// <returns></returns>
    private static int NextId() => ++Id;
    
    
    private List<Client>? _clients;
    /// <summary>
    /// Список клиентов
    /// </summary>
    public List<Client>? Clients => _clients;
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

        if (File.Exists(_path)) // если файл существует, подгружаем данные
        {
            Load();
            return;
        }
        // если файл не существует, создаем новый пустой репозиторий
        File.Create(_path);
        NoClientsForLoad();
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
        client.Id = NextId();
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
    public void Clear()
    {
        if(Clients is null)
            return;
        Clients.Clear();
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
        Save();
    }

    /// <summary>
    /// Сохранение списка клиентов в файл
    /// </summary>
    void Save()
    {
        string json = JsonSerializer.Serialize(_clients);
        File.WriteAllText(_path, json);
        logger.Debug($"Сохранение {Count} клиентов в файл {_path}");
    }

    /// <summary>
    /// Загрузка списка клинтов
    /// </summary>
    void Load()
    {
        string data = File.ReadAllText(_path);
        if (string.IsNullOrEmpty(data))
        {
            NoClientsForLoad();
            return;
        }
        _clients = JsonSerializer.Deserialize<List<Client>>(data, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
        
        if (_clients is null)
        {
            NoClientsForLoad();
            return;
        }
        logger.Debug($"Загрузка {Count} клиентов из файла {_path}");
        Id = Count > 0 ? _clients.Max(c => c.Id) : 0;
    }

    /// <summary>
    /// Обработка ситуации, когда не возможно загрузить клиентов
    /// </summary>
    private void NoClientsForLoad()
    {
        logger.Error($"Не удалось загрузить клиентов из файла {_path}");
        _clients = new List<Client>();
        Id = 0;
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