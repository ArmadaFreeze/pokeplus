using System;
using System.Windows;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Botting;

public class Status
{
	private bool _IsInFight;

	private int _EncountersCounter;

	private int _SelectedCatchPokemonCounter;

	private int _ShinyCounter;

	private int _ThrownBallsCounter;

	private int _ItemCounter;

	private int _SelectedPokemon = 1;

	private int _WalkCycle;

	public bool ShinyHelper { get; set; }

	public bool IsInFight
	{
		get
		{
			return _IsInFight;
		}
		set
		{
			if (!_IsInFight && value)
			{
				SelectedCatchPokemonCounterHelper = true;
			}
			_IsInFight = value;
		}
	}

	public bool HumanizeHelper { get; set; }

	public bool ThiefHelper { get; set; }

	public bool UsedPotion { get; set; }

	public bool FirstAutoSSCycle { get; set; } = true;


	public bool ImprisonHelper { get; set; }

	public bool Changed { get; set; }

	public string SolvedCaptchaText { get; set; } = "";


	public string PotionStatus { get; set; } = "";


	public DateTime Timer { get; set; } = DateTimeOffset.Now.DateTime;


	public int ChannelSwitchTimer { get; set; }

	public int BreakTimer { get; set; }

	public bool Breaking { get; set; }

	public bool MoveDisabled { get; set; }

	public int ChannelSwitchTrigger { get; set; }

	public int BreakTrigger { get; set; }

	public int EncountersCounter
	{
		get
		{
			return _EncountersCounter;
		}
		set
		{
			_EncountersCounter = value;
			UpdateUI(delegate(SubViewModel vm)
			{
				vm.EncountersCounter = BuildEncountersString();
			});
		}
	}

	public bool SelectedCatchPokemonCounterHelper { get; set; }

	public int SelectedCatchPokemonCounter
	{
		get
		{
			return _SelectedCatchPokemonCounter;
		}
		set
		{
			_SelectedCatchPokemonCounter = value;
			UpdateUI(delegate(SubViewModel vm)
			{
				vm.EncountersCounter = BuildEncountersString();
			});
		}
	}

	public int ShinyCounter
	{
		get
		{
			return _ShinyCounter;
		}
		set
		{
			_ShinyCounter = value;
			UpdateUI(delegate(SubViewModel vm)
			{
				vm.ShinyCounter = $"Shinies: {value}";
			});
		}
	}

	public int ThrownBallsCounter
	{
		get
		{
			return _ThrownBallsCounter;
		}
		set
		{
			_ThrownBallsCounter = value;
			UpdateUI(delegate(SubViewModel vm)
			{
				vm.ThrownBallsCounter = $"Thrown Balls: {value}";
			});
		}
	}

	public int ItemCounter
	{
		get
		{
			return _ItemCounter;
		}
		set
		{
			_ItemCounter = value;
			UpdateUI(delegate(SubViewModel vm)
			{
				vm.ItemCounter = $"Items: {value}";
			});
		}
	}

	public int SelectedPokemon
	{
		get
		{
			return _SelectedPokemon;
		}
		set
		{
			_SelectedPokemon = value;
			UpdateUI(delegate(SubViewModel vm)
			{
				vm.Poke = $"Pokemon #{value}";
			});
		}
	}

	public int SelectedPokemonManual { get; set; } = 1;


	public int AttackMove { get; set; } = 1;


	public bool UsedFalseSwipe { get; set; }

	public bool EncounteredSelectedPokemon { get; set; }

	public bool UsedSubstitute { get; set; }

	public bool UsedPayDay { get; set; }

	public bool UsedRock { get; set; }

	public bool UsedBait { get; set; }

	public bool FirstMovePP0 { get; set; }

	public bool SecondMovePP0 { get; set; }

	public bool ThirdMovePP0 { get; set; }

	public bool FourthMovePP0 { get; set; }

	public bool DetectedItem { get; set; }

	public bool FirstMainPokemonItem { get; set; }

	public bool FirstPokemonItem { get; set; }

	public bool SecondPokemonItem { get; set; }

	public bool ThirdPokemonItem { get; set; }

	public bool FourthPokemonItem { get; set; }

	public bool FifthPokemonItem { get; set; }

	public int LastAttackMove { get; set; }

	public int AFKCounter { get; set; }

	public int WalkCycle
	{
		get
		{
			return _WalkCycle;
		}
		set
		{
			_WalkCycle = value;
			UpdateUI(delegate(SubViewModel vm)
			{
				vm.WalkCycle = $"WalkCycle: {value}";
			});
		}
	}

	public string LastWalkDirection { get; set; } = "";


	public bool GoTo { get; set; } = true;


	public bool GoBack { get; set; }

	public int GoBackOnce { get; set; }

	public bool Heal { get; set; }

	public string UserStatus => MainViewModel.Instance.Auth.Status.Replace("Status: ", "") + " USER";

	private void UpdateUI(Action<SubViewModel> update)
	{
		Application.Current.Dispatcher.Invoke(delegate
		{
			update(SubViewModel.Instance);
		});
	}

	private string BuildEncountersString()
	{
		return $"Encounters: {_EncountersCounter} - {MainViewModel.Instance.Home.CatchPokemon}'s {_SelectedCatchPokemonCounter}";
	}
}
