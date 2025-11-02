// Decompiled with JetBrains decompiler
// Type: PokeMMO_.ViewModels.DiscordViewModel
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Mvvm;

#nullable disable
namespace PokeMMO_.ViewModels;

public class DiscordViewModel : BindableBase
{
  private static readonly object padlock = new object();
  private static DiscordViewModel instance = (DiscordViewModel) null;
  private bool _DiscordDMThief = true;
  private bool _DiscordDMIV31 = true;
  private bool _DiscordDMPayDay = true;
  private bool _DiscordDMThrowBall = true;

  public static DiscordViewModel Instance
  {
    get
    {
      lock (DiscordViewModel.padlock)
      {
        if (DiscordViewModel.instance == null)
          DiscordViewModel.instance = new DiscordViewModel();
        return DiscordViewModel.instance;
      }
    }
  }

  public bool DiscordDMThief
  {
    get => this._DiscordDMThief;
    set => this.SetProperty<bool>(ref this._DiscordDMThief, value, nameof (DiscordDMThief));
  }

  public bool DiscordDMIV31
  {
    get => this._DiscordDMIV31;
    set => this.SetProperty<bool>(ref this._DiscordDMIV31, value, nameof (DiscordDMIV31));
  }

  public bool DiscordDMPayDay
  {
    get => this._DiscordDMPayDay;
    set => this.SetProperty<bool>(ref this._DiscordDMPayDay, value, nameof (DiscordDMPayDay));
  }

  public bool DiscordDMThrowBall
  {
    get => this._DiscordDMThrowBall;
    set => this.SetProperty<bool>(ref this._DiscordDMThrowBall, value, nameof (DiscordDMThrowBall));
  }
}
