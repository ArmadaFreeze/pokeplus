// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.CEDetection
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;

#nullable disable
namespace PokeMMO_.Classes;

internal class CEDetection
{
  private static string[] CheatEngineFiles = new string[9]
  {
    "Cheat Engine.exe",
    ".CETRAINER",
    ".CT",
    "lua53-32.dll",
    "lua53-64.dll",
    "speedhack-x86_64.dll",
    "vehdebug-x86_64.dll",
    "winhook-x86_64.dll",
    "cheatengine-x86_64"
  };

  public static List<string> GetProcessesWithWindow()
  {
    List<string> processesWithWindow = new List<string>();
    using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT ProcessId, ExecutablePath, CommandLine FROM Win32_Process"))
    {
      using (ManagementObjectCollection source = managementObjectSearcher.Get())
      {
        foreach (var data in ((IEnumerable<Process>) Process.GetProcesses()).Join(source.Cast<ManagementObject>(), (Func<Process, int>) (p => p.Id), (Func<ManagementObject, int>) (mo => (int) (uint) mo["ProcessId"]), (p, mo) => new
        {
          Process = p,
          Path = (string) mo["ExecutablePath"],
          CommandLine = (string) mo["CommandLine"]
        }))
          processesWithWindow.Add(data.Path);
      }
    }
    return processesWithWindow;
  }

  public static bool FindCEExe(List<string> paths)
  {
    bool ceExe;
    foreach (string path in paths)
    {
      foreach (string file in Directory.GetFiles(path + "\\..\\"))
      {
        foreach (string cheatEngineFile in CEDetection.CheatEngineFiles)
        {
          if (file.Contains(cheatEngineFile))
          {
            ceExe = true;
            goto label_13;
          }
        }
      }
    }
    ceExe = false;
label_13:
    return ceExe;
  }
}
