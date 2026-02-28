using System.Linq;
using System.Windows;
using PokeMMO_.Mvvm;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Model;

public class Premium : BindableBase
{
	private int _OrangePotionSelectedIndex = -1;

	private int _RedPotionSelectedIndex = -1;

	private bool _PremiumEnabled = MainWindow.DevelopmentMode;

	private bool _PotionSystem = false;

	private bool _MultiTarget = false;

	private bool _TeleportBack = false;

	private bool _Substitute = false;

	private bool _FalseSwipe = false;

	private bool _Spore = false;

	private bool _Assist = false;

	private bool _SlowMode = false;

	private bool _EscapeRope = false;

	private string _DiscordUsername = "";

	private CatchMovesRoutine _CatchMovesRoutine = CatchMovesRoutine.SFS;

	public DelegateCommand DiscordWindowCommand { get; }

	public int OrangePotionSelectedIndex
	{
		get
		{
			return _OrangePotionSelectedIndex;
		}
		set
		{
			SetProperty(ref _OrangePotionSelectedIndex, value, "OrangePotionSelectedIndex");
		}
	}

	public int RedPotionSelectedIndex
	{
		get
		{
			return _RedPotionSelectedIndex;
		}
		set
		{
			SetProperty(ref _RedPotionSelectedIndex, value, "RedPotionSelectedIndex");
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

	public bool PotionSystem
	{
		get
		{
			return _PotionSystem;
		}
		set
		{
			SetProperty(ref _PotionSystem, value, "PotionSystem");
		}
	}

	public bool MultiTarget
	{
		get
		{
			return _MultiTarget;
		}
		set
		{
			SetProperty(ref _MultiTarget, value, "MultiTarget");
		}
	}

	public bool TeleportBack
	{
		get
		{
			return _TeleportBack;
		}
		set
		{
			SetProperty(ref _TeleportBack, value, "TeleportBack");
		}
	}

	public bool Substitute
	{
		get
		{
			return _Substitute;
		}
		set
		{
			SetProperty(ref _Substitute, value, "Substitute");
			MainViewModel.Instance.Home.Options[0].Selected = _Substitute;
		}
	}

	public bool FalseSwipe
	{
		get
		{
			return _FalseSwipe;
		}
		set
		{
			SetProperty(ref _FalseSwipe, value, "FalseSwipe");
			MainViewModel.Instance.Home.Options[1].Selected = _FalseSwipe;
		}
	}

	public bool Spore
	{
		get
		{
			return _Spore;
		}
		set
		{
			SetProperty(ref _Spore, value, "Spore");
			MainViewModel.Instance.Home.Options[2].Selected = _Spore;
		}
	}

	public bool Assist
	{
		get
		{
			return _Assist;
		}
		set
		{
			SetProperty(ref _Assist, value, "Assist");
			MainViewModel.Instance.Home.Options[3].Selected = _Assist;
		}
	}

	public bool SlowMode
	{
		get
		{
			return _SlowMode;
		}
		set
		{
			SetProperty(ref _SlowMode, value, "SlowMode");
		}
	}

	public bool EscapeRope
	{
		get
		{
			return _EscapeRope;
		}
		set
		{
			SetProperty(ref _EscapeRope, value, "EscapeRope");
		}
	}

	public string DiscordUsername
	{
		get
		{
			return _DiscordUsername;
		}
		set
		{
			SetProperty(ref _DiscordUsername, value, "DiscordUsername");
		}
	}

	public CatchMovesRoutine CatchMovesRoutine
	{
		get
		{
			return _CatchMovesRoutine;
		}
		set
		{
			SetProperty(ref _CatchMovesRoutine, value, "CatchMovesRoutine");
		}
	}

	public Premium()
	{
		DiscordWindowCommand = new DelegateCommand(delegate
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				Application.Current.Windows.OfType<DiscordWindow>().SingleOrDefault()?.Show();
			});
		});
	}
}
