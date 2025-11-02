// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Views.SecurityPage
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
using Xceed.Wpf.Toolkit;

#nullable disable
namespace PokeMMO_.Views;

public class SecurityPage : UserControl, IComponentConnector
{
  internal CheckBox chk_stoppm;
  internal CheckBox chk_alertpm;
  internal CheckBox chk_stopwalkcycle;
  internal CheckBox chk_alertwalkcycle;
  internal CheckBox chk_alertsweetcent;
  internal IntegerUpDown walkcycles;
  internal TextBlock lbl_text_walkcycles;
  internal TextBlock lbl_walkspeed_in_ms;
  internal IntegerUpDown walkfromrnd;
  internal TextBlock lbl_text_to;
  internal IntegerUpDown walktornd;
  internal CheckBox chk_turnofftimer;
  internal IntegerUpDown turnoff;
  internal TextBlock lbl_AutomaticCatpchaSolver;
  private bool _contentLoaded;

  public SecurityPage() => this.InitializeComponent();

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/PokeMMO+;component/views/securitypage.xaml", UriKind.Relative));
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.chk_stoppm = (CheckBox) target;
        break;
      case 2:
        this.chk_alertpm = (CheckBox) target;
        break;
      case 3:
        this.chk_stopwalkcycle = (CheckBox) target;
        break;
      case 4:
        this.chk_alertwalkcycle = (CheckBox) target;
        break;
      case 5:
        this.chk_alertsweetcent = (CheckBox) target;
        break;
      case 6:
        this.walkcycles = (IntegerUpDown) target;
        break;
      case 7:
        this.lbl_text_walkcycles = (TextBlock) target;
        break;
      case 8:
        this.lbl_walkspeed_in_ms = (TextBlock) target;
        break;
      case 9:
        this.walkfromrnd = (IntegerUpDown) target;
        break;
      case 10:
        this.lbl_text_to = (TextBlock) target;
        break;
      case 11:
        this.walktornd = (IntegerUpDown) target;
        break;
      case 12:
        this.chk_turnofftimer = (CheckBox) target;
        break;
      case 13:
        this.turnoff = (IntegerUpDown) target;
        break;
      case 14:
        this.lbl_AutomaticCatpchaSolver = (TextBlock) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
