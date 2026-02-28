using System.Windows.Media;
using PokeMMO_.Mvvm;

namespace PokeMMO_.Model;

public class Security : BindableBase
{
	private int _WalkCyclesTrigger = 10;

	private int _TurnOffTrigger = 60;

	private int _WalkSpeedFrom = 700;

	private int _WalkSpeedTo = 1300;

	private int _ChannelSwitchFrom = 30;

	private int _ChannelSwitchTo = 60;

	private int _BreakFrom = 30;

	private int _BreakTo = 60;

	private int _BreakLengthFrom = 30;

	private int _BreakLengthTo = 60;

	private bool _Break = false;

	private bool _Humanize = false;

	private bool _AutoChannelSwitch = false;

	private bool _TurnOff = false;

	private bool _AlertPM = true;

	private bool _StopPM = true;

	private bool _AlertWalkCycles = true;

	private bool _StopWalkCycles = true;

	private bool _AlertSweetScent = true;

	private string _AutomaticCaptchaSolverText = "Automatic Captcha Solver: OFF";

	private Brush _AutomaticCaptchaSolverColor = Brushes.Red;

	public int WalkCyclesTrigger
	{
		get
		{
			return _WalkCyclesTrigger;
		}
		set
		{
			SetProperty(ref _WalkCyclesTrigger, value, "WalkCyclesTrigger");
		}
	}

	public int TurnOffTrigger
	{
		get
		{
			return _TurnOffTrigger;
		}
		set
		{
			SetProperty(ref _TurnOffTrigger, value, "TurnOffTrigger");
		}
	}

	public int WalkSpeedFrom
	{
		get
		{
			return _WalkSpeedFrom;
		}
		set
		{
			SetProperty(ref _WalkSpeedFrom, value, "WalkSpeedFrom");
		}
	}

	public int WalkSpeedTo
	{
		get
		{
			return _WalkSpeedTo;
		}
		set
		{
			SetProperty(ref _WalkSpeedTo, value, "WalkSpeedTo");
		}
	}

	public int ChannelSwitchFrom
	{
		get
		{
			return _ChannelSwitchFrom;
		}
		set
		{
			SetProperty(ref _ChannelSwitchFrom, value, "ChannelSwitchFrom");
		}
	}

	public int ChannelSwitchTo
	{
		get
		{
			return _ChannelSwitchTo;
		}
		set
		{
			SetProperty(ref _ChannelSwitchTo, value, "ChannelSwitchTo");
		}
	}

	public int BreakFrom
	{
		get
		{
			return _BreakFrom;
		}
		set
		{
			SetProperty(ref _BreakFrom, value, "BreakFrom");
		}
	}

	public int BreakTo
	{
		get
		{
			return _BreakTo;
		}
		set
		{
			SetProperty(ref _BreakTo, value, "BreakTo");
		}
	}

	public int BreakLengthFrom
	{
		get
		{
			return _BreakLengthFrom;
		}
		set
		{
			SetProperty(ref _BreakLengthFrom, value, "BreakLengthFrom");
		}
	}

	public int BreakLengthTo
	{
		get
		{
			return _BreakLengthTo;
		}
		set
		{
			SetProperty(ref _BreakLengthTo, value, "BreakLengthTo");
		}
	}

	public bool Break
	{
		get
		{
			return _Break;
		}
		set
		{
			SetProperty(ref _Break, value, "Break");
		}
	}

	public bool Humanize
	{
		get
		{
			return _Humanize;
		}
		set
		{
			SetProperty(ref _Humanize, value, "Humanize");
		}
	}

	public bool AutoChannelSwitch
	{
		get
		{
			return _AutoChannelSwitch;
		}
		set
		{
			SetProperty(ref _AutoChannelSwitch, value, "AutoChannelSwitch");
		}
	}

	public bool TurnOff
	{
		get
		{
			return _TurnOff;
		}
		set
		{
			SetProperty(ref _TurnOff, value, "TurnOff");
		}
	}

	public bool AlertPM
	{
		get
		{
			return _AlertPM;
		}
		set
		{
			SetProperty(ref _AlertPM, value, "AlertPM");
		}
	}

	public bool StopPM
	{
		get
		{
			return _StopPM;
		}
		set
		{
			SetProperty(ref _StopPM, value, "StopPM");
		}
	}

	public bool AlertWalkCycles
	{
		get
		{
			return _AlertWalkCycles;
		}
		set
		{
			SetProperty(ref _AlertWalkCycles, value, "AlertWalkCycles");
		}
	}

	public bool StopWalkCycles
	{
		get
		{
			return _StopWalkCycles;
		}
		set
		{
			SetProperty(ref _StopWalkCycles, value, "StopWalkCycles");
		}
	}

	public bool AlertSweetScent
	{
		get
		{
			return _AlertSweetScent;
		}
		set
		{
			SetProperty(ref _AlertSweetScent, value, "AlertSweetScent");
		}
	}

	public string AutomaticCaptchaSolverText
	{
		get
		{
			return _AutomaticCaptchaSolverText;
		}
		set
		{
			SetProperty(ref _AutomaticCaptchaSolverText, value, "AutomaticCaptchaSolverText");
		}
	}

	public Brush AutomaticCaptchaSolverColor
	{
		get
		{
			return _AutomaticCaptchaSolverColor;
		}
		set
		{
			SetProperty(ref _AutomaticCaptchaSolverColor, value, "AutomaticCaptchaSolverColor");
		}
	}
}
