using System.Windows;
using System.Windows.Input;
using Homework_11.Infrastructure.Commands;
using Homework_11.Models;
using Homework_11.Models.Clients;
using Homework_11.Models.Common;
using Homework_11.Models.Worker;
using Homework_11.ViewModels.Base;

namespace Homework_11.ViewModels;

public class ClientCardViewModel : BaseViewModel
{
    private ClientInfo _currentClientInfo { get; set; }
    private Bank _bank { get; set; }
    
    public ClientCardViewModel(ClientInfo clientInfo, Bank bank)
    {
        _currentClientInfo = clientInfo;
        _bank = bank;
        
        FillFields(_currentClientInfo);

        #region commands
        SaveClientData = new LambdaCommand(OnSaveClientDataExecuted, CanSaveClientDataExecute);
        #endregion
    }

    private void FillFields(ClientInfo clientInfo)
    {
        _firstName = clientInfo.FirstName;
        _lastName = clientInfo.LastName;
        _middleName = clientInfo.MiddleName;
        _phoneNumber = clientInfo.PhoneNumber.ToString();
        _passportSerie = clientInfo.PassportData.Serie.ToString();
        _passportNumber = clientInfo.PassportData.Number.ToString();
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
            return;
        }

        client.Id = _currentClientInfo.Id;
        _bank.EditClient(client);
        
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
        set => Set(ref _firstName, value);
    }
    #endregion
    
    #region LastName
    private string _lastName;
    public string LastName
    {
        get => _lastName;
        set => Set(ref _lastName, value);
    }
    #endregion

    #region MiddleName
    private string _middleName;
    public string MiddleName
    {
        get => _middleName;
        set => Set(ref _middleName, value);
    }
    #endregion
    
    #region PhoneNumber
    private string _phoneNumber;
    public string PhoneNumber
    {
        get => _phoneNumber;
        set => Set(ref _phoneNumber, value);
    }
    #endregion
    
    #region PassportSerie
    private string _passportSerie;
    public string PassportSerie
    {
        get => _passportSerie;
        set => Set(ref _passportSerie, value);
    }
    #endregion

    #region PassportNumber
    private string _passportNumber;
    public string PassportNumber
    {
        get => _passportNumber;
        set => Set(ref _passportNumber, value);
    }
    #endregion

    #endregion
    
    
}