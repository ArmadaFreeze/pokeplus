// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Botting.Battle
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

public class Battle
{
  private readonly Search search = new Search();
  private int[] _Coordinates;
  private int[] _Coordinates2;
  private int[] _Coordinates3;
  private int[] _CoordinatesTest;

  public void Shiny()
  {
    Includes.WindowHelper.BringProcessToFront();
    if (!Bot.Instance.Status.ShinyHelper)
    {
      Bot.Instance.Status.ShinyHelper = true;
      ++Bot.Instance.Status.ShinyCounter;
      DiscordBot.Instance.SendMessage(nameof (Shiny), true);
    }
    Sounds.PlayShinySound();
    Includes.WindowHelper.BringProcessToFront();
    Bot.Instance.Sleep(250);
    if ((Bot.Instance.Settings.StopOnShiny ? 1 : (Bot.Instance.Check.Horde ? 1 : 0)) != 0)
      Bot.Instance.Actions.StayAFKBattle();
    else
      Bot.Instance.Actions.ThrowBall();
  }

  public void Fight(IntPtr h, Action<IntPtr> specificLogic)
  {
    this._Coordinates3 = this.search.UseImageSearch("bin/img/Fight.png", Tolerance.Middle);
    if (this._Coordinates3 != null)
    {
      Bot.Instance.Actions.ResetAndUpdateWalkCycle();
      Bot.Instance.Status.IsInFight = true;
      Bot.Instance.Actions.Potion();
      if (!Bot.Instance.Check.CheckShiny())
        specificLogic(h);
    }
    if (!Bot.Instance.Check.NextPoke)
      return;
    Bot.Instance.Actions.NextPoke();
  }

