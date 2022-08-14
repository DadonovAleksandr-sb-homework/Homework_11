using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Homework_11.Infrastructure.Commands;
using Homework_11.Models;
using Homework_11.Models.AppSettings;
using Homework_11.ViewModels.Base;
using Homework_11.Views;
using Homework_11.Views.MainWindow.Pages;
using AppSettings = Homework_11.Models.AppSettings.AppSettings;

namespace Homework_11.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    /// <summary>
    /// Репозиторий настроек приложения
    /// </summary>
    IAppSettingsRepository _appSettingsrepository;
    /// <summary>
    /// Режим доступа к данным: консульант, менеджер
    /// </summary>
    public AppViewMode ViewMode { get; private set; }
    /// <summary>
    /// Настройки приложения
    /// </summary>
    public AppSettings AppSettings { get; private set; }

    internal MainWindowViewModel()
    {
        logger.Debug($"Вызов конструктора {this.GetType().Name} по умолчанию");
    }
    internal MainWindowViewModel(AppViewMode mode)
    {
        logger.Debug($"Вызов конструктора {this.GetType().Name}");
        ViewMode = mode;
        
        _appSettingsrepository = new AppSettingsFileRepository();
        AppSettings = _appSettingsrepository.Load();
        
        #region Pages
        _clients = new ClientsPage();
        _appSettings = new AppSettingsPage();
        _clients.DataContext = new ClientsViewModel(this);
        _appSettings.DataContext = new AppSettingsViewModel(this);

        FrameOpacity = 1.0;
        CurrentPage = new EmptyPage();
        #endregion

        #region commands
        LogOutCommand = new LambdaCommand(OnLogOutCommandExecuted, CanLogOutCommandExecute);
        SetClientsView = new LambdaCommand(OnSetClientsViewExecuted, CanSetClientsViewExecute);
        SetAppSettingsView = new LambdaCommand(OnSetAppSettingsViewExecuted, CanSetAppSettingsViewExecute);
        #endregion
    }
    
    #region Pages
    private readonly Page _clients;
    private readonly Page _appSettings;

    private Page _currentPage;
    /// <summary>
    /// Текущая страница фрейма
    /// </summary>
    public Page CurrentPage
    {
        get => _currentPage;
        set
        {
            Set(ref _currentPage, value);
            logger.Debug($"Переход на страницу: {value.Title}");
        }
    }

    private double _frameOpacity;
    public double FrameOpacity
    {
        get => _frameOpacity;
        set => Set(ref _frameOpacity, value);
    }
    #endregion
    
    #region Commands

    #region CloseApplicationCommand
    public ICommand CloseApplicationCommand { get; }

    private void OnCloseApplicationCommandExecuted(object p)
    {
        Application.Current.Shutdown();
    }

    private bool CanCloseApplicationCommandExecute(object p) => true;

    #endregion

    #region LogOutCommand
    public ICommand LogOutCommand { get; }
    private void OnLogOutCommandExecuted(object p) => LogOut(p);
    private bool CanLogOutCommandExecute(object p) => true;
    
    public void LogOut(object p)
    {
        LoginWindows loginWindow = new LoginWindows();
        loginWindow.Show();
        if(p is Window window)
            window.Close();
    }
    #endregion

    #region SetClientsViewCommand
    public ICommand SetClientsView { get; }
    private void OnSetClientsViewExecuted(object p) => CurrentPage = _clients;
    private bool CanSetClientsViewExecute(object p) => true;
    #endregion
    
    #region SetAppSettingsViewCommand
    public ICommand SetAppSettingsView { get; }
    private void OnSetAppSettingsViewExecuted(object p) => CurrentPage = _appSettings;
    private bool CanSetAppSettingsViewExecute(object p) => true;
    #endregion
    
    #endregion
    
    
}