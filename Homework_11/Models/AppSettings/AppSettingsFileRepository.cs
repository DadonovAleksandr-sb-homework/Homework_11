using System;
using System.IO;
using System.Text;
using NLog;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Homework_11.Models.AppSettings;

public class AppSettingsFileRepository : IAppSettingsRepository
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    private readonly string _path;

    public AppSettingsFileRepository()
    {
        _path = @"app-settings.json";
        logger.Debug($"Вызов конструктора по умолчанию {this.GetType().Name}");
        logger.Debug($"Настройки проекта хранятся в файле: {_path}");
    }

    public AppSettingsFileRepository(string path)
    {
        _path = path;
        logger.Debug($"Вызов конструктора {this.GetType().Name}");
        logger.Debug($"Настройки проекта хранятся в файле: {_path}");
    }
    
    /// <summary>
    /// Сохранение настроек приложения
    /// </summary>
    /// <param name="settings"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Save(AppSettings settings)
    {
        string json = JsonSerializer.Serialize(settings);
        File.WriteAllText(_path, json, Encoding.UTF8);
        logger.Debug($"Сохранение настроек приложения в файл: {_path}");
    }

    /// <summary>
    /// Загрузка настроек приложения
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public AppSettings Load()
    {
        logger.Debug($"Закгрузка настроек приложения из файла: {_path}");
        if (!File.Exists(_path))
            return new AppSettings();
        string data = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<AppSettings>(data) ?? new AppSettings();
    }
}