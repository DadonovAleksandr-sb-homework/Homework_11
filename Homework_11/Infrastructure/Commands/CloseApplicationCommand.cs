using System.Windows;
using Homework_11.Infrastructure.Commands.Base;

namespace Homework_11.Infrastructure.Commands;

internal class CloseApplicationCommand : Command
{
    public override bool CanExecute(object parameter) => true;


    public override void Execute(object parameter) => Application.Current.Shutdown();
        
}