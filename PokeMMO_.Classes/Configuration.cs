using System;
using System.Diagnostics;
using System.IO;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Classes;

public class Configuration
{
	private static bool ReadBool(IniFile ini, string key, bool defaultValue = false)
	{
		bool result;
		return bool.TryParse(ini.Read(key), out result) ? result : defaultValue;
	}

	private static int ReadInt(IniFile ini, string key, int defaultValue = 0)
	{
		int result;
		return int.TryParse(ini.Read(key), out result) ? result : defaultValue;
	}

	public static void SaveDiscordDMSettings()
	{
		try
		{
			IniFile iniFile = new IniFile("Settings.ini");
			iniFile.Write("DiscordDMThief", DiscordViewModel.Instance.DiscordDMThief);
			iniFile.Write("DiscordDMPayDay", DiscordViewModel.Instance.DiscordDMPayDay);
			iniFile.Write("DiscordDMThrowBall", DiscordViewModel.Instance.DiscordDMThrowBall);
			iniFile.Write("DiscordDMIV31", DiscordViewModel.Instance.DiscordDMIV31);
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log(ex.Message);
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
			PokeMMOLogger.Instance.Log(ex.Message);
		}
	}

	public static void Save()
	{
		try
		{
			IniFile iniFile = new IniFile("Settings.ini");
			Home home = MainViewModel.Instance.Home;
			Premium premium = MainViewModel.Instance.Premium;
			Security security = MainViewModel.Instance.Security;
			Settings settings = MainViewModel.Instance.Settings;
			iniFile.Write("BattleMode", home.BotMode.ToString());
			iniFile.Write("CatchWithSecondPokemon", home.CatchWithSecondPokemon);
			iniFile.Write("LevelFirst", home.LevelFirst);
			iniFile.Write("Walk", home.Walk);
			iniFile.Write("Fish", home.Fish);
			iniFile.Write("SweetScent", home.SweetScent);
			iniFile.Write("AutoLeppa", home.AutoLeppa);
			iniFile.Write("AutoEther", home.AutoEther);
			iniFile.Write("AutoWalkFish", home.AutoWalkFish);
			iniFile.Write("AutoSweetScent", home.AutoSweetScent);
			iniFile.Write("SafariAutoWalk", home.SafariAutoWalk);
			iniFile.Write("SafariAutoFish", home.SafariAutoFish);
			iniFile.Write("WalkDirection", home.WalkDirection);
			iniFile.Write("SquaresWalkPattern", home.SquaresWalkPattern);
			iniFile.Write("RandomWalkPattern", home.RandomWalkPattern);
			iniFile.Write("CatchPokemonSelectedIndex", home.CatchPokemonSelectedIndex);
			iniFile.Write("SelectedAutoWalkFishRoute", home.SelectedAutoWalkFishRoute);
			iniFile.Write("SelectedAutoSweetScentRoute", home.SelectedAutoSweetScentRoute);
			iniFile.Write("SelectedSafariAutoWalkRoute", home.SelectedSafariAutoWalkRoute);
			iniFile.Write("SelectedSafariAutoFishRoute", home.SelectedSafariAutoFishRoute);
			iniFile.Write("CatchShiny", home.CatchShiny);
			iniFile.Write("StopOnShiny", home.StopOnShiny);
			iniFile.Write("SkipDialog", home.SkipDialog);
			iniFile.Write("SkipLearnMove", home.SkipLearningNew);
			iniFile.Write("SkipEvolve", home.SkipEvolve);
			iniFile.Write("Lure", home.Lure);
			iniFile.Write("Login", home.Login);
			iniFile.Write("OnlyKeepIV31", home.OnlyKeepIV31);
			iniFile.Write("PayDayMultiTarget", home.PayDayMultiTarget);
			iniFile.Write("MoreThief", home.MoreThief);
			iniFile.Write("MorePayDay", home.MorePayDay);
			iniFile.Write("Imprison", home.Imprison);
			iniFile.Write("Rock", home.Rock);
			iniFile.Write("Bait", home.Bait);
			iniFile.Write("ChosenPokeBall", ChosenPokeBallViewModel.Instance.ChosenPokeBall.ToString());
			iniFile.Write("CatchMovesRoutine", premium.CatchMovesRoutine.ToString());
			iniFile.Write("PotionSystem", premium.PotionSystem);
			iniFile.Write("OrangePotion", premium.OrangePotionSelectedIndex);
			iniFile.Write("RedPotion", premium.RedPotionSelectedIndex);
			iniFile.Write("FalseSwipe", home.Options[1].Selected);
			iniFile.Write("Spore", home.Options[2].Selected);
			iniFile.Write("Substitute", home.Options[0].Selected);
			iniFile.Write("Assist", home.Options[3].Selected);
			iniFile.Write("TeleportBack", premium.TeleportBack);
			iniFile.Write("EscapeRope", premium.EscapeRope);
			iniFile.Write("SlowMode", premium.SlowMode);
			iniFile.Write("MultiTarget", premium.MultiTarget);
			iniFile.Write("DiscordUsername", premium.DiscordUsername);
			iniFile.Write("DiscordDMThief", DiscordViewModel.Instance.DiscordDMThief);
			iniFile.Write("DiscordDMPayDay", DiscordViewModel.Instance.DiscordDMPayDay);
			iniFile.Write("DiscordDMThrowBall", DiscordViewModel.Instance.DiscordDMThrowBall);
			iniFile.Write("DiscordDMIV31", DiscordViewModel.Instance.DiscordDMIV31);
			iniFile.Write("AlertPM", security.AlertPM);
			iniFile.Write("StopPM", security.StopPM);
			iniFile.Write("AlertWalkCycles", security.AlertWalkCycles);
			iniFile.Write("StopWalkCycles", security.StopWalkCycles);
			iniFile.Write("WalkCycles", security.WalkCyclesTrigger);
			iniFile.Write("AlertSweetScent", security.AlertSweetScent);
			iniFile.Write("WalkSpeedFrom", security.WalkSpeedFrom);
			iniFile.Write("WalkSpeedTo", security.WalkSpeedTo);
			iniFile.Write("ChannelSwitchFrom", security.ChannelSwitchFrom);
			iniFile.Write("ChannelSwitchTo", security.ChannelSwitchTo);
			iniFile.Write("AutoChannelSwitch", security.AutoChannelSwitch);
			iniFile.Write("CloseGameAndBot", security.TurnOff);
			iniFile.Write("CloseGameAndBotMinutes", security.TurnOffTrigger);
			iniFile.Write("Break", security.Break);
			iniFile.Write("BreakFrom", security.BreakFrom);
			iniFile.Write("BreakTo", security.BreakTo);
			iniFile.Write("BreakLengthFrom", security.BreakLengthFrom);
			iniFile.Write("BreakLengthTo", security.BreakLengthTo);
			iniFile.Write("Humanize", security.Humanize);
			iniFile.Write("Resolution", settings.ResolutionMode.ToString());
			iniFile.Write("DefaultPath", settings.DefaultPath);
			iniFile.Write("PrimaryMouseButton", settings.PrimaryMouseButton);
			iniFile.Write("HumanizeMouseMovement", settings.HumanizeMouseMovement);
			iniFile.Write("AutoLogin", AuthViewModel.Instance.AutoLogin);
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log(ex.Message);
		}
	}

