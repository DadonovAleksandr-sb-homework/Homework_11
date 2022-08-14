using System.Windows;
using System.Windows.Input;
using Homework_11.Infrastructure.Commands;
using Homework_11.Models;
using Homework_11.ViewModels.Base;
using NLog;

namespace Homework_11.ViewModels;

internal class LoginViewModel : BaseViewModel
{
    public LoginViewModel()
    {
        logger.Debug($"Вызов конструктора {this.GetType().Name}");
        
        #region commands
        SetConsultantMode = new LambdaCommand(OnSetConsultantModeExecuted, CanSetConsultantModeExecute);
        SetManagerMode = new LambdaCommand(OnSetManagerModeExecuted, CanSetManagerModeExecute);
        #endregion
    }
    
    
    #region Commands

    #region SetConsultantMode
    public ICommand SetConsultantMode { get; }
    private void OnSetConsultantModeExecuted(object p)
    {
        logger.Debug($"Команда: Выполнить вход в приложение с правами консультанта");
        OpenMainWindow(AppViewMode.Consultant, p);   
    }
    private bool CanSetConsultantModeExecute(object p) => true;
    #endregion
        
    #region SetManagerMode
    public ICommand SetManagerMode { get; }
    private void OnSetManagerModeExecuted(object p)
    {
        logger.Debug($"Команда: Выполнить вход в приложение с правами менеджера");
        OpenMainWindow(AppViewMode.Manager, p);
    }
    private bool CanSetManagerModeExecute(object p) => true;
    #endregion
    
    private void OpenMainWindow(AppViewMode mode, object p)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.DataContext = new MainWindowViewModel(mode);
        mainWindow.Show();
        logger.Debug($"Открытие окна {mainWindow.Title}");

        if (p is Window window)
        {
            window.Close();
            logger.Debug($"Закрытие окна {window.Title}");
        }
    }
    
    #endregion

}