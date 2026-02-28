using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using PokeMMO_.Classes;
using PokeMMO_.Model;
using PokeMMO_.Proccessing;
using PokeMMO_.ViewModels;
using WindowsInput;

namespace PokeMMO_.Botting;

public class Bot
{
	private static readonly Lazy<Bot> _instance = new Lazy<Bot>(() => new Bot());

	private BotSettings _Settings = BotSettings.Settings;

	private Actions _Actions = new Actions();

	private Status _Status = new Status();

	private Routes _Routes = new Routes();

	private Check _Check = new Check();

	private Behavior _Behavior = new Behavior();

	private Battle _Battle = new Battle();

	private State _State = new State();

	private Handle _GameProcessHandle = new Handle();

	public InputSimulator Sim = new InputSimulator();

	private Timer _Timer = null;

	private Timer _TimeTimer = null;

	public bool RequestStop = false;

	public static Bot Instance => _instance.Value;

	public BotSettings Settings => _Settings;

	public Actions Actions => _Actions;

	public Status Status => _Status;

	public Routes Routes => _Routes;

	public Check Check => _Check;

	public Behavior Behavior => _Behavior;

	public Battle Battle => _Battle;

	public State State => _State;

	public Handle GameProcessHandle => _GameProcessHandle;

	private IntPtr _Handle => _GameProcessHandle.GameHandle;

	public IntPtr Handle => _Handle;

	private Process _Process => _GameProcessHandle.GameProcess;

	public Process Process => _Process;

	private bool IsSpecialMode => _Settings.BotMode == BotMode.SellBox || _Settings.BotMode == BotMode.MailClaim || _Settings.BotMode == BotMode.GTLSniper;

	private void ResetStatus()
	{
		_Status.FirstAutoSSCycle = true;
		_Status.GoTo = true;
		_Status.GoBack = false;
		_Status.Heal = false;
		_Status.GoBackOnce = 0;
		_Status.ChannelSwitchTimer = 0;
		_Status.BreakTimer = 0;
	}

