// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Botting.State
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Classes;
using PokeMMO_.Input;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;
using System;

#nullable disable
namespace PokeMMO_.Botting;

public class State
{
  private Search search = new Search();
  private int[] _Coordinates;

  public void InMainWindow()
  {
    this.ResetStatusVariables();
    this.EncountersCounter();
    if (Bot.Instance.Check.Captcha)
    {
      DiscordBot.Instance.SendMessage("Captcha", false);
      if (MainViewModel.Instance.Home.PremiumEnabled)
        Bot.Instance.Actions.SolveCaptcha();
      Sounds.PlayAlertSound();
    }
    if (Bot.Instance.Settings.Lure)
    {
      if (!Bot.Instance.Settings.AutoSweetScent)
        Bot.Instance.Actions.UseLure();
      Bot.Instance.Actions.Lure();
    }
    if (Bot.Instance.Settings.AutoEther && !Bot.Instance.Settings.AutoSweetScent)
      Bot.Instance.Actions.UseEther();
    this.Skips();
    if ((Bot.Instance.Settings.BotMode == BotMode.Thief || Bot.Instance.Settings.BotMode == BotMode.PayDayThiefMixed ? (MainViewModel.Instance.Premium.PremiumEnabled ? 1 : 0) : 0) != 0)
    {
      Bot.Instance.Actions.TakeItem();
      Bot.Instance.Actions.TakeItem();
    }
    if ((BotSettings.Settings.AlertPM ? 1 : (BotSettings.Settings.StopPM ? 1 : 0)) != 0 && Bot.Instance.Check.PM)
    {
      if (BotSettings.Settings.AlertPM)
      {
        Sounds.PlayPMSound();
        Sounds.PlayPMSound();
        Sounds.PlayPMSound();
      }
      if (BotSettings.Settings.StopPM)
        Bot.Instance.Stop();
    }
    if ((!Bot.Instance.Settings.OnlyKeepIV31 ? 0 : (!Bot.Instance.Status.ShinyHelper ? 1 : 0)) != 0)
      Bot.Instance.Actions.Stats();
    else
      Bot.Instance.Actions.CloseStats();
    Bot.Instance.Status.ShinyHelper = false;
    Bot.Instance.Actions.Humanize();
  }

