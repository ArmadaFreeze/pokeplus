// Decompiled with JetBrains decompiler
// Type: PokeMMO_.MainWindow
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using KeyAuth;
using MahApps.Metro.Controls;
using NHotkey;
using NHotkey.Wpf;
using PokeMMO_.Botting;
using PokeMMO_.Classes;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

namespace PokeMMO_
{
    public class MainWindow : MetroWindow, IComponentConnector
    {
      private AuthWindow _AuthWindow = new AuthWindow();
      private SubWindow _SubWindow = new SubWindow();
      private DiscordWindow _DiscordWindow = new DiscordWindow();
      private ChosenPokeBallWindow _ChosenPokeBallWindow = new ChosenPokeBallWindow();
      public static bool DevelopmentMode = false;
      public static string Version = "1.892";
      public static api KeyAuthApp = new api("PokeMMO", "F3ZHEjs317", MainWindow.Version);
      internal MainWindow MyMainWindow;
      internal HamburgerMenu HamburgerMenu;
      private bool _contentLoaded;

      public MainWindow()
      {
        MainWindow.KeyAuthApp.init();
        MainWindow.KeyAuthApp.log(Environment.UserName + " launched the bot!");
        if (!MainWindow.KeyAuthApp.response.success)
        {
          int num = (int) MessageBox.Show(MainWindow.KeyAuthApp.response.message);
          Environment.Exit(0);
        }
        if (MainWindow.KeyAuthApp.response.message == "invalidver")
        {
          MainWindow.KeyAuthApp.log($"{Environment.UserName} Update {MainWindow.KeyAuthApp.app_data.version} available, redirecting to update!");
          int num = (int) MessageBox.Show($"Update {MainWindow.KeyAuthApp.app_data.version} available, redirecting to update!", this.method_0(), MessageBoxButton.OK, MessageBoxImage.Hand);
          Configuration.ResetExeNameAndSetDeleteName();
          Process.Start("PokeMMO+_Loader.exe");
          Process.GetCurrentProcess().Kill();
        }
        MainWindow.KeyAuthApp.check();
        this.InitializeComponent();
        ToolTipService.ShowOnDisabledProperty.OverrideMetadata(typeof (DependencyObject), (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
        ToolTipService.ShowDurationProperty.OverrideMetadata(typeof (DependencyObject), (PropertyMetadata) new FrameworkPropertyMetadata((object) int.MaxValue));
        ((Window) this.MyMainWindow).Title = Class2.randomTitle();
        this.HamburgerMenu.SelectedIndex = 0;
        ((FrameworkElement) this.MyMainWindow).DataContext = (object) MainViewModel.Instance;
        switch ((int) SystemParameters.PrimaryScreenWidth)
        {
          case 1280 /*0x0500*/:
            MainViewModel.Instance.Settings.ResolutionMode = ResolutionMode.SD;
            break;
          case 1920:
            MainViewModel.Instance.Settings.ResolutionMode = ResolutionMode.HD;
            break;
          default:
            int num1 = (int) MessageBox.Show("To work properly you need to choose one of the Supported Resolutions.\n\n1920x1080 or 1280x720", "Unsupported Resolution", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            break;
        }
        Configuration.Load();
        PathAndFileManager.ReplacePropertiesAndGFXFile(false);
        IniFile iniFile = new IniFile("Settings.ini");
        ((TextBox) this._AuthWindow.txt_login_username).Text = iniFile.Read("PremiumUsername");
        this._AuthWindow.txt_login_password.Password = Includes.Base64Decode(iniFile.Read("PremiumPassword"));
        try
        {
          HotkeyManager.Current.AddOrReplace("Start", Key.F9, ModifierKeys.None, new EventHandler<HotkeyEventArgs>(this.Start));
          HotkeyManager.Current.AddOrReplace("Stop", Key.F10, ModifierKeys.None, new EventHandler<HotkeyEventArgs>(this.Stop));
        }
        catch (Exception ex)
        {
          HotkeyManager.Current.Remove("Start");
          HotkeyManager.Current.Remove("Stop");
          int num2 = (int) MessageBox.Show("Hotkeys F9 [Start] & F10 [Stop] are already registered & can't be used", "Hotkeys Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }
        if (!AuthViewModel.Instance.AutoLogin || (!(((TextBox) this._AuthWindow.txt_login_username).Text != "") ? 0 : (this._AuthWindow.txt_login_password.Password != "" ? 1 : 0)) == 0)
          return;
        this._AuthWindow.Login();
      }

      private void Start(object sender, HotkeyEventArgs e)
      {
        if (!MainViewModel.Instance.Home.StartEnabled)
          return;
        Bot.Instance.Start();
      }

      private void Stop(object sender, HotkeyEventArgs e)
      {
        if (!MainViewModel.Instance.Home.StopEnabled)
          return;
        Bot.Instance.Stop();
      }

      private void CreditsEvent(object sender, RoutedEventArgs e)
      {
        new Process()
        {
          StartInfo = {
            UseShellExecute = true,
            FileName = "https://pokeplus.live/"
          }
        }.Start();
      }

      public static void ErrorCheck()
      {
        if (MainWindow.KeyAuthApp.response.success)
          return;
        int num = (int) MessageBox.Show(MainWindow.KeyAuthApp.response.message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
      }

      private void UnlockEvent(object sender, RoutedEventArgs e) => this._AuthWindow.Show();

      private void HamburgerMenu_ItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
      {
        ((ContentControl) this.HamburgerMenu).Content = e.InvokedItem;
        this.HamburgerMenu.IsPaneOpen = false;
      }

      private void Window_Closing(object sender, CancelEventArgs e) => Environment.Exit(0);

      [DebuggerNonUserCode]
      [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
      public void InitializeComponent()
      {
        if (this._contentLoaded)
          return;
        this._contentLoaded = true;
        Application.LoadComponent((object) this, new Uri("/PokeMMO+;component/mainwindow.xaml", UriKind.Relative));
      }

      [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerNonUserCode]
      void IComponentConnector.Connect(int connectionId, object target)
      {
        switch (connectionId)
        {
          case 1:
            this.MyMainWindow = (MainWindow) target;
            ((Window) this.MyMainWindow).Closing += new CancelEventHandler(this.Window_Closing);
            break;
          case 2:
            ((ButtonBase) target).Click += new RoutedEventHandler(this.CreditsEvent);
            break;
          case 3:
            ((ButtonBase) target).Click += new RoutedEventHandler(this.UnlockEvent);
            break;
          case 4:
            this.HamburgerMenu = (HamburgerMenu) target;
            this.HamburgerMenu.ItemInvoked += new HamburgerMenuItemInvokedRoutedEventHandler(this.HamburgerMenu_ItemInvoked);
            break;
          default:
            this._contentLoaded = true;
            break;
        }
      }

      //string method_0() => __nonvirtual (((FrameworkElement) this).Name);
    }
}
