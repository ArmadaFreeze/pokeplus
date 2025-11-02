// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Model.Settings
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Classes;
using PokeMMO_.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

#nullable disable
namespace PokeMMO_.Model;

public class Settings : BindableBase
{
  private bool _PremiumEnabled = MainWindow.DevelopmentMode;
  private bool _HumanizeMouseMovement = true;
  private string _DefaultPath = InstalledApplications.GetApplictionInstallPath("PokeMMO");
  private ResolutionMode _ResolutionMode = ResolutionMode.HD;
  private bool _PrimaryMouseButton = false;

  public Settings()
  {
    this.LoadCommand = new DelegateCommand((Action) (() => Configuration.Load()), (Func<bool>) (() => true));
    this.SaveCommand = new DelegateCommand((Action) (() => Configuration.Save()), (Func<bool>) (() => true));
    this.DefaultPathCommand = new DelegateCommand((Action) (() => PathAndFileManager.SelectDefaultPath()), (Func<bool>) (() => true));
    this.ReplaceGFXCommand = new DelegateCommand((Action) (() => PathAndFileManager.ReplacePropertiesAndGFXFile(true)), (Func<bool>) (() => true));
    Timer timer1;
    this.SpoofCommand = new DelegateCommand((Action) (() => timer1 = new Timer((TimerCallback) (_ => this.SpoofCallBack()), (object) null, 100, -1)), (Func<bool>) (() => true));
    Timer timer2;
    this.ResetCommand = new DelegateCommand((Action) (() => timer2 = new Timer((TimerCallback) (_ => this.ResetCallBack()), (object) null, 100, -1)), (Func<bool>) (() => true));
    this.LoadCommand.RaiseCanExecuteChanged();
    this.SaveCommand.RaiseCanExecuteChanged();
    this.DefaultPathCommand.RaiseCanExecuteChanged();
    this.ReplaceGFXCommand.RaiseCanExecuteChanged();
    this.SpoofCommand.RaiseCanExecuteChanged();
    this.ResetCommand.RaiseCanExecuteChanged();
  }

  private void SpoofCallBack()
  {
    string str = "";
    List<string> stringList = new List<string>();
    foreach (string deviceId in MAC_Spoofer.GetDeviceIDs())
    {
      MAC_Spoofer macSpoofer = new MAC_Spoofer(deviceId);
      macSpoofer.Spoof();
      str = $"{str}{macSpoofer.DriverDesc}\n";
    }
    int num = (int) MessageBox.Show(str + "\nSuccessfully spoofed.", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
  }

  private void ResetCallBack()
  {
    string str = "";
    List<string> stringList = new List<string>();
    foreach (string deviceId in MAC_Spoofer.GetDeviceIDs())
    {
      MAC_Spoofer macSpoofer = new MAC_Spoofer(deviceId);
      macSpoofer.Reset();
      str = $"{str}{macSpoofer.DriverDesc}\n";
    }
    int num = (int) MessageBox.Show(str + "\nSuccessfully unspoofed.", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
  }

  public bool PremiumEnabled
  {
    get => this._PremiumEnabled;
    set => this.SetProperty<bool>(ref this._PremiumEnabled, value, nameof (PremiumEnabled));
  }

  public bool HumanizeMouseMovement
  {
    get => this._HumanizeMouseMovement;
    set
    {
      this.SetProperty<bool>(ref this._HumanizeMouseMovement, value, nameof (HumanizeMouseMovement));
    }
  }

  public DelegateCommand LoadCommand { get; }

  public DelegateCommand SaveCommand { get; }

  public DelegateCommand DefaultPathCommand { get; }

  public DelegateCommand ReplaceGFXCommand { get; }

  public DelegateCommand SpoofCommand { get; }

  public DelegateCommand ResetCommand { get; }

  public string DefaultPath
  {
    get => this._DefaultPath;
    set => this.SetProperty<string>(ref this._DefaultPath, value, nameof (DefaultPath));
  }

  public ResolutionMode ResolutionMode
  {
    get => this._ResolutionMode;
    set
    {
      this.SetProperty<ResolutionMode>(ref this._ResolutionMode, value, nameof (ResolutionMode));
    }
  }

  public bool PrimaryMouseButton
  {
    get => this._PrimaryMouseButton;
    set => this.SetProperty<bool>(ref this._PrimaryMouseButton, value, nameof (PrimaryMouseButton));
  }
}
