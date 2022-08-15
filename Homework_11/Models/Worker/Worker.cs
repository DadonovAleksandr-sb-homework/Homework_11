using Homework_11.Models.Clients;

namespace Homework_11.Models.Worker;

/// <summary>
/// Абстрактный работник
/// </summary>
public abstract class Worker
{
    /// <summary>
    /// Получение информации о клиенте
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public virtual Client GetClientInfo(Client client)
    {
        return client;
    }
    
    

}