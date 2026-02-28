using System;
using System.Collections.Generic;
using PokeMMO_.Classes;
using PokeMMO_.Input;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Botting;

public class State
{
	private const string IMG = "bin/img/";

	private Search search = new Search();

	private static readonly Dictionary<BotMode, Action<IntPtr>> BattleModeActions = new Dictionary<BotMode, Action<IntPtr>>
	{
		{
			BotMode.Fight,
			delegate(IntPtr h)
			{
				Bot.Instance.Battle.Fight(h, delegate(IntPtr handle, int[] fightCoords)
				{
					Bot.Instance.Battle.FightPokemonCheck(handle, fightCoords);
				});
			}
		},
		{
			BotMode.Run,
			delegate(IntPtr h)
			{
				Bot.Instance.Battle.Run(h);
			}
		},
		{
			BotMode.Catch,
			delegate(IntPtr h)
			{
				Bot.Instance.Battle.Catch(h);
			}
		},
		{
			BotMode.None,
			delegate
			{
			}
		}
	};

	private static readonly Dictionary<BotMode, Action<IntPtr>> PremiumBattleModeActions = new Dictionary<BotMode, Action<IntPtr>>
	{
		{
			BotMode.PayDay,
			delegate(IntPtr h)
			{
				Bot.Instance.Battle.PayDay(h);
			}
		},
		{
			BotMode.PayDayCatchMixed,
			delegate(IntPtr h)
			{
				Bot.Instance.Battle.PayDayCatchMixed(h);
			}
		},
		{
			BotMode.PayDayThiefMixed,
			delegate(IntPtr h)
			{
				Bot.Instance.Battle.PayDayThiefMixed(h);
			}
		},
		{
			BotMode.Thief,
			delegate(IntPtr h)
			{
				Bot.Instance.Battle.Thief(h);
			}
		},
		{
			BotMode.Pickpocket,
			delegate(IntPtr h)
			{
				Bot.Instance.Battle.Pickpocket(h);
			}
		},
		{
			BotMode.Safari,
			delegate(IntPtr h)
			{
				Bot.Instance.Battle.Safari(h);
			}
		}
	};

	public void InMainWindow()
	{
		ResetStatusVariables();
		EncountersCounter();
		if (Bot.Instance.Check.Captcha)
		{
			DiscordBot.Instance.SendMessage("Captcha", embed: false);
			if (MainViewModel.Instance.Home.PremiumEnabled)
			{
				Bot.Instance.Actions.SolveCaptcha();
			}
			Sounds.PlayAlertSound();
		}
		if (Bot.Instance.Settings.Lure)
		{
			if (!Bot.Instance.Settings.AutoSweetScent)
			{
				Bot.Instance.Actions.UseLure();
			}
			Bot.Instance.Actions.Lure();
		}
		if (Bot.Instance.Settings.AutoEther && !Bot.Instance.Settings.AutoSweetScent)
		{
			Bot.Instance.Actions.UseEther();
		}
		Skips();
		if ((Bot.Instance.Settings.BotMode == BotMode.Thief || Bot.Instance.Settings.BotMode == BotMode.PayDayThiefMixed || Bot.Instance.Settings.BotMode == BotMode.Pickpocket) && MainViewModel.Instance.Premium.PremiumEnabled)
		{
			Bot.Instance.Actions.TakeItem();
			Bot.Instance.Actions.TakeItem();
		}
		if ((BotSettings.Settings.AlertPM || BotSettings.Settings.StopPM) && Bot.Instance.Check.PM)
		{
			if (BotSettings.Settings.AlertPM)
			{
				Sounds.PlayPMSound();
				Sounds.PlayPMSound();
				Sounds.PlayPMSound();
			}
			if (BotSettings.Settings.StopPM)
			{
				Bot.Instance.Stop();
			}
		}
		if (Bot.Instance.Settings.OnlyKeepIV31 && !Bot.Instance.Status.ShinyHelper)
		{
			Bot.Instance.Actions.Stats();
		}
		else
		{
			Bot.Instance.Actions.CloseStats();
		}
		Bot.Instance.Status.ShinyHelper = false;
		Bot.Instance.Actions.Humanize();
	}

