// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Input.InputKeyboard
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Botting;
using PokeMMO_.Classes;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using WindowsInput.Native;

#nullable disable
namespace PokeMMO_.Input;

public static class InputKeyboard
{
  [DllImport("user32.dll")]
  private static extern long GetKeyboardLayoutName(StringBuilder pwszKLID);

  public static int GetKeyFromProperties(string key)
  {
    StringBuilder pwszKLID = new StringBuilder(9);
    InputKeyboard.GetKeyboardLayoutName(pwszKLID);
    int keyFromProperties;
    switch (int.Parse(Bot.Instance.Settings.Data["client.controls.gdx." + key]))
    {
      case 7:
        keyFromProperties = 48 /*0x30*/;
        break;
      case 8:
        keyFromProperties = 49;
        break;
      case 9:
        keyFromProperties = 50;
        break;
      case 10:
        keyFromProperties = 51;
        break;
      case 11:
        keyFromProperties = 52;
        break;
      case 12:
        keyFromProperties = 53;
        break;
      case 13:
        keyFromProperties = 54;
        break;
      case 14:
        keyFromProperties = 55;
        break;
      case 15:
        keyFromProperties = 56;
        break;
      case 16 /*0x10*/:
        keyFromProperties = 57;
        break;
      case 19:
        keyFromProperties = 38;
        break;
      case 20:
        keyFromProperties = 40;
        break;
      case 21:
        keyFromProperties = 37;
        break;
      case 22:
        keyFromProperties = 39;
        break;
      case 29:
        keyFromProperties = 65;
        break;
      case 30:
        keyFromProperties = 66;
        break;
      case 31 /*0x1F*/:
        keyFromProperties = 67;
        break;
      case 32 /*0x20*/:
        keyFromProperties = 68;
        break;
      case 33:
        keyFromProperties = 69;
        break;
      case 34:
        keyFromProperties = 70;
        break;
      case 35:
        keyFromProperties = 71;
        break;
      case 36:
        keyFromProperties = 72;
        break;
      case 37:
        keyFromProperties = 73;
        break;
      case 38:
        keyFromProperties = 74;
        break;
      case 39:
        keyFromProperties = 75;
        break;
      case 40:
        keyFromProperties = 76;
        break;
      case 41:
        keyFromProperties = 77;
        break;
      case 42:
        keyFromProperties = 78;
        break;
      case 43:
        keyFromProperties = 79;
        break;
      case 44:
        keyFromProperties = 80 /*0x50*/;
        break;
      case 45:
        keyFromProperties = 81;
        break;
      case 46:
        keyFromProperties = 82;
        break;
      case 47:
        keyFromProperties = 83;
        break;
      case 48 /*0x30*/:
        keyFromProperties = 84;
        break;
      case 49:
        keyFromProperties = 85;
        break;
      case 50:
        keyFromProperties = 86;
        break;
      case 51:
        keyFromProperties = 87;
        break;
      case 52:
        keyFromProperties = 88;
        break;
      case 53:
        keyFromProperties = pwszKLID.ToString().Contains("407") ? 90 : 89;
        break;
      case 54:
        keyFromProperties = pwszKLID.ToString().Contains("407") ? 89 : 90;
        break;
      case 62:
        keyFromProperties = 32 /*0x20*/;
        break;
      case 131:
        keyFromProperties = 112 /*0x70*/;
        break;
      case 132:
        keyFromProperties = 113;
        break;
      case 133:
        keyFromProperties = 114;
        break;
      case 134:
        keyFromProperties = 115;
        break;
      case 135:
        keyFromProperties = 116;
        break;
      case 136:
        keyFromProperties = 117;
        break;
      case 137:
        keyFromProperties = 118;
        break;
      case 138:
        keyFromProperties = 119;
        break;
      case 139:
        keyFromProperties = 120;
        break;
      case 140:
        keyFromProperties = 121;
        break;
      case 141:
        keyFromProperties = 122;
        break;
      case 142:
        keyFromProperties = 123;
        break;
      case 144 /*0x90*/:
        keyFromProperties = 96 /*0x60*/;
        break;
      case 145:
        keyFromProperties = 97;
        break;
      case 146:
        keyFromProperties = 98;
        break;
      case 147:
        keyFromProperties = 99;
        break;
      case 148:
        keyFromProperties = 100;
        break;
      case 149:
        keyFromProperties = 101;
        break;
      case 150:
        keyFromProperties = 102;
        break;
      case 151:
        keyFromProperties = 103;
        break;
      case 152:
        keyFromProperties = 104;
        break;
      case 153:
        keyFromProperties = 105;
        break;
      default:
        keyFromProperties = -1;
        int num = (int) MessageBox.Show("If you set a key in PokeMMO to a key that is not supported, the bot will not work properly.\nPlease change your controls in PokeMMO.\nRefer below to our supported controls!.\nSupported Controls: A-Z, 0-9, F1-F12, ARROW KEYS, SPACE", "ERROR Controls", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        Bot.Instance.Stop();
        break;
    }
    return keyFromProperties;
  }

  public static void PressKeyEscape(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) 27);
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) 27);
  }

  public static void PressKeyA(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("key_a"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("key_a"));
  }

  public static void PressKeyB(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("key_b"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("key_b"));
  }

  public static void PressKeyUp(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    if ((Bot.Instance.Settings.AutoSweetScent || Bot.Instance.Settings.AutoWalkFish || Bot.Instance.Settings.SafariAutoWalk || Bot.Instance.Settings.SafariAutoFish ? (Bot.Instance.Status.LastWalkDirection != "Up" ? 1 : 0) : 0) != 0)
      Bot.Instance.Routes.WaitBeforeTurn();
    Bot.Instance.Status.LastWalkDirection = "Up";
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("key_up"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("key_up"));
  }

  public static void PressKeyDown(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    if ((Bot.Instance.Settings.AutoSweetScent || Bot.Instance.Settings.AutoWalkFish || Bot.Instance.Settings.SafariAutoWalk || Bot.Instance.Settings.SafariAutoFish ? (Bot.Instance.Status.LastWalkDirection != "Down" ? 1 : 0) : 0) != 0)
      Bot.Instance.Routes.WaitBeforeTurn();
    Bot.Instance.Status.LastWalkDirection = "Down";
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("key_down"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("key_down"));
  }

  public static void PressKeyLeft(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    if ((Bot.Instance.Settings.AutoSweetScent || Bot.Instance.Settings.AutoWalkFish || Bot.Instance.Settings.SafariAutoWalk || Bot.Instance.Settings.SafariAutoFish ? (Bot.Instance.Status.LastWalkDirection != "Left" ? 1 : 0) : 0) != 0)
      Bot.Instance.Routes.WaitBeforeTurn();
    Bot.Instance.Status.LastWalkDirection = "Left";
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("key_left"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("key_left"));
  }

  public static void PressKeyRight(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    if ((Bot.Instance.Settings.AutoSweetScent || Bot.Instance.Settings.AutoWalkFish || Bot.Instance.Settings.SafariAutoWalk || Bot.Instance.Settings.SafariAutoFish ? (Bot.Instance.Status.LastWalkDirection != "Right" ? 1 : 0) : 0) != 0)
      Bot.Instance.Routes.WaitBeforeTurn();
    Bot.Instance.Status.LastWalkDirection = "Right";
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("key_right"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("key_right"));
  }

  public static void PressHotkey1(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar1"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar1"));
  }

  public static void PressHotkey2(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar2"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar2"));
  }

  public static void PressHotkey3(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar3"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar3"));
  }

  public static void PressHotkey4(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar4"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar4"));
  }

  public static void PressHotkey5(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar5"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar5"));
  }

  public static void PressHotkey6(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar6"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar6"));
  }

  public static void PressHotkey7(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar7"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar7"));
  }

  public static void PressHotkey8(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar8"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar8"));
  }

  public static void PressHotkey9(int holdtime)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    Bot.Instance.Sim.Keyboard.KeyDown((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar9"));
    Thread.Sleep(holdtime);
    Bot.Instance.Sim.Keyboard.KeyUp((VirtualKeyCode) InputKeyboard.GetKeyFromProperties("hotbar9"));
  }
}
