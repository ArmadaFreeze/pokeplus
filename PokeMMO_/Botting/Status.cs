// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Botting.Status
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.ViewModels;
using System;
using System.Windows;

#nullable disable
namespace PokeMMO_.Botting;

public class Status
{
  private bool _ShinyHelper = false;
  private bool _IsInFight = false;
  private bool _HumanizeHelper = false;
  private bool _ThiefHelper = false;
  private bool _UsedPotion = false;
  private bool _FirstAutoSSCycle = true;
  private bool _ImprisonHelper = false;
  private bool _Changed = false;
  private string _SolvedCaptchaText = "";
  private string _PotionStatus = "";
  private DateTime _Timer = DateTimeOffset.Now.DateTime;
  private int _ChannelSwitchTimer = 0;
  private int _BreakTimer = 0;
  private bool _Breaking = false;
  private bool _MoveDisabled = false;
  private int _ChannelSwitchTrigger = 0;
  private int _BreakTrigger = 0;
  private int _EncountersCounter = 0;
  private bool _SelectedCatchPokemonCounterHelper = false;
  private int _SelectedCatchPokemonCounter = 0;
  private int _ShinyCounter = 0;
  private int _ThrownBallsCounter = 0;
  private int _ItemCounter = 0;
  private int _SelectedPokemon = 1;
  private int _SelectedPokemonManual = 1;
  private int _AttackMove = 1;
  private bool _UsedFalseSwipe = false;
  private bool _EncounteredSelectedPokemon = false;
  private bool _UsedSubstitute = false;
  private bool _UsedPayDay = false;
  private bool _UsedRock = false;
  private bool _UsedBait = false;
  private bool _FirstMovePP0 = false;
  private bool _SecondMovePP0 = false;
  private bool _ThirdMovePP0 = false;
  private bool _FourthMovePP0 = false;
  private bool _DetectedItem = false;
  private bool _FirstMainPokemonItem = false;
  private bool _FirstPokemonItem = false;
  private bool _SecondPokemonItem = false;
  private bool _ThirdPokemonItem = false;
  private bool _FourthPokemonItem = false;
  private bool _FifthPokemonItem = false;
  private int _LastAttackMove = 0;
  private int _AFKCounter = 0;
  private int _WalkCycle = 0;
  private string _LastWalkDirection = "";
  private bool _GoTo = true;
  private bool _GoBack = false;
  private int _GoBackOnce = 0;
  private bool _Heal = false;

  public bool ShinyHelper
  {
    get => this._ShinyHelper;
    set => this._ShinyHelper = value;
  }

  public bool IsInFight
  {
    get => this._IsInFight;
    set
    {
      if ((this._IsInFight ? 0 : (value ? 1 : 0)) != 0)
        this._SelectedCatchPokemonCounterHelper = true;
      this._IsInFight = value;
    }
  }

  public bool HumanizeHelper
  {
    get => this._HumanizeHelper;
    set => this._HumanizeHelper = value;
  }

  public bool ThiefHelper
  {
    get => this._ThiefHelper;
    set => this._ThiefHelper = value;
  }

  public bool UsedPotion
  {
    get => this._UsedPotion;
    set => this._UsedPotion = value;
  }

  public bool FirstAutoSSCycle
  {
    get => this._FirstAutoSSCycle;
    set => this._FirstAutoSSCycle = value;
  }

  public bool ImprisonHelper
  {
    get => this._ImprisonHelper;
    set => this._ImprisonHelper = value;
  }

  public bool Changed
  {
    get => this._Changed;
    set => this._Changed = value;
  }

  public string SolvedCaptchaText
  {
    get => this._SolvedCaptchaText;
    set => this._SolvedCaptchaText = value;
  }

  public string PotionStatus
  {
    get => this._PotionStatus;
    set => this._PotionStatus = value;
  }

  public DateTime Timer
  {
    get => this._Timer;
    set => this._Timer = value;
  }

  public int ChannelSwitchTimer
  {
    get => this._ChannelSwitchTimer;
    set => this._ChannelSwitchTimer = value;
  }

  public int BreakTimer
  {
    get => this._BreakTimer;
    set => this._BreakTimer = value;
  }

  public bool Breaking
  {
    get => this._Breaking;
    set => this._Breaking = value;
  }

  public bool MoveDisabled
  {
    get => this._MoveDisabled;
    set => this._MoveDisabled = value;
  }

  public int ChannelSwitchTrigger
  {
    get => this._ChannelSwitchTrigger;
    set => this._ChannelSwitchTrigger = value;
  }

  public int BreakTrigger
  {
    get => this._BreakTrigger;
    set => this._BreakTrigger = value;
  }

