using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Homework_11.Infrastructure.Commands;
using Homework_11.Models.AppSettings;
using Homework_11.Models.Clients;
using Homework_11.Models.Common;
using Homework_11.ViewModels.Base;

namespace Homework_11.ViewModels;

public class AppSettingsViewModel : BaseViewModel
{
    IAppSettingsRepository _repository;
    AppSettings _appSettings;
    public AppSettingsViewModel()
    {
        logger.Debug($"Вызов конструктора по умолчанию {this.GetType().Name}");
    }

    public AppSettingsViewModel(MainWindowViewModel mainWindowVm)
    {
        logger.Debug($"Вызов конструктора {this.GetType().Name}");
        _appSettings = mainWindowVm.AppSettings;
        
        #region Commands
        SaveAppSettingsCommand = new LambdaCommand(OnSaveAppSettingsCommandExecuted, CanSaveAppSettingsCommandExecute);
        GenTestClientsCommand = new LambdaCommand(OnGenTestClientsCommandExecuted, CanGenTestClientsCommandExecute);
        #endregion
    }
    
    #region Commands

    #region SaveAppSettingsCommand
    public ICommand SaveAppSettingsCommand { get; }

    private void OnSaveAppSettingsCommandExecuted(object p)
    {
        _repository = new AppSettingsFileRepository();
        _repository.Save(_appSettings);
    }

    private bool CanSaveAppSettingsCommandExecute(object p) => true;

    #endregion

    #region GenTestClientsCommand
    public ICommand GenTestClientsCommand { get; }

    private void OnGenTestClientsCommandExecuted(object p)
    {
        IEnumerable<Client?> clients = Enumerable.Range(1, 20).Select(i => new Client
        (
            new PhoneNumber($"+7900800{(i>9 ? i : "0"+i)}"),
            new PassportData(1000+i, 50000+i),
            $"Имя {i}",
            $"Фамиля {i}",
            $"Отчество {i}"
        ));
        ClientRepositoryFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"clients.json");
        _appSettings.ClientsRepositoryFilePath = ClientRepositoryFilePath;
        ClientsFileRepository clientsRepository = new ClientsFileRepository(ClientRepositoryFilePath, clients);
    }

    private bool CanGenTestClientsCommandExecute(object p) => true;
    #endregion
    
    #endregion

        
    #region ClientRepositoryFilePath
    private string _clientRepositoryFilePath;
    /// <summary>
    /// Настройки приложения
    /// </summary>
    public string ClientRepositoryFilePath
    {
        get => _appSettings?.ClientsRepositoryFilePath ?? string.Empty;
        set
        {
            Set(ref _clientRepositoryFilePath, value);
            _appSettings.ClientsRepositoryFilePath = _clientRepositoryFilePath;
        }
    }
    #endregion
    
}