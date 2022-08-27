using System.Windows;
using System.Windows.Input;
using Homework_11.Infrastructure.Commands;
using Homework_11.Models;
using Homework_11.Models.Clients;
using Homework_11.Models.Common;
using Homework_11.Models.Worker;
using Homework_11.ViewModels.Base;
using Homework_11.Views;

namespace Homework_11.ViewModels;

public class ClientCardViewModel : BaseViewModel
{
    private ClientInfo _currentClientInfo { get; set; }
    private Bank _bank { get; set; }
    private ClientsViewModel _clientsVm;
    
    
    public ClientCardViewModel(ClientInfo clientInfo, Bank bank, ClientsViewModel clientsVm, RoleDataAccess dataAccess)
    {
        _currentClientInfo = clientInfo;
        _bank = bank;
        _clientsVm = clientsVm;
        FillFields(_currentClientInfo);
        EnableFields(dataAccess);
        CheckSaveClient();
        
        #region commands
        SaveClientData = new LambdaCommand(OnSaveClientDataExecuted, CanSaveClientDataExecute);
        #endregion
    }

    /// <summary>
    /// Включение/отключения возможности ввода данных
    /// </summary>
    /// <param name="dataAccess"></param>
    private void EnableFields(RoleDataAccess dataAccess)
    {
        _enableFirstName = dataAccess.EditFields.FirstName;
        _enableLastName = dataAccess.EditFields.LastName;
        _enableMiddleName = dataAccess.EditFields.MidleName;
        _enablePassportData = dataAccess.EditFields.PassortData;
        _enablePhoneNumber = dataAccess.EditFields.PhoneNumber;

        _borderFirstName = InputHighlighting(_enableFirstName, _firstName.Length > 0);
        _borderLastName = InputHighlighting(_enableLastName, _lastName.Length > 0);
        _borderMiddleName = InputHighlighting(_enableMiddleName, _middleName.Length > 0);
        _borderPassportSerie = InputHighlighting(_enablePassportData, PassportData.IsSeries(_passportSerie));
        _borderPassportNumber = InputHighlighting(_enablePassportData, PassportData.IsNumber(_passportNumber));
        _borderPhoneNumber = InputHighlighting(_enablePhoneNumber, Models.Common.PhoneNumber.IsPhoneNumber(_phoneNumber));
    }

    private InputValueHighlightingEnum InputHighlighting(bool isEnable, bool isValid)
    {
        if (!isEnable) return InputValueHighlightingEnum.Disable;
        if (!isValid) return InputValueHighlightingEnum.Error;

        return InputValueHighlightingEnum.Default;
    }

    /// <summary>
    /// Заполнение данных
    /// </summary>
    /// <param name="clientInfo"></param>
    private void FillFields(ClientInfo clientInfo)
    {
        _firstName = clientInfo.FirstName;
        _lastName = clientInfo.LastName;
        _middleName = clientInfo.MiddleName;
        _phoneNumber = clientInfo.PhoneNumber.ToString();
        _passportSerie = clientInfo.PassportSerie;
        _passportNumber = clientInfo.PassportNumber;
    }

    private void CheckSaveClient()
    {
        EnableSaveClient = _borderFirstName != InputValueHighlightingEnum.Error
                           && _borderLastName != InputValueHighlightingEnum.Error
                           && _borderMiddleName != InputValueHighlightingEnum.Error
                           && _borderPassportSerie != InputValueHighlightingEnum.Error
                           && _borderPassportNumber != InputValueHighlightingEnum.Error
                           && _borderPhoneNumber != InputValueHighlightingEnum.Error;
    }

    #region Commands

    #region SaveClient
    public ICommand SaveClientData { get; }
    private void OnSaveClientDataExecuted(object p)
    {
        logger.Debug($"Команда: Сохранить измененые данные о клиенте");
        var client = new Client(
            new PhoneNumber(_phoneNumber), 
            new PassportData(int.Parse(_passportSerie), int.Parse(_passportNumber)), 
            _firstName, _lastName, _middleName);
        if (_currentClientInfo.Id == 0) // новый клиент
        {
            _bank.AddClient(client);
            //TODO: наверное окно не закроется, если добавлять нового клиента
            return;
        }

        client.Id = _currentClientInfo.Id;
        _bank.EditClient(client);
        
        _clientsVm.UpdateClientsList.Invoke();
        
        if (p is Window window)
        {
            window.Close();
            logger.Debug($"Закрытие окна {window.Title}");
        }
        
    }
    private bool CanSaveClientDataExecute(object p) => true;
    #endregion