	public void Start()
	{
		MainViewModel.Instance.Home.StartEnabled = false;
		MainViewModel.Instance.Home.StopEnabled = true;
		Application.Current.Dispatcher.Invoke(delegate
		{
			MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault();
			((Window)(object)mainWindow).Hide();
			SubWindow subWindow = Application.Current.Windows.OfType<SubWindow>().SingleOrDefault();
			subWindow.Show();
		});
		string defaultPath = MainViewModel.Instance.Settings.DefaultPath;
		string path = defaultPath + "\\config\\main.properties";
		if (string.IsNullOrEmpty(defaultPath) || !File.Exists(path))
		{
			TopMostMessageBox.Show("PokeMMO path is not set or invalid.\nPlease go to Settings and select your PokeMMO installation folder.", "Invalid Path", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
			Stop();
			return;
		}
		PathAndFileManager.ReadPropertiesFile();
		RequestStop = false;
		_Status.Timer = DateTimeOffset.Now.DateTime;
		_TimeTimer = new Timer(_TimeTimer_Tick, null, 1000, -1);
		_Timer = new Timer(_Timer_Tick, null, 100, -1);
		_Status.ChannelSwitchTrigger = RandomNumber.Between(MainViewModel.Instance.Security.ChannelSwitchFrom, MainViewModel.Instance.Security.ChannelSwitchTo);
		_Status.BreakTrigger = RandomNumber.Between(MainViewModel.Instance.Security.BreakFrom, MainViewModel.Instance.Security.BreakTo);
		Includes.WindowHelper.BringProcessToFront();
		DiscordBot.Instance.SendMessage("Start", embed: false);
	}

	public void Stop()
	{
		MainViewModel.Instance.Home.StartEnabled = true;
		MainViewModel.Instance.Home.StopEnabled = false;
		RequestStop = true;
		_Timer?.Change(-1, -1);
		_TimeTimer?.Change(-1, -1);
		_Actions.ResetAndUpdateWalkCycle();
		UIHelper.SetStatus("Status: Stopped");
		ResetStatus();
		DiscordBot.Instance.SendMessage("Stop", embed: false);
	}

	public void Sleep(int milliseconds)
	{
		Thread.Sleep(milliseconds);
	}

	public async Task AsyncSleep(int milliseconds)
	{
		await Task.Delay(milliseconds);
	}

	private void _TimeTimer_Tick(object state)
	{
		try
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				SubViewModel.Instance.Timer = $"Time: {DateTimeOffset.Now.DateTime - _Status.Timer:hh\\:mm\\:ss}";
			});
			if (!_Status.Breaking)
			{
				_Status.ChannelSwitchTimer++;
				_Status.BreakTimer++;
			}
			else
			{
				_Status.ChannelSwitchTimer = 0;
				_Status.BreakTimer = 0;
			}
			if (_Settings.TurnOff && (DateTimeOffset.Now.DateTime - _Status.Timer).TotalMinutes >= (double)_Settings.TurnOffTrigger)
			{
				Stop();
				try
				{
					_Process?.Kill();
				}
				catch
				{
				}
				Environment.Exit(0);
			}
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log("TimeTimer error: " + ex.Message);
		}
		if (!RequestStop)
		{
			_TimeTimer.Change(1000, -1);
		}
	}

	private bool TryTimerAction(bool enabled, int timer, int trigger, Action action)
	{
		if (!enabled || timer / 60 < trigger || !_Check.Walk)
		{
			return false;
		}
		if ((_Settings.AutoSweetScent || _Settings.AutoWalkFish) && (_Status.Heal || _Status.GoBack || _Status.GoTo))
		{
			return false;
		}
		Sleep(3000);
		if (_Check.Walk)
		{
			action();
		}
		return true;
	}

	private Action GetWalkAction()
	{
		if (_Settings.BotMode == BotMode.SellBox)
		{
			return delegate
			{
				_Actions.SellBox();
			};
		}
		if (_Settings.BotMode == BotMode.MailClaim)
		{
			return delegate
			{
				_Actions.MailClaim();
			};
		}
		if (_Settings.BotMode != BotMode.GTLSniper)
		{
			if (_Settings.Walk)
			{
				return delegate
				{
					_Behavior.Walk();
				};
			}
			if (_Settings.Fish)
			{
				return delegate
				{
					_Behavior.Fish();
				};
			}
			if (_Settings.SweetScent)
			{
				return delegate
				{
					_Behavior.SweetScent();
				};
			}
			if (_Settings.AutoWalkFish && _Settings.BotMode != BotMode.Safari)
			{
				return delegate
				{
					_Behavior.AutoWalkFish();
				};
			}
			if (!_Settings.AutoSweetScent || _Settings.BotMode == BotMode.Safari)
			{
				if (_Settings.SafariAutoWalk && _Settings.BotMode == BotMode.Safari)
				{
					return delegate
					{
						_Behavior.SafariAutoWalk();
					};
				}
				if (_Settings.SafariAutoFish && _Settings.BotMode == BotMode.Safari)
				{
					return delegate
					{
						_Behavior.SafariAutoFish();
					};
				}
				return null;
			}
			return delegate
			{
				_Behavior.AutoSweetScent();
			};
		}
		return delegate
		{
			_Actions.GTLSniper();
		};
	}

	private void _Timer_Tick(object state)
	{
		try
		{
			if (!TryTimerAction(_Settings.Break, _Status.BreakTimer, _Status.BreakTrigger, delegate
			{
				_Actions.LogoutAndBreak();
			}))
			{
				TryTimerAction(_Settings.AutoChannelSwitch, _Status.ChannelSwitchTimer, _Status.ChannelSwitchTrigger, delegate
				{
					_Actions.Logout();
				});
			}
			if (IsSpecialMode)
			{
				GetWalkAction()?.Invoke();
			}
			else
			{
				switch (_Check.DetectGameState())
				{
				case GameState.Walking:
					_State.InMainWindow();
					GetWalkAction()?.Invoke();
					break;
				case GameState.InBattle:
					_State.InBattleWindow(_Handle);
					break;
				case GameState.LoginScreen:
					if (_Settings.Login)
					{
						_State.Login();
					}
					break;
				}
			}
			if (IsSpecialMode && _Settings.Login && _Check.Login)
			{
				_State.Login();
			}
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log("Timer error: " + ex.Message);
		}
		if (!RequestStop)
		{
			_Timer.Change(100, -1);
		}
	}
}
