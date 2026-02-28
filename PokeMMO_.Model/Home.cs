using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using PokeMMO_.Botting;
using PokeMMO_.Classes;
using PokeMMO_.Mvvm;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Model;

public class Home : BindableBase
{
	private bool _StartEnabled = MainWindow.DevelopmentMode;

	private bool _StopEnabled = false;

	private bool _Walk = true;

	private bool _Fish = false;

	private bool _SweetScent = false;

	private bool _AutoWalkFish = false;

	private bool _AutoSweetScent = false;

	private bool _SafariAutoWalk = false;

	private bool _SafariAutoFish = false;

	private Visibility _SellBoxVisibility = Visibility.Hidden;

	private Visibility _CatchPokemonVisibility = Visibility.Hidden;

	private string _CatchPokemon;

	private bool _WalkDirection = true;

	private bool _LevelFirst = false;

	private bool _CatchWithSecondPokemon = false;

	private bool _LevelFirstEnabled = false;

	private bool _SquaresWalkPattern = false;

	private bool _CatchSpellsOrder = false;

	private bool _RandomWalkPattern = false;

	private bool _Login = true;

	private bool _OnlyKeepIV31 = false;

	private Visibility _FightOptionVisibility = Visibility.Hidden;

	private Visibility _IV31Visibility = Visibility.Hidden;

	private Visibility _CatchSpellsVisbility = Visibility.Hidden;

	private Visibility _PayDayOptionVisibility = Visibility.Hidden;

	private Visibility _ThiefOptionVisibility = Visibility.Hidden;

	private Visibility _SafariOptionVisibility = Visibility.Hidden;

	private Visibility _CaughtVisible = Visibility.Visible;

	private Visibility _AutoWalkFishVisibility = Visibility.Visible;

	private Visibility _AutoSweetScentVisibility = Visibility.Visible;

	private string _SellPrice = "";

	private bool _PayDayMultiTarget = false;

	private bool _CatchShiny = true;

	private bool _StopOnShiny = false;

	private bool _SkipDialog = true;

	private bool _SkipLearningNew = false;

	private bool _SkipEvolve = false;

	private bool _MoreThief = false;

	private bool _MorePayDay = false;

	private bool _Lure = false;

	private bool _Imprison = false;

	private bool _Rock = false;

	private bool _Bait = false;

	private bool _AutoLeppa = false;

	private bool _AutoEther = false;

	private BotMode _BotMode = BotMode.Run;

	private Pokemon _Pokemon = Pokemon.All;

	private int _CatchPokemonSelectedIndex = 0;

	private string _SelectedAutoSweetScentRoute = "";

	private string _SelectedAutoWalkFishRoute = "";

	private string _SelectedSafariAutoWalkRoute = "";

	private string _SelectedSafariAutoFishRoute = "";

	private bool _PremiumEnabled = MainWindow.DevelopmentMode;

	private bool _FreeEnabled = false;

	private string _LatestUploadLink = "";

	public ObservableCollection<ItemCatchSpells> Options { get; set; }

	public DelegateCommand ScreenshotPokemonNameCommand { get; }

	public DelegateCommand ChoosePokeBallCommand { get; }

	public bool StartEnabled
	{
		get
		{
			return _StartEnabled;
		}
		set
		{
			SetProperty(ref _StartEnabled, value, "StartEnabled");
		}
	}

	public bool StopEnabled
	{
		get
		{
			return _StopEnabled;
		}
		set
		{
			SetProperty(ref _StopEnabled, value, "StopEnabled");
		}
	}

	public bool ShowAutoWalkFishRoutes => AutoWalkFish && _BotMode != BotMode.Safari;

	public bool ShowAutoSweetScentRoutes => AutoSweetScent && _BotMode != BotMode.Safari;

	public bool ShowSafariAutoWalkRoutes => SafariAutoWalk && _BotMode == BotMode.Safari;

	public bool ShowSafariAutoFishRoutes => SafariAutoFish && _BotMode == BotMode.Safari;

	public bool Walk
	{
		get
		{
			return _Walk;
		}
		set
		{
			SetProperty(ref _Walk, value, "Walk");
			NotifyRouteVisibility();
		}
	}

	public bool Fish
	{
		get
		{
			return _Fish;
		}
		set
		{
			SetProperty(ref _Fish, value, "Fish");
			NotifyRouteVisibility();
		}
	}

	public bool SweetScent
	{
		get
		{
			return _SweetScent;
		}
		set
		{
			SetProperty(ref _SweetScent, value, "SweetScent");
			NotifyRouteVisibility();
		}
	}

	public bool AutoWalkFish
	{
		get
		{
			return _AutoWalkFish;
		}
		set
		{
			SetProperty(ref _AutoWalkFish, value, "AutoWalkFish");
			NotifyRouteVisibility();
		}
	}

