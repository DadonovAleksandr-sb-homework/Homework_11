using System.Collections.ObjectModel;
using System.Windows.Input;
using Homework_11.Infrastructure.Commands;
using Homework_11.Models.Clients;
using Homework_11.ViewModels.Base;

namespace Homework_11.ViewModels;

public class ClientsViewModel : BaseViewModel
{
    
    public ObservableCollection<ClientInfo> Clients { get; }
    
    IClientsRepository ClientsRepository;
    
    public ClientsViewModel()
    {
        logger.Debug($"Вызов конструктора по умолчанию {this.GetType().Name}");
    }

    public ClientsViewModel(MainWindowViewModel mainVm)
    {
        logger.Debug($"Вызов конструктора {this.GetType().Name}");
        
        #region Commands
        AddClientCommand = new LambdaCommand(OnAddClientCommandExecuted, CanAddClientCommandExecute);
        DelClientCommand = new LambdaCommand(OnDelClientCommandExecuted, CanDelClientCommandExecute);
        EditClientCommand = new LambdaCommand(OnEditClientCommandExecuted, CanEditClientCommandExecute);
        #endregion
        //TODO: спросить как сделать отслеживание изменений клиентов, чтобы данные автоматически обновллялись и во вьюхе и репозитории
        Clients = new ObservableCollection<ClientInfo>(mainVm.Bank.GetClientsInfo());
    }
    
    
    #region Commands

    #region AddClient
    public ICommand AddClientCommand { get; }

    private void OnAddClientCommandExecuted(object p)
    {
        ;
    }

    private bool CanAddClientCommandExecute(object p) => true;

    #endregion

    #region DelClient
    public ICommand DelClientCommand { get; }

    private void OnDelClientCommandExecuted(object p)
    {
        ;
    }

    private bool CanDelClientCommandExecute(object p) => true;
    #endregion

    #region EditClient
    public ICommand EditClientCommand { get; }

    private void OnEditClientCommandExecuted(object p)
    {
        ;
    }

    private bool CanEditClientCommandExecute(object p) => true;
    #endregion


    #endregion
    
    
    #region Window title
    
    private bool dataAccess;
    
    public bool DataAccess
    {
        get => dataAccess;
        set => Set(ref dataAccess, value);
    }
    #endregion
}