    #endregion
    
    #region ClientInfo

    #region FirstName
    private string _firstName;
    public string FirstName
    {
        get => _firstName;
        set
        {
            Set(ref _firstName, value);
            BorderFirstName =
                InputHighlighting(_enableFirstName, _firstName.Length > 2);
        }
    }
    
    private bool _enableFirstName;
    public bool EnableFirstName
    {
        get => _enableFirstName;
        set => Set(ref _enableFirstName, value);
    }
    
    private InputValueHighlightingEnum _borderFirstName;
    public InputValueHighlightingEnum BorderFirstName
    {
        get => _borderFirstName;
        set
        {
            Set(ref _borderFirstName, value);
            CheckSaveClient();
        }
    }
    #endregion
    
    #region LastName
    private string _lastName;
    public string LastName
    {
        get => _lastName;
        set
        {
            Set(ref _lastName, value);
            BorderLastName =
                InputHighlighting(_enableLastName, _lastName.Length > 2);
        }
    }
    
    private bool _enableLastName;
    public bool EnableLastName
    {
        get => _enableLastName;
        set => Set(ref _enableLastName, value);
    }
    
    private InputValueHighlightingEnum _borderLastName;
    public InputValueHighlightingEnum BorderLastName
    {
        get => _borderLastName;
        set
        {
            Set(ref _borderLastName, value);
            CheckSaveClient();
        }
    }
    #endregion

    #region MiddleName
    private string _middleName;
    public string MiddleName
    {
        get => _middleName;
        set
        {
            Set(ref _middleName, value);
            BorderMiddleName =
                InputHighlighting(_enableMiddleName, _middleName.Length > 2);
        }
    }
    
    private bool _enableMiddleName;
    public bool EnableMiddleName
    {
        get => _enableMiddleName;
        set => Set(ref _enableMiddleName, value);
    }
    
    private InputValueHighlightingEnum _borderMiddleName;
    public InputValueHighlightingEnum BorderMiddleName
    {
        get => _borderMiddleName;
        set
        {
            Set(ref _borderMiddleName, value);
            CheckSaveClient();
        }
    }
    #endregion
    
    #region PhoneNumber
    private string _phoneNumber;

    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            Set(ref _phoneNumber, value);
            BorderPhoneNumber =
                InputHighlighting(_enablePhoneNumber, Models.Common.PhoneNumber.IsPhoneNumber(_phoneNumber));
        }
    }

    private bool _enablePhoneNumber;
    public bool EnablePhoneNumber
    {
        get => _enablePhoneNumber;
        set => Set(ref _enablePhoneNumber, value);
    }
    
    private InputValueHighlightingEnum _borderPhoneNumber;
    public InputValueHighlightingEnum BorderPhoneNumber
    {
        get => _borderPhoneNumber;
        set
        {
            Set(ref _borderPhoneNumber, value);
            CheckSaveClient();
        }
    }
    #endregion
    
    #region PassportData
    private string _passportSerie;
    public string PassportSerie
    {
        get => _passportSerie;
        set 
        { 
            Set(ref _passportSerie, value); 
            BorderPassportSerie =
                InputHighlighting(_enablePassportData, PassportData.IsSeries(_passportSerie));
        }
    }
    
    private string _passportNumber;
    public string PassportNumber
    {
        get => _passportNumber;
        set
        {
            Set(ref _passportNumber, value);
            BorderPassportNumber =
                InputHighlighting(_enablePassportData, PassportData.IsNumber(_passportNumber));
        }
    }
    
    private bool _enablePassportData;
    public bool EnablePassportData
    {
        get => _enablePassportData;
        set => Set(ref _enablePassportData, value);
    }
    
    private InputValueHighlightingEnum _borderPassportSerie;
    public InputValueHighlightingEnum BorderPassportSerie
    {
        get => _borderPassportSerie;
        set
        {
            Set(ref _borderPassportSerie, value);
            CheckSaveClient();
        }
    }
    
    private InputValueHighlightingEnum _borderPassportNumber;
    public InputValueHighlightingEnum BorderPassportNumber
    {
        get => _borderPassportNumber;
        set
        {
            Set(ref _borderPassportNumber, value);
            CheckSaveClient();
        }
    }

    #endregion

    #endregion
    
    #region EnableSaveClient
    private bool _enableSaveClient;
    public bool EnableSaveClient
    {
        get => _enableSaveClient;
        set => Set(ref _enableSaveClient, value);
    }
    #endregion
    
}