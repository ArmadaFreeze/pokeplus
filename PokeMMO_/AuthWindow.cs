// Decompiled with JetBrains decompiler
// Type: PokeMMO_.AuthWindow
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using KeyAuth;
using PokeMMO_.Botting;
using PokeMMO_.Classes;
using PokeMMO_.ViewModels;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

#nullable disable
namespace PokeMMO_;

public class AuthWindow : Window, IComponentConnector
{
  private Timer _UnlockTimer = (Timer) null;
  public Unlocker unlocker = new Unlocker();
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
    this.InitializeComponent();
    this.MyAuthWindow.DataContext = (object) AuthViewModel.Instance;
    this.lbl_x.MouseLeftButtonDown += new MouseButtonEventHandler(this.lbl_x_Click);
    this.btn_login_login.Click += new RoutedEventHandler(this.btn_login_login_Click);
    this.btn_login_register.Click += new RoutedEventHandler(this.btn_login_register_Click);
    this.btn_login_extend.Click += new RoutedEventHandler(this.btn_login_extend_Click);
    this.btn_login_free.Click += new RoutedEventHandler(this.btn_login_free_Click);
    this.btn_register_register.Click += new RoutedEventHandler(this.btn_register_register_Click);
    this.btn_register_backtologin.Click += new RoutedEventHandler(this.btn_register_backtologin_Click);
    this.btn_extend_extend.Click += new RoutedEventHandler(this.btn_extend_extend_Click);
    this.btn_extend_backtologin.Click += new RoutedEventHandler(this.btn_extend_backtologin_Click);
  }

  private void lbl_x_Click(object sender, MouseButtonEventArgs e) => this.Hide();

  private void OnKeyDown(object sender, KeyEventArgs e)
  {
    if (e.Key != Key.Return)
      return;
    this.btn_login_login_Click(sender, (RoutedEventArgs) e);
  }

  public static bool SubExist(string name)
  {
    return MainWindow.KeyAuthApp.user_data.subscriptions.Exists((Predicate<api.Data>) (x => x.subscription == name));
  }

  public DateTime UnixTimeToDateTime(long unixtime)
  {
    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
    try
    {
      dateTime = dateTime.AddSeconds((double) unixtime).ToLocalTime();
    }
    catch
    {
      dateTime = DateTime.MaxValue;
    }
    return dateTime;
  }

  public void Login()
  {
    IniFile iniFile = new IniFile("Settings.ini");
    iniFile.Write("PremiumUsername", ((TextBox) this.txt_login_username).Text);
    iniFile.Write("PremiumPassword", Includes.Base64Encode(this.txt_login_password.Password));
    iniFile.Write("AutoLogin", AuthViewModel.Instance.AutoLogin.ToString());
    MainWindow.KeyAuthApp.login(((TextBox) this.txt_login_username).Text, this.txt_login_password.Password);
    if (!MainWindow.KeyAuthApp.response.success)
      return;
    this.Hide();
    int num = (int) MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
    MainWindow.KeyAuthApp.check();
    MainViewModel.Instance.Auth.Status = "Status: PREMIUM";
    MainViewModel.Instance.Auth.Username = "Username: " + MainWindow.KeyAuthApp.user_data.username;
    MainViewModel.Instance.Auth.HWID = "HWID: " + MainWindow.KeyAuthApp.user_data.hwid;
    MainViewModel.Instance.Auth.IP = "IP: " + MainWindow.KeyAuthApp.user_data.ip;
    MainViewModel.Instance.Auth.Expiry = "Expiry: " + this.UnixTimeToDateTime(long.Parse(MainWindow.KeyAuthApp.user_data.subscriptions[0].expiry)).ToString();
    MainViewModel.Instance.Auth.LastLogin = "Last Login: " + this.UnixTimeToDateTime(long.Parse(MainWindow.KeyAuthApp.user_data.lastlogin)).ToString();
    MainViewModel.Instance.Auth.RegisterDate = "Register Date: " + this.UnixTimeToDateTime(long.Parse(MainWindow.KeyAuthApp.user_data.createdate)).ToString();
    MainViewModel.Instance.Security.AutomaticCaptchaSolverText = "Automatic Captcha Solver: ON";
    MainViewModel.Instance.Security.AutomaticCaptchaSolverColor = (Brush) Brushes.LawnGreen;
    if ((MainWindow.KeyAuthApp.user_data.username == null || MainWindow.KeyAuthApp.user_data.hwid == null ? 0 : (MainWindow.KeyAuthApp.user_data.ip != null ? 1 : 0)) != 0)
    {
      MainViewModel.Instance.Home.StartEnabled = true;
      MainViewModel.Instance.Home.PremiumEnabled = true;
      MainViewModel.Instance.Premium.PremiumEnabled = true;
      MainViewModel.Instance.Settings.PremiumEnabled = true;
      Routes.Instance.FillPremiumRoutes();
      MainWindow.KeyAuthApp.log(Environment.UserName + " unlocked the bot with PREMIUM!");
    }
    try
    {
      DiscordBot.Instance.StartDiscordBot();
    }
    catch (Exception ex)
    {
      if ((ex.Message.Contains("401") ? 1 : (ex.Message.Contains("connection") ? 1 : 0)) != 0)
        Process.GetCurrentProcess().Kill();
      PokeMMOLogger.Instance.Log(ex.Message);
    }
  }

  private void btn_login_login_Click(object sender, RoutedEventArgs e)
  {
    this.Login();
    MainWindow.ErrorCheck();
  }

  private void btn_register_register_Click(object sender, RoutedEventArgs e)
  {
    if (((TextBox) this.txt_register_password).Text != ((TextBox) this.txt_register_password2).Text)
    {
      int num1 = (int) MessageBox.Show("Passwords doesn't match!", "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
    }
    else
    {
      MainWindow.KeyAuthApp.register(((TextBox) this.txt_register_username).Text, this.txt_register_password.Password, ((TextBox) this.txt_register_license).Text, ((TextBox) this.txt_register_email).Text);
      if (MainWindow.KeyAuthApp.response.success)
      {
        int num2 = (int) MessageBox.Show("Register has been successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
      }
      MainWindow.ErrorCheck();
    }
  }

  private void btn_extend_extend_Click(object sender, RoutedEventArgs e)
  {
    MainWindow.KeyAuthApp.upgrade(((TextBox) this.txt_extend_username).Text, ((TextBox) this.txt_extend_license).Text);
    if ((MainWindow.KeyAuthApp.response.success ? 1 : (MainWindow.KeyAuthApp.response.message.Contains("success") ? 1 : 0)) != 0)
    {
      int num = (int) MessageBox.Show("You have successfully extended your subscription!", MainWindow.KeyAuthApp.name, MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
    }
    MainWindow.ErrorCheck();
  }

  private void btn_login_free_Click(object sender, RoutedEventArgs e)
  {
    this.Hide();
    this._UnlockTimer = new Timer(new TimerCallback(this._UnlockTimer_Tick), (object) null, 500, -1);
  }

  private void _UnlockTimer_Tick(object state) => this.unlocker.NewUnlock();

  private void btn_login_register_Click(object sender, RoutedEventArgs e)
  {
    this.lbl_title.Content = (object) "PokeMMO+ | Register";
    this.stackpanel_txt_login.Visibility = Visibility.Hidden;
    this.stackpanel_btn_login.Visibility = Visibility.Hidden;
    this.stackpanel_txt_register.Visibility = Visibility.Visible;
    this.stackpanel_btn_register.Visibility = Visibility.Visible;
  }

  private void btn_register_backtologin_Click(object sender, RoutedEventArgs e)
  {
    this.lbl_title.Content = (object) "PokeMMO+ | Login";
    this.stackpanel_txt_register.Visibility = Visibility.Hidden;
    this.stackpanel_btn_register.Visibility = Visibility.Hidden;
    this.stackpanel_txt_login.Visibility = Visibility.Visible;
    this.stackpanel_btn_login.Visibility = Visibility.Visible;
  }

  private void btn_login_extend_Click(object sender, RoutedEventArgs e)
  {
    this.lbl_title.Content = (object) "PokeMMO+ | Extend";
    this.stackpanel_txt_login.Visibility = Visibility.Hidden;
    this.stackpanel_btn_login.Visibility = Visibility.Hidden;
    this.stackpanel_txt_extend.Visibility = Visibility.Visible;
    this.stackpanel_btn_extend.Visibility = Visibility.Visible;
  }

  private void btn_extend_backtologin_Click(object sender, RoutedEventArgs e)
  {
    this.lbl_title.Content = (object) "PokeMMO+ | Login";
    this.stackpanel_txt_extend.Visibility = Visibility.Hidden;
    this.stackpanel_btn_extend.Visibility = Visibility.Hidden;
    this.stackpanel_txt_login.Visibility = Visibility.Visible;
    this.stackpanel_btn_login.Visibility = Visibility.Visible;
  }

  private void Window_MouseDown(object sender, MouseButtonEventArgs e)
  {
    if (e.LeftButton != MouseButtonState.Pressed)
      return;
    this.DragMove();
  }

  private void Window_Closing(object sender, CancelEventArgs e) => Environment.Exit(0);

  private void btn_shop_Click(object sender, RoutedEventArgs e)
  {
    new Process()
    {
      StartInfo = {
        UseShellExecute = true,
        FileName = "https://forum.pokeplus.live/store/category/3-pokemmo/"
      }
    }.Start();
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/PokeMMO+;component/authwindow.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.MyAuthWindow = (AuthWindow) target;
        this.MyAuthWindow.MouseDown += new MouseButtonEventHandler(this.Window_MouseDown);
        break;
      case 2:
        this.lbl_title = (Label) target;
        break;
      case 3:
        this.lbl_x = (Label) target;
        this.lbl_x.MouseLeftButtonDown += new MouseButtonEventHandler(this.lbl_x_Click);
        break;
      case 4:
        this.stackpanel_txt_login = (StackPanel) target;
        break;
      case 5:
        this.txt_login_username = (WatermarkTextBox) target;
        ((UIElement) this.txt_login_username).KeyDown += new KeyEventHandler(this.OnKeyDown);
        break;
      case 6:
        this.txt_login_password = (WatermarkPasswordBox) target;
        ((UIElement) this.txt_login_password).KeyDown += new KeyEventHandler(this.OnKeyDown);
        break;
      case 7:
        this.chk_auto_login = (CheckBox) target;
        break;
      case 8:
        this.stackpanel_btn_login = (StackPanel) target;
        break;
      case 9:
        this.btn_login_login = (Button) target;
        break;
      case 10:
        this.btn_login_register = (Button) target;
        break;
      case 11:
        this.btn_login_extend = (Button) target;
        break;
      case 12:
        this.btn_login_free = (Button) target;
        break;
      case 13:
        this.stackpanel_txt_register = (StackPanel) target;
        break;
      case 14:
        this.txt_register_username = (WatermarkTextBox) target;
        break;
      case 15:
        this.txt_register_password = (WatermarkPasswordBox) target;
        break;
      case 16 /*0x10*/:
        this.txt_register_password2 = (WatermarkPasswordBox) target;
        break;
      case 17:
        this.txt_register_email = (WatermarkTextBox) target;
        break;
      case 18:
        this.txt_register_license = (WatermarkTextBox) target;
        break;
      case 19:
        this.btn_shop1 = (Button) target;
        this.btn_shop1.Click += new RoutedEventHandler(this.btn_shop_Click);
        break;
      case 20:
        this.stackpanel_btn_register = (StackPanel) target;
        break;
      case 21:
        this.btn_register_register = (Button) target;
        break;
      case 22:
        this.btn_register_backtologin = (Button) target;
        break;
      case 23:
        this.stackpanel_txt_extend = (StackPanel) target;
        break;
      case 24:
        this.txt_extend_username = (WatermarkTextBox) target;
        break;
      case 25:
        this.txt_extend_password = (WatermarkPasswordBox) target;
        break;
      case 26:
        this.txt_extend_license = (WatermarkTextBox) target;
        break;
      case 27:
        this.btn_shop2 = (Button) target;
        this.btn_shop2.Click += new RoutedEventHandler(this.btn_shop_Click);
        break;
      case 28:
        this.stackpanel_btn_extend = (StackPanel) target;
        break;
      case 29:
        this.btn_extend_extend = (Button) target;
        break;
      case 30:
        this.btn_extend_backtologin = (Button) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
