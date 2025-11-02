// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Mvvm.DelegateCommand
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System;
using System.Diagnostics;
using System.Windows.Input;

#nullable disable
namespace PokeMMO_.Mvvm;

public class DelegateCommand : ICommand
{
  private readonly Action _execute;
  private readonly Func<bool> _canExecute;

  public DelegateCommand(Action execute)
    : this(execute, (Func<bool>) null)
  {
  }

  public DelegateCommand(Action execute, Func<bool> canExecute)
  {
    this._execute = execute != null ? execute : throw new ArgumentNullException(nameof (execute));
    this._canExecute = canExecute;
  }

  public event EventHandler CanExecuteChanged;

  public void RaiseCanExecuteChanged()
  {
    EventHandler canExecuteChanged = this.CanExecuteChanged;
    if (canExecuteChanged == null)
      return;
    canExecuteChanged((object) this, EventArgs.Empty);
  }

  [DebuggerStepThrough]
  public bool CanExecute(object parameter) => this._canExecute == null || this._canExecute();

  public void Execute(object parameter)
  {
    if (!this.CanExecute(parameter))
      return;
    this._execute();
  }
}
