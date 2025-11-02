// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.Includes
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Botting;
using System;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace PokeMMO_.Classes;

public class Includes
{
  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetForegroundWindow();

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

  [DllImport("user32.dll", SetLastError = true)]
  public static extern bool SetCursorPos(int x, int y);

  [DllImport("user32.dll", SetLastError = true)]
  public static extern void mouse_event(
    int dwFlags,
    int dx,
    int dy,
    int cButtons,
    int dwExtraInfo);

  public static string Base64Encode(string plainText)
  {
    try
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
    }
    catch
    {
      return "";
    }
  }

  public static string Base64Decode(string base64EncodedData)
  {
    try
    {
      return Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));
    }
    catch
    {
      return "";
    }
  }

  public static bool ApplicationIsActivated()
  {
    try
    {
      IntPtr foregroundWindow = Includes.GetForegroundWindow();
      if ((foregroundWindow == IntPtr.Zero ? 1 : (Bot.Instance.RequestStop ? 1 : 0)) != 0)
        return false;
      int id = Bot.Instance.Process.Id;
      int processId;
      Includes.GetWindowThreadProcessId(foregroundWindow, out processId);
      return processId == id;
    }
    catch (Exception ex)
    {
      return false;
    }
  }

  public static class WindowHelper
  {
    private const int SW_RESTORE = 9;

    public static void BringProcessToFront()
    {
      try
      {
        if (Bot.Instance.RequestStop)
          return;
        if (Includes.WindowHelper.IsIconic(Bot.Instance.Handle))
          Includes.WindowHelper.ShowWindow(Bot.Instance.Handle, 9);
        Includes.WindowHelper.SetForegroundWindow(Bot.Instance.Handle);
      }
      catch (Exception ex)
      {
      }
    }

    [DllImport("User32.dll")]
    private static extern bool SetForegroundWindow(IntPtr handle);

    [DllImport("User32.dll")]
    private static extern bool ShowWindow(IntPtr handle, int nCmdShow);

    [DllImport("User32.dll")]
    private static extern bool IsIconic(IntPtr handle);
  }
}
