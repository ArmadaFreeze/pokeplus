// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Model.Premium
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Botting;
using PokeMMO_.Mvvm;
using PokeMMO_.ViewModels;
using System;
using System.Linq;
using System.Windows;

#nullable disable
namespace PokeMMO_.Model;

public class Premium : BindableBase
{
  private int _OrangePotionSelectedIndex = -1;
  private int _RedPotionSelectedIndex = -1;
  private bool _PremiumEnabled = MainWindow.DevelopmentMode;
  private bool _PotionSystem = false;
  private bool _MultiTarget = false;
  private bool _TeleportBack = false;
  private bool _Substitute = false;
  private bool _FalseSwipe = false;
  private bool _Spore = false;
  private bool _Assist = false;
  private bool _SlowMode = false;
  private bool _EscapeRope = false;
  private string _DiscordUsername = "";
  private CatchMovesRoutine _CatchMovesRoutine = CatchMovesRoutine.SFS;

  public DelegateCommand DiscordWindowCommand { get; }

  public Premium()
  {
    this.DiscordWindowCommand = new DelegateCommand((Action) (() => Application.Current.Dispatcher.Invoke((Action) (() => Application.Current.Windows.OfType<DiscordWindow>().SingleOrDefault<DiscordWindow>().Show()))), (Func<bool>) (() => true));
    this.DiscordWindowCommand.RaiseCanExecuteChanged();
  }

  public int OrangePotionSelectedIndex
  {
    get => this._OrangePotionSelectedIndex;
    set
    {
      this.SetProperty<int>(ref this._OrangePotionSelectedIndex, value, nameof (OrangePotionSelectedIndex));
    }
  }

  public int RedPotionSelectedIndex
  {
    get => this._RedPotionSelectedIndex;
    set
    {
      this.SetProperty<int>(ref this._RedPotionSelectedIndex, value, nameof (RedPotionSelectedIndex));
    }
  }

  public bool PremiumEnabled
  {
    get => this._PremiumEnabled;
    set => this.SetProperty<bool>(ref this._PremiumEnabled, value, nameof (PremiumEnabled));
  }

  public bool PotionSystem
  {
    get => this._PotionSystem;
    set => this.SetProperty<bool>(ref this._PotionSystem, value, nameof (PotionSystem));
  }

  public bool MultiTarget
  {
    get => this._MultiTarget;
    set => this.SetProperty<bool>(ref this._MultiTarget, value, nameof (MultiTarget));
  }

  public bool TeleportBack
  {
    get => this._TeleportBack;
    set => this.SetProperty<bool>(ref this._TeleportBack, value, nameof (TeleportBack));
  }

  public bool Substitute
  {
    get => Bot.Instance.Settings.Substitute;
    set
    {
      this.SetProperty<bool>(ref this._Substitute, value, nameof (Substitute));
      MainViewModel.Instance.Home.Options[0].Selected = this._Substitute;
    }
  }

  public bool FalseSwipe
  {
    get => Bot.Instance.Settings.FalseSwipe;
    set
    {
      this.SetProperty<bool>(ref this._FalseSwipe, value, nameof (FalseSwipe));
      MainViewModel.Instance.Home.Options[1].Selected = this._FalseSwipe;
    }
  }

  public bool Spore
  {
    get => Bot.Instance.Settings.Spore;
    set
    {
      this.SetProperty<bool>(ref this._Spore, value, nameof (Spore));
      MainViewModel.Instance.Home.Options[2].Selected = this._Spore;
    }
  }

  public bool Assist
  {
    get => Bot.Instance.Settings.Assist;
    set
    {
      this.SetProperty<bool>(ref this._Assist, value, nameof (Assist));
      MainViewModel.Instance.Home.Options[3].Selected = this._Assist;
    }
  }

  public bool SlowMode
  {
    get => this._SlowMode;
    set => this.SetProperty<bool>(ref this._SlowMode, value, nameof (SlowMode));
  }

  public bool EscapeRope
  {
    get => this._EscapeRope;
    set => this.SetProperty<bool>(ref this._EscapeRope, value, nameof (EscapeRope));
  }

  public string DiscordUsername
  {
    get => this._DiscordUsername;
    set => this.SetProperty<string>(ref this._DiscordUsername, value, nameof (DiscordUsername));
  }

  public CatchMovesRoutine CatchMovesRoutine
  {
    get => this._CatchMovesRoutine;
    set
    {
      this.SetProperty<CatchMovesRoutine>(ref this._CatchMovesRoutine, value, nameof (CatchMovesRoutine));
    }
  }
}
