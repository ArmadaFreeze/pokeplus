using System;
using PokeMMO_.Classes;
using PokeMMO_.Input;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Botting;

public class Behavior
{
	private static readonly Action<int>[] DirectionKeys = new Action<int>[4]
	{
		InputKeyboard.PressKeyLeft,
		InputKeyboard.PressKeyRight,
		InputKeyboard.PressKeyUp,
		InputKeyboard.PressKeyDown
	};

	private const int L = 0;

	private const int R = 1;

	private const int U = 2;

	private const int D = 3;

	private static readonly int[][] SquarePatterns = new int[4][]
	{
		new int[4] { 0, 3, 1, 2 },
		new int[4] { 1, 3, 0, 2 },
		new int[4] { 0, 2, 1, 3 },
		new int[4] { 1, 2, 0, 3 }
	};

	private static readonly int[][] RandomPatterns = new int[8][]
	{
		new int[4] { 0, 1, 0, 1 },
		new int[4] { 1, 0, 1, 0 },
		new int[4] { 2, 3, 2, 3 },
		new int[4] { 3, 2, 3, 2 },
		new int[4] { 0, 3, 1, 2 },
		new int[4] { 1, 3, 0, 2 },
		new int[4] { 0, 2, 1, 3 },
		new int[4] { 1, 2, 0, 3 }
	};

	private void WalkPattern(int[][] patterns, int speed)
	{
		int num = RandomNumber.Between(1, patterns.Length) - 1;
		int[] array = patterns[num];
		foreach (int num2 in array)
		{
			DirectionKeys[num2](speed);
		}
	}

	public void Walk()
	{
		if (!Includes.ApplicationIsActivated() || Bot.Instance.Check.Stats)
		{
			return;
		}
		UIHelper.SetStatus($"Status: Walk | Cycle #{Bot.Instance.Status.WalkCycle + 1}");
		bool walkDirection = MainViewModel.Instance.Home.WalkDirection;
		if ((!Bot.Instance.Settings.SquaresWalkPattern && !Bot.Instance.Settings.RandomWalkPattern) || Bot.Instance.Settings.AutoWalkFish)
		{
			int[][] patterns = ((!walkDirection) ? new int[2][]
			{
				new int[4] { 2, 3, 2, 3 },
				new int[4] { 3, 2, 3, 2 }
			} : new int[2][]
			{
				new int[4] { 0, 1, 0, 1 },
				new int[4] { 1, 0, 1, 0 }
			});
			WalkPattern(patterns, BotSettings.Settings.WalkSpeed);
		}
		else if (!Bot.Instance.Settings.SquaresWalkPattern)
		{
			if (Bot.Instance.Settings.RandomWalkPattern)
			{
				WalkPattern(RandomPatterns, BotSettings.Settings.WalkSpeed);
			}
		}
		else
		{
			WalkPattern(SquarePatterns, Bot.Instance.Settings.WaitTimeVeryLong);
		}
		Bot.Instance.Actions.CheckWalkCycle();
	}

	public void Fish()
	{
		UIHelper.SetStatus("Status: Fish");
		if (Includes.ApplicationIsActivated())
		{
			InputKeyboard.PressHotkey(1, BotSettings.Settings.HoldTime);
			Bot.Instance.Sleep(BotSettings.Settings.WaitTime);
			Bot.Instance.Actions.SkipDialogueFish();
		}
	}

	public void SweetScent()
	{
		if (!Includes.ApplicationIsActivated())
		{
			return;
		}
		if (Bot.Instance.Check.SweetCent0)
		{
			if (Bot.Instance.Settings.AutoLeppa && Bot.Instance.Settings.SweetScent)
			{
				Bot.Instance.Actions.UseLeppa();
			}
			else if (!Bot.Instance.Settings.AlertSweetScent || Bot.Instance.Settings.AutoSweetScent)
			{
				if (Bot.Instance.Settings.AutoSweetScent && Bot.Instance.Status.GoBackOnce == 0)
				{
					Bot.Instance.Status.GoBack = true;
					Bot.Instance.Status.GoBackOnce++;
				}
			}
			else
			{
				Sounds.PlayNotificationSound();
			}
		}
		else if (Bot.Instance.Check.Walk)
		{
			InputKeyboard.PressHotkey(9, BotSettings.Settings.HoldTime);
			Bot.Instance.Sleep(BotSettings.Settings.WaitTime);
		}
	}

