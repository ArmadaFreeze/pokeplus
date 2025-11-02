// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Views.HomePage
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

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
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

#nullable disable
namespace PokeMMO_.Views;

public class HomePage : UserControl, IComponentConnector
{
  public Unlocker unlocker = new Unlocker();
  private Timer timer;
  internal HomePage MyHomePage;
  internal CheckBox chk_walk;
  internal CheckBox chk_fish;
  internal CheckBox chk_auto_walk;
  internal CheckBox chk_auto_sweet_cent;
  internal CheckBox chk_shiny;
  internal CheckBox chk_shinystop;
  internal CheckBox chk_skipdialog;
  internal CheckBox chk_skipevolve;
  internal CheckBox chk_learn_move_skip;
  internal CheckBox chk_login;
  internal CheckBox chk_lure;
  internal CheckBox chk_ether;
  internal CheckBox chk_sweet_cent;
  internal Button btn_start;
  internal Button btn_stop;
  private bool _contentLoaded;

  public HomePage() => this.InitializeComponent();

  private void chk_randomwalkpattern_Checked(object sender, RoutedEventArgs e)
  {
    if (!MainViewModel.Instance.Home.SquaresWalkPattern)
      return;
    MainViewModel.Instance.Home.SquaresWalkPattern = false;
  }

  private void chk_squareswalkpattern_Checked(object sender, RoutedEventArgs e)
  {
    if (!MainViewModel.Instance.Home.RandomWalkPattern)
      return;
    MainViewModel.Instance.Home.RandomWalkPattern = false;
  }

  private void chk_walk_Checked(object sender, RoutedEventArgs e)
  {
    if ((MainViewModel.Instance.Home.Fish || MainViewModel.Instance.Home.SweetScent || MainViewModel.Instance.Home.AutoWalkFish || MainViewModel.Instance.Home.AutoSweetScent || MainViewModel.Instance.Home.SafariAutoWalk ? 1 : (MainViewModel.Instance.Home.SafariAutoFish ? 1 : 0)) == 0)
      return;
    MainViewModel.Instance.Home.Fish = false;
    MainViewModel.Instance.Home.SweetScent = false;
    MainViewModel.Instance.Home.AutoWalkFish = false;
    MainViewModel.Instance.Home.AutoSweetScent = false;
    MainViewModel.Instance.Home.SafariAutoWalk = false;
    MainViewModel.Instance.Home.SafariAutoFish = false;
  }

  private void chk_fish_Checked(object sender, RoutedEventArgs e)
  {
    if ((MainViewModel.Instance.Home.Walk || MainViewModel.Instance.Home.SweetScent || MainViewModel.Instance.Home.AutoWalkFish || MainViewModel.Instance.Home.AutoSweetScent || MainViewModel.Instance.Home.SafariAutoWalk ? 1 : (MainViewModel.Instance.Home.SafariAutoFish ? 1 : 0)) == 0)
      return;
    MainViewModel.Instance.Home.Walk = false;
    MainViewModel.Instance.Home.SweetScent = false;
    MainViewModel.Instance.Home.AutoWalkFish = false;
    MainViewModel.Instance.Home.AutoSweetScent = false;
    MainViewModel.Instance.Home.SafariAutoWalk = false;
    MainViewModel.Instance.Home.SafariAutoFish = false;
  }

  private void chk_sweet_cent_Checked(object sender, RoutedEventArgs e)
  {
    if ((MainViewModel.Instance.Home.Fish || MainViewModel.Instance.Home.Walk || MainViewModel.Instance.Home.AutoWalkFish || MainViewModel.Instance.Home.AutoSweetScent ? 1 : (MainViewModel.Instance.Home.SweetScent ? 1 : 0)) == 0)
      return;
    MainViewModel.Instance.Home.Fish = false;
    MainViewModel.Instance.Home.Walk = false;
    MainViewModel.Instance.Home.AutoWalkFish = false;
    MainViewModel.Instance.Home.AutoSweetScent = false;
    MainViewModel.Instance.Home.SafariAutoWalk = false;
    MainViewModel.Instance.Home.SafariAutoFish = false;
  }

  private void chk_auto_walk_Checked(object sender, RoutedEventArgs e)
  {
    if ((MainViewModel.Instance.Home.Fish || MainViewModel.Instance.Home.SweetScent || MainViewModel.Instance.Home.Walk ? 1 : (MainViewModel.Instance.Home.AutoSweetScent ? 1 : 0)) == 0)
      return;
    MainViewModel.Instance.Home.Fish = false;
    MainViewModel.Instance.Home.SweetScent = false;
    MainViewModel.Instance.Home.Walk = false;
    MainViewModel.Instance.Home.AutoSweetScent = false;
  }

  private void chk_auto_sweet_cent_Checked(object sender, RoutedEventArgs e)
  {
    if ((MainViewModel.Instance.Home.Fish || MainViewModel.Instance.Home.SweetScent || MainViewModel.Instance.Home.Walk ? 1 : (MainViewModel.Instance.Home.AutoWalkFish ? 1 : 0)) == 0)
      return;
    MainViewModel.Instance.Home.Fish = false;
    MainViewModel.Instance.Home.SweetScent = false;
    MainViewModel.Instance.Home.AutoWalkFish = false;
    MainViewModel.Instance.Home.Walk = false;
  }

  private void chk_rock_Checked(object sender, RoutedEventArgs e)
  {
    if (!MainViewModel.Instance.Home.Bait)
      return;
    MainViewModel.Instance.Home.Bait = false;
  }

