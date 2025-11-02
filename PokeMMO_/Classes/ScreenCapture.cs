// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.ScreenCapture
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Botting;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

#nullable disable
namespace PokeMMO_.Classes;

public class ScreenCapture
{
  [DllImport("user32.dll")]
  public static extern IntPtr GetForegroundWindow();

  [DllImport("user32.dll")]
  public static extern IntPtr ClientToScreen(IntPtr hWnd, ref System.Drawing.Point p);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetDesktopWindow();

  [DllImport("user32.dll")]
  private static extern IntPtr GetWindowRect(IntPtr hWnd, ref ScreenCapture.Rect rect);

  public static Image CaptureDesktop(Rectangle bounds)
  {
    return (Image) ScreenCapture.CaptureWindow(ScreenCapture.GetDesktopWindow(), bounds);
  }

  public static Bitmap CaptureActiveWindow(Rectangle bounds)
  {
    return ScreenCapture.CaptureWindow(ScreenCapture.GetForegroundWindow(), bounds);
  }

  public static void PokemonName()
  {
    if ((MainViewModel.Instance.Home.CatchPokemon.ToString() == Pokemon.All.ToString() ? 1 : (MainViewModel.Instance.Home.CatchPokemon.ToString() == Pokemon.Uncaught.ToString() ? 1 : 0)) != 0)
    {
      int num1 = (int) MessageBox.Show("Please choose the Pokemon you want to catch and press the Take Screenshot button while in an encounter with the Pokemon.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
    }
    else
    {
      ScreenCapture.Rect rect = new ScreenCapture.Rect();
      Rectangle bounds = new Rectangle();
      ScreenCapture.GetWindowRect(ScreenCapture.GetDesktopWindow(), ref rect);
      bounds = Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD ? new Rectangle(rect.Left + 338, rect.Top + 150, rect.Right - rect.Left - (1910 + MainViewModel.Instance.Home.CatchPokemon.ToString().Length * -6), rect.Bottom - rect.Top - 1060) : new Rectangle(rect.Left + 241, rect.Top + 151, rect.Right - rect.Left - (1270 + MainViewModel.Instance.Home.CatchPokemon.ToString().Length * -6), rect.Bottom - rect.Top - 701);
      Image image = ScreenCapture.CaptureDesktop(bounds);
      if (File.Exists($"bin/pokemon/{MainViewModel.Instance.Home.CatchPokemon.ToString()}.png"))
        File.Delete($"bin/pokemon/{MainViewModel.Instance.Home.CatchPokemon.ToString()}.png");
      image.Save($"bin/pokemon/{MainViewModel.Instance.Home.CatchPokemon.ToString()}.png", ImageFormat.Png);
      image.Dispose();
      int num2 = (int) MessageBox.Show($"Saved to bin/pokemon/{MainViewModel.Instance.Home.CatchPokemon.ToString()}.png of your bot folder.\n\nPlease check the screenshot to make sure it was captured correctly.", "Screenshot", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
    }
  }

  public static void ShinyPokemonImage()
  {
    ScreenCapture.Rect rect = new ScreenCapture.Rect();
    Rectangle bounds = new Rectangle();
    ScreenCapture.GetWindowRect(ScreenCapture.GetDesktopWindow(), ref rect);
    bounds = Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD ? new Rectangle(rect.Left + 1025, rect.Top + 200, rect.Right - rect.Left - 1425, rect.Bottom - rect.Top - 750) : new Rectangle(rect.Left + 700, rect.Top + 200, rect.Right - rect.Left - 1020, rect.Bottom - rect.Top - 600);
    Image image = ScreenCapture.CaptureDesktop(bounds);
    if (File.Exists("Shiny.jpg"))
      File.Delete("Shiny.jpg");
    image.Save("Shiny.jpg", ImageFormat.Jpeg);
    image.Dispose();
  }

  public static void CaptchaMessageImage()
  {
    ScreenCapture.Rect rect = new ScreenCapture.Rect();
    Rectangle bounds = new Rectangle();
    ScreenCapture.GetWindowRect(ScreenCapture.GetDesktopWindow(), ref rect);
    bounds = Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD ? new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top) : new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
    Image image = ScreenCapture.CaptureDesktop(bounds);
    if (File.Exists("CaptchaMessage.png"))
      File.Delete("CaptchaMessage.png");
    image.Save("CaptchaMessage.png", ImageFormat.Png);
    image.Dispose();
  }

  public static void CaptchaImage()
  {
    ScreenCapture.Rect rect = new ScreenCapture.Rect();
    Rectangle bounds = new Rectangle();
    ScreenCapture.GetWindowRect(ScreenCapture.GetDesktopWindow(), ref rect);
    bounds = Bot.Instance.Settings.ResolutionMode != ResolutionMode.HD ? new Rectangle(rect.Left + 340, rect.Top + 281, rect.Right - rect.Left - 680, rect.Bottom - rect.Top - 600) : new Rectangle(rect.Left + 660, rect.Top + 461, rect.Right - rect.Left - 1320, rect.Bottom - rect.Top - 960);
    Image image = ScreenCapture.CaptureDesktop(bounds);
    if (File.Exists("Captcha.jpg"))
      File.Delete("Captcha.jpg");
    image.Save("Captcha.jpg", ImageFormat.Jpeg);
    image.Dispose();
  }

  public static Bitmap CaptureWindow(IntPtr handle, Rectangle bounds)
  {
    try
    {
      Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        graphics.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
      return bitmap;
    }
    catch (Exception ex)
    {
      PokeMMOLogger.Instance.Log(ex.Message);
      Bitmap bitmap = new Bitmap(1, 1);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        graphics.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
      return bitmap;
    }
  }

  private struct Rect
  {
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
  }
}
