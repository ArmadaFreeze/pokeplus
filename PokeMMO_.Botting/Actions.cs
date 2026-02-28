using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Windows;
using _2CaptchaAPI;
using _2CaptchaAPI.Enums;
using PokeMMO_.Classes;
using PokeMMO_.Input;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Botting;

public class Actions
{
	private const string IMG = "bin/img/";

	private Search search = new Search();

	private static readonly int[][] PokemonSlotCoords = new int[6][]
	{
		new int[4] { 410, 700, 320, 450 },
		new int[4] { 620, 700, 530, 450 },
		new int[4] { 830, 700, 740, 450 },
		new int[4] { 410, 750, 320, 500 },
		new int[4] { 630, 750, 530, 500 },
		new int[4] { 830, 750, 750, 500 }
	};

	private static readonly string[][] PotionTypes = new string[3][]
	{
		new string[2] { "Potion", "PotionH" },
		new string[2] { "SuperPotion", "SuperPotionH" },
		new string[2] { "HyperPotion", "HyperPotionH" }
	};

	private void ClickPokemonSlot(int slotNumber)
	{
		if (slotNumber >= 1 && slotNumber <= 6)
		{
			int[] array = PokemonSlotCoords[slotNumber - 1];
			InputMouse.LeftClickHdSd(array[0], array[1], array[2], array[3]);
		}
	}