	public static void Load()
	{
		try
		{
			if (!File.Exists("Settings.ini"))
			{
				string defaultPath = MainViewModel.Instance.Settings.DefaultPath;
				if (!string.IsNullOrEmpty(defaultPath) && File.Exists(defaultPath + "\\config\\main.properties") && File.Exists(defaultPath + "\\data\\themes\\default\\gfx.xml") && File.Exists("gfx.xml"))
				{
					PathAndFileManager.ReplacePropertiesAndGFXFile(messagebox: true);
				}
				Save();
				return;
			}
			IniFile iniFile = new IniFile("Settings.ini");
			Home home = MainViewModel.Instance.Home;
			Premium premium = MainViewModel.Instance.Premium;
			Security security = MainViewModel.Instance.Security;
			Settings settings = MainViewModel.Instance.Settings;
			string value = iniFile.Read("BattleMode");
			if (Enum.TryParse<BotMode>(value, out var result))
			{
				home.BotMode = result;
				home.ApplyBotModeVisibility(result);
			}
			home.LevelFirst = ReadBool(iniFile, "LevelFirst");
			home.CatchWithSecondPokemon = ReadBool(iniFile, "CatchWithSecondPokemon");
			home.OnlyKeepIV31 = ReadBool(iniFile, "OnlyKeepIV31");
			home.PayDayMultiTarget = ReadBool(iniFile, "PayDayMultiTarget");
			home.MoreThief = ReadBool(iniFile, "MoreThief");
			home.MorePayDay = ReadBool(iniFile, "MorePayDay");
			home.Imprison = ReadBool(iniFile, "Imprison");
			home.Walk = ReadBool(iniFile, "Walk", defaultValue: true);
			home.Fish = ReadBool(iniFile, "Fish");
			home.SweetScent = ReadBool(iniFile, "SweetScent");
			home.AutoLeppa = ReadBool(iniFile, "AutoLeppa");
			home.AutoEther = ReadBool(iniFile, "AutoEther");
			home.AutoWalkFish = ReadBool(iniFile, "AutoWalkFish");
			home.AutoSweetScent = ReadBool(iniFile, "AutoSweetScent");
			home.SafariAutoWalk = ReadBool(iniFile, "SafariAutoWalk");
			home.SafariAutoFish = ReadBool(iniFile, "SafariAutoFish");
			home.WalkDirection = ReadBool(iniFile, "WalkDirection", defaultValue: true);
			home.SquaresWalkPattern = ReadBool(iniFile, "SquaresWalkPattern");
			home.RandomWalkPattern = ReadBool(iniFile, "RandomWalkPattern");
			home.SelectedAutoWalkFishRoute = iniFile.Read("SelectedAutoWalkFishRoute");
			home.SelectedAutoSweetScentRoute = iniFile.Read("SelectedAutoSweetScentRoute");
			home.SelectedSafariAutoWalkRoute = iniFile.Read("SelectedSafariAutoWalkRoute");
			home.SelectedSafariAutoFishRoute = iniFile.Read("SelectedSafariAutoFishRoute");
			home.CatchPokemonSelectedIndex = ReadInt(iniFile, "CatchPokemonSelectedIndex");
			home.CatchShiny = ReadBool(iniFile, "CatchShiny", defaultValue: true);
			home.StopOnShiny = ReadBool(iniFile, "StopOnShiny");
			home.SkipDialog = ReadBool(iniFile, "SkipDialog", defaultValue: true);
			home.SkipLearningNew = ReadBool(iniFile, "SkipLearnMove");
			home.SkipEvolve = ReadBool(iniFile, "SkipEvolve");
			home.Lure = ReadBool(iniFile, "Lure");
			home.Login = ReadBool(iniFile, "Login", defaultValue: true);
			home.Rock = ReadBool(iniFile, "Rock");
			home.Bait = ReadBool(iniFile, "Bait");
			if (Enum.TryParse<ChosenPokeBall>(iniFile.Read("ChosenPokeBall"), out var result2))
			{
				ChosenPokeBallViewModel.Instance.ChosenPokeBall = result2;
			}
			if (Enum.TryParse<CatchMovesRoutine>(iniFile.Read("CatchMovesRoutine"), out var result3))
			{
				premium.CatchMovesRoutine = result3;
			}
			premium.PotionSystem = ReadBool(iniFile, "PotionSystem");
			premium.OrangePotionSelectedIndex = ReadInt(iniFile, "OrangePotion", -1);
			premium.RedPotionSelectedIndex = ReadInt(iniFile, "RedPotion", -1);
			premium.FalseSwipe = ReadBool(iniFile, "FalseSwipe");
			premium.Spore = ReadBool(iniFile, "Spore");
			premium.Substitute = ReadBool(iniFile, "Substitute");
			premium.Assist = ReadBool(iniFile, "Assist");
			premium.TeleportBack = ReadBool(iniFile, "TeleportBack");
			premium.EscapeRope = ReadBool(iniFile, "EscapeRope");
			premium.SlowMode = ReadBool(iniFile, "SlowMode");
			premium.MultiTarget = ReadBool(iniFile, "MultiTarget");
			premium.DiscordUsername = iniFile.Read("DiscordUsername");
			DiscordViewModel.Instance.DiscordDMThief = ReadBool(iniFile, "DiscordDMThief");
			DiscordViewModel.Instance.DiscordDMPayDay = ReadBool(iniFile, "DiscordDMPayDay");
			DiscordViewModel.Instance.DiscordDMThrowBall = ReadBool(iniFile, "DiscordDMThrowBall");
			DiscordViewModel.Instance.DiscordDMIV31 = ReadBool(iniFile, "DiscordDMIV31");
			security.AlertPM = ReadBool(iniFile, "AlertPM", defaultValue: true);
			security.StopPM = ReadBool(iniFile, "StopPM", defaultValue: true);
			security.AlertWalkCycles = ReadBool(iniFile, "AlertWalkCycles", defaultValue: true);
			security.StopWalkCycles = ReadBool(iniFile, "StopWalkCycles", defaultValue: true);
			security.WalkCyclesTrigger = ReadInt(iniFile, "WalkCycles", 10);
			security.AlertSweetScent = ReadBool(iniFile, "AlertSweetScent", defaultValue: true);
			security.WalkSpeedFrom = ReadInt(iniFile, "WalkSpeedFrom", 700);
			security.WalkSpeedTo = ReadInt(iniFile, "WalkSpeedTo", 1300);
			security.ChannelSwitchFrom = ReadInt(iniFile, "ChannelSwitchFrom", 30);
			security.ChannelSwitchTo = ReadInt(iniFile, "ChannelSwitchTo", 60);
			security.AutoChannelSwitch = ReadBool(iniFile, "AutoChannelSwitch");
			security.TurnOff = ReadBool(iniFile, "CloseGameAndBot");
			security.TurnOffTrigger = ReadInt(iniFile, "CloseGameAndBotMinutes", 60);
			security.Break = ReadBool(iniFile, "Break");
			security.BreakFrom = ReadInt(iniFile, "BreakFrom", 30);
			security.BreakTo = ReadInt(iniFile, "BreakTo", 60);
			security.BreakLengthFrom = ReadInt(iniFile, "BreakLengthFrom", 30);
			security.BreakLengthTo = ReadInt(iniFile, "BreakLengthTo", 60);
			security.Humanize = ReadBool(iniFile, "Humanize");
			if (Enum.TryParse<ResolutionMode>(iniFile.Read("Resolution"), out var result4))
			{
				settings.ResolutionMode = result4;
			}
			string text = iniFile.Read("DefaultPath");
			if (!string.IsNullOrEmpty(text))
			{
				settings.DefaultPath = text;
			}
			settings.PrimaryMouseButton = ReadBool(iniFile, "PrimaryMouseButton");
			settings.HumanizeMouseMovement = ReadBool(iniFile, "HumanizeMouseMovement", defaultValue: true);
			AuthViewModel.Instance.AutoLogin = ReadBool(iniFile, "AutoLogin");
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log(ex.Message);
		}
	}

