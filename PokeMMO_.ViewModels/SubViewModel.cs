using System;
using PokeMMO_.Botting;
using PokeMMO_.Mvvm;

namespace PokeMMO_.ViewModels;

public class SubViewModel : BindableBase
{
	private static readonly Lazy<SubViewModel> _instance = new Lazy<SubViewModel>(() => new SubViewModel());

	private string _Timer = "Time: " + (DateTimeOffset.Now.DateTime - Bot.Instance.Status.Timer).ToString("hh\\:mm\\:ss");

	private string _Poke = "Pokemon #" + Bot.Instance.Status.SelectedPokemon;

	private string _ShinyCounter = "Shinies: " + Bot.Instance.Status.ShinyCounter;

	private string _EncountersCounter = "Encounters: " + Bot.Instance.Status.EncountersCounter + " - " + MainViewModel.Instance.Home.CatchPokemon + "'s " + Bot.Instance.Status.SelectedCatchPokemonCounter;

	private string _ThrownBallsCounter = "Thrown Balls: " + Bot.Instance.Status.ThrownBallsCounter;

	private string _Status = "Status: ...";

	private const int MaxStatusLines = 200;

	private string _StatusMessages = "[" + (DateTimeOffset.Now.DateTime - Bot.Instance.Status.Timer).ToString("hh\\:mm\\:ss") + "] ...";

	private string _WalkCycle = "WalkCycle: " + Bot.Instance.Status.WalkCycle;

	private string _ItemCounter = "Items: " + Bot.Instance.Status.ItemCounter;

	public static SubViewModel Instance => _instance.Value;

	public string Timer
	{
		get
		{
			return _Timer;
		}
		set
		{
			SetProperty(ref _Timer, value, "Timer");
		}
	}

	public string Poke
	{
		get
		{
			return _Poke;
		}
		set
		{
			SetProperty(ref _Poke, value, "Poke");
		}
	}

	public string ShinyCounter
	{
		get
		{
			return _ShinyCounter;
		}
		set
		{
			SetProperty(ref _ShinyCounter, value, "ShinyCounter");
		}
	}

	public string EncountersCounter
	{
		get
		{
			return _EncountersCounter;
		}
		set
		{
			SetProperty(ref _EncountersCounter, value, "EncountersCounter");
		}
	}

	public string ThrownBallsCounter
	{
		get
		{
			return _ThrownBallsCounter;
		}
		set
		{
			SetProperty(ref _ThrownBallsCounter, value, "ThrownBallsCounter");
		}
	}

	public string Status
	{
		get
		{
			return _Status;
		}
		set
		{
			SetProperty(ref _Status, value, "Status");
			StatusMessages = value.Replace("Status: ", "").Trim();
		}
	}

	public string StatusMessages
	{
		get
		{
			return _StatusMessages;
		}
		set
		{
			string text = _StatusMessages + Environment.NewLine + "[" + (DateTimeOffset.Now.DateTime - Bot.Instance.Status.Timer).ToString("hh\\:mm\\:ss") + "] " + value;
			string[] array = text.Split(new string[1] { Environment.NewLine }, StringSplitOptions.None);
			if (array.Length > 200)
			{
				text = string.Join(Environment.NewLine, array, array.Length - 200, 200);
			}
			SetProperty(ref _StatusMessages, text, "StatusMessages");
		}
	}

	public string WalkCycle
	{
		get
		{
			return _WalkCycle;
		}
		set
		{
			SetProperty(ref _WalkCycle, value, "WalkCycle");
		}
	}

	public string ItemCounter
	{
		get
		{
			return _ItemCounter;
		}
		set
		{
			SetProperty(ref _ItemCounter, value, "ItemCounter");
		}
	}
}
