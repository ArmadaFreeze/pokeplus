// Decompiled with JetBrains decompiler
// Type: PokeMMO_.DiscordWindow
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

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
using System.Windows.Input;
using System.Windows.Markup;

namespace PokeMMO_;

public class DiscordWindow : Window, IComponentConnector
{
  internal DiscordWindow MyDiscordWindow;
  internal Label lbl_x;
  internal CheckBox chk_DiscordDMThief;
  internal CheckBox chk_DiscordDMPayDay;
  internal CheckBox chk_DiscordDMThrowBall;
  internal CheckBox chk_DiscordDMIV31;
  private bool _contentLoaded;

  public DiscordWindow()
  {
    this.InitializeComponent();
    this.MyDiscordWindow.Title = Class2.randomTitle();
    this.MyDiscordWindow.DataContext = (object) DiscordViewModel.Instance;
    this.Left = SystemParameters.PrimaryScreenWidth - this.method_0();
    this.Top = SystemParameters.PrimaryScreenHeight - this.method_1();
    if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD)
    {
      if (Bot.Instance.Settings.ResolutionMode != ResolutionMode.SD)
        return;
      this.Left -= 75.0;
      this.Top -= 10.0;
    }
    else
    {
      this.Top -= 75.0;
      this.Left -= 10.0;
    }
  }

  private void lbl_x_Click(object sender, MouseButtonEventArgs e) => this.Hide();

  private void Window_MouseDown(object sender, MouseButtonEventArgs e)
  {
    if (e.LeftButton != MouseButtonState.Pressed)
      return;
    this.DragMove();
  }

  private void chk_DiscordDMThief_Changed(object sender, RoutedEventArgs e)
  {
    Configuration.SaveDiscordDMSettings();
  }

  private void chk_DiscordDMPayDay_Changed(object sender, RoutedEventArgs e)
  {
    Configuration.SaveDiscordDMSettings();
  }

  private void chk_DiscordDMThrowBall_Changed(object sender, RoutedEventArgs e)
  {
    Configuration.SaveDiscordDMSettings();
  }

  private void chk_DiscordDMIV31_Changed(object sender, RoutedEventArgs e)
  {
    Configuration.SaveDiscordDMSettings();
  }

  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [DebuggerNonUserCode]
  //public void InitializeComponent()
  //{
  //  if (this._contentLoaded)
  //    return;
  //  this._contentLoaded = true;
  //  Application.LoadComponent((object) this, new Uri("/PokeMMO+;component/views/discordwindow.xaml", UriKind.Relative));
  //}

  //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  //[EditorBrowsable(EditorBrowsableState.Never)]
  //[DebuggerNonUserCode]
  //void IComponentConnector.Connect(int connectionId, object target)
  //{
  //  switch (connectionId)
  //  {
  //    case 1:
  //      this.MyDiscordWindow = (DiscordWindow) target;
  //      this.MyDiscordWindow.MouseDown += new MouseButtonEventHandler(this.Window_MouseDown);
  //      break;
  //    case 2:
  //      this.lbl_x = (Label) target;
  //      this.lbl_x.MouseLeftButtonDown += new MouseButtonEventHandler(this.lbl_x_Click);
  //      break;
  //    case 3:
  //      this.chk_DiscordDMThief = (CheckBox) target;
  //      this.chk_DiscordDMThief.Checked += new RoutedEventHandler(this.chk_DiscordDMThief_Changed);
  //      this.chk_DiscordDMThief.Unchecked += new RoutedEventHandler(this.chk_DiscordDMThief_Changed);
  //      break;
  //    case 4:
  //      this.chk_DiscordDMPayDay = (CheckBox) target;
  //      this.chk_DiscordDMPayDay.Checked += new RoutedEventHandler(this.chk_DiscordDMPayDay_Changed);
  //      this.chk_DiscordDMPayDay.Unchecked += new RoutedEventHandler(this.chk_DiscordDMPayDay_Changed);
  //      break;
  //    case 5:
  //      this.chk_DiscordDMThrowBall = (CheckBox) target;
  //      this.chk_DiscordDMThrowBall.Checked += new RoutedEventHandler(this.chk_DiscordDMThrowBall_Changed);
  //      this.chk_DiscordDMThrowBall.Unchecked += new RoutedEventHandler(this.chk_DiscordDMThrowBall_Changed);
  //      break;
  //    case 6:
  //      this.chk_DiscordDMIV31 = (CheckBox) target;
  //      this.chk_DiscordDMIV31.Checked += new RoutedEventHandler(this.chk_DiscordDMIV31_Changed);
  //      this.chk_DiscordDMIV31.Unchecked += new RoutedEventHandler(this.chk_DiscordDMIV31_Changed);
  //      break;
  //    default:
  //      this._contentLoaded = true;
  //      break;
  //  }
  //}

  double method_0() => this.Width;

  double method_1() => this.Height;
}
