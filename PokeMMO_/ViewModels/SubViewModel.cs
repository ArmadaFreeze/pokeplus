// Decompiled with JetBrains decompiler
// Type: PokeMMO_.ViewModels.SubViewModel
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Botting;
using PokeMMO_.Mvvm;
using System;

#nullable disable
namespace PokeMMO_.ViewModels;

public class SubViewModel : BindableBase
{
  private static readonly object padlock = new object();
  private static SubViewModel instance = (SubViewModel) null;
  private string _Timer = "Time: " + (DateTimeOffset.Now.DateTime - Bot.Instance.Status.Timer).ToString("hh\\:mm\\:ss");
  private string _Poke = "Pokemon #" + Bot.Instance.Status.SelectedPokemon.ToString();
  private string _ShinyCounter = "Shinies: " + Bot.Instance.Status.ShinyCounter.ToString();
  private string _EncountersCounter = $"Encounters: {Bot.Instance.Status.EncountersCounter.ToString()} - {MainViewModel.Instance.Home.CatchPokemon}'s {Bot.Instance.Status.SelectedCatchPokemonCounter.ToString()}";
  private string _ThrownBallsCounter = "Thrown Balls: " + Bot.Instance.Status.ThrownBallsCounter.ToString();
  private string _Status = "Status: ...";
  private string _StatusMessages = $"[{(DateTimeOffset.Now.DateTime - Bot.Instance.Status.Timer).ToString("hh\\:mm\\:ss")}] ...";
  private string _WalkCycle = "WalkCycle: " + Bot.Instance.Status.WalkCycle.ToString();
  private string _ItemCounter = "Items: " + Bot.Instance.Status.ItemCounter.ToString();

  public static SubViewModel Instance
  {
    get
    {
      lock (SubViewModel.padlock)
      {
        if (SubViewModel.instance == null)
          SubViewModel.instance = new SubViewModel();
        return SubViewModel.instance;
      }
    }
  }

  public string Timer
  {
    get => this._Timer;
    set => this.SetProperty<string>(ref this._Timer, value, nameof (Timer));
  }

  public string Poke
  {
    get => this._Poke;
    set => this.SetProperty<string>(ref this._Poke, value, nameof (Poke));
  }

  public string ShinyCounter
  {
    get => this._ShinyCounter;
    set => this.SetProperty<string>(ref this._ShinyCounter, value, nameof (ShinyCounter));
  }

  public string EncountersCounter
  {
    get => this._EncountersCounter;
    set => this.SetProperty<string>(ref this._EncountersCounter, value, nameof (EncountersCounter));
  }

  public string ThrownBallsCounter
  {
    get => this._ThrownBallsCounter;
    set
    {
      this.SetProperty<string>(ref this._ThrownBallsCounter, value, nameof (ThrownBallsCounter));
    }
  }

  public string Status
  {
    get => this._Status;
    set
    {
      this.SetProperty<string>(ref this._Status, value, nameof (Status));
      this.StatusMessages = value.Replace("Status: ", "").Trim();
    }
  }

  public string StatusMessages
  {
    get => this._StatusMessages;
    set
    {
      this.SetProperty<string>(ref this._StatusMessages, $"{this._StatusMessages}{Environment.NewLine}[{(DateTimeOffset.Now.DateTime - Bot.Instance.Status.Timer).ToString("hh\\:mm\\:ss")}] {value}", nameof (StatusMessages));
    }
  }

  public string WalkCycle
  {
    get => this._WalkCycle;
    set => this.SetProperty<string>(ref this._WalkCycle, value, nameof (WalkCycle));
  }

  public string ItemCounter
  {
    get => this._ItemCounter;
    set => this.SetProperty<string>(ref this._ItemCounter, value, nameof (ItemCounter));
  }
}
