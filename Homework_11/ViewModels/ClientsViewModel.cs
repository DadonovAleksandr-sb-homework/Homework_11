using Homework_11.ViewModels.Base;

namespace Homework_11.ViewModels;

public class ClientsViewModel : BaseViewModel
{
    public ClientsViewModel()
    {
        logger.Debug($"Вызов конструктора по умолчанию {this.GetType().Name}");
    }

    public ClientsViewModel(MainWindowViewModel mainWindowVm)
    {
        logger.Debug($"Вызов конструктора {this.GetType().Name}");
    }
}