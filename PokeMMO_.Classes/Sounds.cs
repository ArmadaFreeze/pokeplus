using System;
using System.Media;
using System.Threading.Tasks;
using PokeMMO_.Botting;

namespace PokeMMO_.Classes;

public class Sounds
{
	private static async Task PlaySoundAsync(string name)
	{
		try
		{
			new SoundPlayer("bin/snd/" + name + ".wav").Play();
			await Bot.Instance.AsyncSleep(2500);
		}
		catch (Exception ex2)
		{
			Exception ex = ex2;
			PokeMMOLogger.Instance.Log(ex.Message);
		}
	}

	public static void PlayShinySound()
	{
		PlaySoundAsync("Shiny");
	}

	public static void PlayAlertSound()
	{
		PlaySoundAsync("Alert");
	}

	public static void PlayPMSound()
	{
		PlaySoundAsync("PM");
	}

	public static void PlayNotificationSound()
	{
		PlaySoundAsync("Notification");
	}
}
