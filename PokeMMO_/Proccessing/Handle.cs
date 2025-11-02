// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Proccessing.Handle
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Botting;
using PokeMMO_.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

#nullable disable
namespace PokeMMO_.Proccessing;

public class Handle
{
  private IntPtr _GameHandle => this.GetGameHandle();

  public IntPtr GameHandle => this._GameHandle;

  private Process _GameProcess => this.GetGameProcess();

  public Process GameProcess => this._GameProcess;

  private bool _IsCERunning => this.CERunningCheck();

  public bool IsCERunning => this._IsCERunning;

  private bool CERunningCheck()
  {
    try
    {
      return CEDetection.FindCEExe(CEDetection.GetProcessesWithWindow());
    }
    catch (Exception ex)
    {
      return false;
    }
  }

  public Process GetGameProcess()
  {
    try
    {
      return ((IEnumerable<Process>) Process.GetProcesses()).Where<Process>((Func<Process, bool>) (x => x.ProcessName.StartsWith("java", StringComparison.OrdinalIgnoreCase))).FirstOrDefault<Process>();
    }
    catch (Exception ex)
    {
      return (Process) null;
    }
  }

  public IntPtr GetGameHandle()
  {
    try
    {
      return Bot.Instance.RequestStop ? IntPtr.Zero : this._GameProcess.MainWindowHandle;
    }
    catch (Exception ex)
    {
      Bot.Instance.Stop();
      int num = (int) MessageBox.Show("Please start PokeMMO first.", "Bot stopped", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
      return IntPtr.Zero;
    }
  }
}