  public void Run(IntPtr h)
  {
    this._Coordinates2 = this.search.UseImageSearch("bin/img/Run.png", Tolerance.Middle);
    if (this._Coordinates2 != null)
    {
      Bot.Instance.Actions.ResetAndUpdateWalkCycle();
      Bot.Instance.Status.IsInFight = true;
      if (!Bot.Instance.Check.CheckShiny())
      {
        if (!Bot.Instance.Check.CheckShiny())
        {
          InputMouse.LeftClick(this._Coordinates2[1], this._Coordinates2[2]);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime * 2);
        }
      }
      else
        this.Shiny();
    }
    if (Bot.Instance.Check.CantRun)
    {
      Bot.Instance.Sleep(2000);
      this.Fight(h, new Action<IntPtr>(this.FightNoPokemonCheck));
    }
    if (!Bot.Instance.Check.NextPoke)
      return;
    Bot.Instance.Actions.NextPoke();
  }

  public void Catch(IntPtr h)
  {
    this._Coordinates3 = this.search.UseImageSearch("bin/img/Bag.png", Tolerance.Middle);
    if (this._Coordinates3 != null)
    {
      Bot.Instance.Actions.ResetAndUpdateWalkCycle();
      Bot.Instance.Status.IsInFight = true;
      Bot.Instance.Actions.Potion();
      if (Bot.Instance.Check.CheckShiny())
        this.Shiny();
      else if ((Bot.Instance.Check.CheckShiny() ? 0 : (!Bot.Instance.Check.Horde ? 1 : 0)) == 0)
        this.Run(h);
      else
        this.CatchPokemonCheck(h);
    }
    if (!Bot.Instance.Check.NextPoke)
      return;
    Bot.Instance.Actions.NextPoke();
  }

  public void PayDay(IntPtr h)
  {
    if (Bot.Instance.Settings.BotMode == BotMode.PayDayThiefMixed)
    {
      this._Coordinates2 = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
      if ((this._Coordinates2 == null ? 0 : (!Bot.Instance.Status.Changed ? 1 : 0)) != 0)
      {
        if (Includes.ApplicationIsActivated())
        {
          InputMouse.LeftClick(this._Coordinates2[1], this._Coordinates2[2]);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          Bot.Instance.Actions.NextPokeManual();
          Bot.Instance.Status.Changed = true;
        }
      }
      else
      {
        this._Coordinates3 = this.search.UseImageSearch("bin/img/Fight.png", Tolerance.Middle);
        if (this._Coordinates3 != null)
        {
          Bot.Instance.Actions.ResetAndUpdateWalkCycle();
          Bot.Instance.Status.IsInFight = true;
          Bot.Instance.Actions.Potion();
          if (Bot.Instance.Check.CheckShiny())
            this.Shiny();
          else if (!Bot.Instance.Check.CheckShiny())
          {
            if (!Bot.Instance.Status.UsedPayDay)
            {
              InputMouse.LeftClick(this._Coordinates3[1], this._Coordinates3[2]);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
              this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
              if (this._Coordinates != null)
              {
                Bot.Instance.Actions.ScanMovePPStatus();
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                if ((!Bot.Instance.Settings.AutoWalkFish ? 0 : (Bot.Instance.Status.FirstMovePP0 ? 1 : 0)) == 0)
                {
                  if (!Bot.Instance.Status.FirstMovePP0)
                  {
                    if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
                      InputMouse.LeftClick(400, 700);
                    else if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
                      InputMouse.LeftClick(305, 440);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
                      InputMouse.LeftClick(400, 700);
                    else if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
                      InputMouse.LeftClick(305, 440);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    DiscordBot.Instance.SendMessage(nameof (PayDay), true);
                    if (Bot.Instance.Settings.PayDayMultiTarget)
                      Bot.Instance.Status.UsedPayDay = true;
                  }
                  else
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    this._Coordinates2 = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
                    if (this._Coordinates2 != null && Includes.ApplicationIsActivated())
                    {
                      InputMouse.LeftClick(this._Coordinates2[1], this._Coordinates2[2]);
                      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                      Bot.Instance.Actions.NextPokeManual();
                    }
                  }
                }
                else if (!Bot.Instance.Settings.MorePayDay)
                  Bot.Instance.Actions.RunandGoBack();
                else if (Bot.Instance.Status.SelectedPokemonManual == 6)
                {
                  Bot.Instance.Actions.RunandGoBack();
                }
                else
                {
                  InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  this._Coordinates2 = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
                  if (this._Coordinates2 != null && Includes.ApplicationIsActivated())
                  {
                    InputMouse.LeftClick(this._Coordinates2[1], this._Coordinates2[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    Bot.Instance.Actions.NextPokeManual();
                  }
                }
                Bot.Instance.Actions.ResetMovePPStatus();
              }
            }
            else
            {
              InputMouse.LeftClick(this._Coordinates3[1], this._Coordinates3[2]);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
              this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
              if (this._Coordinates != null)
              {
                Bot.Instance.Actions.ScanMovePPStatus();
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                if ((!Bot.Instance.Settings.AutoWalkFish ? 0 : (Bot.Instance.Status.SecondMovePP0 ? 1 : 0)) == 0)
                {
                  if (!Bot.Instance.Status.SecondMovePP0)
                  {
                    if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
                    {
                      if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
                        InputMouse.LeftClick(505, 440);
                    }
                    else
                      InputMouse.LeftClick(600, 700);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
                    {
                      if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
                        InputMouse.LeftClick(505, 440);
                    }
                    else
                      InputMouse.LeftClick(600, 700);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  }
                  else
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    this._Coordinates2 = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
                    if (this._Coordinates2 != null && Includes.ApplicationIsActivated())
                    {
                      InputMouse.LeftClick(this._Coordinates2[1], this._Coordinates2[2]);
                      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                      Bot.Instance.Actions.NextPokeManual();
                    }
                  }
                }
                else if (Bot.Instance.Settings.MorePayDay)
                {
                  if (Bot.Instance.Status.SelectedPokemonManual == 6)
                  {
                    Bot.Instance.Actions.RunandGoBack();
                  }
                  else
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    this._Coordinates2 = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
                    if (this._Coordinates2 != null && Includes.ApplicationIsActivated())
                    {
                      InputMouse.LeftClick(this._Coordinates2[1], this._Coordinates2[2]);
                      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                      Bot.Instance.Actions.NextPokeManual();
                    }
                  }
                }
                else
                  Bot.Instance.Actions.RunandGoBack();
                Bot.Instance.Actions.ResetMovePPStatus();
              }
            }
          }
        }
      }
    }
    else
    {
      this._Coordinates3 = this.search.UseImageSearch("bin/img/Fight.png", Tolerance.Middle);
      if (this._Coordinates3 != null)
      {
        Bot.Instance.Actions.ResetAndUpdateWalkCycle();
        Bot.Instance.Status.IsInFight = true;
        Bot.Instance.Actions.Potion();
        if (!Bot.Instance.Check.CheckShiny())
        {
          if (!Bot.Instance.Check.CheckShiny())
          {
            if (!Bot.Instance.Status.UsedPayDay)
            {
              InputMouse.LeftClick(this._Coordinates3[1], this._Coordinates3[2]);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
              this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
              if (this._Coordinates != null)
              {
                Bot.Instance.Actions.ScanMovePPStatus();
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                if ((!Bot.Instance.Settings.AutoWalkFish ? 0 : (Bot.Instance.Status.FirstMovePP0 ? 1 : 0)) != 0)
                {
                  if (!Bot.Instance.Settings.MorePayDay)
                    Bot.Instance.Actions.RunandGoBack();
                  else if (Bot.Instance.Status.SelectedPokemonManual != 6)
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    this._Coordinates2 = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
                    if (this._Coordinates2 != null && Includes.ApplicationIsActivated())
                    {
                      InputMouse.LeftClick(this._Coordinates2[1], this._Coordinates2[2]);
                      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                      Bot.Instance.Actions.NextPokeManual();
                    }
                  }
                  else
                    Bot.Instance.Actions.RunandGoBack();
                }
                else if (!Bot.Instance.Status.FirstMovePP0)
                {
                  if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
                    InputMouse.LeftClick(400, 700);
                  else if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
                    InputMouse.LeftClick(305, 440);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
                    InputMouse.LeftClick(400, 700);
                  else if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
                    InputMouse.LeftClick(305, 440);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  DiscordBot.Instance.SendMessage(nameof (PayDay), true);
                  if (Bot.Instance.Settings.PayDayMultiTarget)
                    Bot.Instance.Status.UsedPayDay = true;
                }
                else
                {
                  InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  this._Coordinates2 = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
                  if (this._Coordinates2 != null && Includes.ApplicationIsActivated())
                  {
                    InputMouse.LeftClick(this._Coordinates2[1], this._Coordinates2[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    Bot.Instance.Actions.NextPokeManual();
                  }
                }
                Bot.Instance.Actions.ResetMovePPStatus();
              }
            }
            else
            {
              InputMouse.LeftClick(this._Coordinates3[1], this._Coordinates3[2]);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
              this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
              if (this._Coordinates != null)
              {
                Bot.Instance.Actions.ScanMovePPStatus();
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                if ((!Bot.Instance.Settings.AutoWalkFish ? 0 : (Bot.Instance.Status.SecondMovePP0 ? 1 : 0)) != 0)
                {
                  if (!Bot.Instance.Settings.MorePayDay)
                    Bot.Instance.Actions.RunandGoBack();
                  else if (Bot.Instance.Status.SelectedPokemonManual == 6)
                  {
                    Bot.Instance.Actions.RunandGoBack();
                  }
                  else
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    this._Coordinates2 = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
                    if (this._Coordinates2 != null && Includes.ApplicationIsActivated())
                    {
                      InputMouse.LeftClick(this._Coordinates2[1], this._Coordinates2[2]);
                      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                      Bot.Instance.Actions.NextPokeManual();
                    }
                  }
                }
                else if (!Bot.Instance.Status.SecondMovePP0)
                {
                  if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
                  {
                    if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
                      InputMouse.LeftClick(505, 440);
                  }
                  else
                    InputMouse.LeftClick(600, 700);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
                    InputMouse.LeftClick(600, 700);
                  else if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
                    InputMouse.LeftClick(505, 440);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                }
                else
                {
                  InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  this._Coordinates2 = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
                  if (this._Coordinates2 != null && Includes.ApplicationIsActivated())
                  {
                    InputMouse.LeftClick(this._Coordinates2[1], this._Coordinates2[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    Bot.Instance.Actions.NextPokeManual();
                  }
                }
                Bot.Instance.Actions.ResetMovePPStatus();
              }
            }
          }
        }
        else
          this.Shiny();
      }
    }
    if (!Bot.Instance.Check.NextPoke)
      return;
    Bot.Instance.Actions.NextPoke();
  }

  public void Thief(IntPtr h)
  {
    this._CoordinatesTest = this.search.UseImageSearch("bin/img/Battle.png", Tolerance.Middle);
    if (this._CoordinatesTest != null)
    {
      Bot.Instance.Actions.ResetAndUpdateWalkCycle();
      Bot.Instance.Status.IsInFight = true;
      Bot.Instance.Actions.ScanPokemonItemStatus();
      if (!Bot.Instance.Check.CheckShiny())
      {
        if (!Bot.Instance.Check.CheckShiny())
          this.ThiefPokemonCheck(h);
      }
      else
        this.Shiny();
    }
    if (!Bot.Instance.Check.NextPoke)
      return;
    Bot.Instance.Actions.NextPoke();
  }

  public void ThiefWithOutPokemonCheck(IntPtr h)
  {
    this._CoordinatesTest = this.search.UseImageSearch("bin/img/Battle.png", Tolerance.Middle);
    if (this._CoordinatesTest != null)
    {
      Bot.Instance.Actions.ResetAndUpdateWalkCycle();
      Bot.Instance.Status.IsInFight = true;
      Bot.Instance.Actions.ScanPokemonItemStatus();
      if (!Bot.Instance.Check.CheckShiny())
      {
        if (!Bot.Instance.Check.CheckShiny())
          this.ThiefMechanics(h);
      }
      else
        this.Shiny();
    }
    if (!Bot.Instance.Check.NextPoke)
      return;
    Bot.Instance.Actions.NextPoke();
  }

  public void Safari(IntPtr h)
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/SafariC.png", Tolerance.Middle);
    if (this._Coordinates == null)
      return;
    Bot.Instance.Actions.ResetAndUpdateWalkCycle();
    Bot.Instance.Status.IsInFight = true;
    if (!Bot.Instance.Check.CheckShiny())
    {
      if (Bot.Instance.Check.CheckShiny())
        return;
      this.CatchPokemonCheck(h);
    }
    else
      this.Shiny();
  }

  public void FightNoPokemonCheck(IntPtr h)
  {
    if (Bot.Instance.Settings.LevelFirst)
    {
      this._Coordinates2 = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
      if ((this._Coordinates2 == null ? 0 : (!Bot.Instance.Status.Changed ? 1 : 0)) == 0)
      {
        InputMouse.LeftClick(this._Coordinates3[1], this._Coordinates3[2]);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      }
      else if (Includes.ApplicationIsActivated())
      {
        InputMouse.LeftClick(this._Coordinates2[1], this._Coordinates2[2]);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        Bot.Instance.Actions.NextPoke();
        Bot.Instance.Status.Changed = true;
      }
    }
    else
    {
      InputMouse.LeftClick(this._Coordinates3[1], this._Coordinates3[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    this._Coordinates = this.search.UseImageSearch("bin/img/Struggle.png", Tolerance.Middle);
    if (this._Coordinates != null)
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
      if (this._Coordinates == null)
        return;
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
      this._Coordinates = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
      if (this._Coordinates == null)
        return;
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
      Bot.Instance.Actions.NextPoke();
    }
    else if ((!Bot.Instance.Settings.MultiTarget || !Bot.Instance.Settings.AutoWalkFish && !Bot.Instance.Settings.AutoSweetScent && !Bot.Instance.Settings.SweetScent ? 0 : (!Bot.Instance.Status.MoveDisabled ? 1 : 0)) != 0)
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
      if ((this._Coordinates == null ? 0 : (!Bot.Instance.Check.NextPoke ? 1 : 0)) == 0)
        return;
      Bot.Instance.Actions.ScanMovePPStatus();
      if ((!Bot.Instance.Settings.AutoWalkFish ? 0 : (Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 ? 1 : (Bot.Instance.Status.FourthMovePP0 ? 1 : 0))) == 0)
      {
        if (!Bot.Instance.Status.FirstMovePP0)
        {
          if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
            InputMouse.LeftClick(400, 700);
          else if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
            InputMouse.LeftClick(305, 440);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
            InputMouse.LeftClick(400, 700);
          else if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
            InputMouse.LeftClick(305, 440);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        }
        else
        {
          this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
          if (this._Coordinates != null)
          {
            InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
            this._Coordinates = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
            if (this._Coordinates != null)
            {
              InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
              Bot.Instance.Actions.NextPoke();
            }
          }
        }
      }
      else
        Bot.Instance.Actions.RunandGoBack();
      Bot.Instance.Actions.ResetMovePPStatus();
    }
    else
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/Super.png", Tolerance.Middle);
      if ((this._Coordinates == null || Bot.Instance.Check.PPSuperEffective0 ? 0 : (!Bot.Instance.Status.MoveDisabled ? 1 : 0)) != 0)
      {
        InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
        {
          if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
            InputMouse.LeftClick(305, 440);
        }
        else
          InputMouse.LeftClick(400, 700);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      }
      this._Coordinates = this.search.UseImageSearch("bin/img/Effective.png", Tolerance.Middle);
      if ((this._Coordinates == null || Bot.Instance.Check.PPEffective0 ? 0 : (!Bot.Instance.Status.MoveDisabled ? 1 : 0)) != 0)
      {
        InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
        {
          if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
            InputMouse.LeftClick(305, 440);
        }
        else
          InputMouse.LeftClick(400, 700);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      }
      this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
      if ((this._Coordinates == null ? 0 : (!Bot.Instance.Check.NextPoke ? 1 : 0)) == 0)
        return;
      do
      {
        Bot.Instance.Status.AttackMove = RandomNumber.Between(1, 4);
      }
      while (Bot.Instance.Status.AttackMove == Bot.Instance.Status.LastAttackMove);
      Bot.Instance.Status.LastAttackMove = Bot.Instance.Status.AttackMove;
      Bot.Instance.Actions.ScanMovePPStatus();
      if ((!Bot.Instance.Settings.AutoWalkFish ? 0 : (Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 ? 1 : (Bot.Instance.Status.FourthMovePP0 ? 1 : 0))) != 0)
        Bot.Instance.Actions.RunandGoBack();
      else if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
      {
        if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
        {
          if (Bot.Instance.Status.AttackMove != 1)
          {
            if (Bot.Instance.Status.AttackMove != 2)
            {
              if (Bot.Instance.Status.AttackMove != 3)
              {
                if (Bot.Instance.Status.AttackMove == 4)
                  InputMouse.LeftClick(495, 495);
              }
              else
                InputMouse.LeftClick(305, 495);
            }
            else
              InputMouse.LeftClick(505, 440);
          }
          else
            InputMouse.LeftClick(305, 440);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          InputMouse.LeftClick(305, 440);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        }
      }
      else
      {
        if ((Bot.Instance.Status.AttackMove != 1 ? 0 : (!Bot.Instance.Status.FirstMovePP0 ? 1 : 0)) != 0)
          InputMouse.LeftClick(400, 700);
        else if ((Bot.Instance.Status.AttackMove != 2 ? 0 : (!Bot.Instance.Status.SecondMovePP0 ? 1 : 0)) == 0)
        {
          if ((Bot.Instance.Status.AttackMove != 3 ? 0 : (!Bot.Instance.Status.ThirdMovePP0 ? 1 : 0)) == 0)
          {
            if ((Bot.Instance.Status.AttackMove != 4 ? 0 : (!Bot.Instance.Status.FourthMovePP0 ? 1 : 0)) != 0)
              InputMouse.LeftClick(600, 750);
          }
          else
            InputMouse.LeftClick(400, 750);
        }
        else
          InputMouse.LeftClick(600, 700);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        InputMouse.LeftClick(400, 700);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      }
      Bot.Instance.Actions.ResetMovePPStatus();
    }
  }

  public void ThiefMechanics(IntPtr h)
  {
    if ((!Bot.Instance.Status.DetectedItem ? 0 : (!Bot.Instance.Check.ThiefPokemonItem ? 1 : 0)) == 0)
    {
      if ((!Bot.Instance.Status.DetectedItem || !Bot.Instance.Check.ThiefPokemonItem ? 0 : (Bot.Instance.Settings.MoreThief ? 1 : 0)) != 0)
      {
        if (Bot.Instance.Status.ThiefHelper)
        {
          this._Coordinates3 = this.search.UseImageSearch("bin/img/Fight.png", Tolerance.Middle);
          if (this._Coordinates3 == null)
            return;
          Bot.Instance.Actions.Potion();
          InputMouse.LeftClick(this._Coordinates3[1], this._Coordinates3[2]);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
          this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
          if (this._Coordinates == null)
            return;
          Bot.Instance.Actions.ScanMovePPStatus();
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          if ((!Bot.Instance.Settings.AutoWalkFish ? 0 : (Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 ? 1 : (Bot.Instance.Status.FourthMovePP0 ? 1 : 0))) == 0)
          {
            if ((Bot.Instance.Status.ImprisonHelper || !Bot.Instance.Settings.Imprison ? 0 : (!Bot.Instance.Status.SecondMovePP0 ? 1 : 0)) == 0)
            {
              if (Bot.Instance.Status.FirstMovePP0)
              {
                InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                this._Coordinates2 = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
                if ((this._Coordinates2 == null ? 0 : (!Bot.Instance.Status.Changed ? 1 : 0)) != 0 && Includes.ApplicationIsActivated())
                {
                  InputMouse.LeftClick(this._Coordinates2[1], this._Coordinates2[2]);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  Bot.Instance.Actions.NextPokeManual();
                }
              }
              else
              {
                if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
                  InputMouse.LeftClick(400, 700);
                else if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
                  InputMouse.LeftClick(305, 440);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
                  InputMouse.LeftClick(400, 700);
                else if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
                  InputMouse.LeftClick(305, 440);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              }
            }
            else
            {
              if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
                InputMouse.LeftClick(620, 700);
              else if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
                InputMouse.LeftClick(530, 450);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
                InputMouse.LeftClick(630, 750);
              else
                InputMouse.LeftClick(530, 500);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              Bot.Instance.Status.ImprisonHelper = true;
            }
          }
          else
            Bot.Instance.Actions.RunandGoBack();
          Bot.Instance.Actions.ResetMovePPStatus();
        }
        else
        {
          this._Coordinates3 = this.search.UseImageSearch("bin/img/Fight.png", Tolerance.Middle);
          if (this._Coordinates3 == null)
            return;
          Bot.Instance.Actions.Potion();
          InputMouse.LeftClick(this._Coordinates3[1], this._Coordinates3[2]);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
          if (this._Coordinates == null)
            return;
          InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          this._Coordinates2 = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
          if ((this._Coordinates2 == null ? 0 : (!Bot.Instance.Status.Changed ? 1 : 0)) == 0 || !Includes.ApplicationIsActivated())
            return;
          InputMouse.LeftClick(this._Coordinates2[1], this._Coordinates2[2]);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          Bot.Instance.Actions.NextPokeManual();
          Bot.Instance.Status.ThiefHelper = true;
        }
      }
      else if (Bot.Instance.Settings.BotMode != BotMode.PayDayThiefMixed)
        this.Run(h);
      else
        this.PayDay(h);
    }
    else
    {
      this._Coordinates3 = this.search.UseImageSearch("bin/img/Fight.png", Tolerance.Middle);
      if (this._Coordinates3 == null)
        return;
      Bot.Instance.Actions.Potion();
      InputMouse.LeftClick(this._Coordinates3[1], this._Coordinates3[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
      if (this._Coordinates == null)
        return;
      Bot.Instance.Actions.ScanMovePPStatus();
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      if ((!Bot.Instance.Settings.AutoWalkFish ? 0 : (Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 ? 1 : (Bot.Instance.Status.FourthMovePP0 ? 1 : 0))) == 0)
      {
        if ((Bot.Instance.Status.ImprisonHelper || !Bot.Instance.Settings.Imprison ? 0 : (!Bot.Instance.Status.SecondMovePP0 ? 1 : 0)) == 0)
        {
          if (!Bot.Instance.Status.FirstMovePP0)
          {
            if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
            {
              if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
                InputMouse.LeftClick(305, 440);
            }
            else
              InputMouse.LeftClick(400, 700);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
              InputMouse.LeftClick(400, 700);
            else if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
              InputMouse.LeftClick(305, 440);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          }
          else
          {
            InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            this._Coordinates2 = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
            if ((this._Coordinates2 == null ? 0 : (!Bot.Instance.Status.Changed ? 1 : 0)) != 0 && Includes.ApplicationIsActivated())
            {
              InputMouse.LeftClick(this._Coordinates2[1], this._Coordinates2[2]);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              Bot.Instance.Actions.NextPokeManual();
            }
          }
        }
        else
        {
          if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
          {
            if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
              InputMouse.LeftClick(530, 450);
          }
          else
            InputMouse.LeftClick(620, 700);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
            InputMouse.LeftClick(630, 750);
          else
            InputMouse.LeftClick(530, 500);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          Bot.Instance.Status.ImprisonHelper = true;
        }
      }
      else
        Bot.Instance.Actions.RunandGoBack();
      Bot.Instance.Actions.ResetMovePPStatus();
    }
  }

  public void PayDayCatchMixed(IntPtr h)
  {
    if (!(MainViewModel.Instance.Home.CatchPokemon == Pokemon.All.ToString()))
    {
      if (!(MainViewModel.Instance.Home.CatchPokemon == Pokemon.Uncaught.ToString()))
      {
        if (MainViewModel.Instance.Home.CatchPokemon == Bot.Instance.Check.CheckSelectedPokemon())
        {
          Bot.Instance.Actions.SelectedCatchPokemonCounter();
          Bot.Instance.Actions.Catch();
        }
        else
          this.PayDay(h);
      }
      else if (!Bot.Instance.Check.Catched)
        Bot.Instance.Actions.Catch();
      else
        this.PayDay(h);
    }
    else
      this.PayDay(h);
  }

  public void PayDayThiefMixed(IntPtr h)
  {
    if (MainViewModel.Instance.Home.CatchPokemon == Pokemon.All.ToString())
      this.ThiefWithOutPokemonCheck(h);
    else if (MainViewModel.Instance.Home.CatchPokemon == Pokemon.Uncaught.ToString())
    {
      if (!Bot.Instance.Check.Catched)
        this.ThiefWithOutPokemonCheck(h);
      else
        this.PayDay(h);
    }
    else if (!(MainViewModel.Instance.Home.CatchPokemon == Bot.Instance.Check.CheckSelectedPokemon()))
    {
      this.PayDay(h);
    }
    else
    {
      Bot.Instance.Actions.SelectedCatchPokemonCounter();
      this.ThiefWithOutPokemonCheck(h);
    }
  }

  public void FightPokemonCheck(IntPtr h)
  {
    if (MainViewModel.Instance.Home.CatchPokemon == Pokemon.All.ToString())
      this.FightNoPokemonCheck(h);
    else if (MainViewModel.Instance.Home.CatchPokemon == Pokemon.Uncaught.ToString())
    {
      if (!Bot.Instance.Check.Catched)
        this.FightNoPokemonCheck(h);
      else
        this.Run(h);
    }
    else if (!(MainViewModel.Instance.Home.CatchPokemon == Bot.Instance.Check.CheckSelectedPokemon()))
    {
      this.Run(h);
    }
    else
    {
      Bot.Instance.Actions.SelectedCatchPokemonCounter();
      this.FightNoPokemonCheck(h);
    }
  }

  public void CatchPokemonCheck(IntPtr h)
  {
    if (!(MainViewModel.Instance.Home.CatchPokemon == Pokemon.All.ToString()))
    {
      if (MainViewModel.Instance.Home.CatchPokemon == Pokemon.Uncaught.ToString())
      {
        if (Bot.Instance.Check.Catched)
          this.Run(h);
        else
          Bot.Instance.Actions.Catch();
      }
      else if (MainViewModel.Instance.Home.CatchPokemon == Bot.Instance.Check.CheckSelectedPokemon())
      {
        Bot.Instance.Actions.SelectedCatchPokemonCounter();
        Bot.Instance.Actions.Catch();
      }
      else
        this.Run(h);
    }
    else
      Bot.Instance.Actions.Catch();
  }

  public void ThiefPokemonCheck(IntPtr h)
  {
    if (MainViewModel.Instance.Home.CatchPokemon == Pokemon.All.ToString())
      this.ThiefMechanics(h);
    else if (!(MainViewModel.Instance.Home.CatchPokemon == Pokemon.Uncaught.ToString()))
    {
      if (!(MainViewModel.Instance.Home.CatchPokemon == Bot.Instance.Check.CheckSelectedPokemon()))
      {
        this.Run(h);
      }
      else
      {
        Bot.Instance.Actions.SelectedCatchPokemonCounter();
        this.ThiefMechanics(h);
      }
    }
    else if (!Bot.Instance.Check.Catched)
      this.ThiefMechanics(h);
    else
      this.Run(h);
  }
}