	public static void ResetSettings()
	{
		try
		{
			Home home = MainViewModel.Instance.Home;
			Premium premium = MainViewModel.Instance.Premium;
			Security security = MainViewModel.Instance.Security;
			Settings settings = MainViewModel.Instance.Settings;
			home.BotMode = BotMode.Run;
			home.ApplyBotModeVisibility(BotMode.Run);
			home.LevelFirst = false;
			home.CatchWithSecondPokemon = false;
			home.OnlyKeepIV31 = false;
			home.PayDayMultiTarget = false;
			home.MoreThief = false;
			home.MorePayDay = false;
			home.Imprison = false;
			home.Walk = true;
			home.Fish = false;
			home.SweetScent = false;
			home.AutoLeppa = false;
			home.AutoEther = false;
			home.AutoWalkFish = false;
			home.AutoSweetScent = false;
			home.SafariAutoWalk = false;
			home.SafariAutoFish = false;
			home.WalkDirection = true;
			home.SquaresWalkPattern = false;
			home.RandomWalkPattern = false;
			home.SelectedAutoWalkFishRoute = "";
			home.SelectedAutoSweetScentRoute = "";
			home.SelectedSafariAutoWalkRoute = "";
			home.SelectedSafariAutoFishRoute = "";
			home.CatchPokemonSelectedIndex = 0;
			home.CatchShiny = true;
			home.StopOnShiny = false;
			home.SkipDialog = true;
			home.SkipLearningNew = false;
			home.SkipEvolve = false;
			home.Lure = false;
			home.Login = true;
			home.Rock = false;
			home.Bait = false;
			ChosenPokeBallViewModel.Instance.ChosenPokeBall = ChosenPokeBall.PokeBall;
			premium.CatchMovesRoutine = CatchMovesRoutine.SFS;
			premium.PotionSystem = false;
			premium.OrangePotionSelectedIndex = -1;
			premium.RedPotionSelectedIndex = -1;
			premium.FalseSwipe = false;
			premium.Spore = false;
			premium.Substitute = false;
			premium.Assist = false;
			premium.TeleportBack = false;
			premium.EscapeRope = false;
			premium.SlowMode = false;
			premium.MultiTarget = false;
			premium.DiscordUsername = "";
			DiscordViewModel.Instance.DiscordDMThief = false;
			DiscordViewModel.Instance.DiscordDMPayDay = false;
			DiscordViewModel.Instance.DiscordDMThrowBall = false;
			DiscordViewModel.Instance.DiscordDMIV31 = false;
			security.AlertPM = true;
			security.StopPM = true;
			security.AlertWalkCycles = true;
			security.StopWalkCycles = true;
			security.WalkCyclesTrigger = 10;
			security.AlertSweetScent = true;
			security.WalkSpeedFrom = 700;
			security.WalkSpeedTo = 1300;
			security.ChannelSwitchFrom = 30;
			security.ChannelSwitchTo = 60;
			security.AutoChannelSwitch = false;
			security.TurnOff = false;
			security.TurnOffTrigger = 60;
			security.Break = false;
			security.BreakFrom = 30;
			security.BreakTo = 60;
			security.BreakLengthFrom = 30;
			security.BreakLengthTo = 60;
			security.Humanize = false;
			settings.ResolutionMode = ResolutionMode.HD;
			settings.PrimaryMouseButton = false;
			settings.HumanizeMouseMovement = true;
			AuthViewModel.Instance.AutoLogin = false;
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log("ResetSettings error: " + ex.Message);
		}
	}

	public static void DeleteSettings()
	{
		try
		{
			if (File.Exists("Settings.ini"))
			{
				File.Delete("Settings.ini");
			}
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log(ex.Message);
		}
	}
}
