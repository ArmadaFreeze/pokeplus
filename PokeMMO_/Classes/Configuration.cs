// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.Configuration
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Model;
using PokeMMO_.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace PokeMMO_.Classes;

public class Configuration
{
  public static void SaveDiscordDMSettings()
  {
    try
    {
      IniFile iniFile = new IniFile("Settings.ini");
      iniFile.Write("DiscordDMThief", DiscordViewModel.Instance.DiscordDMThief.ToString());
      iniFile.Write("DiscordDMPayDay", DiscordViewModel.Instance.DiscordDMPayDay.ToString());
      iniFile.Write("DiscordDMThrowBall", DiscordViewModel.Instance.DiscordDMThrowBall.ToString());
      iniFile.Write("DiscordDMIV31", DiscordViewModel.Instance.DiscordDMIV31.ToString());
    }
    catch (Exception ex)
    {
    }
  }

  public static void ResetExeNameAndSetDeleteName()
  {
    try
    {
      IniFile iniFile = new IniFile("LoaderSettings.ini");
      iniFile.Write("ExeName", "", "PokeMMO+_Loader");
      iniFile.Write("DeleteExeName", Process.GetCurrentProcess().MainModule.FileName, "PokeMMO+_Loader");
    }
    catch (Exception ex)
    {
    }
  }

