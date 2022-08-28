using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Homework_11.Infrastructure.Commands;
using Homework_11.ViewModels.Base;
using Homework_11.Views;

namespace Homework_11.ViewModels;

public class ClientsViewModel : BaseViewModel
{
    public Action UpdateClientsList;
    public ObservableCollection<ClientInfo> Clients { get; }
    
    public MainWindowViewModel MainVm;
    
    public ClientsViewModel()
    {
        logger.Debug($"Вызов конструктора по умолчанию {this.GetType().Name}");
    }

    public ClientsViewModel(MainWindowViewModel mainVm)
    {
        logger.Debug($"Вызов конструктора {this.GetType().Name}");

        MainVm = mainVm;
        
        #region Commands
        AddClientCommand = new LambdaCommand(OnAddClientCommandExecuted, CanAddClientCommandExecute);
        DelClientCommand = new LambdaCommand(OnDelClientCommandExecuted, CanDelClientCommandExecute);
        EditClientCommand = new LambdaCommand(OnEditClientCommandExecuted, CanEditClientCommandExecute);
        #endregion
        
        //TODO: спросить как сделать отслеживание изменений клиентов, чтобы данные автоматически обновллялись и во вьюхе и репозитории
        Clients = new ObservableCollection<ClientInfo>(mainVm.Bank.GetClientsInfo());
        _enableAddClient = mainVm.Worker.DataAccess.Commands.AddClient;
        _enableDelClient = mainVm.Worker.DataAccess.Commands.DelClient;
        _enableEditClient = mainVm.Worker.DataAccess.Commands.EditClient;

        _selectedIndex = 0;

        UpdateClientsList += UpdateClients;
    }

    /// <summary>
    /// Обновление списка клиентов
    /// </summary>
    private void UpdateClients()
    {
        var selectedIndex = _selectedIndex;
        Clients.Clear();
        foreach (var clientInfo in MainVm.Bank.GetClientsInfo())
        {
            Clients.Add(clientInfo);
        }

        SelectedIndex = selectedIndex;
    }

    #region Commands

    #region AddClient
    public ICommand AddClientCommand { get; }

    private void OnAddClientCommandExecuted(object p)
    {
        ClientCardWindow clientCard = new ClientCardWindow();
        ClientCardViewModel clientCardVm = new ClientCardViewModel(new ClientInfo(), MainVm.Bank, this, MainVm.Worker.DataAccess);
        clientCard.DataContext = clientCardVm;
        clientCard.ShowDialog();
    }

    private bool CanAddClientCommandExecute(object p) => true;

    #endregion

    #region DelClient
    public ICommand DelClientCommand { get; }

    private void OnDelClientCommandExecuted(object p)
    {
        if(SelectedClient is null) return;
        
        MainVm.Bank.DeleteClient(SelectedClient);
        UpdateClients();
    }

    private bool CanDelClientCommandExecute(object p) => true;
    #endregion

    #region EditClient
    public ICommand EditClientCommand { get; }

    private void OnEditClientCommandExecuted(object p)
    {
        if(SelectedClient is null) return;
        
        ClientCardWindow clientCard = new ClientCardWindow();
        ClientCardViewModel clientCardVm = new ClientCardViewModel(SelectedClient, MainVm.Bank, this, MainVm.Worker.DataAccess);
        clientCard.DataContext = clientCardVm;
        clientCard.ShowDialog();
    }

    private bool CanEditClientCommandExecute(object p) => true;
    #endregion


    #endregion
    
    #region EnableAddClient
    private bool _enableAddClient;
    public bool EnableAddClient
    {
        get => _enableAddClient;
        set => Set(ref _enableAddClient, value);
    }
    #endregion
    
    #region EnableDelClient
    private bool _enableDelClient;
    public bool EnableDelClient
    {
        get => _enableDelClient;
        set => Set(ref _enableDelClient, value);
    }
    #endregion
    
    #region EnableEditClient
    private bool _enableEditClient;
    public bool EnableEditClient
    {
        get => _enableEditClient;
        set => Set(ref _enableEditClient, value);
    }
    #endregion
    
    #region SelectedClient
    private ClientInfo _selectedClient;
    public ClientInfo SelectedClient
    {
        get => _selectedClient;
        set => Set(ref _selectedClient, value);
    }
    #endregion
    
    #region SelectedIndex
    private int _selectedIndex;
    public int SelectedIndex
    {
        get => _selectedIndex;
        set => Set(ref _selectedIndex, value);
    }
    #endregion

}