	public void StayAFKBattle()
	{
		if (Bot.Instance.Status.AFKCounter == 0)
		{
			int[] array = search.UseImageSearch("bin/img/Bag.png", 80);
			if (array != null)
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				array = search.UseImageSearch("bin/img/Medicine.png", 80);
				if (array == null)
				{
					for (int i = 0; i < 3; i++)
					{
						InputKeyboard.PressKeyLeft(Bot.Instance.Settings.HoldTime);
						Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
					}
				}
				else
				{
					InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
					InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				}
				array = search.UseImageSearch("bin/img/Medicine.png", 80);
				if (array != null)
				{
					InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
					InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				}
				else
				{
					array = search.UseImageSearch("bin/img/Back.png", 80);
					if (array != null)
					{
						InputMouse.LeftClick(array[1], array[2]);
						Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
					}
				}
			}
			Bot.Instance.Status.AFKCounter++;
		}
		else
		{
			Bot.Instance.Status.AFKCounter++;
			if (Bot.Instance.Status.AFKCounter == 100)
			{
				Bot.Instance.Status.AFKCounter = 0;
			}
		}
		Bot.Instance.Sleep(Bot.Instance.Settings.WalkSpeed);
	}

	public void RunandGoBack()
	{
		int[] array = search.UseImageSearch("bin/img/Back.png", 50);
		if (array != null)
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
		}
		Bot.Instance.Battle.Run(Bot.Instance.Handle);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
		Bot.Instance.Status.GoBack = true;
		Bot.Instance.Status.GoBackOnce++;
	}

	private bool AnyMovePP0()
	{
		return Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 || Bot.Instance.Status.FourthMovePP0;
	}

	private void UseCatchMove(string moveImageName, Action setUsedFlag)
	{
		int[] array = search.UseImageSearch("bin/img/Fight.png", 50);
		if (array != null)
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
		}
		if (Bot.Instance.Settings.AutoWalkFish)
		{
			ScanMovePPStatus();
			if (!AnyMovePP0())
			{
				array = search.UseImageSearch("bin/img/" + moveImageName + ".png", 50);
				if (array != null)
				{
					InputMouse.LeftClick(array[1], array[2]);
					setUsedFlag();
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				}
				else
				{
					ClickBackButton();
				}
			}
			else
			{
				RunandGoBack();
			}
			ResetMovePPStatus();
		}
		else
		{
			array = search.UseImageSearch("bin/img/" + moveImageName + ".png", 50);
			if (array == null)
			{
				ClickBackButton();
				return;
			}
			InputMouse.LeftClick(array[1], array[2]);
			setUsedFlag();
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		}
	}

	private void ClickBackButton()
	{
		int[] array = search.UseImageSearch("bin/img/Back.png", 50);
		if (array != null)
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		}
	}

	public void Catch()
	{
		if (Bot.Instance.Settings.BotMode == BotMode.Safari)
		{
			if (!Bot.Instance.Settings.Rock || Bot.Instance.Status.UsedRock)
			{
				if (!Bot.Instance.Settings.Bait || Bot.Instance.Status.UsedBait)
				{
					InputMouse.LeftClickHdSd(400, 700, 305, 440);
					Bot.Instance.Status.ThrownBallsCounter++;
					DiscordBot.Instance.SendMessage("SafariBall", embed: true);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
					InputMouse.LeftClickHdSd(630, 750, 530, 500);
				}
				else
				{
					InputMouse.LeftClickHdSd(620, 700, 530, 450);
					Bot.Instance.Status.UsedBait = true;
				}
			}
			else
			{
				InputMouse.LeftClickHdSd(410, 750, 320, 500);
				Bot.Instance.Status.UsedRock = true;
			}
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		}
		else if (Bot.Instance.Settings.CatchWithSecondPokemon && !Bot.Instance.Status.Changed)
		{
			int[] array = search.UseImageSearch("bin/img/Level.png", 50);
			if (array != null && !Bot.Instance.Status.Changed && Includes.ApplicationIsActivated())
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				NextPoke();
				Bot.Instance.Status.Changed = true;
			}
		}
		else if (Bot.Instance.Settings.Substitute || Bot.Instance.Settings.Spore || Bot.Instance.Settings.FalseSwipe || Bot.Instance.Settings.Assist)
		{
			if (Bot.Instance.Settings.CatchMovesRoutine == CatchMovesRoutine.SFS)
			{
				ExecuteSFSRoutine();
			}
			else if (Bot.Instance.Settings.CatchMovesRoutine != CatchMovesRoutine.SSF)
			{
				if (Bot.Instance.Settings.CatchMovesRoutine == CatchMovesRoutine.FA)
				{
					ExecuteFARoutine();
				}
			}
			else
			{
				ExecuteSSFRoutine();
			}
			ThrowBall();
		}
		else
		{
			ThrowBall();
		}
	}

	private void ExecuteSFSRoutine()
	{
		if (Bot.Instance.Settings.Substitute && !Bot.Instance.Status.UsedSubstitute)
		{
			UseCatchMove("Substitute", delegate
			{
				Bot.Instance.Status.UsedSubstitute = true;
			});
		}
		if (Bot.Instance.Settings.FalseSwipe && !Bot.Instance.Status.UsedFalseSwipe)
		{
			UseCatchMove("FalseSwipe", delegate
			{
				Bot.Instance.Status.UsedFalseSwipe = true;
			});
		}
		if (Bot.Instance.Settings.Spore && !Bot.Instance.Check.Sleep)
		{
			UseCatchMove("Spore", delegate
			{
			});
		}
	}

	private void ExecuteSSFRoutine()
	{
		if (Bot.Instance.Settings.Spore && !Bot.Instance.Check.Sleep)
		{
			UseCatchMove("Spore", delegate
			{
			});
		}
		if (Bot.Instance.Settings.Substitute && !Bot.Instance.Status.UsedSubstitute)
		{
			UseCatchMove("Substitute", delegate
			{
				Bot.Instance.Status.UsedSubstitute = true;
			});
		}
		if (Bot.Instance.Settings.FalseSwipe && !Bot.Instance.Status.UsedFalseSwipe)
		{
			UseCatchMove("FalseSwipe", delegate
			{
				Bot.Instance.Status.UsedFalseSwipe = true;
			});
		}
	}

	private void ExecuteFARoutine()
	{
		if (Bot.Instance.Settings.FalseSwipe && !Bot.Instance.Status.UsedFalseSwipe)
		{
			UseCatchMove("FalseSwipe", delegate
			{
				Bot.Instance.Status.UsedFalseSwipe = true;
			});
		}
		if (Bot.Instance.Settings.Assist && !Bot.Instance.Check.Sleep)
		{
			UseCatchMove("Assist", delegate
			{
			});
		}
	}

	public void ThrowBall()
	{
		if (Bot.Instance.Settings.BotMode != BotMode.Safari)
		{
			int[] array = search.UseImageSearch("bin/img/Bag.png", 80);
			if (array == null)
			{
				return;
			}
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(500);
			array = search.UseImageSearch("bin/img/Ball.png", 80);
			if (array != null)
			{
				TryThrowChosenBall();
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					InputKeyboard.PressKeyLeft(Bot.Instance.Settings.HoldTime);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				}
				for (int j = 0; j < 2; j++)
				{
					InputKeyboard.PressKeyRight(Bot.Instance.Settings.HoldTime);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				}
			}
			array = search.UseImageSearch("bin/img/Ball.png", 80);
			if (array != null)
			{
				TryThrowChosenBall();
				return;
			}
			array = search.UseImageSearch("bin/img/Back.png", 80);
			if (array != null)
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			}
		}
		else
		{
			InputMouse.LeftClickHdSd(400, 700, 305, 440);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		}
	}

	private void TryThrowChosenBall()
	{
		int[] array = search.UseImageSearch("bin/img/" + Bot.Instance.Settings.ChosenPokeBall.ToString() + ".png", 50);
		if (array == null)
		{
			InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
			Bot.Instance.Status.ThrownBallsCounter++;
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			DiscordBot.Instance.SendMessage("PokeBall", embed: true);
		}
		else
		{
			InputMouse.LeftClick(array[1], array[2]);
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Status.ThrownBallsCounter++;
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			DiscordBot.Instance.SendMessage(Bot.Instance.Settings.ChosenPokeBall.ToString(), embed: true);
		}
	}

	public void CloseStats()
	{
		int[] array = search.UseImageSearch("bin/img/Stats.png", 80);
		if (array != null)
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
		}
	}

	public void Stats()
	{
		int[] array = search.UseImageSearch("bin/img/StatsIV.png", 50);
		if (array != null)
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
			array = search.UseImageSearch("bin/img/31.png", 50);
			if (array == null)
			{
				array = search.UseImageSearch("bin/img/Release.png", 50);
				if (array != null)
				{
					InputMouse.LeftClick(array[1], array[2]);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
					array = search.UseImageSearch("bin/img/CRelease.png", 50);
					if (array != null)
					{
						InputMouse.LeftClick(array[1], array[2]);
						Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
						array = search.UseImageSearch("bin/img/Yes.png", 80);
						if (array != null)
						{
							InputMouse.LeftClick(array[1], array[2]);
							Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
						}
					}
				}
			}
			else
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
				DiscordBot.Instance.SendMessage("IV31", embed: true);
				CloseStats();
			}
		}
		CloseStats();
	}

	public void CheckWalkCycle()
	{
		if (!Bot.Instance.Settings.AutoWalkFish || Bot.Instance.Settings.BotMode == BotMode.Safari)
		{
			if (!Bot.Instance.Settings.StopWalkCycles && !Bot.Instance.Settings.AlertWalkCycles)
			{
				return;
			}
			if (Bot.Instance.Status.WalkCycle >= Bot.Instance.Settings.WalkCyclesTrigger)
			{
				if (Bot.Instance.Settings.AlertWalkCycles)
				{
					Sounds.PlayAlertSound();
					Sounds.PlayAlertSound();
					Sounds.PlayAlertSound();
				}
				if (Bot.Instance.Settings.StopWalkCycles)
				{
					Bot.Instance.Stop();
				}
			}
			else if (Bot.Instance.Check.Walk)
			{
				Bot.Instance.Status.WalkCycle++;
			}
		}
		else if (Bot.Instance.Status.WalkCycle < 4)
		{
			if (Bot.Instance.Check.Walk)
			{
				Bot.Instance.Status.WalkCycle++;
			}
		}
		else
		{
			Bot.Instance.Status.GoBack = true;
			Bot.Instance.Status.GoBackOnce++;
		}
	}

	public void ResetAndUpdateWalkCycle()
	{
		Bot.Instance.Status.WalkCycle = 0;
		Application.Current.Dispatcher.Invoke(() => SubViewModel.Instance.WalkCycle = "WalkCycle: 0");
	}

	public void SkipLearnMove()
	{
		int[] array = search.UseImageSearch("bin/img/LearnMove.png", 80);
		if (array != null)
		{
			InputMouse.LeftClickHdSd(900, 695, 805, 440);
			Bot.Instance.Sleep(500);
			InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			array = search.UseImageSearch("bin/img/Yes.png", 80);
			if (array != null && Includes.ApplicationIsActivated())
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			}
		}
	}

	public void SkipEvolve()
	{
		int[] array = search.UseImageSearch("bin/img/Evolve.png", 80);
		if (array != null)
		{
			array = search.UseImageSearch("bin/img/Cancel.png", 80);
			if (array != null)
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			}
			array = search.UseImageSearch("bin/img/Yes.png", 80);
			if (array != null)
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			}
		}
	}

	public void Lure()
	{
		if (Bot.Instance.Check.Lure)
		{
			int[] array = search.UseImageSearch("bin/img/Yes.png", 80);
			if (array != null)
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			}
		}
	}

	public void UseLure()
	{
		if (Includes.ApplicationIsActivated())
		{
			InputKeyboard.PressHotkey(6, 100);
			Bot.Instance.Sleep(250);
		}
	}

	public void SkipDialogue()
	{
		int[] array = search.UseImageSearch("bin/img/Skip.png", 40);
		if (array != null && Includes.ApplicationIsActivated())
		{
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
			InputKeyboard.PressKeyA(Bot.Instance.Settings.WaitTimeLong);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
		}
	}

	public void SkipDialogueFish()
	{
		int[] array = search.UseImageSearch("bin/img/Skip.png", 40);
		if (array != null && Includes.ApplicationIsActivated())
		{
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
			InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
		}
	}

	public void SolveCaptcha()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		ScreenCapture.CaptchaImage();
		_2Captcha val = new _2Captcha(MainWindow.KeyAuthApp.var("CaptchaAPI"), (HttpClient)null);
		Bot.Instance.Status.SolvedCaptchaText = "";
		using (FileStream fileStream = new FileStream("Captcha.jpg", FileMode.Open))
		{
			Result result = val.SolveImage((Stream)fileStream, (FileType)2, Array.Empty<KeyValuePair<string, string>>()).GetAwaiter().GetResult();
			if (result.Success && ((Result)(ref result)).get_Response() != "ERROR_BAD_DUPLICATES" && ((Result)(ref result)).get_Response() != "ERROR_CAPTCHA_UNSOLVABLE")
			{
				Bot.Instance.Status.SolvedCaptchaText = ((Result)(ref result)).get_Response();
			}
		}
		int[] captchaCoordinates = Bot.Instance.Check.GetCaptchaCoordinates();
		if (captchaCoordinates != null)
		{
			InputMouse.LeftClick(captchaCoordinates[1], captchaCoordinates[2] + 190);
			Bot.Instance.Sleep(100);
			Bot.Instance.Sim.get_Keyboard().TextEntry(Bot.Instance.Status.SolvedCaptchaText);
			Bot.Instance.Sleep(100);
			InputMouse.LeftClick(captchaCoordinates[1], captchaCoordinates[2] + 225);
		}
		DiscordBot.Instance.SendMessage("CaptchaSolved", embed: false);
	}

	public void Humanize()
	{
		if (Bot.Instance.Status.HumanizeHelper)
		{
			HumanizeActions();
			Bot.Instance.Status.HumanizeHelper = false;
		}
	}

	public void HumanizeActions()
	{
		if (!Includes.ApplicationIsActivated() || !Bot.Instance.Settings.Humanize || Bot.Instance.Check.Dialogue)
		{
			return;
		}
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShortRandom);
		Random random = new Random();
		double num = random.NextDouble();
		if (num < 0.03)
		{
			UIHelper.SetStatus("Status: Humanize Action 1");
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeHuman);
		}
		num = random.NextDouble();
		if (num < 0.03)
		{
			UIHelper.SetStatus("Status: Humanize Action 2");
			int[] array = search.UseImageSearch("bin/img/Poke.png", 50);
			if (array != null && Includes.ApplicationIsActivated())
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
				array = search.UseImageSearch("bin/img/Summary.png", 50);
				if (array != null && Includes.ApplicationIsActivated())
				{
					InputMouse.LeftClick(array[1], array[2]);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeHuman);
				}
				num = random.NextDouble();
				if (num < 0.25)
				{
					UIHelper.SetStatus("Status: Humanize Action 2-1");
					array = search.UseImageSearch("bin/img/StatsIV.png", 50);
					if (array != null)
					{
						InputMouse.LeftClick(array[1], array[2]);
						Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeHuman);
					}
				}
				CloseStats();
			}
		}
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShortRandom);
	}

	public void SellBox()
	{
		int[] array = search.UseImageSearch("bin/img/AutoSort.png", 50);
		if (array != null && Includes.ApplicationIsActivated())
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			array = search.UseImageSearch("bin/img/Sort.png", 50);
			if (array != null && Includes.ApplicationIsActivated())
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			}
		}
		array = search.UseImageSearch("bin/img/BoxPokemon.png", 10);
		if (array != null && Includes.ApplicationIsActivated())
		{
			InputMouse.RightClick(array[1] - 20, array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime * 2);
		}
		array = search.UseImageSearch("bin/img/SellOnGTL.png", 50);
		if (array != null && Includes.ApplicationIsActivated())
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime * 2);
		}
		InputMouse.MoveMouse(0, 0);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		array = search.UseImageSearch("bin/img/Price.png", 50);
		if (array != null && Includes.ApplicationIsActivated())
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			if (!string.IsNullOrEmpty(Bot.Instance.Settings.SellPrice))
			{
				Bot.Instance.Sim.get_Keyboard().TextEntry(Bot.Instance.Settings.SellPrice);
			}
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		}
		array = search.UseImageSearch("bin/img/Sell.png", 50);
		if (array != null && Includes.ApplicationIsActivated())
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			InputKeyboard.PressKeyEscape(Bot.Instance.Settings.HoldTime);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		}
		if (array == null)
		{
			InputMouse.MoveMouse(0, 0);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		}
	}

	public void MailClaim()
	{
		int[] array = search.UseImageSearch("bin/img/Mail.png", 50);
		if (array != null && Includes.ApplicationIsActivated())
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		}
		array = search.UseImageSearch("bin/img/MailClaim.png", 50);
		if (array != null && Includes.ApplicationIsActivated())
		{
			InputMouse.LeftClick(array[1], array[2]);
		}
	}

	public void GTLSniper()
	{
		if (!Includes.ApplicationIsActivated())
		{
			return;
		}
		int[] array = search.UseImageSearch("bin/img/GTLRefresh.png", 50);
		if (array != null)
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		}
		array = search.UseImageSearch("bin/img/GTLBuy.png", 20);
		if (array != null && Includes.ApplicationIsActivated())
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime * 2);
			if (Bot.Instance.Check.GTLNoMoney)
			{
				DiscordBot.Instance.SendMessage("GTLSniperFailedNoMoney", embed: true);
				Bot.Instance.Stop();
			}
			else
			{
				DiscordBot.Instance.SendMessage("GTLSniperBought", embed: true);
			}
		}
		Bot.Instance.Sleep(5000);
	}

	public void TakeItem()
	{
		int[] array = search.UseImageSearch("bin/img/Item2.png", 65);
		if (array != null && Includes.ApplicationIsActivated())
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime * 2);
		}
		array = search.UseImageSearch("bin/img/TakeItem.png", 50);
		if (array != null && Includes.ApplicationIsActivated())
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime * 4);
			Bot.Instance.Status.ItemCounter++;
			DiscordBot.Instance.SendMessage("Thief", embed: true);
		}
	}

	public void ClickPokemonItem()
	{
		bool[] array = new bool[5]
		{
			Bot.Instance.Status.FirstPokemonItem,
			Bot.Instance.Status.SecondPokemonItem,
			Bot.Instance.Status.ThirdPokemonItem,
			Bot.Instance.Status.FourthPokemonItem,
			Bot.Instance.Status.FifthPokemonItem
		};
		int num = 0;
		while (true)
		{
			if (num < array.Length)
			{
				if (array[num])
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		ClickPokemonSlot(num + 1);
	}

	public void ScanMovePPStatus()
	{
		search.UseImageSearch("bin/img/ZeroPP.png", 80);
	}

	public void ScanPokemonItemStatus()
	{
		search.UseImageSearch("bin/img/Item.png", 45);
	}

	public void ResetMovePPStatus()
	{
		Bot.Instance.Status.FirstMovePP0 = false;
		Bot.Instance.Status.SecondMovePP0 = false;
		Bot.Instance.Status.ThirdMovePP0 = false;
		Bot.Instance.Status.FourthMovePP0 = false;
	}

	public void ResetPokemonItemStatus()
	{
		Bot.Instance.Status.FirstMainPokemonItem = false;
		Bot.Instance.Status.FirstPokemonItem = false;
		Bot.Instance.Status.SecondPokemonItem = false;
		Bot.Instance.Status.ThirdPokemonItem = false;
		Bot.Instance.Status.FourthPokemonItem = false;
		Bot.Instance.Status.FifthPokemonItem = false;
	}

	public void UseLeppa()
	{
		if (!Includes.ApplicationIsActivated() || Bot.Instance.Check.Leppa0)
		{
			if (Bot.Instance.Check.Leppa0)
			{
				Bot.Instance.Stop();
			}
			return;
		}
		InputKeyboard.PressHotkey(7, Bot.Instance.Settings.HoldTime);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
		InputKeyboard.PressKeyRight(Bot.Instance.Settings.HoldTime);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
		InputKeyboard.PressKeyA(Bot.Instance.Settings.WaitTimeVeryLong);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
		InputKeyboard.PressKeyA(Bot.Instance.Settings.WaitTimeVeryLong);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
		InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
		InputKeyboard.PressKeyRight(Bot.Instance.Settings.HoldTime);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
		InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
		InputKeyboard.PressKeyDown(Bot.Instance.Settings.HoldTime);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
		InputKeyboard.PressKeyA(Bot.Instance.Settings.HoldTime);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
	}

	public void UseEther()
	{
		if (Includes.ApplicationIsActivated())
		{
			InputKeyboard.PressHotkey(7, Bot.Instance.Settings.HoldTime);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
			InputKeyboard.PressKeyA(Bot.Instance.Settings.WaitTimeVeryLong);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
			InputKeyboard.PressKeyA(Bot.Instance.Settings.WaitTimeVeryLong);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
		}
	}

	public void Potion()
	{
		if (!Bot.Instance.Settings.PotionSystem)
		{
			return;
		}
		if (!Bot.Instance.Check.HPOrange)
		{
			if (Bot.Instance.Check.HPRed)
			{
				int redPotionSelectedIndex = Bot.Instance.Settings.RedPotionSelectedIndex;
				if ((redPotionSelectedIndex == 0 && !Bot.Instance.Check.Potion0) || (redPotionSelectedIndex == 1 && !Bot.Instance.Check.SuperPotion0) || (redPotionSelectedIndex == 2 && !Bot.Instance.Check.HyperPotion0))
				{
					Bot.Instance.Status.PotionStatus = "Red";
					UsePotion();
				}
			}
		}
		else
		{
			int orangePotionSelectedIndex = Bot.Instance.Settings.OrangePotionSelectedIndex;
			if ((orangePotionSelectedIndex == 0 && !Bot.Instance.Check.Potion0) || (orangePotionSelectedIndex == 1 && !Bot.Instance.Check.SuperPotion0) || (orangePotionSelectedIndex == 2 && !Bot.Instance.Check.HyperPotion0))
			{
				Bot.Instance.Status.PotionStatus = "Orange";
				UsePotion();
			}
		}
	}

	private bool FindPotion(int potionIndex)
	{
		int num = 0;
		int num2 = 6;
		string[] array = PotionTypes[potionIndex];
		int[] array2 = null;
		do
		{
			array2 = search.UseImageSearch("bin/img/" + array[0] + ".png", 80);
			if (array2 == null)
			{
				array2 = search.UseImageSearch("bin/img/" + array[1] + ".png", 80);
				if (array2 == null)
				{
					for (int i = 0; i < 5; i++)
					{
						InputKeyboard.PressKeyUp(Bot.Instance.Settings.HoldTime);
						Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
					}
				}
				else
				{
					InputMouse.LeftClick(array2[1], array2[2]);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
					num = num2;
					UsePotionOnPokemon();
				}
			}
			else
			{
				InputMouse.LeftClick(array2[1], array2[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				InputMouse.LeftClick(array2[1], array2[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				num = num2;
				UsePotionOnPokemon();
			}
			num++;
		}
		while (array2 == null && num <= num2);
		return array2 != null;
	}

	public void PotionRoutine()
	{
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
		InputKeyboard.PressKeyDown(2000);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
		int num = ((!(Bot.Instance.Status.PotionStatus == "Orange")) ? Bot.Instance.Settings.RedPotionSelectedIndex : Bot.Instance.Settings.OrangePotionSelectedIndex);
		bool[] array = new bool[3]
		{
			Bot.Instance.Check.Potion0,
			Bot.Instance.Check.SuperPotion0,
			Bot.Instance.Check.HyperPotion0
		};
		if (!array[num])
		{
			FindPotion(num);
		}
	}

	public void UsePotion()
	{
		int num = 0;
		do
		{
			int[] array = search.UseImageSearch("bin/img/Bag.png", 80);
			if (array != null)
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			}
			array = search.UseImageSearch("bin/img/Medicine.png", 80);
			if (array != null)
			{
				PotionRoutine();
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					InputKeyboard.PressKeyLeft(Bot.Instance.Settings.HoldTime);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				}
			}
			array = search.UseImageSearch("bin/img/Medicine.png", 80);
			if (array != null)
			{
				PotionRoutine();
			}
			else
			{
				array = search.UseImageSearch("bin/img/Back.png", 80);
				if (array != null)
				{
					InputMouse.LeftClick(array[1], array[2]);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				}
			}
			array = search.UseImageSearch("bin/img/Back.png", 80);
			if (array != null)
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			}
			num++;
		}
		while (!Bot.Instance.Status.UsedPotion && num < 10);
		Bot.Instance.Status.UsedPotion = false;
		Bot.Instance.Status.PotionStatus = "";
	}

	public void UsePotionOnPokemon()
	{
		ClickPokemonSlot(Bot.Instance.Status.SelectedPokemon);
		Bot.Instance.Sleep(500);
		int[] array = search.UseImageSearch("bin/img/NoEffect.png", 80);
		if (array != null)
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			Bot.Instance.Status.SelectedPokemon++;
			if (Bot.Instance.Status.SelectedPokemon == 7)
			{
				Bot.Instance.Status.SelectedPokemon = 1;
			}
		}
		else
		{
			Bot.Instance.Status.UsedPotion = true;
		}
	}

	public void NextPoke()
	{
		int num = 2;
		for (int i = 2; i <= 6; i++)
		{
			if (num != i)
			{
				continue;
			}
			if (i != 6)
			{
				ClickPokemonSlot(i);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
				if (Bot.Instance.Check.NextPoke)
				{
					num = i + 1;
				}
				continue;
			}
			ClickPokemonSlot(1);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
			InputKeyboard.PressKeyB(Bot.Instance.Settings.HoldTime);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
			if (Bot.Instance.Check.NextPoke)
			{
				num = 6;
			}
		}
		Bot.Instance.Status.SelectedPokemon = num + 1;
		if (Bot.Instance.Status.SelectedPokemon == 7)
		{
			Bot.Instance.Status.SelectedPokemon = 1;
		}
	}

	public void NextPokeManual()
	{
		int num = Bot.Instance.Status.SelectedPokemonManual;
		for (int i = 1; i <= 6; i++)
		{
			if (num != i)
			{
				continue;
			}
			if (i == 6)
			{
				ClickPokemonSlot(1);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeShort);
				InputKeyboard.PressKeyB(Bot.Instance.Settings.HoldTime);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
				if (Bot.Instance.Check.NextPoke)
				{
					num = 6;
				}
			}
			else
			{
				ClickPokemonSlot(i + 1);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
				if (Bot.Instance.Check.NextPoke)
				{
					num = i + 1;
				}
			}
		}
		Bot.Instance.Status.SelectedPokemonManual = num + 1;
		if (Bot.Instance.Status.SelectedPokemonManual == 7)
		{
			Bot.Instance.Status.SelectedPokemonManual = 1;
		}
	}

	private bool PerformLogout()
	{
		if (Includes.ApplicationIsActivated())
		{
			InputKeyboard.PressKeyEscape(Bot.Instance.Settings.HoldTime);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			int[] array = search.UseImageSearch("bin/img/Logout.png", 50);
			if (array == null || !Includes.ApplicationIsActivated())
			{
				return false;
			}
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			InputMouse.LeftClickHdSd(960, 525, 645, 345);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			return true;
		}
		return false;
	}

	public void Logout()
	{
		PerformLogout();
		Bot.Instance.Status.ChannelSwitchTimer = 0;
		Bot.Instance.Status.ChannelSwitchTrigger = RandomNumber.Between(MainViewModel.Instance.Security.ChannelSwitchFrom, MainViewModel.Instance.Security.ChannelSwitchTo);
	}

	public void LogoutAndBreak()
	{
		if (PerformLogout())
		{
			Bot.Instance.Status.Breaking = true;
			UIHelper.SetStatus("Status: Taking a Break");
			Bot.Instance.Sleep(RandomNumber.Between(MainViewModel.Instance.Security.BreakLengthFrom, MainViewModel.Instance.Security.BreakLengthTo) * 60000);
			Bot.Instance.Status.Breaking = false;
		}
		Bot.Instance.Status.BreakTimer = 0;
		Bot.Instance.Status.BreakTrigger = RandomNumber.Between(MainViewModel.Instance.Security.BreakFrom, MainViewModel.Instance.Security.BreakTo);
	}

	public void SelectedCatchPokemonCounter()
	{
		if (Bot.Instance.Status.SelectedCatchPokemonCounterHelper)
		{
			Bot.Instance.Status.SelectedCatchPokemonCounter++;
			Bot.Instance.Status.SelectedCatchPokemonCounterHelper = false;
		}
	}

	public int SafeWalkInt(int value)
	{
		string s = value.ToString().Replace(".", "").Replace(",", "")
			.Replace(" ", "");
		return int.Parse(s);
	}

	public int SafeWalkFromInt()
	{
		return SafeWalkInt(MainViewModel.Instance.Security.WalkSpeedFrom);
	}

	public int SafeWalkToInt()
	{
		return SafeWalkInt(MainViewModel.Instance.Security.WalkSpeedTo);
	}
}
