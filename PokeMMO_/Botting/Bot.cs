// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Botting.Bot
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Classes;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WindowsInput;

#nullable disable
namespace PokeMMO_.Botting;

public class Bot
{
  private static readonly object padlock = new object();
  private static Bot instance = (Bot) null;
  private BotSettings _Settings = BotSettings.Settings;
  private Actions _Actions = new Actions();
  private Status _Status = new Status();
  private Routes _Routes = new Routes();
  private Check _Check = new Check();
  private Behavior _Behavior = new Behavior();
  private Battle _Battle = new Battle();
  private State _State = new State();
  private PokeMMO_.Proccessing.Handle _GameProcessHandle = new PokeMMO_.Proccessing.Handle();
  public InputSimulator Sim = new InputSimulator();
  private Timer _Timer = (Timer) null;
  private Timer _TimeTimer = (Timer) null;
  public bool RequestStop = false;

  public static Bot Instance
  {
    get
    {
      lock (Bot.padlock)
      {
        if (Bot.instance == null)
          Bot.instance = new Bot();
        return Bot.instance;
      }
    }
  }

  public BotSettings Settings => this._Settings;

  public Actions Actions => this._Actions;

  public Status Status => this._Status;

  public Routes Routes => this._Routes;

  public Check Check => this._Check;

  public Behavior Behavior => this._Behavior;

  public Battle Battle => this._Battle;

  public State State => this._State;

  public PokeMMO_.Proccessing.Handle GameProcessHandle => this._GameProcessHandle;

  private IntPtr _Handle => this._GameProcessHandle.GameHandle;

  public IntPtr Handle => this._Handle;

  private Process _Process => this._GameProcessHandle.GameProcess;

  public Process Process => this._Process;

  public void Start()
  {
    MainViewModel.Instance.Home.StartEnabled = false;
    MainViewModel.Instance.Home.StopEnabled = true;
    Application.Current.Dispatcher.Invoke((Action) (() =>
    {
      ((Window) Application.Current.Windows.OfType<MainWindow>().SingleOrDefault<MainWindow>()).Hide();
      Application.Current.Windows.OfType<SubWindow>().SingleOrDefault<SubWindow>().Show();
    }));
    PathAndFileManager.ReadPropertiesFile();
    this.RequestStop = false;
    this._Status.Timer = DateTimeOffset.Now.DateTime;
    this._TimeTimer = new Timer(new TimerCallback(this._TimeTimer_Tick), (object) null, 1000, -1);
    this._Timer = new Timer(new TimerCallback(this._Timer_Tick), (object) null, 100, -1);
    this._Status.ChannelSwitchTrigger = RandomNumber.Between(MainViewModel.Instance.Security.ChannelSwitchFrom, MainViewModel.Instance.Security.ChannelSwitchTo);
    this._Status.BreakTrigger = RandomNumber.Between(MainViewModel.Instance.Security.BreakFrom, MainViewModel.Instance.Security.BreakTo);
    Includes.WindowHelper.BringProcessToFront();
    DiscordBot.Instance.SendMessage(nameof (Start), false);
  }

  public void Stop()
  {
    MainViewModel.Instance.Home.StartEnabled = true;
    MainViewModel.Instance.Home.StopEnabled = false;
    this.RequestStop = true;
    this._Timer.Change(-1, -1);
    this._TimeTimer.Change(-1, -1);
    this._Actions.ResetAndUpdateWalkCycle();
    Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Stopped"));
    this._Status.FirstAutoSSCycle = true;
    this._Status.GoTo = true;
    this._Status.GoBack = false;
    this._Status.Heal = false;
    this._Status.GoBackOnce = 0;
    this._Status.ChannelSwitchTimer = 0;
    this._Status.BreakTimer = 0;
    DiscordBot.Instance.SendMessage(nameof (Stop), false);
  }

  public void Sleep(int milliseconds) => Thread.Sleep(milliseconds);

  public async Task AsyncSleep(int milliseconds) => await Task.Delay(milliseconds);

