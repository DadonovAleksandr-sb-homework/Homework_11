using System;
using Homework_11.Infrastructure.Commands.Base;

namespace Homework_11.Infrastructure.Commands;

internal class LambdaCommand : Command
{
    private readonly Action<object> _Execute;
    private readonly Func<object, bool> _CanExecute;


    public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
    {
        _Execute = Execute ?? throw new ArgumentException(nameof(Execute));
        _CanExecute = CanExecute;
    }

    public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true; 
    
    public override void Execute(object parameter) => _Execute(parameter);
    
}
