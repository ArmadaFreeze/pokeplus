// Decompiled with JetBrains decompiler
// Type: PokeMMO_.ViewModels.AuthViewModel
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Mvvm;

#nullable disable
namespace PokeMMO_.ViewModels;

public class AuthViewModel : BindableBase
{
  private static readonly object padlock = new object();
  private static AuthViewModel instance = (AuthViewModel) null;
  private bool _AutoLogin = false;

  public static AuthViewModel Instance
  {
    get
    {
      lock (AuthViewModel.padlock)
      {
        if (AuthViewModel.instance == null)
          AuthViewModel.instance = new AuthViewModel();
        return AuthViewModel.instance;
      }
    }
  }

  public bool AutoLogin
  {
    get => this._AutoLogin;
    set => this.SetProperty<bool>(ref this._AutoLogin, value, nameof (AutoLogin));
  }
}
