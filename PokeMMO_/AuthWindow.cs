using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using KeyAuth;
using PokeMMO_.Classes;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;
using Xceed.Wpf.Toolkit;

namespace PokeMMO_;

public class AuthWindow : Window, IComponentConnector
{
	private Timer _UnlockTimer = null;

	private readonly Unlocker _unlocker = new Unlocker();

	internal AuthWindow MyAuthWindow;

	internal Label lbl_title;

	internal Label lbl_x;

	internal StackPanel stackpanel_txt_login;

	internal WatermarkTextBox txt_login_username;

	internal WatermarkPasswordBox txt_login_password;

	internal CheckBox chk_auto_login;

	internal StackPanel stackpanel_btn_login;

	internal Button btn_login_login;

	internal Button btn_login_register;

	internal Button btn_login_extend;

	internal Button btn_login_free;

	internal StackPanel stackpanel_txt_register;

	internal WatermarkTextBox txt_register_username;

	internal WatermarkPasswordBox txt_register_password;

	internal WatermarkPasswordBox txt_register_password2;

	internal WatermarkTextBox txt_register_email;

	internal WatermarkTextBox txt_register_license;

	internal Button btn_shop1;

	internal StackPanel stackpanel_btn_register;

	internal Button btn_register_register;

	internal Button btn_register_backtologin;

	internal StackPanel stackpanel_txt_extend;

	internal WatermarkTextBox txt_extend_username;

	internal WatermarkPasswordBox txt_extend_password;

	internal WatermarkTextBox txt_extend_license;

	internal Button btn_shop2;

	internal StackPanel stackpanel_btn_extend;

	internal Button btn_extend_extend;

	internal Button btn_extend_backtologin;

	private bool _contentLoaded;

	public AuthWindow()
	{
		InitializeComponent();
		MyAuthWindow.DataContext = AuthViewModel.Instance;
		lbl_x.MouseLeftButtonDown += lbl_x_Click;
		btn_login_login.Click += btn_login_login_Click;
		btn_login_register.Click += btn_login_register_Click;
		btn_login_extend.Click += btn_login_extend_Click;
		btn_login_free.Click += btn_login_free_Click;
		btn_register_register.Click += btn_register_register_Click;
		btn_register_backtologin.Click += btn_register_backtologin_Click;
		btn_extend_extend.Click += btn_extend_extend_Click;
		btn_extend_backtologin.Click += btn_extend_backtologin_Click;
	}

	private void lbl_x_Click(object sender, MouseButtonEventArgs e)
	{
		Hide();
	}

