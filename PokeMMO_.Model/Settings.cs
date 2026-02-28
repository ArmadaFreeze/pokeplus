using System;
using System.Threading;
using System.Windows;
using PokeMMO_.Classes;
using PokeMMO_.Mvvm;

namespace PokeMMO_.Model;

public class Settings : BindableBase
{
	private Timer _macActionTimer;

	private bool _PremiumEnabled = MainWindow.DevelopmentMode;

	private bool _HumanizeMouseMovement = true;

	private string _DefaultPath = InstalledApplications.GetApplicationInstallPath("PokeMMO");

	private ResolutionMode _ResolutionMode = ResolutionMode.HD;

	private bool _PrimaryMouseButton = false;

	public DelegateCommand LoadCommand { get; }

	public DelegateCommand SaveCommand { get; }

	public DelegateCommand DefaultPathCommand { get; }

	public DelegateCommand ReplaceGFXCommand { get; }

	public DelegateCommand ResetSettingsCommand { get; }

	public DelegateCommand DeleteSettingsCommand { get; }

	public DelegateCommand SpoofCommand { get; }

	public DelegateCommand ResetCommand { get; }

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

	public bool HumanizeMouseMovement
	{
		get
		{
			return _HumanizeMouseMovement;
		}
		set
		{
			SetProperty(ref _HumanizeMouseMovement, value, "HumanizeMouseMovement");
		}
	}

	public string DefaultPath
	{
		get
		{
			return _DefaultPath;
		}
		set
		{
			SetProperty(ref _DefaultPath, value, "DefaultPath");
		}
	}

	public ResolutionMode ResolutionMode
	{
		get
		{
			return _ResolutionMode;
		}
		set
		{
			SetProperty(ref _ResolutionMode, value, "ResolutionMode");
		}
	}

	public bool PrimaryMouseButton
	{
		get
		{
			return _PrimaryMouseButton;
		}
		set
		{
			SetProperty(ref _PrimaryMouseButton, value, "PrimaryMouseButton");
		}
	}

	public Settings()
	{
		LoadCommand = new DelegateCommand(delegate
		{
			Configuration.Load();
		});
		SaveCommand = new DelegateCommand(delegate
		{
			Configuration.Save();
		});
		DefaultPathCommand = new DelegateCommand(delegate
		{
			PathAndFileManager.SelectDefaultPath();
		});
		ReplaceGFXCommand = new DelegateCommand(delegate
		{
			PathAndFileManager.ReplacePropertiesAndGFXFile(messagebox: true);
		});
		ResetSettingsCommand = new DelegateCommand(delegate
		{
			Configuration.ResetSettings();
		});
		DeleteSettingsCommand = new DelegateCommand(delegate
		{
			Configuration.DeleteSettings();
			Configuration.ResetSettings();
		});
		SpoofCommand = new DelegateCommand(delegate
		{
			((Settings)(object)_macActionTimer)?.method_0();
			_macActionTimer = new Timer(delegate
			{
				MacAction(delegate(MAC_Spoofer s)
				{
					s.Spoof();
				}, "spoofed");
			}, null, 100, -1);
		});
		ResetCommand = new DelegateCommand(delegate
		{
			((Settings)(object)_macActionTimer)?.method_0();
			_macActionTimer = new Timer(delegate
			{
				MacAction(delegate(MAC_Spoofer s)
				{
					s.Reset();
				}, "unspoofed");
			}, null, 100, -1);
		});
	}

	private void MacAction(Action<MAC_Spoofer> action, string resultWord)
	{
		string text = "";
		foreach (string deviceID in MAC_Spoofer.GetDeviceIDs())
		{
			MAC_Spoofer mAC_Spoofer = new MAC_Spoofer(deviceID);
			action(mAC_Spoofer);
			text = text + mAC_Spoofer.DriverDesc + "\n";
		}
		TopMostMessageBox.Show(text + "\nSuccessfully " + resultWord + ".", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
	}

	void method_0()
	{
		((Timer)(object)this).Dispose();
	}
}
