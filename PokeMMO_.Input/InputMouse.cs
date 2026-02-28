using System;
using System.Runtime.InteropServices;
using System.Windows;
using PokeMMO_.Botting;
using PokeMMO_.Classes;

namespace PokeMMO_.Input;

public static class InputMouse
{
	internal static class CursorPosition
	{
		public struct PointInter
		{
			public int X;

			public int Y;

			public static explicit operator Point(PointInter point)
			{
				return new Point(point.X, point.Y);
			}
		}

		[DllImport("user32.dll")]
		public static extern bool GetCursorPos(out PointInter lpPoint);

		public static Point GetCursorPosition()
		{
			GetCursorPos(out var lpPoint);
			return (Point)lpPoint;
		}
	}

	public struct RECT
	{
		public int X1;

		public int Y1;

		public int X2;

		public int Y2;
	}

	private static void PerformMouseAction(int xpos, int ypos, Action clickAction)
	{
		if (Includes.ApplicationIsActivated())
		{
			int num = RandomNumber.Between(1, 3);
			Point cursorPosition = CursorPosition.GetCursorPosition();
			Point position = new Point(xpos + num, ypos + num);
			if (Bot.Instance.Settings.HumanizeMouseMovement)
			{
				MoveMouseHuman(position, 1000, 100);
			}
			else
			{
				Includes.SetCursorPos((int)position.X, (int)position.Y);
			}
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
			clickAction?.Invoke();
			if (!Bot.Instance.Settings.HumanizeMouseMovement)
			{
				Includes.SetCursorPos((int)cursorPosition.X, (int)cursorPosition.Y);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
			}
		}
	}

	private static void DoClick(bool useLeft)
	{
		if (!useLeft)
		{
			Bot.Instance.Sim.get_Mouse().RightButtonDown();
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
			Bot.Instance.Sim.get_Mouse().RightButtonUp();
		}
		else
		{
			Bot.Instance.Sim.get_Mouse().LeftButtonDown();
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
			Bot.Instance.Sim.get_Mouse().LeftButtonUp();
		}
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
	}

	public static void LeftClick(int xpos, int ypos)
	{
		PerformMouseAction(xpos, ypos, delegate
		{
			DoClick(!Bot.Instance.Settings.PrimaryMouseButton);
		});
	}

	public static void RightClick(int xpos, int ypos)
	{
		PerformMouseAction(xpos, ypos, delegate
		{
			DoClick(Bot.Instance.Settings.PrimaryMouseButton);
		});
	}

	public static void MoveMouse(int xpos, int ypos)
	{
		PerformMouseAction(xpos, ypos, null);
	}

	public static void LeftClickHdSd(int hdX, int hdY, int sdX, int sdY)
	{
		if (BotSettings.Settings.ResolutionMode != 0)
		{
			LeftClick(sdX, sdY);
		}
		else
		{
			LeftClick(hdX, hdY);
		}
	}

	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

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

	private static double PointDirection(Point A, Point B)
	{
		return Math.Atan2(B.Y - A.Y, B.X - A.X);
	}

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
		double num = 0.12;
		double num2 = LineLength(A, B);
		Point point = new Point((int)(num * B.X - num * A.X + B.X), (int)(num * B.Y - num * A.Y + B.Y));
		Point point2 = new Point((int)(num * A.X - num * B.X + A.X), (int)(num * A.Y - num * B.Y + A.Y));
		double num3 = PointDirection(B, A);
		Point a = new Point((int)(point.X + LengthDirX(num2 * num * 2.0, num3 + Math.PI / 2.0)), (int)(point.Y + LengthDirY(num2 * num * 2.0, num3 + Math.PI / 2.0)));
		Point b = new Point((int)(point.X + LengthDirX(num2 * num * 2.0, num3 - Math.PI / 2.0)), (int)(point.Y + LengthDirY(num2 * num * 2.0, num3 - Math.PI / 2.0)));
		Point b2 = new Point((int)(point2.X + LengthDirX(num2 * num * 2.0, num3 + Math.PI / 2.0)), (int)(point2.Y + LengthDirY(num2 * num * 2.0, num3 + Math.PI / 2.0)));
		Point point3 = new Point((int)(point2.X + LengthDirX(num2 * num * 2.0, num3 - Math.PI / 2.0)), (int)(point2.Y + LengthDirY(num2 * num * 2.0, num3 - Math.PI / 2.0)));
		double num4 = random.NextDouble() * LineLength(a, b);
		double num5 = random.NextDouble() * LineLength(a, b2);
		Point a2 = new Point((int)(num4 / LineLength(a, b) * (a.X - b.X) + b.X), (int)(num4 / LineLength(a, b) * (a.Y - b.Y) + b.Y));
		Point b3 = new Point((int)(num4 / LineLength(a, b) * (b2.X - point3.X) + point3.X), (int)(num4 / LineLength(a, b) * (b2.Y - point3.Y) + point3.Y));
		return new Point((int)(num5 / LineLength(a2, b3) * (a2.X - b3.X) + b3.X), (int)(num5 / LineLength(a2, b3) * (a2.Y - b3.Y) + b3.Y));
	}

	public static void MoveMouseHuman(Point Position, int Speed, int Wiggle)
	{
		Random random = new Random();
		Point cursorPosition = CursorPosition.GetCursorPosition();
		Point b = Position;
		Point pointCurve = GetPointCurve(cursorPosition, b);
		int num = 0;
		int num2 = 2;
		int num3 = (int)LineLength(cursorPosition, b);
		double num4 = (double)num3 / ((double)Speed / 100.0);
		for (double num5 = 0.0; num5 < 1.0; num5 += 1.0 / num4)
		{
			Point point = new Point((int)(cursorPosition.X * (1.0 - num5) * (1.0 - num5) + 2.0 * pointCurve.X * num5 * (1.0 - num5) + num5 * num5 * b.X), (int)(cursorPosition.Y * (1.0 - num5) * (1.0 - num5) + 2.0 * pointCurve.Y * num5 * (1.0 - num5) + num5 * num5 * b.Y));
			if (num == 0)
			{
				if (random.Next(0, 2) == 0)
				{
					point.X += -num2 + random.Next(0, 2 * num2);
					point.Y += -num2 + random.Next(0, 2 * num2);
					num = Wiggle;
				}
			}
			else
			{
				num--;
			}
			Includes.SetCursorPos((int)point.X, (int)point.Y);
			Bot.Instance.Sleep(1);
		}
		Includes.SetCursorPos((int)b.X, (int)b.Y);
	}
}
