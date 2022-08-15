﻿using Homework_11.Models.Clients;
using Homework_11.ViewModels;

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
    public virtual ClientInfo GetClientInfo(Client client)
    {
        return new ClientInfo(client);
    }
    
    

}