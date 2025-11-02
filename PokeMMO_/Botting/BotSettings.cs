// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Botting.BotSettings
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Classes;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;
using System.Collections.Generic;

#nullable disable
namespace PokeMMO_.Botting;

public class BotSettings
{
  private static readonly object padlock = new object();
  private static BotSettings settings = (BotSettings) null;
  public Dictionary<string, string> Data = new Dictionary<string, string>();

  public static BotSettings Settings
  {
    get
    {
      lock (BotSettings.padlock)
      {
        if (BotSettings.settings == null)
          BotSettings.settings = new BotSettings();
        return BotSettings.settings;
      }
    }
  }

  public int WalkSpeed
  {
    get
    {
      return RandomNumber.Between(Bot.Instance.Actions.SafeWalkFromInt(), Bot.Instance.Actions.SafeWalkToInt());
    }
  }

  public int HoldTime => RandomNumber.Between(100, 150);

  public int WaitTimeShort => RandomNumber.Between(50, 100);

  public int WaitTimeShortRandom => RandomNumber.Between(100, 600);

  public int WaitTime => RandomNumber.Between(150, 200);

  public int WaitTimeLong => RandomNumber.Between(250, 300);

  public int WaitTimeVeryLong => RandomNumber.Between(500, 600);

  public int WaitTimeHuman => RandomNumber.Between(1000, 10000);

  public int AutoWalkFishRoutesSelectedIndex
  {
    get => MainViewModel.Instance.Home.AutoWalkFishRoutesSelectedIndex;
  }

  public int AutoSweetScentRoutesSelectedIndex
  {
    get => MainViewModel.Instance.Home.AutoSweetScentRoutesSelectedIndex;
  }

  public int SafariAutoWalkRoutesSelectedIndex
  {
    get => MainViewModel.Instance.Home.SafariAutoWalkRoutesSelectedIndex;
  }

  public int SafariAutoFishRoutesSelectedIndex
  {
    get => MainViewModel.Instance.Home.SafariAutoFishRoutesSelectedIndex;
  }

  public string SellPrice => MainViewModel.Instance.Home.SellPrice;

  public bool PotionSystem
  {
    get
    {
      return MainViewModel.Instance.Premium.PremiumEnabled && MainViewModel.Instance.Premium.PotionSystem;
    }
  }

  public bool Substitute
  {
    get
    {
      return MainViewModel.Instance.Premium.PremiumEnabled && MainViewModel.Instance.Home.Options[0].Selected;
    }
  }

  public bool FalseSwipe
  {
    get
    {
      return MainViewModel.Instance.Premium.PremiumEnabled && MainViewModel.Instance.Home.Options[1].Selected;
    }
  }

  public bool Spore
  {
    get
    {
      return MainViewModel.Instance.Premium.PremiumEnabled && MainViewModel.Instance.Home.Options[2].Selected;
    }
  }

  public bool Assist
  {
    get
    {
      return MainViewModel.Instance.Premium.PremiumEnabled && MainViewModel.Instance.Home.Options[3].Selected;
    }
  }

  public bool TeleportBack
  {
    get
    {
      return MainViewModel.Instance.Premium.PremiumEnabled && MainViewModel.Instance.Premium.TeleportBack;
    }
  }

  public bool EscapeRope
  {
    get
    {
      return MainViewModel.Instance.Premium.PremiumEnabled && MainViewModel.Instance.Premium.EscapeRope;
    }
  }

  public bool SlowMode
  {
    get => MainViewModel.Instance.Premium.PremiumEnabled && MainViewModel.Instance.Premium.SlowMode;
  }

  public bool MultiTarget
  {
    get
    {
      return MainViewModel.Instance.Premium.PremiumEnabled && MainViewModel.Instance.Premium.MultiTarget;
    }
  }

  public int OrangePotionSelectedIndex => MainViewModel.Instance.Premium.OrangePotionSelectedIndex;

  public int RedPotionSelectedIndex => MainViewModel.Instance.Premium.RedPotionSelectedIndex;

