using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using KeyAuth;
using MahApps.Metro.Controls;
using NHotkey;
using NHotkey.Wpf;
using PokeMMO_.Botting;
using PokeMMO_.Classes;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;

namespace PokeMMO_;

public class MainWindow : MetroWindow, IComponentConnector
{
	private AuthWindow _AuthWindow = new AuthWindow();

	private SubWindow _SubWindow = new SubWindow();

	private DiscordWindow _DiscordWindow = new DiscordWindow();

	private ChosenPokeBallWindow _ChosenPokeBallWindow = new ChosenPokeBallWindow();

	public static bool DevelopmentMode = false;

	public static string Version = "1.896";

	public static api KeyAuthApp = new api("PokeMMO", "F3ZHEjs317", Version);

	internal MainWindow MyMainWindow;

	internal HamburgerMenu HamburgerMenu;

	private bool _contentLoaded;

	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern ushort GlobalAddAtom(string lpString);

	public MainWindow()
	{
		KeyAuthApp.init();
		GlobalAddAtom(KeyAuthApp.seed);
		GlobalAddAtom(KeyAuthApp.ownerid);
		KeyAuthApp.log(Environment.UserName + " launched the bot!");
		if (!KeyAuthApp.response.success)
		{
			TopMostMessageBox.Show(KeyAuthApp.response.message);
			Environment.Exit(0);
		}
		if (KeyAuthApp.response.message == "invalidver")
		{
			KeyAuthApp.log(Environment.UserName + " Update " + KeyAuthApp.app_data.version + " available, redirecting to update!");
			TopMostMessageBox.Show("Update " + KeyAuthApp.app_data.version + " available, redirecting to update!", this.method_0(), MessageBoxButton.OK, MessageBoxImage.Hand);
			Configuration.ResetExeNameAndSetDeleteName();
			Process.Start("PokeMMO+_Loader.exe");
			Process.GetCurrentProcess().Kill();
		}
		KeyAuthApp.check();
		InitializeComponent();
		ToolTipService.ShowOnDisabledProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(true));
		ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(int.MaxValue));
		((Window)(object)MyMainWindow).Title = RandomTitle.Generate();
		HamburgerMenu.set_SelectedIndex(0);
		((FrameworkElement)(object)MyMainWindow).DataContext = MainViewModel.Instance;
		switch ((int)SystemParameters.PrimaryScreenWidth)
		{
		case 1920:
			MainViewModel.Instance.Settings.ResolutionMode = ResolutionMode.HD;
			break;
		case 1280:
			MainViewModel.Instance.Settings.ResolutionMode = ResolutionMode.SD;
			break;
		default:
			TopMostMessageBox.Show("To work properly you need to choose one of the Supported Resolutions.\n\n1920x1080 or 1280x720", "Unsupported Resolution", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
			break;
		}
		Configuration.Load();
		PathAndFileManager.ReplacePropertiesAndGFXFile(messagebox: false);
		IniFile iniFile = new IniFile("Settings.ini");
		((TextBox)(object)_AuthWindow.txt_login_username).Text = iniFile.Read("PremiumUsername");
		_AuthWindow.txt_login_password.set_Password(Includes.Base64Decode(iniFile.Read("PremiumPassword")));
		try
		{
			HotkeyManager.Current.AddOrReplace("Start", Key.F9, ModifierKeys.None, Start);
			HotkeyManager.Current.AddOrReplace("Stop", Key.F10, ModifierKeys.None, Stop);
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log("Hotkey registration error: " + ex.Message);
			((HotkeyManagerBase)HotkeyManager.Current).Remove("Start");
			((HotkeyManagerBase)HotkeyManager.Current).Remove("Stop");
			TopMostMessageBox.Show("Hotkeys F9 [Start] & F10 [Stop] are already registered & can't be used", "Hotkeys Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
		}
		if (AuthViewModel.Instance.AutoLogin && ((TextBox)(object)_AuthWindow.txt_login_username).Text != "" && _AuthWindow.txt_login_password.get_Password() != "")
		{
			_AuthWindow.Login();
		}
	}

	private void Start(object sender, HotkeyEventArgs e)
	{
		if (MainViewModel.Instance.Home.StartEnabled)
		{
			Bot.Instance.Start();
		}
	}

	private void Stop(object sender, HotkeyEventArgs e)
	{
		if (MainViewModel.Instance.Home.StopEnabled)
		{
			Bot.Instance.Stop();
		}
	}

	private void CreditsEvent(object sender, RoutedEventArgs e)
	{
		Process process = new Process();
		process.StartInfo.UseShellExecute = true;
		process.StartInfo.FileName = "https://pokeplus.live/";
		process.Start();
	}

	public static void ErrorCheck()
	{
		if (!KeyAuthApp.response.success)
		{
			TopMostMessageBox.Show(KeyAuthApp.response.message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
		}
	}

	private void UnlockEvent(object sender, RoutedEventArgs e)
	{
		_AuthWindow.Show();
	}

	private void HamburgerMenu_ItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
	{
		((ContentControl)(object)HamburgerMenu).Content = e.get_InvokedItem();
		HamburgerMenu.set_IsPaneOpen(false);
	}

	private void Window_Closing(object sender, CancelEventArgs e)
	{
		Environment.Exit(0);
	}

	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/PokeMMO+;component/mainwindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		switch (connectionId)
		{
		default:
			_contentLoaded = true;
			break;
		case 1:
			MyMainWindow = (MainWindow)target;
			((Window)(object)MyMainWindow).Closing += Window_Closing;
			break;
		case 2:
			((Button)target).Click += CreditsEvent;
			break;
		case 3:
			((Button)target).Click += UnlockEvent;
			break;
		case 4:
			HamburgerMenu = (HamburgerMenu)target;
			HamburgerMenu.add_ItemInvoked(new HamburgerMenuItemInvokedRoutedEventHandler(HamburgerMenu_ItemInvoked));
			break;
		}
	}

	string method_0()
	{
		return ((FrameworkElement)this).Name;
	}
}
