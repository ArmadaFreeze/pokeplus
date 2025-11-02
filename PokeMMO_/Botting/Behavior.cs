// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Botting.Behavior
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Classes;
using PokeMMO_.Input;
using PokeMMO_.ViewModels;
using System;
using System.Windows;

#nullable disable
namespace PokeMMO_.Botting;

public class Behavior
{
  public void Walk()
  {
    if ((!Includes.ApplicationIsActivated() ? 0 : (!Bot.Instance.Check.Stats ? 1 : 0)) == 0)
      return;
    Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Walk | Cycle #" + (Bot.Instance.Status.WalkCycle + 1).ToString()));
    int num1 = RandomNumber.Between(1, 2);
    if ((!MainViewModel.Instance.Home.WalkDirection ? 0 : (Bot.Instance.Settings.SquaresWalkPattern || Bot.Instance.Settings.RandomWalkPattern ? (Bot.Instance.Settings.AutoWalkFish ? 1 : 0) : 1)) == 0)
    {
      if ((MainViewModel.Instance.Home.WalkDirection ? 0 : (Bot.Instance.Settings.SquaresWalkPattern || Bot.Instance.Settings.RandomWalkPattern ? (Bot.Instance.Settings.AutoWalkFish ? 1 : 0) : 1)) != 0)
      {
        switch (num1)
        {
          case 1:
            InputKeyboard.PressKeyUp(BotSettings.Settings.WalkSpeed);
            InputKeyboard.PressKeyDown(BotSettings.Settings.WalkSpeed);
            InputKeyboard.PressKeyUp(BotSettings.Settings.WalkSpeed);
            InputKeyboard.PressKeyDown(BotSettings.Settings.WalkSpeed);
            break;
          case 2:
            InputKeyboard.PressKeyDown(BotSettings.Settings.WalkSpeed);
            InputKeyboard.PressKeyUp(BotSettings.Settings.WalkSpeed);
            InputKeyboard.PressKeyDown(BotSettings.Settings.WalkSpeed);
            InputKeyboard.PressKeyUp(BotSettings.Settings.WalkSpeed);
            break;
        }
      }
      else if (Bot.Instance.Settings.SquaresWalkPattern)
      {
        int num2 = RandomNumber.Between(1, 4);
        int waitTimeVeryLong = Bot.Instance.Settings.WaitTimeVeryLong;
        if (num2 == 1)
        {
          InputKeyboard.PressKeyLeft(waitTimeVeryLong);
          InputKeyboard.PressKeyDown(waitTimeVeryLong);
          InputKeyboard.PressKeyRight(waitTimeVeryLong);
          InputKeyboard.PressKeyUp(waitTimeVeryLong);
        }
        else if (num2 == 2)
        {
          InputKeyboard.PressKeyRight(waitTimeVeryLong);
          InputKeyboard.PressKeyDown(waitTimeVeryLong);
          InputKeyboard.PressKeyLeft(waitTimeVeryLong);
          InputKeyboard.PressKeyUp(waitTimeVeryLong);
        }
        if (num2 == 3)
        {
          InputKeyboard.PressKeyLeft(waitTimeVeryLong);
          InputKeyboard.PressKeyUp(waitTimeVeryLong);
          InputKeyboard.PressKeyRight(waitTimeVeryLong);
          InputKeyboard.PressKeyDown(waitTimeVeryLong);
        }
        else if (num2 == 4)
        {
          InputKeyboard.PressKeyRight(waitTimeVeryLong);
          InputKeyboard.PressKeyUp(waitTimeVeryLong);
          InputKeyboard.PressKeyLeft(waitTimeVeryLong);
          InputKeyboard.PressKeyDown(waitTimeVeryLong);
        }
      }
      else if (Bot.Instance.Settings.RandomWalkPattern)
      {
        int num3 = RandomNumber.Between(1, 8);
        if (num3 != 1)
        {
          if (num3 != 2)
          {
            if (num3 == 3)
            {
              InputKeyboard.PressKeyUp(BotSettings.Settings.WalkSpeed);
              InputKeyboard.PressKeyDown(BotSettings.Settings.WalkSpeed);
              InputKeyboard.PressKeyUp(BotSettings.Settings.WalkSpeed);
              InputKeyboard.PressKeyDown(BotSettings.Settings.WalkSpeed);
            }
            else if (num3 != 4)
            {
              if (num3 == 5)
              {
                InputKeyboard.PressKeyLeft(BotSettings.Settings.WalkSpeed);
                InputKeyboard.PressKeyDown(BotSettings.Settings.WalkSpeed);
                InputKeyboard.PressKeyRight(BotSettings.Settings.WalkSpeed);
                InputKeyboard.PressKeyUp(BotSettings.Settings.WalkSpeed);
              }
              else if (num3 == 6)
              {
                InputKeyboard.PressKeyRight(BotSettings.Settings.WalkSpeed);
                InputKeyboard.PressKeyDown(BotSettings.Settings.WalkSpeed);
                InputKeyboard.PressKeyLeft(BotSettings.Settings.WalkSpeed);
                InputKeyboard.PressKeyUp(BotSettings.Settings.WalkSpeed);
              }
            }
            else
            {
              InputKeyboard.PressKeyDown(BotSettings.Settings.WalkSpeed);
              InputKeyboard.PressKeyUp(BotSettings.Settings.WalkSpeed);
              InputKeyboard.PressKeyDown(BotSettings.Settings.WalkSpeed);
              InputKeyboard.PressKeyUp(BotSettings.Settings.WalkSpeed);
            }
          }
          else
          {
            InputKeyboard.PressKeyRight(BotSettings.Settings.WalkSpeed);
            InputKeyboard.PressKeyLeft(BotSettings.Settings.WalkSpeed);
            InputKeyboard.PressKeyRight(BotSettings.Settings.WalkSpeed);
            InputKeyboard.PressKeyLeft(BotSettings.Settings.WalkSpeed);
          }
        }
        else
        {
          InputKeyboard.PressKeyLeft(BotSettings.Settings.WalkSpeed);
          InputKeyboard.PressKeyRight(BotSettings.Settings.WalkSpeed);
          InputKeyboard.PressKeyLeft(BotSettings.Settings.WalkSpeed);
          InputKeyboard.PressKeyRight(BotSettings.Settings.WalkSpeed);
        }
        if (num3 != 7)
        {
          if (num3 == 8)
          {
            InputKeyboard.PressKeyRight(BotSettings.Settings.WalkSpeed);
            InputKeyboard.PressKeyUp(BotSettings.Settings.WalkSpeed);
            InputKeyboard.PressKeyLeft(BotSettings.Settings.WalkSpeed);
            InputKeyboard.PressKeyDown(BotSettings.Settings.WalkSpeed);
          }
        }
        else
        {
          InputKeyboard.PressKeyLeft(BotSettings.Settings.WalkSpeed);
          InputKeyboard.PressKeyUp(BotSettings.Settings.WalkSpeed);
          InputKeyboard.PressKeyRight(BotSettings.Settings.WalkSpeed);
          InputKeyboard.PressKeyDown(BotSettings.Settings.WalkSpeed);
        }
      }
    }
    else
    {
      switch (num1)
      {
        case 1:
          InputKeyboard.PressKeyLeft(BotSettings.Settings.WalkSpeed);
          InputKeyboard.PressKeyRight(BotSettings.Settings.WalkSpeed);
          InputKeyboard.PressKeyLeft(BotSettings.Settings.WalkSpeed);
          InputKeyboard.PressKeyRight(BotSettings.Settings.WalkSpeed);
          break;
        case 2:
          InputKeyboard.PressKeyRight(BotSettings.Settings.WalkSpeed);
          InputKeyboard.PressKeyLeft(BotSettings.Settings.WalkSpeed);
          InputKeyboard.PressKeyRight(BotSettings.Settings.WalkSpeed);
          InputKeyboard.PressKeyLeft(BotSettings.Settings.WalkSpeed);
          break;
      }
    }
    Bot.Instance.Actions.CheckWalkCycle();
  }

