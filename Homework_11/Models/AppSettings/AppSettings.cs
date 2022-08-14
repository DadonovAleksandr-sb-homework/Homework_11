using System.Windows;
using NLog;

namespace Homework_11.Models.AppSettings;
/// <summary>
/// Настройки приложения
/// </summary>
public class AppSettings
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    public AppSettings()
    {
        logger.Debug($"Вызов конструктора {GetType().Name}");
        _clientsRepositoryFilePath = string.Empty;
    }
    
    private string _clientsRepositoryFilePath; 
    /// <summary>
    /// Путь до базы клиентов
    /// </summary>
    public string ClientsRepositoryFilePath
    {
        get
        {
            if (string.IsNullOrEmpty(_clientsRepositoryFilePath))
            {
                _clientsRepositoryFilePath = @"clients.json";
                logger.Warn($"Устанавливаем путь по умолчанию для базы клиентов: {_clientsRepositoryFilePath}");
            }
            return _clientsRepositoryFilePath;
        }
        set
        {
            _clientsRepositoryFilePath = value;
        }
    }
}