namespace Homework_11.Models.AppSettings;

public interface IAppSettingsRepository
{
    /// <summary>
    /// Сохранение настрек приложения
    /// </summary>
    public void Save(AppSettings settings);

    /// <summary>
    /// Загрузка настроек приложения
    /// </summary>
    public AppSettings Load();
}