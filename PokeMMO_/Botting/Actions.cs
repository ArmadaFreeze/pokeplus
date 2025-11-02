// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Botting.Actions
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using _2CaptchaAPI;
using _2CaptchaAPI.Enums;
using PokeMMO_.Classes;
using PokeMMO_.Input;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Windows;

#nullable disable
namespace PokeMMO_.Botting;

public class Actions
{
  private Search search = new Search();
  private int[] _Coordinates;

  public void StayAFKBattle()
  {
    if (Bot.Instance.Status.AFKCounter != 0)
    {
      ++Bot.Instance.Status.AFKCounter;
      if (Bot.Instance.Status.AFKCounter == 100)
        Bot.Instance.Status.AFKCounter = 0;
    }
    else
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/Bag.png", Tolerance.High);
      if (this._Coordinates != null)
      {
        InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        this._Coordinates = this.search.UseImageSearch("bin/img/Medicine.png", Tolerance.High);
        if (this._Coordinates != null)
        {
          InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        }
        else
        {
          InputKeyboard.PressKeyLeft(Bot.Instance.Settings.HoldTime);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          InputKeyboard.PressKeyLeft(Bot.Instance.Settings.HoldTime);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          InputKeyboard.PressKeyLeft(Bot.Instance.Settings.HoldTime);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        }
        this._Coordinates = this.search.UseImageSearch("bin/img/Medicine.png", Tolerance.High);
        if (this._Coordinates == null)
        {
          this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.High);
          if (this._Coordinates != null)
          {
            InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          }
        }
        else
        {
          InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        }
      }
      ++Bot.Instance.Status.AFKCounter;
    }
    Bot.Instance.Sleep(Bot.Instance.Settings.WalkSpeed);
  }

  public void RunandGoBack()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
    if (this._Coordinates != null)
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
    }
    Bot.Instance.Battle.Run(Bot.Instance.Handle);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
    Bot.Instance.Status.GoBack = true;
    ++Bot.Instance.Status.GoBackOnce;
  }

  public void Catch()
  {
    if (Bot.Instance.Settings.BotMode == BotMode.Safari)
    {
      if ((!Bot.Instance.Settings.Rock ? 0 : (!Bot.Instance.Status.UsedRock ? 1 : 0)) != 0)
      {
        if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
          InputMouse.LeftClick(320, 500);
        else
          InputMouse.LeftClick(410, 750);
        Bot.Instance.Status.UsedRock = true;
      }
      else if ((!Bot.Instance.Settings.Bait ? 0 : (!Bot.Instance.Status.UsedBait ? 1 : 0)) != 0)
      {
        if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
          InputMouse.LeftClick(530, 450);
        else
          InputMouse.LeftClick(620, 700);
        Bot.Instance.Status.UsedBait = true;
      }
      else
      {
        if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
          InputMouse.LeftClick(400, 700);
        else if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
          InputMouse.LeftClick(305, 440);
        ++Bot.Instance.Status.ThrownBallsCounter;
        DiscordBot.Instance.SendMessage("SafariBall", true);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
        if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
          InputMouse.LeftClick(530, 500);
        else
          InputMouse.LeftClick(630, 750);
      }
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    else if ((!Bot.Instance.Settings.CatchWithSecondPokemon ? 0 : (!Bot.Instance.Status.Changed ? 1 : 0)) == 0)
    {
      if ((Bot.Instance.Settings.Substitute || Bot.Instance.Settings.Spore || Bot.Instance.Settings.FalseSwipe ? 1 : (Bot.Instance.Settings.Assist ? 1 : 0)) == 0)
      {
        this.ThrowBall();
      }
      else
      {
        this._Coordinates = this.search.UseImageSearch("bin/img/Bag.png", Tolerance.Middle);
        if (this._Coordinates == null)
          return;
        if (Bot.Instance.Settings.CatchMovesRoutine != CatchMovesRoutine.SFS)
        {
          if (Bot.Instance.Settings.CatchMovesRoutine != CatchMovesRoutine.SFS)
          {
            if (Bot.Instance.Settings.CatchMovesRoutine == CatchMovesRoutine.FA)
            {
              if ((!Bot.Instance.Settings.FalseSwipe ? 0 : (!Bot.Instance.Status.UsedFalseSwipe ? 1 : 0)) != 0)
              {
                this._Coordinates = this.search.UseImageSearch("bin/img/Fight.png", Tolerance.Middle);
                if (this._Coordinates != null)
                {
                  InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
                }
                if (!Bot.Instance.Settings.AutoWalkFish)
                {
                  this._Coordinates = this.search.UseImageSearch("bin/img/FalseSwipe.png", Tolerance.Middle);
                  if (this._Coordinates != null)
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Status.UsedFalseSwipe = true;
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  }
                  else
                  {
                    this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                    if (this._Coordinates != null)
                    {
                      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    }
                  }
                }
                else
                {
                  Bot.Instance.Actions.ScanMovePPStatus();
                  if ((Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 ? 1 : (Bot.Instance.Status.FourthMovePP0 ? 1 : 0)) == 0)
                  {
                    this._Coordinates = this.search.UseImageSearch("bin/img/FalseSwipe.png", Tolerance.Middle);
                    if (this._Coordinates == null)
                    {
                      this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                      if (this._Coordinates != null)
                      {
                        InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                      }
                    }
                    else
                    {
                      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                      Bot.Instance.Status.UsedFalseSwipe = true;
                      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    }
                  }
                  else
                    this.RunandGoBack();
                  Bot.Instance.Actions.ResetMovePPStatus();
                }
              }
              if (Bot.Instance.Settings.Assist && !Bot.Instance.Check.Sleep)
              {
                this._Coordinates = this.search.UseImageSearch("bin/img/Fight.png", Tolerance.Middle);
                if (this._Coordinates != null)
                {
                  InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
                }
                if (Bot.Instance.Settings.AutoWalkFish)
                {
                  Bot.Instance.Actions.ScanMovePPStatus();
                  if ((Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 ? 1 : (Bot.Instance.Status.FourthMovePP0 ? 1 : 0)) == 0)
                  {
                    this._Coordinates = this.search.UseImageSearch("bin/img/Assist.png", Tolerance.Middle);
                    if (this._Coordinates != null)
                    {
                      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    }
                    else
                    {
                      this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                      if (this._Coordinates != null)
                      {
                        InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                      }
                    }
                  }
                  else
                    this.RunandGoBack();
                  Bot.Instance.Actions.ResetMovePPStatus();
                }
                else
                {
                  this._Coordinates = this.search.UseImageSearch("bin/img/Assist.png", Tolerance.Middle);
                  if (this._Coordinates != null)
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  }
                  else
                  {
                    this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                    if (this._Coordinates != null)
                    {
                      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    }
                  }
                }
              }
            }
          }
          else
          {
            if ((!Bot.Instance.Settings.Substitute ? 0 : (!Bot.Instance.Status.UsedSubstitute ? 1 : 0)) != 0)
            {
              this._Coordinates = this.search.UseImageSearch("bin/img/Fight.png", Tolerance.Middle);
              if (this._Coordinates != null)
              {
                InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
              }
              if (!Bot.Instance.Settings.AutoWalkFish)
              {
                this._Coordinates = this.search.UseImageSearch("bin/img/Substitute.png", Tolerance.Middle);
                if (this._Coordinates == null)
                {
                  this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                  if (this._Coordinates != null)
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  }
                }
                else
                {
                  InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                  Bot.Instance.Status.UsedSubstitute = true;
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                }
              }
              else
              {
                Bot.Instance.Actions.ScanMovePPStatus();
                if ((Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 ? 1 : (Bot.Instance.Status.FourthMovePP0 ? 1 : 0)) == 0)
                {
                  this._Coordinates = this.search.UseImageSearch("bin/img/Substitute.png", Tolerance.Middle);
                  if (this._Coordinates != null)
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Status.UsedSubstitute = true;
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  }
                  else
                  {
                    this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                    if (this._Coordinates != null)
                    {
                      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    }
                  }
                }
                else
                  this.RunandGoBack();
                Bot.Instance.Actions.ResetMovePPStatus();
              }
            }
            if (Bot.Instance.Settings.Spore && !Bot.Instance.Check.Sleep)
            {
              this._Coordinates = this.search.UseImageSearch("bin/img/Fight.png", Tolerance.Middle);
              if (this._Coordinates != null)
              {
                InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
              }
              if (Bot.Instance.Settings.AutoWalkFish)
              {
                Bot.Instance.Actions.ScanMovePPStatus();
                if ((Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 ? 1 : (Bot.Instance.Status.FourthMovePP0 ? 1 : 0)) != 0)
                {
                  this.RunandGoBack();
                }
                else
                {
                  this._Coordinates = this.search.UseImageSearch("bin/img/Spore.png", Tolerance.Middle);
                  if (this._Coordinates == null)
                  {
                    this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                    if (this._Coordinates != null)
                    {
                      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    }
                  }
                  else
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  }
                }
                Bot.Instance.Actions.ResetMovePPStatus();
              }
              else
              {
                this._Coordinates = this.search.UseImageSearch("bin/img/Spore.png", Tolerance.Middle);
                if (this._Coordinates != null)
                {
                  InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                }
                else
                {
                  this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                  if (this._Coordinates != null)
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  }
                }
              }
            }
            if ((!Bot.Instance.Settings.FalseSwipe ? 0 : (!Bot.Instance.Status.UsedFalseSwipe ? 1 : 0)) != 0)
            {
              this._Coordinates = this.search.UseImageSearch("bin/img/Fight.png", Tolerance.Middle);
              if (this._Coordinates != null)
              {
                InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
              }
              if (Bot.Instance.Settings.AutoWalkFish)
              {
                Bot.Instance.Actions.ScanMovePPStatus();
                if ((Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 ? 1 : (Bot.Instance.Status.FourthMovePP0 ? 1 : 0)) != 0)
                {
                  this.RunandGoBack();
                }
                else
                {
                  this._Coordinates = this.search.UseImageSearch("bin/img/FalseSwipe.png", Tolerance.Middle);
                  if (this._Coordinates != null)
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Status.UsedFalseSwipe = true;
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  }
                  else
                  {
                    this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                    if (this._Coordinates != null)
                    {
                      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                    }
                  }
                }
                Bot.Instance.Actions.ResetMovePPStatus();
              }
              else
              {
                this._Coordinates = this.search.UseImageSearch("bin/img/FalseSwipe.png", Tolerance.Middle);
                if (this._Coordinates == null)
                {
                  this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                  if (this._Coordinates != null)
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  }
                }
                else
                {
                  InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                  Bot.Instance.Status.UsedFalseSwipe = true;
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                }
              }
            }
          }
        }
        else
        {
          if ((!Bot.Instance.Settings.Substitute ? 0 : (!Bot.Instance.Status.UsedSubstitute ? 1 : 0)) != 0)
          {
            this._Coordinates = this.search.UseImageSearch("bin/img/Fight.png", Tolerance.Middle);
            if (this._Coordinates != null)
            {
              InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
            }
            if (!Bot.Instance.Settings.AutoWalkFish)
            {
              this._Coordinates = this.search.UseImageSearch("bin/img/Substitute.png", Tolerance.Middle);
              if (this._Coordinates == null)
              {
                this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                if (this._Coordinates != null)
                {
                  InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                }
              }
              else
              {
                InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                Bot.Instance.Status.UsedSubstitute = true;
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              }
            }
            else
            {
              Bot.Instance.Actions.ScanMovePPStatus();
              if ((Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 ? 1 : (Bot.Instance.Status.FourthMovePP0 ? 1 : 0)) == 0)
              {
                this._Coordinates = this.search.UseImageSearch("bin/img/Substitute.png", Tolerance.Middle);
                if (this._Coordinates != null)
                {
                  InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                  Bot.Instance.Status.UsedSubstitute = true;
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                }
                else
                {
                  this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                  if (this._Coordinates != null)
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  }
                }
              }
              else
                this.RunandGoBack();
              Bot.Instance.Actions.ResetMovePPStatus();
            }
          }
          if ((!Bot.Instance.Settings.FalseSwipe ? 0 : (!Bot.Instance.Status.UsedFalseSwipe ? 1 : 0)) != 0)
          {
            this._Coordinates = this.search.UseImageSearch("bin/img/Fight.png", Tolerance.Middle);
            if (this._Coordinates != null)
            {
              InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
            }
            if (Bot.Instance.Settings.AutoWalkFish)
            {
              Bot.Instance.Actions.ScanMovePPStatus();
              if ((Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 ? 1 : (Bot.Instance.Status.FourthMovePP0 ? 1 : 0)) != 0)
              {
                this.RunandGoBack();
              }
              else
              {
                this._Coordinates = this.search.UseImageSearch("bin/img/FalseSwipe.png", Tolerance.Middle);
                if (this._Coordinates == null)
                {
                  this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                  if (this._Coordinates != null)
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  }
                }
                else
                {
                  InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                  Bot.Instance.Status.UsedFalseSwipe = true;
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                }
              }
              Bot.Instance.Actions.ResetMovePPStatus();
            }
            else
            {
              this._Coordinates = this.search.UseImageSearch("bin/img/FalseSwipe.png", Tolerance.Middle);
              if (this._Coordinates != null)
              {
                InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                Bot.Instance.Status.UsedFalseSwipe = true;
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              }
              else
              {
                this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                if (this._Coordinates != null)
                {
                  InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                }
              }
            }
          }
          if (Bot.Instance.Settings.Spore && !Bot.Instance.Check.Sleep)
          {
            this._Coordinates = this.search.UseImageSearch("bin/img/Fight.png", Tolerance.Middle);
            if (this._Coordinates != null)
            {
              InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
            }
            if (Bot.Instance.Settings.AutoWalkFish)
            {
              Bot.Instance.Actions.ScanMovePPStatus();
              if ((Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 ? 1 : (Bot.Instance.Status.FourthMovePP0 ? 1 : 0)) != 0)
              {
                this.RunandGoBack();
              }
              else
              {
                this._Coordinates = this.search.UseImageSearch("bin/img/Spore.png", Tolerance.Middle);
                if (this._Coordinates != null)
                {
                  InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                }
                else
                {
                  this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                  if (this._Coordinates != null)
                  {
                    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                  }
                }
              }
              Bot.Instance.Actions.ResetMovePPStatus();
            }
            else
            {
              this._Coordinates = this.search.UseImageSearch("bin/img/Spore.png", Tolerance.Middle);
              if (this._Coordinates != null)
              {
                InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              }
              else
              {
                this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.Middle);
                if (this._Coordinates != null)
                {
                  InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                  Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                }
              }
            }
          }
        }
        this.ThrowBall();
      }
    }
    else
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/Level.png", Tolerance.Middle);
      if ((this._Coordinates == null ? 0 : (!Bot.Instance.Status.Changed ? 1 : 0)) == 0 || !Includes.ApplicationIsActivated())
        return;
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      Bot.Instance.Actions.NextPoke();
      Bot.Instance.Status.Changed = true;
    }
  }

  public void ThrowBall()
  {
    if (Bot.Instance.Settings.BotMode != BotMode.Safari)
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/Bag.png", Tolerance.High);
      if (this._Coordinates == null)
        return;
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(500);
      this._Coordinates = this.search.UseImageSearch("bin/img/Ball.png", Tolerance.High);
      if (this._Coordinates != null)
      {
        this._Coordinates = this.search.UseImageSearch($"bin/img/{Bot.Instance.Settings.ChosenPokeBall.ToString()}.png", Tolerance.Middle);
        if (this._Coordinates != null)
        {
          InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
          InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
          ++Bot.Instance.Status.ThrownBallsCounter;
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          DiscordBot.Instance.SendMessage(Bot.Instance.Settings.ChosenPokeBall.ToString(), true);
        }
        else
        {
          InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
          ++Bot.Instance.Status.ThrownBallsCounter;
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          DiscordBot.Instance.SendMessage("PokeBall", true);
        }
      }
      else
      {
        InputKeyboard.PressKeyLeft(Bot.Instance.Settings.HoldTime);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        InputKeyboard.PressKeyLeft(Bot.Instance.Settings.HoldTime);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        InputKeyboard.PressKeyLeft(Bot.Instance.Settings.HoldTime);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        InputKeyboard.PressKeyRight(Bot.Instance.Settings.HoldTime);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        InputKeyboard.PressKeyRight(Bot.Instance.Settings.HoldTime);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      }
      this._Coordinates = this.search.UseImageSearch("bin/img/Ball.png", Tolerance.High);
      if (this._Coordinates == null)
      {
        this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.High);
        if (this._Coordinates == null)
          return;
        InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      }
      else
      {
        this._Coordinates = this.search.UseImageSearch($"bin/img/{Bot.Instance.Settings.ChosenPokeBall.ToString()}.png", Tolerance.Middle);
        if (this._Coordinates != null)
        {
          InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
          InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
          ++Bot.Instance.Status.ThrownBallsCounter;
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          DiscordBot.Instance.SendMessage(Bot.Instance.Settings.ChosenPokeBall.ToString(), true);
        }
        else
        {
          InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
          ++Bot.Instance.Status.ThrownBallsCounter;
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          DiscordBot.Instance.SendMessage("PokeBall", true);
        }
      }
    }
    else
    {
      if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
      {
        if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
          InputMouse.LeftClick(305, 440);
      }
      else
        InputMouse.LeftClick(400, 700);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
  }

  public void CloseStats()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/Stats.png", Tolerance.High);
    if (this._Coordinates == null)
      return;
    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
  }

  public void Stats()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/StatsIV.png", Tolerance.Middle);
    if (this._Coordinates != null)
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
      this._Coordinates = this.search.UseImageSearch("bin/img/31.png", Tolerance.Middle);
      if (this._Coordinates == null)
      {
        this._Coordinates = this.search.UseImageSearch("bin/img/Release.png", Tolerance.Middle);
        if (this._Coordinates != null)
        {
          InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
          this._Coordinates = this.search.UseImageSearch("bin/img/CRelease.png", Tolerance.Middle);
          if (this._Coordinates != null)
          {
            InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
            this._Coordinates = this.search.UseImageSearch("bin/img/Yes.png", Tolerance.High);
            if (this._Coordinates != null)
            {
              InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
            }
          }
        }
      }
      else
      {
        InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
        DiscordBot.Instance.SendMessage("IV31", true);
        this.CloseStats();
      }
    }
    this.CloseStats();
  }

  public void CheckWalkCycle()
  {
    if ((!Bot.Instance.Settings.AutoWalkFish ? 0 : (Bot.Instance.Settings.BotMode != BotMode.Safari ? 1 : 0)) == 0)
    {
      if ((Bot.Instance.Settings.StopWalkCycles ? 1 : (Bot.Instance.Settings.AlertWalkCycles ? 1 : 0)) == 0)
        return;
      if (Bot.Instance.Status.WalkCycle < Bot.Instance.Settings.WalkCyclesTrigger)
      {
        if (!Bot.Instance.Check.Walk)
          return;
        ++Bot.Instance.Status.WalkCycle;
      }
      else
      {
        if (Bot.Instance.Settings.AlertWalkCycles)
        {
          Sounds.PlayAlertSound();
          Sounds.PlayAlertSound();
          Sounds.PlayAlertSound();
        }
        if (!Bot.Instance.Settings.StopWalkCycles)
          return;
        Bot.Instance.Stop();
      }
    }
    else if (Bot.Instance.Status.WalkCycle < 4)
    {
      if (!Bot.Instance.Check.Walk)
        return;
      ++Bot.Instance.Status.WalkCycle;
    }
    else
    {
      Bot.Instance.Status.GoBack = true;
      ++Bot.Instance.Status.GoBackOnce;
    }
  }

  public void ResetAndUpdateWalkCycle()
  {
    Bot.Instance.Status.WalkCycle = 0;
    Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.WalkCycle = "WalkCycle: 0"));
  }

  public void SkipLearnMove()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/LearnMove.png", Tolerance.High);
    if (this._Coordinates == null)
      return;
    if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
      InputMouse.LeftClick(900, 695);
    if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
      InputMouse.LeftClick(805, 440);
    Bot.Instance.Sleep(500);
    InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    this._Coordinates = this.search.UseImageSearch("bin/img/Yes.png", Tolerance.High);
    if (this._Coordinates == null || !Includes.ApplicationIsActivated())
      return;
    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
  }

  public void SkipEvolve()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/Evolve.png", Tolerance.High);
    if (this._Coordinates == null)
      return;
    this._Coordinates = this.search.UseImageSearch("bin/img/Cancel.png", Tolerance.High);
    if (this._Coordinates != null)
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    this._Coordinates = this.search.UseImageSearch("bin/img/Yes.png", Tolerance.High);
    if (this._Coordinates == null)
      return;
    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
  }

  public void Lure()
  {
    if (!Bot.Instance.Check.Lure)
      return;
    this._Coordinates = this.search.UseImageSearch("bin/img/Yes.png", Tolerance.High);
    if (this._Coordinates == null)
      return;
    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
  }

  public void UseLure()
  {
    if (!Includes.ApplicationIsActivated())
      return;
    InputKeyboard.PressHotkey6(100);
    Bot.Instance.Sleep(250);
  }

  public void SkipDialogue()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/Skip.png", Tolerance.Skip);
    if (this._Coordinates == null || !Includes.ApplicationIsActivated())
      return;
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
    InputKeyboard.PressKeyA(Bot.Instance.Settings.WaitTimeLong);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
  }

  public void SkipDialogueFish()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/Skip.png", Tolerance.Skip);
    if (this._Coordinates == null || !Includes.ApplicationIsActivated())
      return;
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
    InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
  }

  public void SolveCaptcha()
  {
    ScreenCapture.CaptchaImage();
    _2Captcha.Result result = new _2Captcha(MainWindow.KeyAuthApp.var("CaptchaAPI"), (HttpClient) null).SolveImage((Stream) new FileStream("Captcha.jpg", FileMode.Open), (FileType) 2, Array.Empty<KeyValuePair<string, string>>()).GetAwaiter().GetResult();
    Bot.Instance.Status.SolvedCaptchaText = "";
    if ((!result.Success || !(((_2Captcha.Result) ref result).Response != "ERROR_BAD_DUPLICATES") ? 0 : (((_2Captcha.Result) ref result).Response != "ERROR_CAPTCHA_UNSOLVABLE" ? 1 : 0)) != 0)
      Bot.Instance.Status.SolvedCaptchaText = ((_2Captcha.Result) ref result).Response;
    if (Bot.Instance.Check.Captcha)
    {
      InputMouse.LeftClick(Bot.Instance.Check._Coordinates[1], Bot.Instance.Check._Coordinates[2] + 190);
      Bot.Instance.Sleep(100);
      Bot.Instance.Sim.Keyboard.TextEntry(Bot.Instance.Status.SolvedCaptchaText);
      Bot.Instance.Sleep(100);
      InputMouse.LeftClick(Bot.Instance.Check._Coordinates[1], Bot.Instance.Check._Coordinates[2] + 225);
    }
    DiscordBot.Instance.SendMessage("CaptchaSolved", false);
  }

  public void Humanize()
  {
    if (!Bot.Instance.Status.HumanizeHelper)
      return;
    Bot.Instance.Actions.HumanizeActions();
    Bot.Instance.Status.HumanizeHelper = false;
  }

  public void HumanizeActions()
  {
    if ((!Includes.ApplicationIsActivated() || !Bot.Instance.Settings.Humanize ? 0 : (!Bot.Instance.Check.Dialogue ? 1 : 0)) == 0)
      return;
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShortRandom);
    Random random = new Random();
    if (random.NextDouble() < 0.03)
    {
      Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Humanize Action 1"));
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeHuman);
    }
    if (random.NextDouble() < 0.03)
    {
      Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Humanize Action 2"));
      this._Coordinates = this.search.UseImageSearch("bin/img/Poke.png", Tolerance.Middle);
      if (this._Coordinates != null && Includes.ApplicationIsActivated())
      {
        InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
        this._Coordinates = this.search.UseImageSearch("bin/img/Summary.png", Tolerance.Middle);
        if (this._Coordinates != null && Includes.ApplicationIsActivated())
        {
          InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeHuman);
        }
        if ((double) RandomNumber.Between(1, 10) < 0.25)
        {
          Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Humanize Action 2-1"));
          this._Coordinates = this.search.UseImageSearch("bin/img/StatsIV.png", Tolerance.Middle);
          if (this._Coordinates != null)
          {
            InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeHuman);
          }
        }
        this.CloseStats();
      }
    }
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShortRandom);
  }

  public void SellBox()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/AutoSort.png", Tolerance.Middle);
    if (this._Coordinates != null && Includes.ApplicationIsActivated())
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    this._Coordinates = this.search.UseImageSearch("bin/img/Sort.png", Tolerance.Middle);
    if (this._Coordinates != null && Includes.ApplicationIsActivated())
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    this._Coordinates = this.search.UseImageSearch("bin/img/BoxPokemon.png", "10");
    if (this._Coordinates != null && Includes.ApplicationIsActivated())
    {
      InputMouse.RightClick(this._Coordinates[1] - 20, this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    this._Coordinates = this.search.UseImageSearch("bin/img/SellOnGTL.png", Tolerance.Middle);
    if (this._Coordinates != null && Includes.ApplicationIsActivated())
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    InputMouse.MoveMouse(0, 0);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    this._Coordinates = this.search.UseImageSearch("bin/img/Price.png", Tolerance.Middle);
    if (this._Coordinates != null && Includes.ApplicationIsActivated())
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      if ((Bot.Instance.Settings.SellPrice != null ? 1 : (Bot.Instance.Settings.SellPrice != "" ? 1 : 0)) != 0)
        Bot.Instance.Sim.Keyboard.TextEntry(Bot.Instance.Settings.SellPrice);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    this._Coordinates = this.search.UseImageSearch("bin/img/Sell.png", Tolerance.Middle);
    if (this._Coordinates != null && Includes.ApplicationIsActivated())
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      InputKeyboard.PressKeyEscape(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    if (this._Coordinates != null)
      return;
    InputMouse.MoveMouse(0, 0);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
  }

  public void MailClaim()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/Mail.png", Tolerance.Middle);
    if (this._Coordinates != null && Includes.ApplicationIsActivated())
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    this._Coordinates = this.search.UseImageSearch("bin/img/MailClaim.png", Tolerance.Middle);
    if (this._Coordinates == null || !Includes.ApplicationIsActivated())
      return;
    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
  }

  public void GTLSniper()
  {
    if (!Includes.ApplicationIsActivated())
      return;
    this._Coordinates = this.search.UseImageSearch("bin/img/GTLRefresh.png", Tolerance.Middle);
    if (this._Coordinates != null)
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
    }
    this._Coordinates = this.search.UseImageSearch("bin/img/GTLBuy.png", Tolerance.Low);
    if (this._Coordinates != null && Includes.ApplicationIsActivated())
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime * 2);
      if (Bot.Instance.Check.GTLNoMoney)
      {
        DiscordBot.Instance.SendMessage("GTLSniperFailedNoMoney", true);
        Bot.Instance.Stop();
      }
      else
        DiscordBot.Instance.SendMessage("GTLSniperBought", true);
    }
    Bot.Instance.Sleep(5000);
  }

  public void TakeItem()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/Item2.png", Tolerance.MiddleHigh);
    if (this._Coordinates != null && Includes.ApplicationIsActivated())
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime * 2);
    }
    this._Coordinates = this.search.UseImageSearch("bin/img/TakeItem.png", Tolerance.Middle);
    if (this._Coordinates == null || !Includes.ApplicationIsActivated())
      return;
    InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime * 4);
    ++Bot.Instance.Status.ItemCounter;
    DiscordBot.Instance.SendMessage("Thief", true);
  }

  public void ClickPokemonItem()
  {
    if (!Bot.Instance.Status.FirstPokemonItem)
    {
      if (Bot.Instance.Status.SecondPokemonItem)
      {
        if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
          InputMouse.LeftClick(620, 700);
        else
          InputMouse.LeftClick(530, 450);
      }
      else if (Bot.Instance.Status.ThirdPokemonItem)
      {
        if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
        {
          if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.SD)
            return;
          InputMouse.LeftClick(740, 450);
        }
        else
          InputMouse.LeftClick(830, 700);
      }
      else if (!Bot.Instance.Status.FourthPokemonItem)
      {
        if (!Bot.Instance.Status.FifthPokemonItem)
          return;
        if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
        {
          if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.SD)
            return;
          InputMouse.LeftClick(750, 500);
        }
        else
          InputMouse.LeftClick(830, 750);
      }
      else if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
      {
        if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.SD)
          return;
        InputMouse.LeftClick(320, 500);
      }
      else
        InputMouse.LeftClick(410, 750);
    }
    else if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
    {
      if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.SD)
        return;
      InputMouse.LeftClick(320, 450);
    }
    else
      InputMouse.LeftClick(410, 700);
  }

  public void ScanMovePPStatus()
  {
    this.search.UseImageSearch("bin/img/ZeroPP.png", Tolerance.High);
  }

  public void ScanPokemonItemStatus() => this.search.UseImageSearch("bin/img/Item.png", "45");

  public void ResetMovePPStatus()
  {
    Bot.Instance.Status.FirstMovePP0 = false;
    Bot.Instance.Status.SecondMovePP0 = false;
    Bot.Instance.Status.ThirdMovePP0 = false;
    Bot.Instance.Status.FourthMovePP0 = false;
  }

  public void ResetPokemonItemStatus()
  {
    Bot.Instance.Status.FirstMainPokemonItem = false;
    Bot.Instance.Status.FirstPokemonItem = false;
    Bot.Instance.Status.SecondPokemonItem = false;
    Bot.Instance.Status.ThirdPokemonItem = false;
    Bot.Instance.Status.FourthPokemonItem = false;
    Bot.Instance.Status.FifthPokemonItem = false;
  }

  public void UseLeppa()
  {
    if ((!Includes.ApplicationIsActivated() ? 0 : (!Bot.Instance.Check.Leppa0 ? 1 : 0)) != 0)
    {
      InputKeyboard.PressHotkey7(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
      InputKeyboard.PressKeyRight(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
      InputKeyboard.PressKeyA(Bot.Instance.Settings.WaitTimeVeryLong);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
      InputKeyboard.PressKeyA(Bot.Instance.Settings.WaitTimeVeryLong);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
      InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
      InputKeyboard.PressKeyRight(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
      InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
      InputKeyboard.PressKeyDown(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
      InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
    }
    else
    {
      if (!Bot.Instance.Check.Leppa0)
        return;
      Bot.Instance.Stop();
    }
  }

  public void UseEther()
  {
    if (!Includes.ApplicationIsActivated())
      return;
    InputKeyboard.PressHotkey7(Bot.Instance.Settings.HoldTime);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
    InputKeyboard.PressKeyA(Bot.Instance.Settings.WaitTimeVeryLong);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
    InputKeyboard.PressKeyA(Bot.Instance.Settings.WaitTimeVeryLong);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
  }

  public void Potion()
  {
    if (!Bot.Instance.Settings.PotionSystem)
      return;
    if (Bot.Instance.Check.HPOrange)
    {
      if ((Bot.Instance.Settings.OrangePotionSelectedIndex != 0 ? 0 : (!Bot.Instance.Check.Potion0 ? 1 : 0)) == 0)
      {
        if ((Bot.Instance.Settings.OrangePotionSelectedIndex != 1 ? 0 : (!Bot.Instance.Check.SuperPotion0 ? 1 : 0)) != 0)
        {
          Bot.Instance.Status.PotionStatus = "Orange";
          this.UsePotion();
        }
        else
        {
          if ((Bot.Instance.Settings.OrangePotionSelectedIndex != 2 ? 0 : (!Bot.Instance.Check.HyperPotion0 ? 1 : 0)) == 0)
            return;
          Bot.Instance.Status.PotionStatus = "Orange";
          this.UsePotion();
        }
      }
      else
      {
        Bot.Instance.Status.PotionStatus = "Orange";
        this.UsePotion();
      }
    }
    else
    {
      if (!Bot.Instance.Check.HPRed)
        return;
      if ((Bot.Instance.Settings.RedPotionSelectedIndex != 0 ? 0 : (!Bot.Instance.Check.Potion0 ? 1 : 0)) != 0)
      {
        Bot.Instance.Status.PotionStatus = "Red";
        this.UsePotion();
      }
      else if ((Bot.Instance.Settings.RedPotionSelectedIndex != 1 ? 0 : (!Bot.Instance.Check.SuperPotion0 ? 1 : 0)) != 0)
      {
        Bot.Instance.Status.PotionStatus = "Red";
        this.UsePotion();
      }
      else
      {
        if ((Bot.Instance.Settings.RedPotionSelectedIndex != 2 ? 0 : (!Bot.Instance.Check.HyperPotion0 ? 1 : 0)) == 0)
          return;
        Bot.Instance.Status.PotionStatus = "Red";
        this.UsePotion();
      }
    }
  }

  public void PotionRoutine()
  {
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
    InputKeyboard.PressKeyDown(2000);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
    int num1 = 0;
    int num2 = 6;
    if (Bot.Instance.Status.PotionStatus == "Orange")
    {
      if ((Bot.Instance.Settings.OrangePotionSelectedIndex != 0 ? 0 : (!Bot.Instance.Check.Potion0 ? 1 : 0)) == 0)
      {
        if ((Bot.Instance.Settings.OrangePotionSelectedIndex != 1 ? 0 : (!Bot.Instance.Check.SuperPotion0 ? 1 : 0)) == 0)
        {
          if ((Bot.Instance.Settings.OrangePotionSelectedIndex != 2 ? 0 : (!Bot.Instance.Check.HyperPotion0 ? 1 : 0)) == 0)
            return;
          do
          {
            this._Coordinates = this.search.UseImageSearch("bin/img/HyperPotion.png", Tolerance.High);
            if (this._Coordinates == null)
            {
              this._Coordinates = this.search.UseImageSearch("bin/img/HyperPotionH.png", Tolerance.High);
              if (this._Coordinates != null)
              {
                InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                num1 = num2;
                this.UsePotionOnPokemon();
              }
              else
              {
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              }
            }
            else
              goto label_8;
label_7:
            ++num1;
            continue;
label_8:
            InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            num1 = num2;
            this.UsePotionOnPokemon();
            goto label_7;
          }
          while ((this._Coordinates != null ? 0 : (num1 <= num2 ? 1 : 0)) != 0);
        }
        else
        {
          do
          {
            this._Coordinates = this.search.UseImageSearch("bin/img/SuperPotion.png", Tolerance.High);
            if (this._Coordinates == null)
            {
              this._Coordinates = this.search.UseImageSearch("bin/img/SuperPotionH.png", Tolerance.High);
              if (this._Coordinates != null)
              {
                InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                num1 = num2;
                this.UsePotionOnPokemon();
              }
              else
              {
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              }
            }
            else
              goto label_14;
label_13:
            ++num1;
            continue;
label_14:
            InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            num1 = num2;
            this.UsePotionOnPokemon();
            goto label_13;
          }
          while ((this._Coordinates != null ? 0 : (num1 <= num2 ? 1 : 0)) != 0);
        }
      }
      else
      {
        do
        {
          this._Coordinates = this.search.UseImageSearch("bin/img/Potion.png", Tolerance.High);
          if (this._Coordinates == null)
          {
            this._Coordinates = this.search.UseImageSearch("bin/img/PotionH.png", Tolerance.High);
            if (this._Coordinates != null)
            {
              InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              num1 = num2;
              this.UsePotionOnPokemon();
            }
            else
            {
              InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            }
          }
          else
            goto label_21;
label_19:
          ++num1;
          continue;
label_21:
          InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          num1 = num2;
          this.UsePotionOnPokemon();
          goto label_19;
        }
        while ((this._Coordinates != null ? 0 : (num1 <= num2 ? 1 : 0)) != 0);
      }
    }
    else
    {
      if (!(Bot.Instance.Status.PotionStatus == "Red"))
        return;
      if ((Bot.Instance.Settings.RedPotionSelectedIndex != 0 ? 0 : (!Bot.Instance.Check.Potion0 ? 1 : 0)) == 0)
      {
        if ((Bot.Instance.Settings.RedPotionSelectedIndex != 1 ? 0 : (!Bot.Instance.Check.SuperPotion0 ? 1 : 0)) == 0)
        {
          if ((Bot.Instance.Settings.RedPotionSelectedIndex != 2 ? 0 : (!Bot.Instance.Check.HyperPotion0 ? 1 : 0)) == 0)
            return;
          do
          {
            this._Coordinates = this.search.UseImageSearch("bin/img/HyperPotion.png", Tolerance.High);
            if (this._Coordinates == null)
            {
              this._Coordinates = this.search.UseImageSearch("bin/img/HyperPotionH.png", Tolerance.High);
              if (this._Coordinates == null)
              {
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              }
              else
              {
                InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
                Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
                num1 = num2;
                this.UsePotionOnPokemon();
              }
            }
            else
              goto label_34;
label_32:
            ++num1;
            continue;
label_34:
            InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            num1 = num2;
            this.UsePotionOnPokemon();
            goto label_32;
          }
          while ((this._Coordinates != null ? 0 : (num1 <= num2 ? 1 : 0)) != 0);
        }
        else
        {
          do
          {
            this._Coordinates = this.search.UseImageSearch("bin/img/SuperPotion.png", Tolerance.High);
            if (this._Coordinates != null)
            {
              InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              num1 = num2;
              this.UsePotionOnPokemon();
            }
            else
              goto label_40;
label_38:
            ++num1;
            continue;
label_40:
            this._Coordinates = this.search.UseImageSearch("bin/img/SuperPotionH.png", Tolerance.High);
            if (this._Coordinates != null)
            {
              InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
              Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
              num1 = num2;
              this.UsePotionOnPokemon();
              goto label_38;
            }
            InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            goto label_38;
          }
          while ((this._Coordinates != null ? 0 : (num1 <= num2 ? 1 : 0)) != 0);
        }
      }
      else
      {
        do
        {
          this._Coordinates = this.search.UseImageSearch("bin/img/Potion.png", Tolerance.High);
          if (this._Coordinates != null)
          {
            InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            num1 = num2;
            this.UsePotionOnPokemon();
          }
          else
            goto label_48;
label_46:
          ++num1;
          continue;
label_48:
          this._Coordinates = this.search.UseImageSearch("bin/img/PotionH.png", Tolerance.High);
          if (this._Coordinates != null)
          {
            InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
            Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
            num1 = num2;
            this.UsePotionOnPokemon();
            goto label_46;
          }
          InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          goto label_46;
        }
        while ((this._Coordinates != null ? 0 : (num1 <= num2 ? 1 : 0)) != 0);
      }
    }
  }

  public void UsePotion()
  {
    do
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/Bag.png", Tolerance.High);
      if (this._Coordinates != null)
      {
        InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      }
      this._Coordinates = this.search.UseImageSearch("bin/img/Medicine.png", Tolerance.High);
      if (this._Coordinates != null)
      {
        this.PotionRoutine();
      }
      else
      {
        InputKeyboard.PressKeyLeft(Bot.Instance.Settings.HoldTime);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        InputKeyboard.PressKeyLeft(Bot.Instance.Settings.HoldTime);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        InputKeyboard.PressKeyLeft(Bot.Instance.Settings.HoldTime);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      }
      this._Coordinates = this.search.UseImageSearch("bin/img/Medicine.png", Tolerance.High);
      if (this._Coordinates != null)
      {
        this.PotionRoutine();
      }
      else
      {
        this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.High);
        if (this._Coordinates != null)
        {
          InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        }
      }
      this._Coordinates = this.search.UseImageSearch("bin/img/Back.png", Tolerance.High);
      if (this._Coordinates != null)
      {
        InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      }
    }
    while (!Bot.Instance.Status.UsedPotion);
    Bot.Instance.Status.UsedPotion = false;
    Bot.Instance.Status.PotionStatus = "";
  }

  public void UsePotionOnPokemon()
  {
    if (Bot.Instance.Status.SelectedPokemon == 1)
    {
      if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
        InputMouse.LeftClick(410, 700);
      else
        InputMouse.LeftClick(320, 450);
    }
    else if (Bot.Instance.Status.SelectedPokemon == 2)
    {
      if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
        InputMouse.LeftClick(620, 700);
      else
        InputMouse.LeftClick(530, 450);
    }
    else if (Bot.Instance.Status.SelectedPokemon != 3)
    {
      if (Bot.Instance.Status.SelectedPokemon == 4)
      {
        if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
          InputMouse.LeftClick(410, 750);
        else
          InputMouse.LeftClick(320, 500);
      }
      else if (Bot.Instance.Status.SelectedPokemon != 5)
      {
        if (Bot.Instance.Status.SelectedPokemon == 6)
        {
          if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
            InputMouse.LeftClick(830, 750);
          else
            InputMouse.LeftClick(750, 500);
        }
      }
      else if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
        InputMouse.LeftClick(530, 500);
      else
        InputMouse.LeftClick(630, 750);
    }
    else if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
      InputMouse.LeftClick(830, 700);
    else
      InputMouse.LeftClick(740, 450);
    Bot.Instance.Sleep(500);
    this._Coordinates = this.search.UseImageSearch("bin/img/NoEffect.png", Tolerance.High);
    if (this._Coordinates != null)
    {
      InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      ++Bot.Instance.Status.SelectedPokemon;
      if (Bot.Instance.Status.SelectedPokemon != 7)
        return;
      Bot.Instance.Status.SelectedPokemon = 1;
    }
    else
      Bot.Instance.Status.UsedPotion = true;
  }

  public void NextPoke()
  {
    Bot.Instance.Status.SelectedPokemon = 1;
    if (Bot.Instance.Status.SelectedPokemon == 1)
    {
      if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
        InputMouse.LeftClick(530, 450);
      else
        InputMouse.LeftClick(620, 700);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
      if (Bot.Instance.Check.NextPoke)
        Bot.Instance.Status.SelectedPokemon = 2;
    }
    if (Bot.Instance.Status.SelectedPokemon == 2)
    {
      if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
        InputMouse.LeftClick(740, 450);
      else
        InputMouse.LeftClick(830, 700);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
      if (Bot.Instance.Check.NextPoke)
        Bot.Instance.Status.SelectedPokemon = 3;
    }
    if (Bot.Instance.Status.SelectedPokemon == 3)
    {
      if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
        InputMouse.LeftClick(320, 500);
      else
        InputMouse.LeftClick(410, 750);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
      if (Bot.Instance.Check.NextPoke)
        Bot.Instance.Status.SelectedPokemon = 4;
    }
    if (Bot.Instance.Status.SelectedPokemon == 4)
    {
      if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
        InputMouse.LeftClick(530, 500);
      else
        InputMouse.LeftClick(630, 750);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
      if (Bot.Instance.Check.NextPoke)
        Bot.Instance.Status.SelectedPokemon = 5;
    }
    if (Bot.Instance.Status.SelectedPokemon == 5)
    {
      if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
        InputMouse.LeftClick(750, 500);
      else
        InputMouse.LeftClick(830, 750);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
      if (Bot.Instance.Check.NextPoke)
        Bot.Instance.Status.SelectedPokemon = 6;
    }
    if (Bot.Instance.Status.SelectedPokemon == 6)
    {
      if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
        InputMouse.LeftClick(410, 700);
      else
        InputMouse.LeftClick(320, 450);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
      InputKeyboard.PressKeyB(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
      if (Bot.Instance.Check.NextPoke)
        Bot.Instance.Status.SelectedPokemon = 6;
    }
    ++Bot.Instance.Status.SelectedPokemon;
    if (Bot.Instance.Status.SelectedPokemon != 7)
      return;
    Bot.Instance.Status.SelectedPokemon = 1;
  }

  public void NextPokeManual()
  {
    if (Bot.Instance.Status.SelectedPokemonManual == 1)
    {
      if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
        InputMouse.LeftClick(620, 700);
      else
        InputMouse.LeftClick(530, 450);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
      if (Bot.Instance.Check.NextPoke)
        Bot.Instance.Status.SelectedPokemonManual = 2;
    }
    if (Bot.Instance.Status.SelectedPokemonManual == 2)
    {
      if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
        InputMouse.LeftClick(830, 700);
      else
        InputMouse.LeftClick(740, 450);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
      if (Bot.Instance.Check.NextPoke)
        Bot.Instance.Status.SelectedPokemonManual = 3;
    }
    if (Bot.Instance.Status.SelectedPokemonManual == 3)
    {
      if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
        InputMouse.LeftClick(410, 750);
      else
        InputMouse.LeftClick(320, 500);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
      if (Bot.Instance.Check.NextPoke)
        Bot.Instance.Status.SelectedPokemonManual = 4;
    }
    if (Bot.Instance.Status.SelectedPokemonManual == 4)
    {
      if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
        InputMouse.LeftClick(530, 500);
      else
        InputMouse.LeftClick(630, 750);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
      if (Bot.Instance.Check.NextPoke)
        Bot.Instance.Status.SelectedPokemonManual = 5;
    }
    if (Bot.Instance.Status.SelectedPokemonManual == 5)
    {
      if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
        InputMouse.LeftClick(750, 500);
      else
        InputMouse.LeftClick(830, 750);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
      if (Bot.Instance.Check.NextPoke)
        Bot.Instance.Status.SelectedPokemonManual = 6;
    }
    if (Bot.Instance.Status.SelectedPokemonManual == 6)
    {
      if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
        InputMouse.LeftClick(320, 450);
      else
        InputMouse.LeftClick(410, 700);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
      InputKeyboard.PressKeyB(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
      if (Bot.Instance.Check.NextPoke)
        Bot.Instance.Status.SelectedPokemonManual = 6;
    }
    ++Bot.Instance.Status.SelectedPokemonManual;
    if (Bot.Instance.Status.SelectedPokemonManual != 7)
      return;
    Bot.Instance.Status.SelectedPokemonManual = 1;
  }

  public void Logout()
  {
    if (Includes.ApplicationIsActivated())
    {
      InputKeyboard.PressKeyEscape(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      this._Coordinates = this.search.UseImageSearch("bin/img/Logout.png", Tolerance.Middle);
      if (this._Coordinates != null && Includes.ApplicationIsActivated())
      {
        InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
          InputMouse.LeftClick(960, 525);
        else
          InputMouse.LeftClick(645, 345);
        Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      }
    }
    Bot.Instance.Status.ChannelSwitchTimer = 0;
    Bot.Instance.Status.ChannelSwitchTrigger = RandomNumber.Between(MainViewModel.Instance.Security.ChannelSwitchFrom, MainViewModel.Instance.Security.ChannelSwitchTo);
  }

  public void LogoutAndBreak()
  {
    if (Includes.ApplicationIsActivated())
    {
      InputKeyboard.PressKeyEscape(Bot.Instance.Settings.HoldTime);
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
      this._Coordinates = this.search.UseImageSearch("bin/img/Logout.png", Tolerance.Middle);
      if (this._Coordinates != null)
      {
        if (Includes.ApplicationIsActivated())
        {
          InputMouse.LeftClick(this._Coordinates[1], this._Coordinates[2]);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
          if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
            InputMouse.LeftClick(960, 525);
          else
            InputMouse.LeftClick(645, 345);
          Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
        }
        Bot.Instance.Status.Breaking = true;
        Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Taking a Break"));
        Bot.Instance.Sleep(RandomNumber.Between(MainViewModel.Instance.Security.BreakLengthFrom, MainViewModel.Instance.Security.BreakLengthTo) * 60000);
        Bot.Instance.Status.Breaking = false;
      }
    }
    Bot.Instance.Status.BreakTimer = 0;
    Bot.Instance.Status.BreakTrigger = RandomNumber.Between(MainViewModel.Instance.Security.BreakFrom, MainViewModel.Instance.Security.BreakTo);
  }

  public void SelectedCatchPokemonCounter()
  {
    if (!Bot.Instance.Status.SelectedCatchPokemonCounterHelper)
      return;
    ++Bot.Instance.Status.SelectedCatchPokemonCounter;
    Bot.Instance.Status.SelectedCatchPokemonCounterHelper = false;
  }

  public int SafeWalkFromInt()
  {
    return int.Parse(MainViewModel.Instance.Security.WalkSpeedFrom.ToString().Replace(".", "").Replace(",", "").Replace(" ", "").Replace(" ", ""));
  }

  public int SafeWalkToInt()
  {
    return int.Parse(MainViewModel.Instance.Security.WalkSpeedTo.ToString().Replace(".", "").Replace(",", "").Replace(" ", "").Replace(" ", ""));
  }
}
