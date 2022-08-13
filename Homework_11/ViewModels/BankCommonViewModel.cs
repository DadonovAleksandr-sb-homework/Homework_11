using System.Windows.Input;
using Homework_11.Infrastructure.Commands;
using Homework_11.ViewModels.Base;

namespace Homework_11.ViewModels;

public class BankCommonViewModel : BaseViewModel
{
    MainWindowViewModel _mainVm;

    public BankCommonViewModel() { }

    public BankCommonViewModel(MainWindowViewModel mainVm)
    {
        _mainVm = mainVm;
        
        #region commands
        
        LogOutCommand = new LambdaCommand(OnLogOutCommandExecuted, CanLogOutCommandExecute);
        
        #endregion
    }
    
    
    #region Commands

    #region LogOutCommand
    public ICommand LogOutCommand { get; }

    private void OnLogOutCommandExecuted(object p)
    {
        //_mainVm.LogOut();
    }

    private bool CanLogOutCommandExecute(object p) => true;
    #endregion

    #endregion
}