	public void InBattleWindow(IntPtr h)
	{
		if (Bot.Instance.Check.Login || !Includes.ApplicationIsActivated())
		{
			return;
		}
		Bot.Instance.Status.HumanizeHelper = true;
		if (MainViewModel.Instance.Home.PremiumEnabled)
		{
			try
			{
				if (Bot.Instance.Check.Captcha)
				{
					DiscordBot.Instance.SendMessage("Captcha", embed: false);
					if (MainViewModel.Instance.Home.PremiumEnabled)
					{
						Bot.Instance.Actions.SolveCaptcha();
					}
					Sounds.PlayAlertSound();
				}
			}
			catch (Exception ex)
			{
				PokeMMOLogger.Instance.Log(ex.Message);
			}
		}
		Bot.Instance.Check.CheckDisabled();
		if (Bot.Instance.Settings.SkipEvolve)
		{
			Bot.Instance.Actions.SkipEvolve();
		}
		if (Bot.Instance.Check.RepelInBattle)
		{
			int[] array = search.UseImageSearch("bin/img/No.png", 50);
			if (array != null)
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			}
		}
		BotMode botMode = Bot.Instance.Settings.BotMode;
		if (!BattleModeActions.TryGetValue(botMode, out var value))
		{
			if (MainViewModel.Instance.Home.PremiumEnabled && PremiumBattleModeActions.TryGetValue(botMode, out var value2))
			{
				value2(h);
			}
		}
		else
		{
			value(h);
		}
	}

	public void Skips()
	{
		if (Bot.Instance.Settings.SkipLearningNew)
		{
			Bot.Instance.Actions.SkipLearnMove();
		}
		if (BotSettings.Settings.SkipDialog && !Bot.Instance.Settings.Fish && !Bot.Instance.Settings.AutoWalkFish && !Bot.Instance.Settings.SafariAutoFish)
		{
			Bot.Instance.Actions.SkipDialogue();
		}
	}

	public void ResetStatusVariables()
	{
		Bot.Instance.Status.SelectedPokemonManual = 1;
		Bot.Instance.Status.MoveDisabled = false;
		Bot.Instance.Status.Changed = false;
		Bot.Instance.Status.UsedFalseSwipe = false;
		Bot.Instance.Status.UsedSubstitute = false;
		Bot.Instance.Status.UsedPayDay = false;
		Bot.Instance.Status.UsedRock = false;
		Bot.Instance.Status.UsedBait = false;
		Bot.Instance.Status.EncounteredSelectedPokemon = false;
		Bot.Instance.Status.ThiefHelper = false;
		Bot.Instance.Status.ImprisonHelper = false;
		Bot.Instance.Status.DetectedItem = false;
	}

	public void EncountersCounter()
	{
		if (Bot.Instance.Status.IsInFight)
		{
			Bot.Instance.Status.EncountersCounter++;
			Bot.Instance.Status.IsInFight = false;
		}
	}

	private void TryClickImage(string imagePath, int yOffset, bool pressA)
	{
		int[] array = search.UseImageSearch(imagePath, 50);
		if (array != null)
		{
			InputMouse.LeftClick(array[1], array[2] + yOffset);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			if (pressA)
			{
				InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			}
		}
	}

	public void Login()
	{
		TryClickImage("bin/img/DC.png", 15, pressA: true);
		Bot.Instance.Sleep(1000);
		TryClickImage("bin/img/DCLogin.png", 15, pressA: true);
		Bot.Instance.Sleep(1000);
		TryClickImage("bin/img/Session.png", 15, pressA: true);
		Bot.Instance.Sleep(1000);
		TryClickImage("bin/img/Login.png", 0, pressA: false);
		Bot.Instance.Sleep(1000);
		TryClickImage("bin/img/Login.png", 0, pressA: false);
		Bot.Instance.Sleep(1000);
		int[] array = search.UseImageSearch("bin/img/Character.png", 50);
		if (array != null)
		{
			InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		}
	}
}