  public static void Save()
  {
    try
    {
      IniFile iniFile = new IniFile("Settings.ini");
      iniFile.Write("BattleMode", MainViewModel.Instance.Home.BotMode.ToString());
      iniFile.Write("CatchWithSecondPokemon", MainViewModel.Instance.Home.CatchWithSecondPokemon.ToString());
      iniFile.Write("LevelFirst", MainViewModel.Instance.Home.LevelFirst.ToString());
      iniFile.Write("Walk", MainViewModel.Instance.Home.Walk.ToString());
      iniFile.Write("Fish", MainViewModel.Instance.Home.Fish.ToString());
      iniFile.Write("SweetScent", MainViewModel.Instance.Home.SweetScent.ToString());
      iniFile.Write("AutoLeppa", MainViewModel.Instance.Home.AutoLeppa.ToString());
      iniFile.Write("AutoEther", MainViewModel.Instance.Home.AutoEther.ToString());
      iniFile.Write("AutoWalkFish", MainViewModel.Instance.Home.AutoWalkFish.ToString());
      iniFile.Write("AutoSweetScent", MainViewModel.Instance.Home.AutoSweetScent.ToString());
      iniFile.Write("SafariAutoWalk", MainViewModel.Instance.Home.SafariAutoWalk.ToString());
      iniFile.Write("SafariAutoFish", MainViewModel.Instance.Home.SafariAutoFish.ToString());
      iniFile.Write("WalkDirection", MainViewModel.Instance.Home.WalkDirection.ToString());
      iniFile.Write("SquaresWalkPattern", MainViewModel.Instance.Home.SquaresWalkPattern.ToString());
      iniFile.Write("RandomWalkPattern", MainViewModel.Instance.Home.RandomWalkPattern.ToString());
      iniFile.Write("CatchPokemonSelectedIndex", MainViewModel.Instance.Home.CatchPokemonSelectedIndex.ToString());
      iniFile.Write("AutoWalkFishRoutesSelectedIndex", MainViewModel.Instance.Home.AutoWalkFishRoutesSelectedIndex.ToString());
      iniFile.Write("AutoSweetScentRoutesSelectedIndex", MainViewModel.Instance.Home.AutoSweetScentRoutesSelectedIndex.ToString());
      iniFile.Write("SafariAutoWalkRoutesSelectedIndex", MainViewModel.Instance.Home.SafariAutoWalkRoutesSelectedIndex.ToString());
      iniFile.Write("SafariAutoFishRoutesSelectedIndex", MainViewModel.Instance.Home.SafariAutoFishRoutesSelectedIndex.ToString());
      iniFile.Write("CatchShiny", MainViewModel.Instance.Home.CatchShiny.ToString());
      iniFile.Write("StopOnShiny", MainViewModel.Instance.Home.StopOnShiny.ToString());
      iniFile.Write("SkipDialog", MainViewModel.Instance.Home.SkipDialog.ToString());
      iniFile.Write("SkipLearnMove", MainViewModel.Instance.Home.SkipLearningNew.ToString());
      iniFile.Write("SkipEvolve", MainViewModel.Instance.Home.SkipEvolve.ToString());
      iniFile.Write("Lure", MainViewModel.Instance.Home.Lure.ToString());
      iniFile.Write("Lure", MainViewModel.Instance.Home.Lure.ToString());
      iniFile.Write("Login", MainViewModel.Instance.Home.Login.ToString());
      iniFile.Write("OnlyKeepIV31", MainViewModel.Instance.Home.OnlyKeepIV31.ToString());
      iniFile.Write("PayDayMultiTarget", MainViewModel.Instance.Home.PayDayMultiTarget.ToString());
      iniFile.Write("MoreThief", MainViewModel.Instance.Home.MoreThief.ToString());
      iniFile.Write("MorePayDay", MainViewModel.Instance.Home.MorePayDay.ToString());
      iniFile.Write("Imprison", MainViewModel.Instance.Home.Imprison.ToString());
      iniFile.Write("Rock", MainViewModel.Instance.Home.Rock.ToString());
      iniFile.Write("Bait", MainViewModel.Instance.Home.Bait.ToString());
      iniFile.Write("ChosenPokeBall", ChosenPokeBallViewModel.Instance.ChosenPokeBall.ToString());
      iniFile.Write("CatchMovesRoutine", MainViewModel.Instance.Premium.CatchMovesRoutine.ToString());
      iniFile.Write("PotionSystem", MainViewModel.Instance.Premium.PotionSystem.ToString());
      iniFile.Write("OrangePotion", MainViewModel.Instance.Premium.OrangePotionSelectedIndex.ToString());
      iniFile.Write("RedPotion", MainViewModel.Instance.Premium.RedPotionSelectedIndex.ToString());
      iniFile.Write("FalseSwipe", MainViewModel.Instance.Premium.FalseSwipe.ToString());
      iniFile.Write("Spore", MainViewModel.Instance.Premium.Spore.ToString());
      iniFile.Write("Substitute", MainViewModel.Instance.Premium.Substitute.ToString());
      iniFile.Write("Assist", MainViewModel.Instance.Premium.Assist.ToString());
      iniFile.Write("TeleportBack", MainViewModel.Instance.Premium.TeleportBack.ToString());
      iniFile.Write("EscapeRope", MainViewModel.Instance.Premium.EscapeRope.ToString());
      iniFile.Write("SlowMode", MainViewModel.Instance.Premium.SlowMode.ToString());
      iniFile.Write("MultiTarget", MainViewModel.Instance.Premium.MultiTarget.ToString());
      iniFile.Write("DiscordUsername", MainViewModel.Instance.Premium.DiscordUsername.ToString());
      iniFile.Write("DiscordDMThief", DiscordViewModel.Instance.DiscordDMThief.ToString());
      iniFile.Write("DiscordDMPayDay", DiscordViewModel.Instance.DiscordDMPayDay.ToString());
      iniFile.Write("DiscordDMThrowBall", DiscordViewModel.Instance.DiscordDMThrowBall.ToString());
      iniFile.Write("DiscordDMIV31", DiscordViewModel.Instance.DiscordDMIV31.ToString());
      iniFile.Write("AlertPM", MainViewModel.Instance.Security.AlertPM.ToString());
      iniFile.Write("StopPM", MainViewModel.Instance.Security.StopPM.ToString());
      iniFile.Write("AlertWalkCycles", MainViewModel.Instance.Security.AlertWalkCycles.ToString());
      iniFile.Write("StopWalkCycles", MainViewModel.Instance.Security.StopWalkCycles.ToString());
      iniFile.Write("WalkCycles", MainViewModel.Instance.Security.WalkCyclesTrigger.ToString());
      iniFile.Write("AlertSweetScent", MainViewModel.Instance.Security.AlertSweetScent.ToString());
      iniFile.Write("WalkSpeedFrom", MainViewModel.Instance.Security.WalkSpeedFrom.ToString());
      iniFile.Write("WalkSpeedTo", MainViewModel.Instance.Security.WalkSpeedTo.ToString());
      iniFile.Write("ChannelSwitchFrom", MainViewModel.Instance.Security.ChannelSwitchFrom.ToString());
      iniFile.Write("ChannelSwitchTo", MainViewModel.Instance.Security.ChannelSwitchTo.ToString());
      iniFile.Write("AutoChannelSwitch", MainViewModel.Instance.Security.AutoChannelSwitch.ToString());
      iniFile.Write("CloseGameAndBot", MainViewModel.Instance.Security.TurnOff.ToString());
      iniFile.Write("CloseGameAndBotMinutes", MainViewModel.Instance.Security.TurnOffTrigger.ToString());
      iniFile.Write("Break", MainViewModel.Instance.Security.Break.ToString());
      iniFile.Write("BreakFrom", MainViewModel.Instance.Security.BreakFrom.ToString());
      iniFile.Write("BreakTo", MainViewModel.Instance.Security.BreakTo.ToString());
      iniFile.Write("BreakLengthFrom", MainViewModel.Instance.Security.BreakLengthFrom.ToString());
      iniFile.Write("BreakLengthTo", MainViewModel.Instance.Security.BreakLengthTo.ToString());
      iniFile.Write("Humanize", MainViewModel.Instance.Security.Humanize.ToString());
      iniFile.Write("Resolution", MainViewModel.Instance.Settings.ResolutionMode.ToString());
      iniFile.Write("DefaultPath", MainViewModel.Instance.Settings.DefaultPath.ToString());
      iniFile.Write("PrimaryMouseButton", MainViewModel.Instance.Settings.PrimaryMouseButton.ToString());
      iniFile.Write("HumanizeMouseMovement", MainViewModel.Instance.Settings.HumanizeMouseMovement.ToString());
      iniFile.Write("AutoLogin", AuthViewModel.Instance.AutoLogin.ToString());
    }
    catch (Exception ex)
    {
    }
  }

