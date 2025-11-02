// Decompiled with JetBrains decompiler
// Type: PokeMMO_.SubWindow
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Classes;
using PokeMMO_.ViewModels;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;


namespace PokeMMO_
{
    public class SubWindow : Window, IComponentConnector
    {
      internal SubWindow MySubWindow;
      internal TextBlock lbl_shinycounter;
      internal TextBlock lbl_timer;
      internal TextBlock lbl_ballsthrowncounter;
      internal TextBlock lbl_encounterscount;
      internal TextBlock lbl_status;
      internal RichTextBox txt_richtextbox;
      internal Button btn_show;
      internal Button btn_hide;
      private bool _contentLoaded;

      public SubWindow()
      {
        this.InitializeComponent();
        this.MySubWindow.Title = Class2.randomTitle();
        this.MySubWindow.DataContext = (object) SubViewModel.Instance;
        this.Left = SystemParameters.PrimaryScreenWidth - this.method_0();
        this.Top = SystemParameters.PrimaryScreenHeight - this.method_1();
        this.btn_show.Click += new RoutedEventHandler(this.btn_show_Click);
        this.btn_hide.Click += new RoutedEventHandler(this.btn_hide_Click);
      }

      private void btn_show_Click(object sender, RoutedEventArgs e)
      {
        ((Window) Application.Current.Windows.OfType<MainWindow>().SingleOrDefault<MainWindow>()).Show();
      }

      private void btn_hide_Click(object sender, RoutedEventArgs e)
      {
        ((Window) Application.Current.Windows.OfType<MainWindow>().SingleOrDefault<MainWindow>()).Hide();
        Includes.WindowHelper.BringProcessToFront();
      }

      private void Window_MouseDown(object sender, MouseButtonEventArgs e)
      {
        if (e.LeftButton != MouseButtonState.Pressed)
          return;
        this.DragMove();
      }

      private void txt_richtextbox_TextChanged(object sender, TextChangedEventArgs e)
      {
        this.txt_richtextbox.ScrollToEnd();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
      public void InitializeComponent()
      {
        if (this._contentLoaded)
          return;
        this._contentLoaded = true;
        Application.LoadComponent((object) this, new Uri("/PokeMMO+;component/subwindow.xaml", UriKind.Relative));
      }

      [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
      [DebuggerNonUserCode]
      [EditorBrowsable(EditorBrowsableState.Never)]
      void IComponentConnector.Connect(int connectionId, object target)
      {
        switch (connectionId)
        {
          case 1:
            this.MySubWindow = (SubWindow) target;
            this.MySubWindow.MouseDown += new MouseButtonEventHandler(this.Window_MouseDown);
            break;
          case 2:
            this.lbl_shinycounter = (TextBlock) target;
            break;
          case 3:
            this.lbl_timer = (TextBlock) target;
            break;
          case 4:
            this.lbl_ballsthrowncounter = (TextBlock) target;
            break;
          case 5:
            this.lbl_encounterscount = (TextBlock) target;
            break;
          case 6:
            this.lbl_status = (TextBlock) target;
            break;
          case 7:
            this.txt_richtextbox = (RichTextBox) target;
            this.txt_richtextbox.TextChanged += new TextChangedEventHandler(this.txt_richtextbox_TextChanged);
            break;
          case 8:
            this.btn_show = (Button) target;
            break;
          case 9:
            this.btn_hide = (Button) target;
            break;
          default:
            this._contentLoaded = true;
            break;
        }
      }

      double method_0() => this.Width;

      double method_1() => this.Height;
    }
}