	private void OnKeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Return)
		{
			btn_login_login_Click(sender, e);
		}
	}

	public DateTime UnixTimeToDateTime(long unixtime)
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
		try
		{
			return dateTime.AddSeconds(unixtime).ToLocalTime();
		}
		catch
		{
			return DateTime.MaxValue;
		}
	}

	public void Login()
	{
		IniFile iniFile = new IniFile("Settings.ini");
		iniFile.Write("PremiumUsername", ((TextBox)(object)txt_login_username).Text);
		iniFile.Write("PremiumPassword", Includes.Base64Encode(txt_login_password.get_Password()));
		iniFile.Write("AutoLogin", AuthViewModel.Instance.AutoLogin.ToString());
		MainWindow.KeyAuthApp.login(((TextBox)(object)txt_login_username).Text, txt_login_password.get_Password());
		if (!MainWindow.KeyAuthApp.response.success)
		{
			return;
		}
		Hide();
		TopMostMessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
		MainWindow.KeyAuthApp.check();
		MainViewModel.Instance.Auth.Status = "Status: PREMIUM";
		MainViewModel.Instance.Auth.Username = "Username: " + MainWindow.KeyAuthApp.user_data.username;
		MainViewModel.Instance.Auth.HWID = "HWID: " + MainWindow.KeyAuthApp.user_data.hwid;
		MainViewModel.Instance.Auth.IP = "IP: " + MainWindow.KeyAuthApp.user_data.ip;
		Auth auth = MainViewModel.Instance.Auth;
		List<api.Data> subscriptions = MainWindow.KeyAuthApp.user_data.subscriptions;
		auth.Expiry = "Expiry: " + ((subscriptions != null && subscriptions.Count > 0) ? UnixTimeToDateTime(long.Parse(MainWindow.KeyAuthApp.user_data.subscriptions[0].expiry)).ToString() : "N/A");
		MainViewModel.Instance.Auth.LastLogin = $"Last Login: {UnixTimeToDateTime(long.Parse(MainWindow.KeyAuthApp.user_data.lastlogin))}";
		MainViewModel.Instance.Auth.RegisterDate = $"Register Date: {UnixTimeToDateTime(long.Parse(MainWindow.KeyAuthApp.user_data.createdate))}";
		MainViewModel.Instance.Security.AutomaticCaptchaSolverText = "Automatic Captcha Solver: ON";
		MainViewModel.Instance.Security.AutomaticCaptchaSolverColor = Brushes.LawnGreen;
		if (MainWindow.KeyAuthApp.user_data.username != null && MainWindow.KeyAuthApp.user_data.hwid != null && MainWindow.KeyAuthApp.user_data.ip != null)
		{
			MainViewModel.Instance.Home.StartEnabled = true;
			MainViewModel.Instance.Home.PremiumEnabled = true;
			MainViewModel.Instance.Premium.PremiumEnabled = true;
			MainViewModel.Instance.Settings.PremiumEnabled = true;
			MainWindow.KeyAuthApp.log(Environment.UserName + " unlocked the bot with PREMIUM!");
		}
		try
		{
			DiscordBot.Instance.StartDiscordBot();
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log("StartDiscordBot error: " + ex.Message);
			if (ex.Message.Contains("401") || ex.Message.Contains("connection"))
			{
				Process.GetCurrentProcess().Kill();
			}
		}
	}

	private void btn_login_login_Click(object sender, RoutedEventArgs e)
	{
		Login();
		MainWindow.ErrorCheck();
	}

	private void btn_register_register_Click(object sender, RoutedEventArgs e)
	{
		if (!(((TextBox)(object)txt_register_password).Text != ((TextBox)(object)txt_register_password2).Text))
		{
			MainWindow.KeyAuthApp.register(((TextBox)(object)txt_register_username).Text, txt_register_password.get_Password(), ((TextBox)(object)txt_register_license).Text, ((TextBox)(object)txt_register_email).Text);
			if (MainWindow.KeyAuthApp.response.success)
			{
				TopMostMessageBox.Show("Register has been successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
			}
			MainWindow.ErrorCheck();
		}
		else
		{
			TopMostMessageBox.Show("Passwords doesn't match!", "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
		}
	}

	private void btn_extend_extend_Click(object sender, RoutedEventArgs e)
	{
		MainWindow.KeyAuthApp.upgrade(((TextBox)(object)txt_extend_username).Text, ((TextBox)(object)txt_extend_license).Text);
		if (MainWindow.KeyAuthApp.response.success || MainWindow.KeyAuthApp.response.message.Contains("success"))
		{
			TopMostMessageBox.Show("You have successfully extended your subscription!", MainWindow.KeyAuthApp.name, MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
		}
		MainWindow.ErrorCheck();
	}

	private void btn_login_free_Click(object sender, RoutedEventArgs e)
	{
		Hide();
		((AuthWindow)(object)_UnlockTimer)?.method_0();
		_UnlockTimer = new Timer(_UnlockTimer_Tick, null, 500, -1);
	}

	private void _UnlockTimer_Tick(object state)
	{
		_unlocker.NewUnlock();
	}

	private void ShowPanel(string title, UIElement hideText, UIElement hideBtn, UIElement showText, UIElement showBtn)
	{
		lbl_title.Content = title;
		hideText.Visibility = Visibility.Hidden;
		hideBtn.Visibility = Visibility.Hidden;
		showText.Visibility = Visibility.Visible;
		showBtn.Visibility = Visibility.Visible;
	}

	private void btn_login_register_Click(object sender, RoutedEventArgs e)
	{
		ShowPanel("PokeMMO+ | Register", stackpanel_txt_login, stackpanel_btn_login, stackpanel_txt_register, stackpanel_btn_register);
	}

	private void btn_register_backtologin_Click(object sender, RoutedEventArgs e)
	{
		ShowPanel("PokeMMO+ | Login", stackpanel_txt_register, stackpanel_btn_register, stackpanel_txt_login, stackpanel_btn_login);
	}

	private void btn_login_extend_Click(object sender, RoutedEventArgs e)
	{
		ShowPanel("PokeMMO+ | Extend", stackpanel_txt_login, stackpanel_btn_login, stackpanel_txt_extend, stackpanel_btn_extend);
	}

	private void btn_extend_backtologin_Click(object sender, RoutedEventArgs e)
	{
		ShowPanel("PokeMMO+ | Login", stackpanel_txt_extend, stackpanel_btn_extend, stackpanel_txt_login, stackpanel_btn_login);
	}

	private void Window_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (e.LeftButton == MouseButtonState.Pressed)
		{
			DragMove();
		}
	}

	private void Window_Closing(object sender, CancelEventArgs e)
	{
		Environment.Exit(0);
	}

	private void btn_shop_Click(object sender, RoutedEventArgs e)
	{
		Process process = new Process();
		process.StartInfo.UseShellExecute = true;
		process.StartInfo.FileName = "https://forum.pokeplus.live/store/category/3-pokemmo/";
		process.Start();
	}

	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/PokeMMO+;component/views/authwindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Expected O, but got Unknown
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Expected O, but got Unknown
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Expected O, but got Unknown
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Expected O, but got Unknown
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Expected O, but got Unknown
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Expected O, but got Unknown
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Expected O, but got Unknown
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Expected O, but got Unknown
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Expected O, but got Unknown
		switch (connectionId)
		{
		default:
			_contentLoaded = true;
			break;
		case 1:
			MyAuthWindow = (AuthWindow)target;
			MyAuthWindow.MouseDown += Window_MouseDown;
			break;
		case 2:
			lbl_title = (Label)target;
			break;
		case 3:
			lbl_x = (Label)target;
			lbl_x.MouseLeftButtonDown += lbl_x_Click;
			break;
		case 4:
			stackpanel_txt_login = (StackPanel)target;
			break;
		case 5:
			txt_login_username = (WatermarkTextBox)target;
			((UIElement)(object)txt_login_username).KeyDown += OnKeyDown;
			break;
		case 6:
			txt_login_password = (WatermarkPasswordBox)target;
			((UIElement)(object)txt_login_password).KeyDown += OnKeyDown;
			break;
		case 7:
			chk_auto_login = (CheckBox)target;
			break;
		case 8:
			stackpanel_btn_login = (StackPanel)target;
			break;
		case 9:
			btn_login_login = (Button)target;
			break;
		case 10:
			btn_login_register = (Button)target;
			break;
		case 11:
			btn_login_extend = (Button)target;
			break;
		case 12:
			btn_login_free = (Button)target;
			break;
		case 13:
			stackpanel_txt_register = (StackPanel)target;
			break;
		case 14:
			txt_register_username = (WatermarkTextBox)target;
			break;
		case 15:
			txt_register_password = (WatermarkPasswordBox)target;
			break;
		case 16:
			txt_register_password2 = (WatermarkPasswordBox)target;
			break;
		case 17:
			txt_register_email = (WatermarkTextBox)target;
			break;
		case 18:
			txt_register_license = (WatermarkTextBox)target;
			break;
		case 19:
			btn_shop1 = (Button)target;
			btn_shop1.Click += btn_shop_Click;
			break;
		case 20:
			stackpanel_btn_register = (StackPanel)target;
			break;
		case 21:
			btn_register_register = (Button)target;
			break;
		case 22:
			btn_register_backtologin = (Button)target;
			break;
		case 23:
			stackpanel_txt_extend = (StackPanel)target;
			break;
		case 24:
			txt_extend_username = (WatermarkTextBox)target;
			break;
		case 25:
			txt_extend_password = (WatermarkPasswordBox)target;
			break;
		case 26:
			txt_extend_license = (WatermarkTextBox)target;
			break;
		case 27:
			btn_shop2 = (Button)target;
			btn_shop2.Click += btn_shop_Click;
			break;
		case 28:
			stackpanel_btn_extend = (StackPanel)target;
			break;
		case 29:
			btn_extend_extend = (Button)target;
			break;
		case 30:
			btn_extend_backtologin = (Button)target;
			break;
		}
	}

	void method_0()
	{
		((Timer)(object)this).Dispose();
	}
}
