// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Views.SettingsPage
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

#nullable disable
namespace PokeMMO_.Views;

public class SettingsPage : UserControl, IComponentConnector
{
  internal TextBlock SupportedResolutionsLabel;
  internal RadioButton chk_fullhd;
  internal RadioButton chk_sd;
  private bool _contentLoaded;

  public SettingsPage() => this.InitializeComponent();

  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [DebuggerNonUserCode]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/PokeMMO+;component/views/settingspage.xaml", UriKind.Relative));
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [DebuggerNonUserCode]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.SupportedResolutionsLabel = (TextBlock) target;
        break;
      case 2:
        this.chk_fullhd = (RadioButton) target;
        break;
      case 3:
        this.chk_sd = (RadioButton) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