  public void Fish()
  {
    Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Fish"));
    if (!Includes.ApplicationIsActivated())
      return;
    InputKeyboard.PressHotkey1(BotSettings.Settings.HoldTime);
    Bot.Instance.Sleep(BotSettings.Settings.WaitTime);
    Bot.Instance.Actions.SkipDialogueFish();
  }

  public void SweetScent()
  {
    if (!Includes.ApplicationIsActivated())
      return;
    if (Bot.Instance.Check.SweetCent0)
    {
      if ((!Bot.Instance.Settings.AutoLeppa ? 0 : (Bot.Instance.Settings.SweetScent ? 1 : 0)) == 0)
      {
        if ((!Bot.Instance.Settings.AlertSweetScent ? 0 : (!Bot.Instance.Settings.AutoSweetScent ? 1 : 0)) != 0)
        {
          Sounds.PlayNotificationSound();
        }
        else
        {
          if ((!Bot.Instance.Settings.AutoSweetScent ? 0 : (Bot.Instance.Status.GoBackOnce == 0 ? 1 : 0)) == 0)
            return;
          Bot.Instance.Status.GoBack = true;
          ++Bot.Instance.Status.GoBackOnce;
        }
      }
      else
        Bot.Instance.Actions.UseLeppa();
    }
    else
    {
      if ((!Includes.ApplicationIsActivated() ? 0 : (Bot.Instance.Check.Walk ? 1 : 0)) == 0)
        return;
      InputKeyboard.PressHotkey9(BotSettings.Settings.HoldTime);
      Bot.Instance.Sleep(BotSettings.Settings.WaitTime);
    }
  }