  public int EncountersCounter
  {
    get => this._EncountersCounter;
    set
    {
      this._EncountersCounter = value;
      Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.EncountersCounter = $"Encounters: {Bot.Instance.Status.EncountersCounter.ToString()} - {MainViewModel.Instance.Home.CatchPokemon}'s {Bot.Instance.Status.SelectedCatchPokemonCounter.ToString()}"));
    }
  }

  public bool SelectedCatchPokemonCounterHelper
  {
    get => this._SelectedCatchPokemonCounterHelper;
    set => this._SelectedCatchPokemonCounterHelper = value;
  }

  public int SelectedCatchPokemonCounter
  {
    get => this._SelectedCatchPokemonCounter;
    set
    {
      this._SelectedCatchPokemonCounter = value;
      Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.EncountersCounter = $"Encounters: {Bot.Instance.Status.EncountersCounter.ToString()} - {MainViewModel.Instance.Home.CatchPokemon}'s {Bot.Instance.Status.SelectedCatchPokemonCounter.ToString()}"));
    }
  }

  public int ShinyCounter
  {
    get => this._ShinyCounter;
    set
    {
      this._ShinyCounter = value;
      Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.ShinyCounter = "Shinies: " + value.ToString()));
    }
  }

  public int ThrownBallsCounter
  {
    get => this._ThrownBallsCounter;
    set
    {
      this._ThrownBallsCounter = value;
      Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.ThrownBallsCounter = "Thrown Balls: " + value.ToString()));
    }
  }

  public int ItemCounter
  {
    get => this._ItemCounter;
    set
    {
      this._ItemCounter = value;
      Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.ItemCounter = "Items: " + value.ToString()));
    }
  }

  public int SelectedPokemon
  {
    get => this._SelectedPokemon;
    set
    {
      Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Poke = "Pokemon #" + value.ToString()));
      this._SelectedPokemon = value;
    }
  }

  public int SelectedPokemonManual
  {
    get => this._SelectedPokemonManual;
    set => this._SelectedPokemonManual = value;
  }

  public int AttackMove
  {
    get => this._AttackMove;
    set => this._AttackMove = value;
  }

  public bool UsedFalseSwipe
  {
    get => this._UsedFalseSwipe;
    set => this._UsedFalseSwipe = value;
  }

  public bool EncounteredSelectedPokemon
  {
    get => this._EncounteredSelectedPokemon;
    set => this._EncounteredSelectedPokemon = value;
  }

  public bool UsedSubstitute
  {
    get => this._UsedSubstitute;
    set => this._UsedSubstitute = value;
  }

  public bool UsedPayDay
  {
    get => this._UsedPayDay;
    set => this._UsedPayDay = value;
  }

  public bool UsedRock
  {
    get => this._UsedRock;
    set => this._UsedRock = value;
  }

  public bool UsedBait
  {
    get => this._UsedBait;
    set => this._UsedBait = value;
  }

  public bool FirstMovePP0
  {
    get => this._FirstMovePP0;
    set => this._FirstMovePP0 = value;
  }

  public bool SecondMovePP0
  {
    get => this._SecondMovePP0;
    set => this._SecondMovePP0 = value;
  }

  public bool ThirdMovePP0
  {
    get => this._ThirdMovePP0;
    set => this._ThirdMovePP0 = value;
  }

  public bool FourthMovePP0
  {
    get => this._FourthMovePP0;
    set => this._FourthMovePP0 = value;
  }

  public bool DetectedItem
  {
    get => this._DetectedItem;
    set => this._DetectedItem = value;
  }

  public bool FirstMainPokemonItem
  {
    get => this._FirstMainPokemonItem;
    set => this._FirstMainPokemonItem = value;
  }

  public bool FirstPokemonItem
  {
    get => this._FirstPokemonItem;
    set => this._FirstPokemonItem = value;
  }

  public bool SecondPokemonItem
  {
    get => this._SecondPokemonItem;
    set => this._SecondPokemonItem = value;
  }

  public bool ThirdPokemonItem
  {
    get => this._ThirdPokemonItem;
    set => this._ThirdPokemonItem = value;
  }

  public bool FourthPokemonItem
  {
    get => this._FourthPokemonItem;
    set => this._FourthPokemonItem = value;
  }

  public bool FifthPokemonItem
  {
    get => this._FifthPokemonItem;
    set => this._FifthPokemonItem = value;
  }

  public int LastAttackMove
  {
    get => this._LastAttackMove;
    set => this._LastAttackMove = value;
  }

  public int AFKCounter
  {
    get => this._AFKCounter;
    set => this._AFKCounter = value;
  }

  public int WalkCycle
  {
    get => this._WalkCycle;
    set
    {
      this._WalkCycle = value;
      Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.WalkCycle = "WalkCycle: " + value.ToString()));
    }
  }

  public string LastWalkDirection
  {
    get => this._LastWalkDirection;
    set => this._LastWalkDirection = value;
  }

  public bool GoTo
  {
    get => this._GoTo;
    set => this._GoTo = value;
  }

  public bool GoBack
  {
    get => this._GoBack;
    set => this._GoBack = value;
  }

  public int GoBackOnce
  {
    get => this._GoBackOnce;
    set => this._GoBackOnce = value;
  }

  public bool Heal
  {
    get => this._Heal;
    set => this._Heal = value;
  }

  public string UserStatus => MainViewModel.Instance.Auth.Status.Replace("Status: ", "") + " USER";
}
