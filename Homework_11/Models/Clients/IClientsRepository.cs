using System.Collections.Generic;

namespace Homework_11.Models.Clients;

interface IClientsRepository
{
    /// <summary>
    /// Получить список клиентов
    /// </summary>
    /// <returns></returns>
    IEnumerable<Client?> GetAllClients();

    /// <summary>
    /// Получение информации о клиенте по ИД
    /// </summary>
    /// <param name="clientId">ИД клиента</param>
    /// <returns></returns>
    Client? GetClientByID(int clientId);

    /// <summary>
    /// Добавление клиента в репозиторий
    /// </summary>
    /// <param name="client">Клиент</param>
    void InsertClient(Client? client);

    /// <summary>
    /// Удаление клиента
    /// </summary>
    /// <param name="clientId">ИД клиента</param>
    void DeleteClient(int clientId);

    /// <summary>
    /// Обновление данных о клиенте
    /// </summary>
    /// <param name="client">Клиент</param>
    void UpdateClient(Client? client);
}