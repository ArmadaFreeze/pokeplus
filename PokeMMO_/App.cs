// Decompiled with JetBrains decompiler
// Type: PokeMMO_.App
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace PokeMMO_
{
    public partial class App : Application
    {
      private bool _contentLoaded;

      [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void InitializeComponent()
      {
        this.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
        Application.LoadComponent((object) this, new Uri("/PokeMMO+;component/app.xaml", UriKind.Relative));
      }

      [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
      [DebuggerNonUserCode]
      [STAThread]
      public static void Main()
      {
        App app = new App();
        app.Run();
      }
    }
}