	public bool AutoSweetScent
	{
		get
		{
			return _AutoSweetScent;
		}
		set
		{
			SetProperty(ref _AutoSweetScent, value, "AutoSweetScent");
			NotifyRouteVisibility();
		}
	}

	public bool SafariAutoWalk
	{
		get
		{
			return _SafariAutoWalk;
		}
		set
		{
			SetProperty(ref _SafariAutoWalk, value, "SafariAutoWalk");
			NotifyRouteVisibility();
		}
	}

	public bool SafariAutoFish
	{
		get
		{
			return _SafariAutoFish;
		}
		set
		{
			SetProperty(ref _SafariAutoFish, value, "SafariAutoFish");
			NotifyRouteVisibility();
		}
	}

	public Visibility SellBoxVisibility
	{
		get
		{
			return _SellBoxVisibility;
		}
		set
		{
			SetProperty(ref _SellBoxVisibility, value, "SellBoxVisibility");
		}
	}

	public Visibility CatchPokemonVisibility
	{
		get
		{
			return _CatchPokemonVisibility;
		}
		set
		{
			SetProperty(ref _CatchPokemonVisibility, value, "CatchPokemonVisibility");
		}
	}

	public string CatchPokemon
	{
		get
		{
			return _CatchPokemon;
		}
		set
		{
			SetProperty(ref _CatchPokemon, value, "CatchPokemon");
			try
			{
				Application.Current?.Dispatcher.Invoke(delegate
				{
					SubViewModel.Instance.EncountersCounter = $"Encounters: {Bot.Instance.Status.EncountersCounter} - {value}'s {Bot.Instance.Status.SelectedCatchPokemonCounter}";
				});
			}
			catch
			{
			}
		}
	}

	public bool WalkDirection
	{
		get
		{
			return _WalkDirection;
		}
		set
		{
			SetProperty(ref _WalkDirection, value, "WalkDirection");
		}
	}

	public bool LevelFirst
	{
		get
		{
			return _LevelFirst;
		}
		set
		{
			SetProperty(ref _LevelFirst, value, "LevelFirst");
		}
	}

	public bool CatchWithSecondPokemon
	{
		get
		{
			return _CatchWithSecondPokemon;
		}
		set
		{
			SetProperty(ref _CatchWithSecondPokemon, value, "CatchWithSecondPokemon");
		}
	}

	public bool LevelFirstEnabled
	{
		get
		{
			return _LevelFirstEnabled;
		}
		set
		{
			SetProperty(ref _LevelFirstEnabled, value, "LevelFirstEnabled");
		}
	}

	public bool SquaresWalkPattern
	{
		get
		{
			return _SquaresWalkPattern;
		}
		set
		{
			SetProperty(ref _SquaresWalkPattern, value, "SquaresWalkPattern");
		}
	}

	public bool CatchSpellsOrder
	{
		get
		{
			return _CatchSpellsOrder;
		}
		set
		{
			SetProperty(ref _CatchSpellsOrder, value, "CatchSpellsOrder");
		}
	}

	public bool RandomWalkPattern
	{
		get
		{
			return _RandomWalkPattern;
		}
		set
		{
			SetProperty(ref _RandomWalkPattern, value, "RandomWalkPattern");
		}
	}

	public bool Login
	{
		get
		{
			return _Login;
		}
		set
		{
			SetProperty(ref _Login, value, "Login");
		}
	}

	public bool OnlyKeepIV31
	{
		get
		{
			return _OnlyKeepIV31;
		}
		set
		{
			SetProperty(ref _OnlyKeepIV31, value, "OnlyKeepIV31");
		}
	}

	public Visibility FightOptionVisibility
	{
		get
		{
			return _FightOptionVisibility;
		}
		set
		{
			SetProperty(ref _FightOptionVisibility, value, "FightOptionVisibility");
		}
	}

	public Visibility IV31Visibility
	{
		get
		{
			return _IV31Visibility;
		}
		set
		{
			SetProperty(ref _IV31Visibility, value, "IV31Visibility");
		}
	}

	public Visibility CatchSpellsVisbility
	{
		get
		{
			return _CatchSpellsVisbility;
		}
		set
		{
			SetProperty(ref _CatchSpellsVisbility, value, "CatchSpellsVisbility");
		}
	}

	public Visibility PayDayOptionVisibility
	{
		get
		{
			return _PayDayOptionVisibility;
		}
		set
		{
			SetProperty(ref _PayDayOptionVisibility, value, "PayDayOptionVisibility");
		}
	}

