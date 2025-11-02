// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Views.AuthPage
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

public class AuthPage : UserControl, IComponentConnector
{
  internal TextBlock lbl_auth_status;
  internal TextBlock lbl_auth_username;
  internal TextBlock lbl_auth_hwid;
  internal TextBlock lbl_auth_ip;
  internal TextBlock lbl_auth_expiry;
  internal TextBlock lbl_auth_last_login;
  internal TextBlock lbl_auth_register_date;
  private bool _contentLoaded;

  public AuthPage() => this.InitializeComponent();

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/PokeMMO+;component/views/authpage.xaml", UriKind.Relative));
  }

  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  [DebuggerNonUserCode]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.lbl_auth_status = (TextBlock) target;
        break;
      case 2:
        this.lbl_auth_username = (TextBlock) target;
        break;
      case 3:
        this.lbl_auth_hwid = (TextBlock) target;
        break;
      case 4:
        this.lbl_auth_ip = (TextBlock) target;
        break;
      case 5:
        this.lbl_auth_expiry = (TextBlock) target;
        break;
      case 6:
        this.lbl_auth_last_login = (TextBlock) target;
        break;
      case 7:
        this.lbl_auth_register_date = (TextBlock) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