  public int WalkCyclesTrigger => MainViewModel.Instance.Security.WalkCyclesTrigger;

  public int TurnOffTrigger => MainViewModel.Instance.Security.TurnOffTrigger;

  public bool LevelFirst => MainViewModel.Instance.Home.LevelFirst;

  public bool CatchWithSecondPokemon => MainViewModel.Instance.Home.CatchWithSecondPokemon;

  public bool Humanize => MainViewModel.Instance.Security.Humanize;

  public bool SquaresWalkPattern => MainViewModel.Instance.Home.SquaresWalkPattern;

  public bool RandomWalkPattern => MainViewModel.Instance.Home.RandomWalkPattern;

  public bool HumanizeMouseMovement => MainViewModel.Instance.Settings.HumanizeMouseMovement;

  public bool Login => MainViewModel.Instance.Home.Login;

  public bool OnlyKeepIV31 => MainViewModel.Instance.Home.OnlyKeepIV31;

  public bool TurnOff => MainViewModel.Instance.Security.TurnOff;

  public bool MoreThief => MainViewModel.Instance.Home.MoreThief;

  public bool MorePayDay => MainViewModel.Instance.Home.MorePayDay;

  public bool Imprison => MainViewModel.Instance.Home.Imprison;

  public bool Rock => MainViewModel.Instance.Home.Rock;

  public bool Lure => MainViewModel.Instance.Home.Lure;

  public bool Bait => MainViewModel.Instance.Home.Bait;

  public bool PayDayMultiTarget => MainViewModel.Instance.Home.PayDayMultiTarget;

  public bool AutoChannelSwitch => MainViewModel.Instance.Security.AutoChannelSwitch;

  public bool Break => MainViewModel.Instance.Security.Break;

  public bool CatchShiny => MainViewModel.Instance.Home.CatchShiny;

  public bool StopOnShiny => MainViewModel.Instance.Home.StopOnShiny;

  public bool SkipDialog => MainViewModel.Instance.Home.SkipDialog;

  public bool SkipLearningNew => MainViewModel.Instance.Home.SkipLearningNew;

  public bool SkipEvolve => MainViewModel.Instance.Home.SkipEvolve;

  public bool AlertPM => MainViewModel.Instance.Security.AlertPM;

  public bool StopPM => MainViewModel.Instance.Security.StopPM;

  public bool AlertWalkCycles => MainViewModel.Instance.Security.AlertWalkCycles;

  public bool StopWalkCycles => MainViewModel.Instance.Security.StopWalkCycles;

  public bool AlertSweetScent => MainViewModel.Instance.Security.AlertSweetScent;

  public BotMode BotMode => MainViewModel.Instance.Home.BotMode;

  public bool Walk => MainViewModel.Instance.Home.Walk;

  public bool Fish => MainViewModel.Instance.Home.Fish;

  public bool SweetScent => MainViewModel.Instance.Home.SweetScent;

  public bool AutoLeppa => MainViewModel.Instance.Home.AutoLeppa;

  public bool AutoEther => MainViewModel.Instance.Home.AutoEther;

  public bool AutoWalkFish => MainViewModel.Instance.Home.AutoWalkFish;

  public bool AutoSweetScent => MainViewModel.Instance.Home.AutoSweetScent;

  public bool SafariAutoWalk => MainViewModel.Instance.Home.SafariAutoWalk;

  public bool SafariAutoFish => MainViewModel.Instance.Home.SafariAutoFish;

  public ResolutionMode ResolutionMode => MainViewModel.Instance.Settings.ResolutionMode;

  public ChosenPokeBall ChosenPokeBall => ChosenPokeBallViewModel.Instance.ChosenPokeBall;

  public CatchMovesRoutine CatchMovesRoutine => MainViewModel.Instance.Premium.CatchMovesRoutine;

  public bool PrimaryMouseButton => MainViewModel.Instance.Settings.PrimaryMouseButton;
}