	public Visibility ThiefOptionVisibility
	{
		get
		{
			return _ThiefOptionVisibility;
		}
		set
		{
			SetProperty(ref _ThiefOptionVisibility, value, "ThiefOptionVisibility");
		}
	}

	public Visibility SafariOptionVisibility
	{
		get
		{
			return _SafariOptionVisibility;
		}
		set
		{
			SetProperty(ref _SafariOptionVisibility, value, "SafariOptionVisibility");
		}
	}

	public Visibility CaughtVisible
	{
		get
		{
			return _CaughtVisible;
		}
		set
		{
			SetProperty(ref _CaughtVisible, value, "CaughtVisible");
		}
	}

	public Visibility AutoWalkFishVisibility
	{
		get
		{
			return _AutoWalkFishVisibility;
		}
		set
		{
			SetProperty(ref _AutoWalkFishVisibility, value, "AutoWalkFishVisibility");
		}
	}

	public Visibility AutoSweetScentVisibility
	{
		get
		{
			return _AutoSweetScentVisibility;
		}
		set
		{
			SetProperty(ref _AutoSweetScentVisibility, value, "AutoSweetScentVisibility");
		}
	}

	public string SellPrice
	{
		get
		{
			return _SellPrice;
		}
		set
		{
			SetProperty(ref _SellPrice, value, "SellPrice");
		}
	}

	public bool PayDayMultiTarget
	{
		get
		{
			return _PayDayMultiTarget;
		}
		set
		{
			SetProperty(ref _PayDayMultiTarget, value, "PayDayMultiTarget");
		}
	}

	public bool CatchShiny
	{
		get
		{
			return _CatchShiny;
		}
		set
		{
			SetProperty(ref _CatchShiny, value, "CatchShiny");
		}
	}

	public bool StopOnShiny
	{
		get
		{
			return _StopOnShiny;
		}
		set
		{
			SetProperty(ref _StopOnShiny, value, "StopOnShiny");
		}
	}

	public bool SkipDialog
	{
		get
		{
			return _SkipDialog;
		}
		set
		{
			SetProperty(ref _SkipDialog, value, "SkipDialog");
		}
	}

	public bool SkipLearningNew
	{
		get
		{
			return _SkipLearningNew;
		}
		set
		{
			SetProperty(ref _SkipLearningNew, value, "SkipLearningNew");
		}
	}

	public bool SkipEvolve
	{
		get
		{
			return _SkipEvolve;
		}
		set
		{
			SetProperty(ref _SkipEvolve, value, "SkipEvolve");
		}
	}

	public bool MoreThief
	{
		get
		{
			return _MoreThief;
		}
		set
		{
			SetProperty(ref _MoreThief, value, "MoreThief");
		}
	}

	public bool MorePayDay
	{
		get
		{
			return _MorePayDay;
		}
		set
		{
			SetProperty(ref _MorePayDay, value, "MorePayDay");
		}
	}

	public bool Lure
	{
		get
		{
			return _Lure;
		}
		set
		{
			SetProperty(ref _Lure, value, "Lure");
		}
	}

	public bool Imprison
	{
		get
		{
			return _Imprison;
		}
		set
		{
			SetProperty(ref _Imprison, value, "Imprison");
		}
	}

	public bool Rock
	{
		get
		{
			return _Rock;
		}
		set
		{
			SetProperty(ref _Rock, value, "Rock");
		}
	}

	public bool Bait
	{
		get
		{
			return _Bait;
		}
		set
		{
			SetProperty(ref _Bait, value, "Bait");
		}
	}

	public bool AutoLeppa
	{
		get
		{
			return _AutoLeppa;
		}
		set
		{
			SetProperty(ref _AutoLeppa, value, "AutoLeppa");
		}
	}

	public bool AutoEther
	{
		get
		{
			return _AutoEther;
		}
		set
		{
			SetProperty(ref _AutoEther, value, "AutoEther");
		}
	}

	public DelegateCommand StartCommand { get; }

	public DelegateCommand StopCommand { get; }

	public BotMode BotMode
	{
		get
		{
			return _BotMode;
		}
		set
		{
			if (SetProperty(ref _BotMode, value, "BotMode"))
			{
				ApplyBotModeVisibility(value);
			}
		}
	}

	public Pokemon Pokemon
	{
		get
		{
			return _Pokemon;
		}
		set
		{
			SetProperty(ref _Pokemon, value, "Pokemon");
		}
	}

	public int CatchPokemonSelectedIndex
	{
		get
		{
			return _CatchPokemonSelectedIndex;
		}
		set
		{
			SetProperty(ref _CatchPokemonSelectedIndex, value, "CatchPokemonSelectedIndex");
		}
	}

