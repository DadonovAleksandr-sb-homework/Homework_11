using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Homework_11.Infrastructure.Commands;
using Homework_11.Models;
using Homework_11.ViewModels.Base;
using Homework_11.Views;
using Homework_11.Views.MainWindow.Pages;
using NLog;

namespace Homework_11.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    public AppViewMode ViewMode { get; set; }

    internal MainWindowViewModel() { }
    internal MainWindowViewModel(AppViewMode mode)
    {
        logger.Debug($"Вызов конструктора {this.GetType().Name}");
        this.ViewMode = mode;
        
        #region Pages
        
        _BankCommon = new BankCommon();
        _BankCommon.DataContext = new BankCommonViewModel(this);

        FrameOpacity = 1.0;
        CurrentPage = _BankCommon;
        #endregion

        #region commands

        LogOutCommand = new LambdaCommand(OnLogOutCommandExecuted, CanLogOutCommandExecute);

        #endregion
    }
    
    #region Pages
    private Page _BankCommon;
        
    private Page _CurrentPage;
    /// <summary>
    /// Текущая страница фрейма
    /// </summary>
    public Page CurrentPage
    {
        get => _CurrentPage;
        set
        {
            Set(ref _CurrentPage, value);
            logger.Debug($"Переход на страницу: {value.Title}");
        }
    }

    private double _FrameOpacity;
    public double FrameOpacity
    {
        get => _FrameOpacity;
        set => Set(ref _FrameOpacity, value);
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
    #endregion

    #endregion
    public void LogOut(object p)
    {
        LoginWindows loginWindow = new LoginWindows();
        loginWindow.Show();
        if(p is Window window)
            window.Close();
    }
    
}