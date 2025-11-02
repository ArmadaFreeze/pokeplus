// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Mvvm.BindableBase
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System.ComponentModel;
using System.Runtime.CompilerServices;

#nullable disable
namespace PokeMMO_.Mvvm;

public abstract class BindableBase : INotifyPropertyChanged
{
  public event PropertyChangedEventHandler PropertyChanged;

  protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
  {
    PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
    if (propertyChanged == null)
      return;
    propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
  }

  protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
  {
    bool flag;
    if (object.Equals((object) storage, (object) value))
    {
      flag = false;
    }
    else
    {
      storage = value;
      this.OnPropertyChanged(propertyName);
      flag = true;
    }
    return flag;
  }
}
