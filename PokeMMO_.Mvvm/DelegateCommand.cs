using System;
using System.Diagnostics;
using System.Windows.Input;

namespace PokeMMO_.Mvvm;

public class DelegateCommand : ICommand
{
	private readonly Action _execute;

	private readonly Func<bool> _canExecute;

	public event EventHandler CanExecuteChanged;

	public DelegateCommand(Action execute)
		: this(execute, null)
	{
	}

	public DelegateCommand(Action execute, Func<bool> canExecute)
	{
		if (execute == null)
		{
			throw new ArgumentNullException("execute");
		}
		_execute = execute;
		_canExecute = canExecute;
	}

	public void RaiseCanExecuteChanged()
	{
		this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}

	[DebuggerStepThrough]
	public bool CanExecute(object parameter)
	{
		return _canExecute == null || _canExecute();
	}

	public void Execute(object parameter)
	{
		if (CanExecute(parameter))
		{
			_execute();
		}
	}
}
