// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Model.Security
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Mvvm;
using System.Windows.Media;

#nullable disable
namespace PokeMMO_.Model;

public class Security : BindableBase
{
  private int _WalkCyclesTrigger = 10;
  private int _TurnOffTrigger = 60;
  private int _WalkSpeedFrom = 700;
  private int _WalkSpeedTo = 1300;
  private int _ChannelSwitchFrom = 30;
  private int _ChannelSwitchTo = 60;
  private int _BreakFrom = 30;
  private int _BreakTo = 60;
  private int _BreakLengthFrom = 30;
  private int _BreakLengthTo = 60;
  private bool _Break = false;
  private bool _Humanize = false;
  private bool _AutoChannelSwitch = false;
  private bool _TurnOff = false;
  private bool _AlertPM = true;
  private bool _StopPM = true;
  private bool _AlertWalkCycles = true;
  private bool _StopWalkCycles = true;
  private bool _AlertSweetScent = true;
  private string _AutomaticCaptchaSolverText = "Automatic Captcha Solver: OFF";
  private Brush _AutomaticCaptchaSolverColor = (Brush) Brushes.Red;

  public int WalkCyclesTrigger
  {
    get => this._WalkCyclesTrigger;
    set => this.SetProperty<int>(ref this._WalkCyclesTrigger, value, nameof (WalkCyclesTrigger));
  }

  public int TurnOffTrigger
  {
    get => this._TurnOffTrigger;
    set => this.SetProperty<int>(ref this._TurnOffTrigger, value, nameof (TurnOffTrigger));
  }

  public int WalkSpeedFrom
  {
    get => this._WalkSpeedFrom;
    set => this.SetProperty<int>(ref this._WalkSpeedFrom, value, nameof (WalkSpeedFrom));
  }

  public int WalkSpeedTo
  {
    get => this._WalkSpeedTo;
    set => this.SetProperty<int>(ref this._WalkSpeedTo, value, nameof (WalkSpeedTo));
  }

  public int ChannelSwitchFrom
  {
    get => this._ChannelSwitchFrom;
    set => this.SetProperty<int>(ref this._ChannelSwitchFrom, value, nameof (ChannelSwitchFrom));
  }

  public int ChannelSwitchTo
  {
    get => this._ChannelSwitchTo;
    set => this.SetProperty<int>(ref this._ChannelSwitchTo, value, nameof (ChannelSwitchTo));
  }

  public int BreakFrom
  {
    get => this._BreakFrom;
    set => this.SetProperty<int>(ref this._BreakFrom, value, nameof (BreakFrom));
  }

  public int BreakTo
  {
    get => this._BreakTo;
    set => this.SetProperty<int>(ref this._BreakTo, value, nameof (BreakTo));
  }

  public int BreakLengthFrom
  {
    get => this._BreakLengthFrom;
    set => this.SetProperty<int>(ref this._BreakLengthFrom, value, nameof (BreakLengthFrom));
  }

  public int BreakLengthTo
  {
    get => this._BreakLengthTo;
    set => this.SetProperty<int>(ref this._BreakLengthTo, value, nameof (BreakLengthTo));
  }

  public bool Break
  {
    get => this._Break;
    set => this.SetProperty<bool>(ref this._Break, value, nameof (Break));
  }

  public bool Humanize
  {
    get => this._Humanize;
    set => this.SetProperty<bool>(ref this._Humanize, value, nameof (Humanize));
  }

  public bool AutoChannelSwitch
  {
    get => this._AutoChannelSwitch;
    set => this.SetProperty<bool>(ref this._AutoChannelSwitch, value, nameof (AutoChannelSwitch));
  }

  public bool TurnOff
  {
    get => this._TurnOff;
    set => this.SetProperty<bool>(ref this._TurnOff, value, nameof (TurnOff));
  }

  public bool AlertPM
  {
    get => this._AlertPM;
    set => this.SetProperty<bool>(ref this._AlertPM, value, nameof (AlertPM));
  }

  public bool StopPM
  {
    get => this._StopPM;
    set => this.SetProperty<bool>(ref this._StopPM, value, nameof (StopPM));
  }

  public bool AlertWalkCycles
  {
    get => this._AlertWalkCycles;
    set => this.SetProperty<bool>(ref this._AlertWalkCycles, value, nameof (AlertWalkCycles));
  }

  public bool StopWalkCycles
  {
    get => this._StopWalkCycles;
    set => this.SetProperty<bool>(ref this._StopWalkCycles, value, nameof (StopWalkCycles));
  }

  public bool AlertSweetScent
  {
    get => this._AlertSweetScent;
    set => this.SetProperty<bool>(ref this._AlertSweetScent, value, nameof (AlertSweetScent));
  }

  public string AutomaticCaptchaSolverText
  {
    get => this._AutomaticCaptchaSolverText;
    set
    {
      this.SetProperty<string>(ref this._AutomaticCaptchaSolverText, value, nameof (AutomaticCaptchaSolverText));
    }
  }

  public Brush AutomaticCaptchaSolverColor
  {
    get => this._AutomaticCaptchaSolverColor;
    set
    {
      this.SetProperty<Brush>(ref this._AutomaticCaptchaSolverColor, value, nameof (AutomaticCaptchaSolverColor));
    }
  }
}
