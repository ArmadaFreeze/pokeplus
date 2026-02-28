using System;
using System.Windows;
using PokeMMO_.Classes;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Botting;

public class Check
{
	private const string IMG = "bin/img/";

	private Search search = new Search();

	public bool Potion0 => CheckImage("bin/img/Potion0.png", 20);

	public bool SuperPotion0 => CheckImage("bin/img/SuperPotion0.png", 20);

	public bool HyperPotion0 => CheckImage("bin/img/HyperPotion0.png", 20);

	public bool Error1 => CheckImage("bin/img/Error1.png", 50);

	public bool Error2 => CheckImage("bin/img/Error2.png", 50);

	public bool Error3 => CheckImage("bin/img/Error3.png", 50);

	public bool GTLNoMoney => CheckImage("bin/img/GTLNoMoney.png", 50);

	public bool Lure => CheckImage("bin/img/Lure.png", 80);

	public bool SafariTimeOver => CheckImage("bin/img/TimeOver.png", 50);

	public bool HPOrange => CheckImage("bin/img/HPOrange.png", 20);

	public bool HPRed => CheckImage("bin/img/HPRed.png", 20);

	public bool Sleep => CheckImage("bin/img/Sleep.png", 80);

	public bool Catched => CheckImage("bin/img/Catched.png", 50);

	public bool Repel => CheckImage("bin/img/Repel.png", 20);

	public bool RepelInBattle => CheckImage("bin/img/Repel2.png", 20);

	public bool Dialogue => CheckImage("bin/img/Skip.png", 40);

	public bool CantRun => CheckImage("bin/img/CantRun.png", 80);

	public bool ThiefPokemonItem => CheckImage("bin/img/Item2.png", 80);

	public bool Stats => CheckImage("bin/img/Stats.png", 50);

	public bool Leppa0 => CheckImage("bin/img/Leppa0.png", 50);

	public bool SweetCent0 => CheckImage("bin/img/SC0.png", 50);

	public bool PM => CheckAnyImage("bin/img/PM.png", "bin/img/PM2.png", 50);

	public bool Captcha => CheckAnyImage("bin/img/Captcha.png", "bin/img/Captcha2.png", 50);

	public bool PPEffective0 => CheckAnyImage("bin/img/0PPE.png", "bin/img/0PPE2.png", 98);

	public bool PPSuperEffective0 => CheckAnyImage("bin/img/0PPS.png", "bin/img/0PPS2.png", 98);

	public bool NextPoke => CheckAnyImage(20, "bin/img/NextPoke.png", "bin/img/NextPoke2.png", "bin/img/NextPoke3.png");

	public bool Horde => CheckAnyImage(50, "bin/img/Horde.png", "bin/img/Horde2.png", "bin/img/Horde_old.png");

	public bool Login => CheckAnyImage(50, "bin/img/DC.png", "bin/img/DCLogin.png", "bin/img/Session.png", "bin/img/Login.png", "bin/img/Character.png");

	public bool Walk => CheckWalk();

	public bool CheckImage(string imagePath, int tolerance)
	{
		return search.UseImageSearch(imagePath, tolerance) != null;
	}

	private bool CheckAnyImage(string image1, string image2, int tolerance)
	{
		return search.UseImageSearch(image1, tolerance) != null || search.UseImageSearch(image2, tolerance) != null;
	}

	private bool CheckAnyImage(int tolerance, params string[] images)
	{
		int num = 0;
		while (true)
		{
			if (num < images.Length)
			{
				string path = images[num];
				if (search.UseImageSearch(path, tolerance) != null)
				{
					break;
				}
				num++;
				continue;
			}
			return false;
		}
		return true;
	}

	public int[] GetCaptchaCoordinates()
	{
		return search.UseImageSearch("bin/img/Captcha.png", 50) ?? search.UseImageSearch("bin/img/Captcha2.png", 50);
	}

	public bool CheckShiny()
	{
		if (!Bot.Instance.Settings.CatchShiny && !Bot.Instance.Settings.StopOnShiny)
		{
			return false;
		}
		return search.UseImageSearch("bin/img/Shiny.png", 90) != null;
	}

	public bool CheckDisabled()
	{
		if (search.UseImageSearch("bin/img/Disabled.png", 80) == null)
		{
			return false;
		}
		Bot.Instance.Status.MoveDisabled = true;
		return true;
	}

	public bool CheckWalk()
	{
		string path = ((Bot.Instance.Settings.BotMode == BotMode.Safari) ? "bin/img/Safari.png" : "bin/img/Battle.png");
		if (search.UseImageSearch(path, 20) == null && !CheckAnyImage(50, "bin/img/DC.png", "bin/img/DCLogin.png", "bin/img/Session.png", "bin/img/Login.png", "bin/img/Character.png"))
		{
			UIHelper.SetStatus("Status: Not in Battle");
			return true;
		}
		return false;
	}

	public GameState DetectGameState()
	{
		if (CheckAnyImage(50, "bin/img/DC.png", "bin/img/DCLogin.png", "bin/img/Session.png", "bin/img/Login.png", "bin/img/Character.png"))
		{
			return GameState.LoginScreen;
		}
		string path = ((Bot.Instance.Settings.BotMode == BotMode.Safari) ? "bin/img/Safari.png" : "bin/img/Battle.png");
		if (search.UseImageSearch(path, 20) != null)
		{
			return GameState.InBattle;
		}
		UIHelper.SetStatus("Status: Not in Battle");
		return GameState.Walking;
	}

	public bool CheckPokemon(Pokemon pokemon)
	{
		try
		{
			return search.UseImageSearch("bin/img/" + pokemon.ToString() + ".png", 50) != null;
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log("CheckPokemon error: " + ex.Message);
			Bot.Instance.Stop();
			TopMostMessageBox.Show("No image was found of the Pokemon you are trying to catch.\nPlease take a screenshot of the Pokemon name and place it in the bin/img folder.", "Bot stopped", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
			return false;
		}
	}

	public string CheckSelectedPokemon()
	{
		try
		{
			if (search.UseImageSearch("bin/pokemon/" + MainViewModel.Instance.Home.CatchPokemon.ToString() + ".png", 80) != null)
			{
				Bot.Instance.Status.EncounteredSelectedPokemon = true;
				return MainViewModel.Instance.Home.CatchPokemon.ToString();
			}
			return "";
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log("CheckSelectedPokemon error: " + ex.Message);
			Bot.Instance.Stop();
			TopMostMessageBox.Show("No image was found of the Pokemon you are trying to catch.\nPlease choose the Pokemon you want to catch and press the Take Screenshot button while in an encounter with the Pokemon.", "Bot stopped", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
			return "";
		}
	}
}