  public void AutoWalkFish()
  {
    if (Bot.Instance.Settings.AutoWalkFishRoutesSelectedIndex != -1)
    {
      string selectedRoute = Routes.Instance.GetSelectedRoute();
      if (!Includes.ApplicationIsActivated())
        return;
      if (Bot.Instance.Status.GoTo)
      {
        Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Trying Route to Destination"));
        if (Bot.Instance.Status.FirstAutoSSCycle)
        {
          Routes.Instance.UseTeleport();
          Bot.Instance.Status.FirstAutoSSCycle = false;
        }
        Routes.Instance.Router(selectedRoute, "goto");
        Bot.Instance.Sleep(1500);
        Bot.Instance.Status.GoTo = false;
      }
      else if (!Bot.Instance.Status.GoBack)
      {
        if (Bot.Instance.Status.Heal)
        {
          Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Trying Heal Pokemon"));
          Routes.Instance.HealPokemon();
          if (Bot.Instance.Check.Dialogue)
            return;
          Bot.Instance.Status.Heal = false;
          Bot.Instance.Status.GoBack = false;
          Bot.Instance.Status.GoBackOnce = 0;
          Bot.Instance.Status.GoTo = true;
          Bot.Instance.Status.SelectedPokemon = 1;
        }
        else if (!selectedRoute.Contains("Fish"))
        {
          this.Walk();
        }
        else
        {
          this.Fish();
          if (Bot.Instance.Settings.Lure)
          {
            if ((Bot.Instance.Check.Error2 ? 1 : (Bot.Instance.Check.Error3 ? 1 : 0)) == 0)
              return;
            Bot.Instance.Status.GoBack = true;
            ++Bot.Instance.Status.GoBackOnce;
          }
          else
          {
            if ((Bot.Instance.Check.Error1 || Bot.Instance.Check.Error2 ? 1 : (Bot.Instance.Check.Error3 ? 1 : 0)) == 0)
              return;
            Bot.Instance.Status.GoBack = true;
            ++Bot.Instance.Status.GoBackOnce;
          }
        }
      }
      else
      {
        if (Bot.Instance.Settings.TeleportBack)
        {
          Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Trying Teleport"));
          Routes.Instance.Router(selectedRoute, "teleportback");
          Bot.Instance.Sleep(1500);
        }
        else
        {
          Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Trying Route to Origin"));
          Routes.Instance.Router(selectedRoute, "goback");
          Bot.Instance.Sleep(1500);
        }
        Bot.Instance.Status.GoBack = false;
        Bot.Instance.Status.Heal = true;
      }
    }
    else
      Bot.Instance.Stop();
  }