  public void InBattleWindow(IntPtr h)
  {
    if ((Bot.Instance.Check.Login ? 0 : (Includes.ApplicationIsActivated() ? 1 : 0)) == 0)
      return;
    Bot.Instance.Status.HumanizeHelper = true;
    if (MainViewModel.Instance.Home.PremiumEnabled)
    {
      try
      {
        if (Bot.Instance.Check.Captcha)
        {
          DiscordBot.Instance.SendMessage("Captcha", false);
          if (MainViewModel.Instance.Home.PremiumEnabled)
            Bot.Instance.Actions.SolveCaptcha();
          Sounds.PlayAlertSound();
        }
      }
      catch (Exception ex)
      {
        PokeMMOLogger.Instance.Log(ex.Message);
      }
    }
    Bot.Instance.Check.CheckDisabled();
    if (Bot.Instance.Settings.SkipEvolve)
      Bot.Instance.Actions.SkipEvolve();
    if (Bot.Instance.Check.RepelInBattle)
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/No.png", Tolerance.Middle);
      if (this._Coordinates != null)
      {
        InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      }
    }
    if (Bot.Instance.Settings.BotMode == BotMode.Fight)
      Bot.Instance.Battle.Fight(h, new Action<IntPtr>(Bot.Instance.Battle.FightPokemonCheck));
    else if (Bot.Instance.Settings.BotMode != BotMode.Run)
    {
      if (Bot.Instance.Settings.BotMode == BotMode.Catch)
        Bot.Instance.Battle.Catch(h);
      else if ((Bot.Instance.Settings.BotMode != BotMode.PayDay ? 0 : (MainViewModel.Instance.Premium.PremiumEnabled ? 1 : 0)) == 0)
      {
        if ((Bot.Instance.Settings.BotMode != BotMode.PayDayCatchMixed ? 0 : (MainViewModel.Instance.Premium.PremiumEnabled ? 1 : 0)) != 0)
          Bot.Instance.Battle.PayDayCatchMixed(h);
        else if ((Bot.Instance.Settings.BotMode != BotMode.PayDayThiefMixed ? 0 : (MainViewModel.Instance.Premium.PremiumEnabled ? 1 : 0)) != 0)
          Bot.Instance.Battle.PayDayThiefMixed(h);
        else if ((Bot.Instance.Settings.BotMode != BotMode.Thief ? 0 : (MainViewModel.Instance.Premium.PremiumEnabled ? 1 : 0)) != 0)
          Bot.Instance.Battle.Thief(h);
        else if ((Bot.Instance.Settings.BotMode != BotMode.Safari ? 0 : (MainViewModel.Instance.Premium.PremiumEnabled ? 1 : 0)) == 0)
        {
          if (Bot.Instance.Settings.BotMode != BotMode.None)
            ;
        }
        else
          Bot.Instance.Battle.Safari(h);
      }
      else
        Bot.Instance.Battle.PayDay(h);
    }
    else
      Bot.Instance.Battle.Run(h);
  }

  public void Skips()
  {
    if (Bot.Instance.Settings.SkipLearningNew)
      Bot.Instance.Actions.SkipLearnMove();
    if ((!BotSettings.Settings.SkipDialog || Bot.Instance.Settings.Fish || Bot.Instance.Settings.AutoWalkFish ? 0 : (!Bot.Instance.Settings.SafariAutoFish ? 1 : 0)) == 0)
      return;
    Bot.Instance.Actions.SkipDialogue();
  }

  public void ResetStatusVariables()
  {
    Bot.Instance.Status.SelectedPokemonManual = 1;
    Bot.Instance.Status.MoveDisabled = false;
    Bot.Instance.Status.Changed = false;
    Bot.Instance.Status.UsedFalseSwipe = false;
    Bot.Instance.Status.UsedSubstitute = false;
    Bot.Instance.Status.UsedPayDay = false;
    Bot.Instance.Status.UsedRock = false;
    Bot.Instance.Status.UsedBait = false;
    Bot.Instance.Status.EncounteredSelectedPokemon = false;
    Bot.Instance.Status.ThiefHelper = false;
    Bot.Instance.Status.ImprisonHelper = false;
    Bot.Instance.Status.DetectedItem = false;
  }

  public void EncountersCounter()
  {
    if (!Bot.Instance.Status.IsInFight)
      return;
    ++Bot.Instance.Status.EncountersCounter;
    Bot.Instance.Status.IsInFight = false;
  }

  public void Login()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/DC.png", Tolerance.Middle);
    if (this._Coordinates != null)
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2] + 15);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    Bot.Instance.Sleep(1000);
    this._Coordinates = this.search.UseImageSearch("bin/img/DCLogin.png", Tolerance.Middle);
    if (this._Coordinates != null)
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2] + 15);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    Bot.Instance.Sleep(1000);
    this._Coordinates = this.search.UseImageSearch("bin/img/Session.png", Tolerance.Middle);
    if (this._Coordinates != null)
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2] + 15);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    Bot.Instance.Sleep(1000);
    this._Coordinates = this.search.UseImageSearch("bin/img/Login.png", Tolerance.Middle);
    if (this._Coordinates != null)
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    Bot.Instance.Sleep(1000);
    this._Coordinates = this.search.UseImageSearch("bin/img/Login.png", Tolerance.Middle);
    if (this._Coordinates != null)
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    Bot.Instance.Sleep(1000);
    this._Coordinates = this.search.UseImageSearch("bin/img/Character.png", Tolerance.Middle);
    if (this._Coordinates == null)
      return;
    InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
  }
}
