// Decompiled with JetBrains decompiler
// Type: PokeMMO_.ChosenPokeBallWindow
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Botting;
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

#nullable disable
namespace PokeMMO_;

public class ChosenPokeBallWindow : Window, IComponentConnector
{
  internal ChosenPokeBallWindow MyChosenPokeBallWindow;
  internal Label lbl_x;
  private bool _contentLoaded;

  public ChosenPokeBallWindow()
  {
    this.InitializeComponent();
    this.MyChosenPokeBallWindow.Title = Class2.randomTitle();
    this.MyChosenPokeBallWindow.DataContext = (object) ChosenPokeBallViewModel.Instance;
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

  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [DebuggerNonUserCode]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/PokeMMO+;component/views/chosenpokeballwindow.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [EditorBrowsable(EditorBrowsableState.Never)]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.MyChosenPokeBallWindow = (ChosenPokeBallWindow) target;
        this.MyChosenPokeBallWindow.MouseDown += new MouseButtonEventHandler(this.Window_MouseDown);
        break;
      case 2:
        this.lbl_x = (Label) target;
        this.lbl_x.MouseLeftButtonDown += new MouseButtonEventHandler(this.lbl_x_Click);
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }

  double method_0() => this.Width;

  double method_1() => this.Height;
}