  public void AutoSweetScent()
  {
    if (Bot.Instance.Settings.AutoSweetScentRoutesSelectedIndex != -1)
    {
      string selectedRoute = Routes.Instance.GetSelectedRoute();
      if (!Includes.ApplicationIsActivated())
        return;
      if (!Bot.Instance.Status.GoTo)
      {
        if (Bot.Instance.Status.GoBack)
        {
          if (Bot.Instance.Settings.TeleportBack)
          {
            Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Trying Teleport"));
            Routes.Instance.Router(selectedRoute, "teleportback");
            Bot.Instance.Sleep(1500);
          }
          else
          {
            Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Trying Route to Origin"));
            Routes.Instance.Router(selectedRoute, "goback");
            Bot.Instance.Sleep(1500);
          }
          Bot.Instance.Status.GoBack = false;
          Bot.Instance.Status.Heal = true;
        }
        else if (Bot.Instance.Status.Heal)
        {
          Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Trying Heal Pokemon"));
          Routes.Instance.HealPokemon();
          if (Bot.Instance.Check.Dialogue)
            return;
          Bot.Instance.Status.Heal = false;
          Bot.Instance.Status.GoBack = false;
          Bot.Instance.Status.GoBackOnce = 0;
          Bot.Instance.Status.GoTo = true;
          Bot.Instance.Status.SelectedPokemon = 1;
        }
        else
        {
          Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: SweetCent"));
          this.SweetScent();
          Bot.Instance.Sleep(2500);
          if ((Bot.Instance.Check.Error1 ? 1 : (Bot.Instance.Check.Error2 ? 1 : 0)) == 0)
            return;
          Bot.Instance.Status.GoBack = true;
          ++Bot.Instance.Status.GoBackOnce;
        }
      }
      else
      {
        Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Trying Route to Destination"));
        if (Bot.Instance.Status.FirstAutoSSCycle)
        {
          Routes.Instance.UseTeleport();
          Bot.Instance.Status.FirstAutoSSCycle = false;
        }
        Routes.Instance.Router(selectedRoute, "goto");
        Bot.Instance.Sleep(1500);
        Bot.Instance.Status.GoTo = false;
      }
    }
    else
      Bot.Instance.Stop();
  }

  public void SafariAutoWalk()
  {
    if (Bot.Instance.Settings.SafariAutoWalkRoutesSelectedIndex == -1)
      return;
    string selectedRoute = Routes.Instance.GetSelectedRoute();
    if (!Includes.ApplicationIsActivated())
      return;
    if (!Bot.Instance.Status.GoTo)
    {
      if (!Bot.Instance.Status.GoBack)
      {
        if (Bot.Instance.Check.Repel)
          InputKeyboard.PressKeyB(150);
        this.Walk();
        if (!Bot.Instance.Check.SafariTimeOver)
          return;
        Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: GoBack"));
        InputKeyboard.PressKeyA(5000);
        Bot.Instance.Status.GoBack = true;
        ++Bot.Instance.Status.GoBackOnce;
      }
      else
      {
        Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: GoBack"));
        Bot.Instance.Status.GoBack = false;
        Bot.Instance.Status.GoBackOnce = 0;
        Bot.Instance.Status.GoTo = true;
      }
    }
    else
    {
      Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: GoTo"));
      Routes.Instance.Router(selectedRoute, "goto");
      Bot.Instance.Sleep(1500);
      Bot.Instance.Status.GoTo = false;
    }
  }

  public void SafariAutoFish()
  {
    if (Bot.Instance.Settings.SafariAutoFishRoutesSelectedIndex == -1)
      return;
    string selectedRoute = Routes.Instance.GetSelectedRoute();
    if (!Includes.ApplicationIsActivated())
      return;
    if (!Bot.Instance.Status.GoTo)
    {
      if (!Bot.Instance.Status.GoBack)
      {
        this.Fish();
        if (Bot.Instance.Check.SafariTimeOver)
        {
          Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: GoBack"));
          InputKeyboard.PressKeyA(5000);
          Bot.Instance.Status.GoBack = true;
          ++Bot.Instance.Status.GoBackOnce;
        }
        else
        {
          if (!Bot.Instance.Check.Error3)
            return;
          this.Walk();
        }
      }
      else
      {
        Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: GoBack"));
        Bot.Instance.Status.GoBack = false;
        Bot.Instance.Status.GoBackOnce = 0;
        Bot.Instance.Status.GoTo = true;
      }
    }
    else
    {
      Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: GoTo"));
      Routes.Instance.Router(selectedRoute, "goto");
      Bot.Instance.Sleep(1500);
      Bot.Instance.Status.GoTo = false;
    }
  }
}
