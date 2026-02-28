using System;
using System.Collections.Generic;
using PokeMMO_.Classes;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Botting;

public class BotSettings
{
	private static readonly object padlock = new object();

	private static BotSettings settings = null;

	public Dictionary<string, string> Data = new Dictionary<string, string>();

	public static BotSettings Settings
	{
		get
		{
			lock (padlock)
			{
				if (settings == null)
				{
					settings = new BotSettings();
				}
				return settings;
			}
		}
	}

	public int WalkSpeed => RandomNumber.Between(Bot.Instance.Actions.SafeWalkFromInt(), Bot.Instance.Actions.SafeWalkToInt());

	public int HoldTime => RandomNumber.Between(100, 150);

	public int WaitTimeShort => RandomNumber.Between(50, 100);

	public int WaitTimeShortRandom => RandomNumber.Between(100, 600);

	public int WaitTime => RandomNumber.Between(150, 200);

	public int WaitTimeLong => RandomNumber.Between(250, 300);

	public int WaitTimeVeryLong => RandomNumber.Between(500, 600);

	public int WaitTimeHuman => RandomNumber.Between(1000, 10000);

	public bool PotionSystem => GetPremiumBool(() => MainViewModel.Instance.Premium.PotionSystem);

	public bool Substitute => GetPremiumBool(() => MainViewModel.Instance.Home.Options[0].Selected);

	public bool FalseSwipe => GetPremiumBool(() => MainViewModel.Instance.Home.Options[1].Selected);

	public bool Spore => GetPremiumBool(() => MainViewModel.Instance.Home.Options[2].Selected);

	public bool Assist => GetPremiumBool(() => MainViewModel.Instance.Home.Options[3].Selected);

	public bool TeleportBack => GetPremiumBool(() => MainViewModel.Instance.Premium.TeleportBack);

	public bool EscapeRope => GetPremiumBool(() => MainViewModel.Instance.Premium.EscapeRope);

	public bool SlowMode => GetPremiumBool(() => MainViewModel.Instance.Premium.SlowMode);

	public bool MultiTarget => GetPremiumBool(() => MainViewModel.Instance.Premium.MultiTarget);

	public bool Walk => MainViewModel.Instance.Home.Walk;

	public bool Fish => MainViewModel.Instance.Home.Fish;

	public bool SweetScent => MainViewModel.Instance.Home.SweetScent;

	public bool AutoLeppa => MainViewModel.Instance.Home.AutoLeppa;

	public bool AutoEther => MainViewModel.Instance.Home.AutoEther;

	public bool AutoWalkFish => MainViewModel.Instance.Home.AutoWalkFish;

	public bool AutoSweetScent => MainViewModel.Instance.Home.AutoSweetScent;

	public bool SafariAutoWalk => MainViewModel.Instance.Home.SafariAutoWalk;

	public bool SafariAutoFish => MainViewModel.Instance.Home.SafariAutoFish;

	public bool LevelFirst => MainViewModel.Instance.Home.LevelFirst;

	public bool CatchWithSecondPokemon => MainViewModel.Instance.Home.CatchWithSecondPokemon;

	public bool SquaresWalkPattern => MainViewModel.Instance.Home.SquaresWalkPattern;

	public bool RandomWalkPattern => MainViewModel.Instance.Home.RandomWalkPattern;

	public bool Login => MainViewModel.Instance.Home.Login;

	public bool OnlyKeepIV31 => MainViewModel.Instance.Home.OnlyKeepIV31;

	public bool MoreThief => MainViewModel.Instance.Home.MoreThief;

	public bool MorePayDay => MainViewModel.Instance.Home.MorePayDay;

	public bool Imprison => MainViewModel.Instance.Home.Imprison;

	public bool Rock => MainViewModel.Instance.Home.Rock;

	public bool Lure => MainViewModel.Instance.Home.Lure;

	public bool Bait => MainViewModel.Instance.Home.Bait;

	public bool PayDayMultiTarget => MainViewModel.Instance.Home.PayDayMultiTarget;

	public bool CatchShiny => MainViewModel.Instance.Home.CatchShiny;

	public bool StopOnShiny => MainViewModel.Instance.Home.StopOnShiny;

	public bool SkipDialog => MainViewModel.Instance.Home.SkipDialog;

	public bool SkipLearningNew => MainViewModel.Instance.Home.SkipLearningNew;

	public bool SkipEvolve => MainViewModel.Instance.Home.SkipEvolve;

	public BotMode BotMode => MainViewModel.Instance.Home.BotMode;

	public string SellPrice => MainViewModel.Instance.Home.SellPrice;

	public string SelectedAutoWalkFishRoute => MainViewModel.Instance.Home.SelectedAutoWalkFishRoute ?? "";

	public string SelectedAutoSweetScentRoute => MainViewModel.Instance.Home.SelectedAutoSweetScentRoute ?? "";

	public string SelectedSafariAutoWalkRoute => MainViewModel.Instance.Home.SelectedSafariAutoWalkRoute ?? "";

	public string SelectedSafariAutoFishRoute => MainViewModel.Instance.Home.SelectedSafariAutoFishRoute ?? "";

	public bool AlertPM => MainViewModel.Instance.Security.AlertPM;

	public bool StopPM => MainViewModel.Instance.Security.StopPM;

	public bool AlertWalkCycles => MainViewModel.Instance.Security.AlertWalkCycles;

	public bool StopWalkCycles => MainViewModel.Instance.Security.StopWalkCycles;

	public bool AlertSweetScent => MainViewModel.Instance.Security.AlertSweetScent;

	public bool AutoChannelSwitch => MainViewModel.Instance.Security.AutoChannelSwitch;

	public bool Break => MainViewModel.Instance.Security.Break;

	public bool Humanize => MainViewModel.Instance.Security.Humanize;

	public bool TurnOff => MainViewModel.Instance.Security.TurnOff;

	public int WalkCyclesTrigger => MainViewModel.Instance.Security.WalkCyclesTrigger;

	public int TurnOffTrigger => MainViewModel.Instance.Security.TurnOffTrigger;

	public bool HumanizeMouseMovement => MainViewModel.Instance.Settings.HumanizeMouseMovement;

	public bool PrimaryMouseButton => MainViewModel.Instance.Settings.PrimaryMouseButton;

	public ResolutionMode ResolutionMode => MainViewModel.Instance.Settings.ResolutionMode;

	public ChosenPokeBall ChosenPokeBall => ChosenPokeBallViewModel.Instance.ChosenPokeBall;

	public CatchMovesRoutine CatchMovesRoutine => MainViewModel.Instance.Premium.CatchMovesRoutine;

	public int OrangePotionSelectedIndex => MainViewModel.Instance.Premium.OrangePotionSelectedIndex;

	public int RedPotionSelectedIndex => MainViewModel.Instance.Premium.RedPotionSelectedIndex;

	private bool GetPremiumBool(Func<bool> getter)
	{
		return MainViewModel.Instance.Premium.PremiumEnabled && getter();
	}
}