  private void _TimeTimer_Tick(object state)
  {
    Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Timer = "Time: " + (DateTimeOffset.Now.DateTime - this._Status.Timer).ToString("hh\\:mm\\:ss")));
    if (this._Status.Breaking)
    {
      this._Status.ChannelSwitchTimer = 0;
      this._Status.BreakTimer = 0;
    }
    else
    {
      ++this._Status.ChannelSwitchTimer;
      ++this._Status.BreakTimer;
    }
    if ((!this._Settings.TurnOff ? 0 : ((DateTimeOffset.Now.DateTime - this._Status.Timer).Minutes >= this._Settings.TurnOffTrigger ? 1 : 0)) != 0)
    {
      this.Stop();
      this._Process.Kill();
      Environment.Exit(0);
    }
    if (this.RequestStop)
      return;
    this._TimeTimer.Change(1000, -1);
  }

  private void _Timer_Tick(object state)
  {
    if ((!this._Settings.Break || this._Status.BreakTimer / 60 < this._Status.BreakTrigger ? 0 : (this._Check.Walk ? 1 : 0)) == 0)
    {
      if ((!this._Settings.AutoChannelSwitch || this._Status.ChannelSwitchTimer / 60 < this._Status.ChannelSwitchTrigger ? 0 : (this._Check.Walk ? 1 : 0)) != 0)
      {
        if ((this._Settings.AutoSweetScent ? 1 : (this._Settings.AutoWalkFish ? 1 : 0)) == 0)
        {
          this.Sleep(3000);
          if (this._Check.Walk)
            this._Actions.Logout();
        }
        else if ((this._Status.Heal || this._Status.GoBack ? 0 : (!this._Status.GoTo ? 1 : 0)) != 0)
        {
          this.Sleep(3000);
          if (this._Check.Walk)
            this._Actions.Logout();
        }
      }
    }
    else if ((this._Settings.AutoSweetScent ? 1 : (this._Settings.AutoWalkFish ? 1 : 0)) != 0)
    {
      if ((this._Status.Heal || this._Status.GoBack ? 0 : (!this._Status.GoTo ? 1 : 0)) != 0)
      {
        this.Sleep(3000);
        if (this._Check.Walk)
          this._Actions.LogoutAndBreak();
      }
    }
    else
    {
      this.Sleep(3000);
      if (this._Check.Walk)
        this._Actions.LogoutAndBreak();
    }
    if (this._Settings.BotMode == BotMode.SellBox)
      this._Actions.SellBox();
    else if (this._Settings.BotMode == BotMode.MailClaim)
      this._Actions.MailClaim();
    else if (this._Settings.BotMode != BotMode.GTLSniper)
    {
      if (!this._Settings.Walk)
      {
        if (this._Settings.Fish)
        {
          if (this._Check.Walk)
          {
            this._State.InMainWindow();
            this._Behavior.Fish();
          }
          else
            this._State.InBattleWindow(this._Handle);
        }
        else if (!this._Settings.SweetScent)
        {
          if ((!this._Settings.AutoWalkFish ? 0 : (this._Settings.BotMode != BotMode.Safari ? 1 : 0)) == 0)
          {
            if ((!this._Settings.AutoSweetScent ? 0 : (this._Settings.BotMode != BotMode.Safari ? 1 : 0)) == 0)
            {
              if ((!this._Settings.SafariAutoWalk ? 0 : (this._Settings.BotMode == BotMode.Safari ? 1 : 0)) == 0)
              {
                if ((!this._Settings.SafariAutoFish ? 0 : (this._Settings.BotMode == BotMode.Safari ? 1 : 0)) == 0)
                {
                  if (!this._Check.Walk)
                    this._State.InBattleWindow(this._Handle);
                  else
                    this._State.InMainWindow();
                }
                else if (this._Check.Walk)
                {
                  this._State.InMainWindow();
                  this._Behavior.SafariAutoFish();
                }
                else
                  this._State.InBattleWindow(this._Handle);
              }
              else if (!this._Check.Walk)
              {
                this._State.InBattleWindow(this._Handle);
              }
              else
              {
                this._State.InMainWindow();
                this._Behavior.SafariAutoWalk();
              }
            }
            else if (!this._Check.Walk)
            {
              this._State.InBattleWindow(this._Handle);
            }
            else
            {
              this._State.InMainWindow();
              this._Behavior.AutoSweetScent();
            }
          }
          else if (!this._Check.Walk)
          {
            this._State.InBattleWindow(this._Handle);
          }
          else
          {
            this._State.InMainWindow();
            this._Behavior.AutoWalkFish();
          }
        }
        else if (this._Check.Walk)
        {
          this._State.InMainWindow();
          this._Behavior.SweetScent();
        }
        else
          this._State.InBattleWindow(this._Handle);
      }
      else if (this._Check.Walk)
      {
        this._State.InMainWindow();
        this._Behavior.Walk();
      }
      else
        this._State.InBattleWindow(this._Handle);
    }
    else
      this._Actions.GTLSniper();
    if ((!this._Settings.Login ? 0 : (this._Check.Login ? 1 : 0)) != 0)
      this._State.Login();
    if (this.RequestStop)
    {
      MainViewModel.Instance.Home.StartEnabled = true;
      MainViewModel.Instance.Home.StopEnabled = false;
      this.RequestStop = true;
      this._Timer.Change(-1, -1);
      this._TimeTimer.Change(-1, -1);
      this._Actions.ResetAndUpdateWalkCycle();
      Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Stopped"));
      this._Status.GoTo = true;
      this._Status.GoBack = false;
      this._Status.Heal = false;
      this._Status.GoBackOnce = 0;
      this._Status.ChannelSwitchTimer = 0;
      this._Status.BreakTimer = 0;
    }
    else
      this._Timer.Change(100, -1);
  }
}