  public static void Load()
  {
    try
    {
      IniFile iniFile = new IniFile("Settings.ini");
      switch (iniFile.Read("BattleMode"))
      {
        case "Fight":
          MainViewModel.Instance.Home.BotMode = BotMode.Fight;
          MainViewModel.Instance.Home.LevelFirstEnabled = true;
          MainViewModel.Instance.Home.FightOptionVisibility = Visibility.Visible;
          MainViewModel.Instance.Home.CatchPokemonVisibility = Visibility.Visible;
          break;
        case "Run":
          MainViewModel.Instance.Home.BotMode = BotMode.Run;
          break;
        case "Catch":
          MainViewModel.Instance.Home.BotMode = BotMode.Catch;
          MainViewModel.Instance.Home.IV31Visibility = Visibility.Visible;
          MainViewModel.Instance.Home.CatchSpellsVisbility = Visibility.Visible;
          MainViewModel.Instance.Home.CatchPokemonVisibility = Visibility.Visible;
          break;
        case "PayDay":
          MainViewModel.Instance.Home.BotMode = BotMode.PayDay;
          MainViewModel.Instance.Home.PayDayOptionVisibility = Visibility.Visible;
          break;
        case "Thief":
          MainViewModel.Instance.Home.BotMode = BotMode.Thief;
          MainViewModel.Instance.Home.ThiefOptionVisibility = Visibility.Visible;
          MainViewModel.Instance.Home.CatchPokemonVisibility = Visibility.Visible;
          break;
        case "Safari":
          MainViewModel.Instance.Home.BotMode = BotMode.Safari;
          MainViewModel.Instance.Home.SafariOptionVisibility = Visibility.Visible;
          MainViewModel.Instance.Home.AutoSweetScentRoutesVisibility = Visibility.Hidden;
          MainViewModel.Instance.Home.AutoWalkFishRoutesVisibility = Visibility.Hidden;
          MainViewModel.Instance.Home.AutoWalkFishVisibility = Visibility.Hidden;
          MainViewModel.Instance.Home.AutoSweetScentVisibility = Visibility.Hidden;
          break;
        case "SellBox":
          MainViewModel.Instance.Home.BotMode = BotMode.SellBox;
          break;
        case "MailClaim":
          MainViewModel.Instance.Home.BotMode = BotMode.MailClaim;
          break;
        case "GTLSniper":
          MainViewModel.Instance.Home.BotMode = BotMode.GTLSniper;
          break;
        case "PayDayCatchMixed":
          MainViewModel.Instance.Home.BotMode = BotMode.PayDayCatchMixed;
          break;
        case "PayDayThiefMixed":
          MainViewModel.Instance.Home.BotMode = BotMode.PayDayThiefMixed;
          break;
      }
      if (bool.Parse(iniFile.Read("LevelFirst")))
        MainViewModel.Instance.Home.LevelFirst = true;
      if (bool.Parse(iniFile.Read("CatchWithSecondPokemon")))
        MainViewModel.Instance.Home.CatchWithSecondPokemon = true;
      if (bool.Parse(iniFile.Read("OnlyKeepIV31")))
        MainViewModel.Instance.Home.OnlyKeepIV31 = true;
      if (bool.Parse(iniFile.Read("PayDayMultiTarget")))
        MainViewModel.Instance.Home.PayDayMultiTarget = true;
      if (bool.Parse(iniFile.Read("MoreThief")))
        MainViewModel.Instance.Home.MoreThief = true;
      if (bool.Parse(iniFile.Read("MorePayDay")))
        MainViewModel.Instance.Home.MorePayDay = true;
      if (bool.Parse(iniFile.Read("Imprison")))
        MainViewModel.Instance.Home.Imprison = true;
      MainViewModel.Instance.Home.WalkOptionsVisibility = bool.Parse(iniFile.Read("Walk")) ? Visibility.Visible : Visibility.Hidden;
      MainViewModel.Instance.Home.Walk = bool.Parse(iniFile.Read("Walk"));
      MainViewModel.Instance.Home.Fish = bool.Parse(iniFile.Read("Fish"));
      MainViewModel.Instance.Home.SweetScent = bool.Parse(iniFile.Read("SweetScent"));
      MainViewModel.Instance.Home.AutoLeppa = bool.Parse(iniFile.Read("AutoLeppa"));
      MainViewModel.Instance.Home.AutoEther = bool.Parse(iniFile.Read("AutoEther"));
      MainViewModel.Instance.Home.AutoWalkFish = bool.Parse(iniFile.Read("AutoWalkFish"));
      MainViewModel.Instance.Home.AutoSweetScent = bool.Parse(iniFile.Read("AutoSweetScent"));
      MainViewModel.Instance.Home.SafariAutoWalk = bool.Parse(iniFile.Read("SafariAutoWalk"));
      MainViewModel.Instance.Home.SafariAutoFish = bool.Parse(iniFile.Read("SafariAutoFish"));
      MainViewModel.Instance.Home.WalkDirection = bool.Parse(iniFile.Read("WalkDirection"));
      MainViewModel.Instance.Home.SquaresWalkPattern = bool.Parse(iniFile.Read("SquaresWalkPattern"));
      MainViewModel.Instance.Home.RandomWalkPattern = bool.Parse(iniFile.Read("RandomWalkPattern"));
      MainViewModel.Instance.Home.AutoWalkFishRoutesSelectedIndex = int.Parse(iniFile.Read("AutoWalkFishRoutesSelectedIndex"));
      MainViewModel.Instance.Home.AutoSweetScentRoutesSelectedIndex = int.Parse(iniFile.Read("AutoSweetScentRoutesSelectedIndex"));
      MainViewModel.Instance.Home.SafariAutoWalkRoutesSelectedIndex = int.Parse(iniFile.Read("SafariAutoWalkRoutesSelectedIndex"));
      MainViewModel.Instance.Home.SafariAutoFishRoutesSelectedIndex = int.Parse(iniFile.Read("SafariAutoFishRoutesSelectedIndex"));
      MainViewModel.Instance.Home.CatchPokemonSelectedIndex = int.Parse(iniFile.Read("CatchPokemonSelectedIndex"));
      MainViewModel.Instance.Home.CatchShiny = bool.Parse(iniFile.Read("CatchShiny"));
      MainViewModel.Instance.Home.StopOnShiny = bool.Parse(iniFile.Read("StopOnShiny"));
      MainViewModel.Instance.Home.SkipDialog = bool.Parse(iniFile.Read("SkipDialog"));
      MainViewModel.Instance.Home.SkipLearningNew = bool.Parse(iniFile.Read("SkipLearnMove"));
      MainViewModel.Instance.Home.SkipEvolve = bool.Parse(iniFile.Read("SkipEvolve"));
      MainViewModel.Instance.Home.Lure = bool.Parse(iniFile.Read("Lure"));
      MainViewModel.Instance.Home.Login = bool.Parse(iniFile.Read("Login"));
      MainViewModel.Instance.Home.Rock = bool.Parse(iniFile.Read("Rock"));
      MainViewModel.Instance.Home.Bait = bool.Parse(iniFile.Read("Bait"));
      switch (iniFile.Read("ChosenPokeBall"))
      {
        case "PokeBall":
          ChosenPokeBallViewModel.Instance.ChosenPokeBall = ChosenPokeBall.PokeBall;
          break;
        case "GreatBall":
          ChosenPokeBallViewModel.Instance.ChosenPokeBall = ChosenPokeBall.GreatBall;
          break;
        case "UltraBall":
          ChosenPokeBallViewModel.Instance.ChosenPokeBall = ChosenPokeBall.UltraBall;
          break;
        case "DuskBall":
          ChosenPokeBallViewModel.Instance.ChosenPokeBall = ChosenPokeBall.DuskBall;
          break;
        case "DiveBall":
          ChosenPokeBallViewModel.Instance.ChosenPokeBall = ChosenPokeBall.DiveBall;
          break;
        case "RepeatBall":
          ChosenPokeBallViewModel.Instance.ChosenPokeBall = ChosenPokeBall.RepeatBall;
          break;
      }
      switch (iniFile.Read("CatchMovesRoutine"))
      {
        case "SFS":
          MainViewModel.Instance.Premium.CatchMovesRoutine = CatchMovesRoutine.SFS;
          break;
        case "SSF":
          MainViewModel.Instance.Premium.CatchMovesRoutine = CatchMovesRoutine.SSF;
          break;
        case "FA":
          MainViewModel.Instance.Premium.CatchMovesRoutine = CatchMovesRoutine.FA;
          break;
      }
      MainViewModel.Instance.Premium.PotionSystem = bool.Parse(iniFile.Read("PotionSystem"));
      MainViewModel.Instance.Premium.OrangePotionSelectedIndex = int.Parse(iniFile.Read("OrangePotion"));
      MainViewModel.Instance.Premium.RedPotionSelectedIndex = int.Parse(iniFile.Read("RedPotion"));
      MainViewModel.Instance.Premium.FalseSwipe = bool.Parse(iniFile.Read("FalseSwipe"));
      MainViewModel.Instance.Premium.Spore = bool.Parse(iniFile.Read("Spore"));
      MainViewModel.Instance.Premium.Substitute = bool.Parse(iniFile.Read("Substitute"));
      MainViewModel.Instance.Premium.Assist = bool.Parse(iniFile.Read("Assist"));
      MainViewModel.Instance.Premium.TeleportBack = bool.Parse(iniFile.Read("TeleportBack"));
      MainViewModel.Instance.Premium.EscapeRope = bool.Parse(iniFile.Read("EscapeRope"));
      MainViewModel.Instance.Premium.SlowMode = bool.Parse(iniFile.Read("SlowMode"));
      MainViewModel.Instance.Premium.MultiTarget = bool.Parse(iniFile.Read("MultiTarget"));
      MainViewModel.Instance.Premium.DiscordUsername = iniFile.Read("DiscordUsername");
      DiscordViewModel.Instance.DiscordDMThief = bool.Parse(iniFile.Read("DiscordDMThief"));
      DiscordViewModel.Instance.DiscordDMPayDay = bool.Parse(iniFile.Read("DiscordDMPayDay"));
      DiscordViewModel.Instance.DiscordDMThrowBall = bool.Parse(iniFile.Read("DiscordDMThrowBall"));
      DiscordViewModel.Instance.DiscordDMIV31 = bool.Parse(iniFile.Read("DiscordDMIV31"));
      MainViewModel.Instance.Security.AlertPM = bool.Parse(iniFile.Read("AlertPM"));
      MainViewModel.Instance.Security.StopPM = bool.Parse(iniFile.Read("StopPM"));
      MainViewModel.Instance.Security.AlertWalkCycles = bool.Parse(iniFile.Read("AlertWalkCycles"));
      MainViewModel.Instance.Security.StopWalkCycles = bool.Parse(iniFile.Read("StopWalkCycles"));
      MainViewModel.Instance.Security.WalkCyclesTrigger = int.Parse(iniFile.Read("WalkCycles"));
      MainViewModel.Instance.Security.AlertSweetScent = bool.Parse(iniFile.Read("AlertSweetScent"));
      MainViewModel.Instance.Security.WalkSpeedFrom = int.Parse(iniFile.Read("WalkSpeedFrom"));
      MainViewModel.Instance.Security.WalkSpeedTo = int.Parse(iniFile.Read("WalkSpeedTo"));
      MainViewModel.Instance.Security.ChannelSwitchFrom = int.Parse(iniFile.Read("ChannelSwitchFrom"));
      MainViewModel.Instance.Security.ChannelSwitchTo = int.Parse(iniFile.Read("ChannelSwitchTo"));
      MainViewModel.Instance.Security.AutoChannelSwitch = bool.Parse(iniFile.Read("AutoChannelSwitch"));
      MainViewModel.Instance.Security.TurnOff = bool.Parse(iniFile.Read("CloseGameAndBot"));
      MainViewModel.Instance.Security.TurnOffTrigger = int.Parse(iniFile.Read("CloseGameAndBotMinutes"));
      MainViewModel.Instance.Security.Break = bool.Parse(iniFile.Read("Break"));
      MainViewModel.Instance.Security.BreakFrom = int.Parse(iniFile.Read("BreakFrom"));
      MainViewModel.Instance.Security.BreakTo = int.Parse(iniFile.Read("BreakTo"));
      MainViewModel.Instance.Security.BreakLengthFrom = int.Parse(iniFile.Read("BreakLengthFrom"));
      MainViewModel.Instance.Security.BreakLengthTo = int.Parse(iniFile.Read("BreakLengthTo"));
      MainViewModel.Instance.Security.Humanize = bool.Parse(iniFile.Read("Humanize"));
      switch (iniFile.Read("Resolution"))
      {
        case "HD":
          MainViewModel.Instance.Settings.ResolutionMode = ResolutionMode.HD;
          break;
        case "SD":
          MainViewModel.Instance.Settings.ResolutionMode = ResolutionMode.SD;
          break;
      }
      MainViewModel.Instance.Settings.DefaultPath = iniFile.Read("DefaultPath");
      MainViewModel.Instance.Settings.PrimaryMouseButton = bool.Parse(iniFile.Read("PrimaryMouseButton"));
      MainViewModel.Instance.Settings.HumanizeMouseMovement = bool.Parse(iniFile.Read("HumanizeMouseMovement"));
      AuthViewModel.Instance.AutoLogin = bool.Parse(iniFile.Read("AutoLogin"));
    }
    catch (Exception ex)
    {
    }
  }
}
