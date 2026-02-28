using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using PokeMMO_.Botting;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Classes;

public class ScreenCapture
{
	private struct Rect
	{
		public int Left;

		public int Top;

		public int Right;

		public int Bottom;
	}

	[DllImport("user32.dll")]
	public static extern IntPtr GetForegroundWindow();

	[DllImport("user32.dll")]
	public static extern IntPtr ClientToScreen(IntPtr hWnd, ref System.Drawing.Point p);

	[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	public static extern IntPtr GetDesktopWindow();

	[DllImport("user32.dll")]
	private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

	public static Image CaptureDesktop(Rectangle bounds)
	{
		return CaptureWindow(GetDesktopWindow(), bounds);
	}

	public static Bitmap CaptureActiveWindow(Rectangle bounds)
	{
		return CaptureWindow(GetForegroundWindow(), bounds);
	}

	private static void CaptureAndSave(string filename, ImageFormat format, Func<Rect, Rectangle> boundsCalc)
	{
		Rect rect = default(Rect);
		GetWindowRect(GetDesktopWindow(), ref rect);
		Rectangle bounds = boundsCalc(rect);
		using Image image = CaptureDesktop(bounds);
		image.Save(filename, format);
	}

	public static void PokemonName()
	{
		if (!(MainViewModel.Instance.Home.CatchPokemon == "All") && !(MainViewModel.Instance.Home.CatchPokemon == "Uncaught"))
		{
			string pokemonName = MainViewModel.Instance.Home.CatchPokemon.ToString();
			string text = "bin/pokemon/" + pokemonName + ".png";
			CaptureAndSave(text, ImageFormat.Png, (Rect rect) => (Bot.Instance.Settings.ResolutionMode != 0) ? new Rectangle(rect.Left + 241, rect.Top + 151, rect.Right - rect.Left - (1270 + pokemonName.Length * -6), rect.Bottom - rect.Top - 701) : new Rectangle(rect.Left + 338, rect.Top + 150, rect.Right - rect.Left - (1910 + pokemonName.Length * -6), rect.Bottom - rect.Top - 1060));
			TopMostMessageBox.Show("Saved to " + text + " of your bot folder.\n\nPlease check the screenshot to make sure it was captured correctly.", "Screenshot", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
		}
		else
		{
			TopMostMessageBox.Show("Please choose the Pokemon you want to catch and press the Take Screenshot button while in an encounter with the Pokemon.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
		}
	}

	public static void ShinyPokemonImage()
	{
		CaptureAndSave("Shiny.jpg", ImageFormat.Jpeg, (Rect rect) => (Bot.Instance.Settings.ResolutionMode != 0) ? new Rectangle(rect.Left + 700, rect.Top + 200, rect.Right - rect.Left - 1020, rect.Bottom - rect.Top - 600) : new Rectangle(rect.Left + 1025, rect.Top + 200, rect.Right - rect.Left - 1425, rect.Bottom - rect.Top - 750));
	}

	public static void CaptchaMessageImage()
	{
		CaptureAndSave("CaptchaMessage.png", ImageFormat.Png, (Rect rect) => new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top));
	}

	public static void CaptchaImage()
	{
		CaptureAndSave("Captcha.jpg", ImageFormat.Jpeg, (Rect rect) => (Bot.Instance.Settings.ResolutionMode != 0) ? new Rectangle(rect.Left + 340, rect.Top + 281, rect.Right - rect.Left - 680, rect.Bottom - rect.Top - 600) : new Rectangle(rect.Left + 660, rect.Top + 461, rect.Right - rect.Left - 1320, rect.Bottom - rect.Top - 960));
	}

	public static Bitmap CaptureWindow(IntPtr handle, Rectangle bounds)
	{
		try
		{
			Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
			}
			return bitmap;
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log("CaptureWindow error: " + ex.Message);
			return new Bitmap(1, 1);
		}
	}
}