	public string SelectedAutoSweetScentRoute
	{
		get
		{
			return _SelectedAutoSweetScentRoute;
		}
		set
		{
			SetProperty(ref _SelectedAutoSweetScentRoute, value, "SelectedAutoSweetScentRoute");
		}
	}

	public string SelectedAutoWalkFishRoute
	{
		get
		{
			return _SelectedAutoWalkFishRoute;
		}
		set
		{
			SetProperty(ref _SelectedAutoWalkFishRoute, value, "SelectedAutoWalkFishRoute");
		}
	}

	public string SelectedSafariAutoWalkRoute
	{
		get
		{
			return _SelectedSafariAutoWalkRoute;
		}
		set
		{
			SetProperty(ref _SelectedSafariAutoWalkRoute, value, "SelectedSafariAutoWalkRoute");
		}
	}

	public string SelectedSafariAutoFishRoute
	{
		get
		{
			return _SelectedSafariAutoFishRoute;
		}
		set
		{
			SetProperty(ref _SelectedSafariAutoFishRoute, value, "SelectedSafariAutoFishRoute");
		}
	}

	public bool PremiumEnabled
	{
		get
		{
			return _PremiumEnabled;
		}
		set
		{
			SetProperty(ref _PremiumEnabled, value, "PremiumEnabled");
		}
	}

	public bool FreeEnabled
	{
		get
		{
			return _FreeEnabled;
		}
		set
		{
			SetProperty(ref _FreeEnabled, value, "FreeEnabled");
		}
	}

	public string LatestUploadLink
	{
		get
		{
			return _LatestUploadLink;
		}
		set
		{
			SetProperty(ref _LatestUploadLink, value, "LatestUploadLink");
		}
	}

	public Home()
	{
		ChoosePokeBallCommand = new DelegateCommand(delegate
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				Application.Current.Windows.OfType<ChosenPokeBallWindow>().SingleOrDefault()?.Show();
			});
		});
		ScreenshotPokemonNameCommand = new DelegateCommand(delegate
		{
			ScreenCapture.PokemonName();
		});
		Options = new ObservableCollection<ItemCatchSpells>();
		string[] array = new string[4] { "Substitute", "False Swipe", "Spore", "Assist" };
		foreach (string catchSpells in array)
		{
			Options.Add(new ItemCatchSpells
			{
				CatchSpells = catchSpells,
				Selected = false
			});
		}
		StartCommand = new DelegateCommand(delegate
		{
			Bot.Instance.Start();
		});
		StopCommand = new DelegateCommand(delegate
		{
			Bot.Instance.Stop();
		});
	}

	public void ApplyBotModeVisibility(BotMode mode)
	{
		Visibility visibility = Visibility.Visible;
		Visibility visibility2 = Visibility.Hidden;
		LevelFirstEnabled = false;
		FightOptionVisibility = Visibility.Hidden;
		CatchSpellsVisbility = Visibility.Hidden;
		IV31Visibility = Visibility.Hidden;
		PayDayOptionVisibility = Visibility.Hidden;
		ThiefOptionVisibility = Visibility.Hidden;
		SafariOptionVisibility = Visibility.Hidden;
		SellBoxVisibility = Visibility.Hidden;
		CatchPokemonVisibility = Visibility.Hidden;
		switch (mode)
		{
		case BotMode.Fight:
			LevelFirstEnabled = true;
			FightOptionVisibility = visibility;
			CatchPokemonVisibility = visibility;
			break;
		case BotMode.Catch:
			CatchSpellsVisbility = visibility;
			IV31Visibility = visibility;
			CatchPokemonVisibility = visibility;
			break;
		case BotMode.PayDay:
			PayDayOptionVisibility = visibility;
			break;
		case BotMode.Thief:
			ThiefOptionVisibility = visibility;
			CatchPokemonVisibility = visibility;
			break;
		case BotMode.Pickpocket:
			CatchPokemonVisibility = visibility;
			break;
		case BotMode.Safari:
			IV31Visibility = visibility;
			CatchPokemonVisibility = visibility;
			SafariOptionVisibility = visibility;
			AutoWalkFishVisibility = visibility2;
			AutoSweetScentVisibility = visibility2;
			break;
		case BotMode.SellBox:
			SellBoxVisibility = visibility;
			break;
		}
		if (mode != BotMode.Safari)
		{
			AutoWalkFishVisibility = visibility;
			AutoSweetScentVisibility = visibility;
		}
		NotifyRouteVisibility();
	}

	private void NotifyRouteVisibility()
	{
		OnPropertyChanged("ShowAutoWalkFishRoutes");
		OnPropertyChanged("ShowAutoSweetScentRoutes");
		OnPropertyChanged("ShowSafariAutoWalkRoutes");
		OnPropertyChanged("ShowSafariAutoFishRoutes");
	}
}
