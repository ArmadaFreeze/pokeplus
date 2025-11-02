// Decompiled with JetBrains decompiler
// Type: PokeMMO_.ViewModels.MainViewModel
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Model;
using PokeMMO_.Mvvm;

#nullable disable
namespace PokeMMO_.ViewModels;

public class MainViewModel : BindableBase
{
  private static readonly object padlock = new object();
  private static MainViewModel instance = (MainViewModel) null;
  private Home _Home = new Home();
  private Premium _Premium = new Premium();
  private Security _Security = new Security();
  private Auth _Auth = new Auth();
  private Settings _Settings = new Settings();

  public static MainViewModel Instance
  {
    get
    {
      lock (MainViewModel.padlock)
      {
        if (MainViewModel.instance == null)
          MainViewModel.instance = new MainViewModel();
        return MainViewModel.instance;
      }
    }
  }

  public Home Home
  {
    get => this._Home;
    set => this.SetProperty<Home>(ref this._Home, value, nameof (Home));
  }

  public Premium Premium
  {
    get => this._Premium;
    set => this.SetProperty<Premium>(ref this._Premium, value, nameof (Premium));
  }

  public Security Security
  {
    get => this._Security;
    set => this.SetProperty<Security>(ref this._Security, value, nameof (Security));
  }

  public Auth Auth
  {
    get => this._Auth;
    set => this.SetProperty<Auth>(ref this._Auth, value, nameof (Auth));
  }

  public Settings Settings
  {
    get => this._Settings;
    set => this.SetProperty<Settings>(ref this._Settings, value, nameof (Settings));
  }
}