  private void chk_bait_Checked(object sender, RoutedEventArgs e)
  {
    if (!MainViewModel.Instance.Home.Rock)
      return;
    MainViewModel.Instance.Home.Rock = false;
  }

  private void chk_safari_auto_walk_Checked(object sender, RoutedEventArgs e)
  {
    if ((MainViewModel.Instance.Home.SafariAutoFish || MainViewModel.Instance.Home.Walk || MainViewModel.Instance.Home.Fish ? 1 : (MainViewModel.Instance.Home.SweetScent ? 1 : 0)) == 0)
      return;
    MainViewModel.Instance.Home.Walk = false;
    MainViewModel.Instance.Home.Fish = false;
    MainViewModel.Instance.Home.SafariAutoFish = false;
    MainViewModel.Instance.Home.SweetScent = false;
  }

  private void chk_safari_auto_fish_Checked(object sender, RoutedEventArgs e)
  {
    if ((MainViewModel.Instance.Home.SafariAutoWalk || MainViewModel.Instance.Home.Walk || MainViewModel.Instance.Home.Fish ? 1 : (MainViewModel.Instance.Home.SweetScent ? 1 : 0)) == 0)
      return;
    MainViewModel.Instance.Home.Walk = false;
    MainViewModel.Instance.Home.Fish = false;
    MainViewModel.Instance.Home.SafariAutoWalk = false;
    MainViewModel.Instance.Home.SweetScent = false;
  }

  private void chk_shiny_Checked(object sender, RoutedEventArgs e)
  {
    if (!MainViewModel.Instance.Home.StopOnShiny)
      return;
    MainViewModel.Instance.Home.StopOnShiny = false;
  }

  private void chk_shiny_stop_Checked(object sender, RoutedEventArgs e)
  {
    if (!MainViewModel.Instance.Home.CatchShiny)
      return;
    MainViewModel.Instance.Home.CatchShiny = false;
  }

  private void btn_test_Click(object sender, RoutedEventArgs e) => Bot.Instance.Actions.MailClaim();

  private void btn_start_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
  {
  }

  private void chk_walk_Unchecked(object sender, RoutedEventArgs e)
  {
    MainViewModel.Instance.Home.WalkOptionsVisibility = Visibility.Hidden;
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/PokeMMO+;component/views/homepage.xaml", UriKind.Relative));
  }

  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [DebuggerNonUserCode]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.MyHomePage = (HomePage) target;
        break;
      case 2:
        ((ToggleButton) target).Checked += new RoutedEventHandler(this.chk_rock_Checked);
        break;
      case 3:
        ((ToggleButton) target).Checked += new RoutedEventHandler(this.chk_bait_Checked);
        break;
      case 4:
        this.chk_walk = (CheckBox) target;
        this.chk_walk.Checked += new RoutedEventHandler(this.chk_walk_Checked);
        this.chk_walk.Unchecked += new RoutedEventHandler(this.chk_walk_Unchecked);
        break;
      case 5:
        this.chk_fish = (CheckBox) target;
        this.chk_fish.Checked += new RoutedEventHandler(this.chk_fish_Checked);
        break;
      case 6:
        this.chk_auto_walk = (CheckBox) target;
        this.chk_auto_walk.Checked += new RoutedEventHandler(this.chk_auto_walk_Checked);
        break;
      case 7:
        this.chk_auto_sweet_cent = (CheckBox) target;
        this.chk_auto_sweet_cent.Checked += new RoutedEventHandler(this.chk_auto_sweet_cent_Checked);
        break;
      case 8:
        ((ToggleButton) target).Checked += new RoutedEventHandler(this.chk_safari_auto_walk_Checked);
        break;
      case 9:
        ((ToggleButton) target).Checked += new RoutedEventHandler(this.chk_safari_auto_fish_Checked);
        break;
      case 10:
        ((ToggleButton) target).Checked += new RoutedEventHandler(this.chk_squareswalkpattern_Checked);
        break;
      case 11:
        ((ToggleButton) target).Checked += new RoutedEventHandler(this.chk_randomwalkpattern_Checked);
        break;
      case 12:
        this.chk_shiny = (CheckBox) target;
        this.chk_shiny.Checked += new RoutedEventHandler(this.chk_shiny_Checked);
        break;
      case 13:
        this.chk_shinystop = (CheckBox) target;
        this.chk_shinystop.Checked += new RoutedEventHandler(this.chk_shiny_stop_Checked);
        break;
      case 14:
        this.chk_skipdialog = (CheckBox) target;
        break;
      case 15:
        this.chk_skipevolve = (CheckBox) target;
        break;
      case 16 /*0x10*/:
        this.chk_learn_move_skip = (CheckBox) target;
        break;
      case 17:
        this.chk_login = (CheckBox) target;
        break;
      case 18:
        this.chk_lure = (CheckBox) target;
        break;
      case 19:
        this.chk_ether = (CheckBox) target;
        break;
      case 20:
        this.chk_sweet_cent = (CheckBox) target;
        this.chk_sweet_cent.Checked += new RoutedEventHandler(this.chk_sweet_cent_Checked);
        break;
      case 21:
        this.btn_start = (Button) target;
        this.btn_start.IsEnabledChanged += new DependencyPropertyChangedEventHandler(this.btn_start_IsEnabledChanged);
        break;
      case 22:
        this.btn_stop = (Button) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
