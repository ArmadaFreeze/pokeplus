using System;
using System.Runtime.InteropServices;
using System.Text;
using PokeMMO_.Botting;

namespace PokeMMO_.Classes;

public class Includes
{
	public static class WindowHelper
	{
		private const int SW_RESTORE = 9;

		[DllImport("User32.dll")]
		private static extern bool SetForegroundWindow(IntPtr handle);

		[DllImport("User32.dll")]
		private static extern bool ShowWindow(IntPtr handle, int nCmdShow);

		[DllImport("User32.dll")]
		private static extern bool IsIconic(IntPtr handle);

		public static void BringProcessToFront()
		{
			try
			{
				if (!Bot.Instance.RequestStop)
				{
					if (IsIconic(Bot.Instance.Handle))
					{
						ShowWindow(Bot.Instance.Handle, 9);
					}
					SetForegroundWindow(Bot.Instance.Handle);
				}
			}
			catch
			{
			}
		}
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	public static extern IntPtr GetForegroundWindow();

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool SetCursorPos(int x, int y);

	public static string Base64Encode(string plainText)
	{
		try
		{
			byte[] bytes = Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(bytes);
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
			byte[] bytes = Convert.FromBase64String(base64EncodedData);
			return Encoding.UTF8.GetString(bytes);
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
			IntPtr foregroundWindow = GetForegroundWindow();
			if (foregroundWindow == IntPtr.Zero || Bot.Instance.RequestStop)
			{
				return false;
			}
			int id = Bot.Instance.Process.Id;
			GetWindowThreadProcessId(foregroundWindow, out var processId);
			return processId == id;
		}
		catch
		{
			return false;
		}
	}
}
