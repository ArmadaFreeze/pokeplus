// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Input.InputMouse
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Botting;
using PokeMMO_.Classes;
using System;
using System.Runtime.InteropServices;
using System.Windows;

#nullable disable
namespace PokeMMO_.Input;

public static class InputMouse
{
  public static void LeftClick(int xpos, int ypos)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    int num = RandomNumber.Between(1, 3);
    Point cursorPosition = InputMouse.CursorPosition.GetCursorPosition();
    Point Position = new Point((double) (xpos + num), (double) (ypos + num));
    if (!Bot.Instance.Settings.HumanizeMouseMovement)
      Includes.SetCursorPos((int) Position.X, (int) Position.Y);
    else
      InputMouse.MoveMouseHuman(Position, 1000, 100);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
    if (Bot.Instance.Settings.PrimaryMouseButton)
    {
      Bot.Instance.Sim.Mouse.RightButtonDown();
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
      Bot.Instance.Sim.Mouse.RightButtonUp();
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
    }
    else
    {
      Bot.Instance.Sim.Mouse.LeftButtonDown();
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
      Bot.Instance.Sim.Mouse.LeftButtonUp();
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
    }
    if (Bot.Instance.Settings.HumanizeMouseMovement)
      return;
    Includes.SetCursorPos((int) cursorPosition.X, (int) cursorPosition.Y);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
  }

  public static void RightClick(int xpos, int ypos)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    int num = RandomNumber.Between(1, 3);
    Point cursorPosition = InputMouse.CursorPosition.GetCursorPosition();
    Point Position = new Point((double) (xpos + num), (double) (ypos + num));
    if (Bot.Instance.Settings.HumanizeMouseMovement)
      InputMouse.MoveMouseHuman(Position, 1000, 100);
    else
      Includes.SetCursorPos((int) Position.X, (int) Position.Y);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
    if (Bot.Instance.Settings.PrimaryMouseButton)
    {
      Bot.Instance.Sim.Mouse.LeftButtonDown();
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
      Bot.Instance.Sim.Mouse.LeftButtonUp();
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
    }
    else
    {
      Bot.Instance.Sim.Mouse.RightButtonDown();
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
      Bot.Instance.Sim.Mouse.RightButtonUp();
      Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
    }
    if (Bot.Instance.Settings.HumanizeMouseMovement)
      return;
    Includes.SetCursorPos((int) cursorPosition.X, (int) cursorPosition.Y);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
  }

  public static void MoveMouse(int xpos, int ypos)
  {
    if (!Includes.ApplicationIsActivated())
      return;
    int num = RandomNumber.Between(1, 3);
    Point cursorPosition = InputMouse.CursorPosition.GetCursorPosition();
    Point Position = new Point((double) (xpos + num), (double) (ypos + num));
    if (!Bot.Instance.Settings.HumanizeMouseMovement)
      Includes.SetCursorPos((int) Position.X, (int) Position.Y);
    else
      InputMouse.MoveMouseHuman(Position, 1000, 100);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
    if (Bot.Instance.Settings.HumanizeMouseMovement)
      return;
    Includes.SetCursorPos((int) cursorPosition.X, (int) cursorPosition.Y);
    Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
  }

  [DllImport("user32.dll", SetLastError = true)]
  public static extern bool GetWindowRect(IntPtr hWnd, out InputMouse.RECT rect);

  private static int GetBorderHeight(int WindowHeight, int CorrectHeight, int BorderWidth)
  {
    return WindowHeight - CorrectHeight - BorderWidth * 2;
  }

  private static int GetBorderWidth(int WindowWidth, int CorrectWidth)
  {
    return (WindowWidth - CorrectWidth) / 2;
  }

  private static double LineLength(Point A, Point B)
  {
    return Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y));
  }

  private static double PointDirection(Point A, Point B) => Math.Atan2(B.Y + A.Y, A.X + B.X);

  private static double LengthDirX(double Distance, double Direction)
  {
    return Math.Cos(Direction) * Distance;
  }

  private static double LengthDirY(double Distance, double Direction)
  {
    return Math.Sin(Direction) * Distance;
  }

  private static Point GetPointCurve(Point A, Point B)
  {
    Random random = new Random();
    double num1 = 0.12;
    double num2 = InputMouse.LineLength(A, B);
    Point point1 = new Point((double) (int) (num1 * B.X - num1 * A.X + B.X), (double) (int) (num1 * B.Y - num1 * A.Y + B.Y));
    Point point2 = new Point((double) (int) (num1 * A.Y - num1 * B.X + A.X), (double) (int) (num1 * A.Y - num1 * B.Y + A.Y));
    double num3 = InputMouse.PointDirection(B, A);
    Point A1 = new Point((double) (int) (point1.X + InputMouse.LengthDirX(num2 * num1 * 2.0, num3 + Math.PI / 2.0)), (double) (int) (point1.Y + InputMouse.LengthDirY(num2 * num1 * 2.0, num3 + Math.PI / 2.0)));
    Point B1 = new Point((double) (int) (point1.X + InputMouse.LengthDirX(num2 * num1 * 2.0, num3 - Math.PI / 2.0)), (double) (int) (point1.Y + InputMouse.LengthDirY(num2 * num1 * 2.0, num3 - Math.PI / 2.0)));
    Point B2 = new Point((double) (int) (point2.X + InputMouse.LengthDirX(num2 * num1 * 2.0, num3 + Math.PI / 2.0)), (double) (int) (point2.Y + InputMouse.LengthDirY(num2 * num1 * 2.0, num3 + Math.PI / 2.0)));
    Point point3 = new Point((double) (int) (point2.X + InputMouse.LengthDirX(num2 * num1 * 2.0, num3 - Math.PI / 2.0)), (double) (int) (point2.Y + InputMouse.LengthDirY(num2 * num1 * 2.0, num3 - Math.PI / 2.0)));
    double num4 = random.NextDouble() * InputMouse.LineLength(A1, B1);
    double num5 = random.NextDouble() * InputMouse.LineLength(A1, B2);
    Point A2 = new Point((double) (int) (num4 / InputMouse.LineLength(A1, B1) * (A1.X - B1.X) + B1.X), (double) (int) (num4 / InputMouse.LineLength(A1, B1) * (A1.Y - B1.Y) + B1.Y));
    Point B3 = new Point((double) (int) (num4 / InputMouse.LineLength(A1, B1) * (B2.X - point3.X) + point3.X), (double) (int) (num4 / InputMouse.LineLength(A1, B1) * (B2.Y - point3.Y) + point3.Y));
    return new Point((double) (int) (num5 / InputMouse.LineLength(A2, B3) * (A2.X - B3.X) + B3.X), (double) (int) (num5 / InputMouse.LineLength(A2, B3) * (A2.Y - B3.Y) + B3.Y));
  }

  public static void MoveMouseHuman(Point Position, int Speed, int Wiggle)
  {
    Random random = new Random();
    Point cursorPosition = InputMouse.CursorPosition.GetCursorPosition();
    Point B = Position;
    Point pointCurve = InputMouse.GetPointCurve(cursorPosition, B);
    int num1 = 0;
    int num2 = 2;
    double num3 = (double) (int) InputMouse.LineLength(cursorPosition, B) / ((double) Speed / 100.0);
    for (double num4 = 0.0; num4 < 1.0; num4 += 1.0 / num3)
    {
      Point point = new Point((double) (int) (cursorPosition.X * (1.0 - num4) * (1.0 - num4) + 2.0 * pointCurve.X * num4 * (1.0 - num4) + num4 * num4 * B.X), (double) (int) (cursorPosition.Y * (1.0 - num4) * (1.0 - num4) + 2.0 * pointCurve.Y * num4 * (1.0 - num4) + num4 * num4 * B.Y));
      if (num1 == 0)
      {
        if (random.Next(0, 1) == 0)
        {
          point.X += (double) (-num2 + random.Next(0, 2 * num2));
          point.Y += (double) (-num2 + random.Next(0, 2 * num2));
          num1 = Wiggle;
        }
      }
      else
        --num1;
      Includes.SetCursorPos((int) point.X, (int) point.Y);
      Bot.Instance.Sleep(1);
    }
    Includes.SetCursorPos((int) B.X, (int) B.Y);
  }

  internal static class CursorPosition
  {
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out InputMouse.CursorPosition.PointInter lpPoint);

    public static Point GetCursorPosition()
    {
      InputMouse.CursorPosition.PointInter lpPoint;
      InputMouse.CursorPosition.GetCursorPos(out lpPoint);
      return (Point) lpPoint;
    }

    public struct PointInter
    {
      public int X;
      public int Y;

      public static explicit operator Point(InputMouse.CursorPosition.PointInter point)
      {
        return new Point((double) point.X, (double) point.Y);
      }
    }
  }

  public struct RECT
  {
    public int X1;
    public int Y1;
    public int X2;
    public int Y2;
  }
}