	private void AutoRouteAction(string route, bool isFishMode)
	{
		bool error;
		if (!string.IsNullOrEmpty(route))
		{
			if (!Includes.ApplicationIsActivated())
			{
				return;
			}
			if (Bot.Instance.Status.GoTo)
			{
				UIHelper.SetStatus("Status: Trying Route to Destination");
				if (Bot.Instance.Status.FirstAutoSSCycle)
				{
					Routes.Instance.UseTeleport();
					Bot.Instance.Status.FirstAutoSSCycle = false;
				}
				Routes.Instance.Router(route, "goto");
				Bot.Instance.Sleep(1500);
				Bot.Instance.Status.GoTo = false;
				return;
			}
			if (!Bot.Instance.Status.GoBack)
			{
				if (!Bot.Instance.Status.Heal)
				{
					if (isFishMode)
					{
						if (!route.Contains("Fish"))
						{
							Walk();
							return;
						}
						Fish();
						if (!Bot.Instance.Settings.Lure)
						{
							if (!Bot.Instance.Check.Error1 && !Bot.Instance.Check.Error2)
							{
								error = Bot.Instance.Check.Error3;
								goto IL_014b;
							}
						}
						else if (!Bot.Instance.Check.Error2)
						{
							error = Bot.Instance.Check.Error3;
							goto IL_014b;
						}
						goto IL_0150;
					}
					UIHelper.SetStatus("Status: SweetCent");
					SweetScent();
					Bot.Instance.Sleep(2500);
					if (Bot.Instance.Check.Error1 || Bot.Instance.Check.Error2)
					{
						Bot.Instance.Status.GoBack = true;
						Bot.Instance.Status.GoBackOnce++;
					}
					return;
				}
				UIHelper.SetStatus("Status: Trying Heal Pokemon");
				Routes.Instance.HealPokemon();
				if (!Bot.Instance.Check.Dialogue)
				{
					Bot.Instance.Status.Heal = false;
					Bot.Instance.Status.GoBack = false;
					Bot.Instance.Status.GoBackOnce = 0;
					Bot.Instance.Status.GoTo = true;
					Bot.Instance.Status.SelectedPokemon = 1;
				}
				return;
			}
			if (Bot.Instance.Settings.TeleportBack)
			{
				UIHelper.SetStatus("Status: Trying Teleport");
				Routes.Instance.Router(route, "teleportback");
				Bot.Instance.Sleep(1500);
			}
			else
			{
				UIHelper.SetStatus("Status: Trying Route to Origin");
				Routes.Instance.Router(route, "goback");
				Bot.Instance.Sleep(1500);
			}
			Bot.Instance.Status.GoBack = false;
			Bot.Instance.Status.Heal = true;
			return;
		}
		Bot.Instance.Stop();
		return;
		IL_014b:
		if (!error)
		{
			return;
		}
		goto IL_0150;
		IL_0150:
		Bot.Instance.Status.GoBack = true;
		Bot.Instance.Status.GoBackOnce++;
	}

	public void AutoWalkFish()
	{
		AutoRouteAction(Bot.Instance.Settings.SelectedAutoWalkFishRoute, isFishMode: true);
	}

	public void AutoSweetScent()
	{
		AutoRouteAction(Bot.Instance.Settings.SelectedAutoSweetScentRoute, isFishMode: false);
	}

	private void SafariAutoAction(string route, bool isFishMode)
	{
		if (string.IsNullOrEmpty(route) || !Includes.ApplicationIsActivated())
		{
			return;
		}
		if (!Bot.Instance.Status.GoTo)
		{
			if (!Bot.Instance.Status.GoBack)
			{
				if (isFishMode)
				{
					Fish();
					if (Bot.Instance.Check.Error3)
					{
						Walk();
					}
				}
				else
				{
					if (Bot.Instance.Check.Repel)
					{
						InputKeyboard.PressKeyB(150);
					}
					Walk();
				}
				if (Bot.Instance.Check.SafariTimeOver)
				{
					UIHelper.SetStatus("Status: GoBack");
					InputKeyboard.PressKeyA(5000);
					Bot.Instance.Status.GoBack = true;
					Bot.Instance.Status.GoBackOnce++;
				}
			}
			else
			{
				UIHelper.SetStatus("Status: GoBack");
				Bot.Instance.Status.GoBack = false;
				Bot.Instance.Status.GoBackOnce = 0;
				Bot.Instance.Status.GoTo = true;
			}
		}
		else
		{
			UIHelper.SetStatus("Status: GoTo");
			Routes.Instance.Router(route, "goto");
			Bot.Instance.Sleep(1500);
			Bot.Instance.Status.GoTo = false;
		}
	}

	public void SafariAutoWalk()
	{
		SafariAutoAction(Bot.Instance.Settings.SelectedSafariAutoWalkRoute, isFishMode: false);
	}

	public void SafariAutoFish()
	{
		SafariAutoAction(Bot.Instance.Settings.SelectedSafariAutoFishRoute, isFishMode: true);
	}
}
