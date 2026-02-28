using System;
using PokeMMO_.Classes;
using PokeMMO_.Input;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Botting;

public class Battle
{
	private const string IMG = "bin/img/";

	private readonly Search search = new Search();

	private static readonly int[][] MoveButtonCoords = new int[4][]
	{
		new int[4] { 400, 700, 305, 440 },
		new int[4] { 600, 700, 505, 440 },
		new int[4] { 400, 750, 305, 495 },
		new int[4] { 600, 750, 495, 495 }
	};

	private void ClickMoveButton(int moveIndex)
	{
		int[] array = MoveButtonCoords[moveIndex];
		InputMouse.LeftClickHdSd(array[0], array[1], array[2], array[3]);
	}

	private void ClickMoveButtonTwice(int moveIndex)
	{
		ClickMoveButton(moveIndex);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		ClickMoveButton(moveIndex);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
	}

	public void Shiny()
	{
		Includes.WindowHelper.BringProcessToFront();
		if (!Bot.Instance.Status.ShinyHelper)
		{
			Bot.Instance.Status.ShinyHelper = true;
			Bot.Instance.Status.ShinyCounter++;
			DiscordBot.Instance.SendMessage("Shiny", embed: true);
		}
		Sounds.PlayShinySound();
		Includes.WindowHelper.BringProcessToFront();
		Bot.Instance.Sleep(250);
		if (Bot.Instance.Settings.StopOnShiny || Bot.Instance.Check.Horde)
		{
			Bot.Instance.Actions.StayAFKBattle();
		}
		else
		{
			Bot.Instance.Actions.ThrowBall();
		}
	}

	public void Fight(IntPtr h, Action<IntPtr, int[]> specificLogic)
	{
		int[] array = search.UseImageSearch("bin/img/Fight.png", 50);
		if (array != null)
		{
			Bot.Instance.Actions.ResetAndUpdateWalkCycle();
			Bot.Instance.Status.IsInFight = true;
			Bot.Instance.Actions.Potion();
			if (!Bot.Instance.Check.CheckShiny())
			{
				specificLogic(h, array);
			}
			else
			{
				Shiny();
			}
		}
		if (Bot.Instance.Check.NextPoke)
		{
			Bot.Instance.Actions.NextPoke();
		}
	}

	public void Run(IntPtr h)
	{
		int[] array = search.UseImageSearch("bin/img/Run.png", 50);
		if (array != null)
		{
			Bot.Instance.Actions.ResetAndUpdateWalkCycle();
			Bot.Instance.Status.IsInFight = true;
			if (!Bot.Instance.Check.CheckShiny())
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime * 2);
			}
			else
			{
				Shiny();
			}
		}
		if (Bot.Instance.Check.CantRun)
		{
			Bot.Instance.Sleep(2000);
			Fight(h, delegate(IntPtr handle, int[] fightCoords)
			{
				FightNoPokemonCheck(handle, fightCoords);
			});
		}
		if (Bot.Instance.Check.NextPoke)
		{
			Bot.Instance.Actions.NextPoke();
		}
	}

	public void Catch(IntPtr h)
	{
		int[] array = search.UseImageSearch("bin/img/Bag.png", 50);
		if (array != null)
		{
			Bot.Instance.Actions.ResetAndUpdateWalkCycle();
			Bot.Instance.Status.IsInFight = true;
			Bot.Instance.Actions.Potion();
			if (!Bot.Instance.Check.CheckShiny())
			{
				if (Bot.Instance.Check.Horde)
				{
					Run(h);
				}
				else
				{
					CatchPokemonCheck(h);
				}
			}
			else
			{
				Shiny();
			}
		}
		if (Bot.Instance.Check.NextPoke)
		{
			Bot.Instance.Actions.NextPoke();
		}
	}

	private void PayDayMoveBlock(int moveIndex, Func<bool> ppIsZero, int[] fightCoords)
	{
		InputMouse.LeftClick(fightCoords[1], fightCoords[2]);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
		int[] array = search.UseImageSearch("bin/img/Back.png", 50);
		if (array == null)
		{
			return;
		}
		Bot.Instance.Actions.ScanMovePPStatus();
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		if (Bot.Instance.Settings.AutoWalkFish && ppIsZero())
		{
			if (Bot.Instance.Settings.MorePayDay)
			{
				if (Bot.Instance.Status.SelectedPokemonManual != 6)
				{
					InputMouse.LeftClick(array[1], array[2]);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
					int[] array2 = search.UseImageSearch("bin/img/Level.png", 50);
					if (array2 != null && Includes.ApplicationIsActivated())
					{
						InputMouse.LeftClick(array2[1], array2[2]);
						Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
						Bot.Instance.Actions.NextPokeManual();
					}
				}
				else
				{
					Bot.Instance.Actions.RunandGoBack();
				}
			}
			else
			{
				Bot.Instance.Actions.RunandGoBack();
			}
		}
		else if (!ppIsZero())
		{
			ClickMoveButtonTwice(moveIndex);
			if (moveIndex == 0)
			{
				DiscordBot.Instance.SendMessage("PayDay", embed: true);
			}
			if (moveIndex == 0 && Bot.Instance.Settings.PayDayMultiTarget)
			{
				Bot.Instance.Status.UsedPayDay = true;
			}
		}
		else
		{
			InputMouse.LeftClick(array[1], array[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			int[] array3 = search.UseImageSearch("bin/img/Level.png", 50);
			if (array3 != null && Includes.ApplicationIsActivated())
			{
				InputMouse.LeftClick(array3[1], array3[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				Bot.Instance.Actions.NextPokeManual();
			}
		}
		Bot.Instance.Actions.ResetMovePPStatus();
	}

	public void PayDay(IntPtr h)
	{
		if (Bot.Instance.Settings.BotMode != BotMode.PayDayThiefMixed)
		{
			int[] array = search.UseImageSearch("bin/img/Fight.png", 50);
			if (array != null)
			{
				Bot.Instance.Actions.ResetAndUpdateWalkCycle();
				Bot.Instance.Status.IsInFight = true;
				Bot.Instance.Actions.Potion();
				if (!Bot.Instance.Check.CheckShiny())
				{
					if (!Bot.Instance.Status.UsedPayDay)
					{
						PayDayMoveBlock(0, () => Bot.Instance.Status.FirstMovePP0, array);
					}
					else
					{
						PayDayMoveBlock(1, () => Bot.Instance.Status.SecondMovePP0, array);
					}
				}
				else
				{
					Shiny();
				}
			}
		}
		else if (!Bot.Instance.Status.Changed)
		{
			int[] array2 = search.UseImageSearch("bin/img/Level.png", 50);
			if (array2 != null && Includes.ApplicationIsActivated())
			{
				InputMouse.LeftClick(array2[1], array2[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				Bot.Instance.Actions.NextPokeManual();
				Bot.Instance.Status.Changed = true;
			}
		}
		else
		{
			int[] array3 = search.UseImageSearch("bin/img/Fight.png", 50);
			if (array3 != null)
			{
				Bot.Instance.Actions.ResetAndUpdateWalkCycle();
				Bot.Instance.Status.IsInFight = true;
				Bot.Instance.Actions.Potion();
				if (!Bot.Instance.Check.CheckShiny())
				{
					if (Bot.Instance.Status.UsedPayDay)
					{
						PayDayMoveBlock(1, () => Bot.Instance.Status.SecondMovePP0, array3);
					}
					else
					{
						PayDayMoveBlock(0, () => Bot.Instance.Status.FirstMovePP0, array3);
					}
				}
				else
				{
					Shiny();
				}
			}
		}
		if (Bot.Instance.Check.NextPoke)
		{
			Bot.Instance.Actions.NextPoke();
		}
	}

	public void Thief(IntPtr h)
	{
		int[] array = search.UseImageSearch("bin/img/Battle.png", 50);
		if (array != null)
		{
			Bot.Instance.Actions.ResetAndUpdateWalkCycle();
			Bot.Instance.Status.IsInFight = true;
			Bot.Instance.Actions.ScanPokemonItemStatus();
			if (!Bot.Instance.Check.CheckShiny())
			{
				ThiefPokemonCheck(h);
			}
			else
			{
				Shiny();
			}
		}
		if (Bot.Instance.Check.NextPoke)
		{
			Bot.Instance.Actions.NextPoke();
		}
	}

	public void ThiefWithOutPokemonCheck(IntPtr h)
	{
		int[] array = search.UseImageSearch("bin/img/Battle.png", 50);
		if (array != null)
		{
			Bot.Instance.Actions.ResetAndUpdateWalkCycle();
			Bot.Instance.Status.IsInFight = true;
			Bot.Instance.Actions.ScanPokemonItemStatus();
			if (Bot.Instance.Check.CheckShiny())
			{
				Shiny();
			}
			else
			{
				ThiefMechanics(h);
			}
		}
		if (Bot.Instance.Check.NextPoke)
		{
			Bot.Instance.Actions.NextPoke();
		}
	}

	public void Safari(IntPtr h)
	{
		int[] array = search.UseImageSearch("bin/img/SafariC.png", 50);
		if (array != null)
		{
			Bot.Instance.Actions.ResetAndUpdateWalkCycle();
			Bot.Instance.Status.IsInFight = true;
			if (!Bot.Instance.Check.CheckShiny())
			{
				CatchPokemonCheck(h);
			}
			else
			{
				Shiny();
			}
		}
	}

	public void FightNoPokemonCheck(IntPtr h, int[] fightCoords)
	{
		if (!Bot.Instance.Settings.LevelFirst)
		{
			InputMouse.LeftClick(fightCoords[1], fightCoords[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		}
		else if (Bot.Instance.Status.Changed)
		{
			InputMouse.LeftClick(fightCoords[1], fightCoords[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		}
		else
		{
			int[] array = search.UseImageSearch("bin/img/Level.png", 50);
			if (array != null && Includes.ApplicationIsActivated())
			{
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				Bot.Instance.Actions.NextPoke();
				Bot.Instance.Status.Changed = true;
			}
		}
		int[] array2 = search.UseImageSearch("bin/img/Struggle.png", 50);
		if (array2 != null)
		{
			array2 = search.UseImageSearch("bin/img/Back.png", 50);
			if (array2 != null)
			{
				InputMouse.LeftClick(array2[1], array2[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
				array2 = search.UseImageSearch("bin/img/Level.png", 50);
				if (array2 != null)
				{
					InputMouse.LeftClick(array2[1], array2[2]);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
					Bot.Instance.Actions.NextPoke();
				}
			}
			return;
		}
		if (!Bot.Instance.Settings.MultiTarget || (!Bot.Instance.Settings.AutoWalkFish && !Bot.Instance.Settings.AutoSweetScent && !Bot.Instance.Settings.SweetScent) || Bot.Instance.Status.MoveDisabled)
		{
			array2 = search.UseImageSearch("bin/img/Super.png", 50);
			if (array2 != null && !Bot.Instance.Check.PPSuperEffective0 && !Bot.Instance.Status.MoveDisabled)
			{
				InputMouse.LeftClick(array2[1], array2[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				ClickMoveButton(0);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			}
			array2 = search.UseImageSearch("bin/img/Effective.png", 50);
			if (array2 != null && !Bot.Instance.Check.PPEffective0 && !Bot.Instance.Status.MoveDisabled)
			{
				InputMouse.LeftClick(array2[1], array2[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				ClickMoveButton(0);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			}
			array2 = search.UseImageSearch("bin/img/Back.png", 50);
			if (array2 == null || Bot.Instance.Check.NextPoke)
			{
				return;
			}
			do
			{
				Bot.Instance.Status.AttackMove = RandomNumber.Between(1, 4);
			}
			while (Bot.Instance.Status.AttackMove == Bot.Instance.Status.LastAttackMove);
			Bot.Instance.Status.LastAttackMove = Bot.Instance.Status.AttackMove;
			Bot.Instance.Actions.ScanMovePPStatus();
			if (Bot.Instance.Settings.AutoWalkFish && (Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 || Bot.Instance.Status.FourthMovePP0))
			{
				Bot.Instance.Actions.RunandGoBack();
			}
			else
			{
				int attackMove = Bot.Instance.Status.AttackMove;
				bool[] array3 = new bool[4]
				{
					Bot.Instance.Status.FirstMovePP0,
					Bot.Instance.Status.SecondMovePP0,
					Bot.Instance.Status.ThirdMovePP0,
					Bot.Instance.Status.FourthMovePP0
				};
				if (attackMove >= 1 && attackMove <= 4 && !array3[attackMove - 1])
				{
					ClickMoveButton(attackMove - 1);
				}
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				ClickMoveButton(0);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			}
			Bot.Instance.Actions.ResetMovePPStatus();
			return;
		}
		array2 = search.UseImageSearch("bin/img/Back.png", 50);
		if (array2 == null || Bot.Instance.Check.NextPoke)
		{
			return;
		}
		Bot.Instance.Actions.ScanMovePPStatus();
		if (Bot.Instance.Settings.AutoWalkFish && (Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 || Bot.Instance.Status.FourthMovePP0))
		{
			Bot.Instance.Actions.RunandGoBack();
		}
		else if (!Bot.Instance.Status.FirstMovePP0)
		{
			ClickMoveButtonTwice(0);
		}
		else
		{
			array2 = search.UseImageSearch("bin/img/Back.png", 50);
			if (array2 != null)
			{
				InputMouse.LeftClick(array2[1], array2[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
				array2 = search.UseImageSearch("bin/img/Level.png", 50);
				if (array2 != null)
				{
					InputMouse.LeftClick(array2[1], array2[2]);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeVeryLong);
					Bot.Instance.Actions.NextPoke();
				}
			}
		}
		Bot.Instance.Actions.ResetMovePPStatus();
	}

	public void ThiefMechanics(IntPtr h)
	{
		if (!Bot.Instance.Status.DetectedItem || Bot.Instance.Check.ThiefPokemonItem)
		{
			if (!Bot.Instance.Status.DetectedItem || !Bot.Instance.Check.ThiefPokemonItem || !Bot.Instance.Settings.MoreThief)
			{
				if (Bot.Instance.Settings.BotMode != BotMode.PayDayThiefMixed)
				{
					Run(h);
				}
				else
				{
					PayDay(h);
				}
				return;
			}
			if (Bot.Instance.Status.ThiefHelper)
			{
				int[] array = search.UseImageSearch("bin/img/Fight.png", 50);
				if (array == null)
				{
					return;
				}
				Bot.Instance.Actions.Potion();
				InputMouse.LeftClick(array[1], array[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTimeLong);
				int[] array2 = search.UseImageSearch("bin/img/Back.png", 50);
				if (array2 == null)
				{
					return;
				}
				Bot.Instance.Actions.ScanMovePPStatus();
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				if (!Bot.Instance.Settings.AutoWalkFish || (!Bot.Instance.Status.FirstMovePP0 && !Bot.Instance.Status.SecondMovePP0 && !Bot.Instance.Status.ThirdMovePP0 && !Bot.Instance.Status.FourthMovePP0))
				{
					if (!Bot.Instance.Status.ImprisonHelper && Bot.Instance.Settings.Imprison && !Bot.Instance.Status.SecondMovePP0)
					{
						InputMouse.LeftClickHdSd(620, 700, 530, 450);
						Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
						InputMouse.LeftClickHdSd(630, 750, 530, 500);
						Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
						Bot.Instance.Status.ImprisonHelper = true;
					}
					else if (Bot.Instance.Status.FirstMovePP0)
					{
						InputMouse.LeftClick(array2[1], array2[2]);
						Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
						int[] array3 = search.UseImageSearch("bin/img/Level.png", 50);
						if (array3 != null && !Bot.Instance.Status.Changed && Includes.ApplicationIsActivated())
						{
							InputMouse.LeftClick(array3[1], array3[2]);
							Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
							Bot.Instance.Actions.NextPokeManual();
						}
					}
					else
					{
						ClickMoveButtonTwice(0);
					}
				}
				else
				{
					Bot.Instance.Actions.RunandGoBack();
				}
				Bot.Instance.Actions.ResetMovePPStatus();
				return;
			}
			int[] array4 = search.UseImageSearch("bin/img/Fight.png", 50);
			if (array4 == null)
			{
				return;
			}
			Bot.Instance.Actions.Potion();
			InputMouse.LeftClick(array4[1], array4[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			int[] array5 = search.UseImageSearch("bin/img/Back.png", 50);
			if (array5 != null)
			{
				InputMouse.LeftClick(array5[1], array5[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				int[] array6 = search.UseImageSearch("bin/img/Level.png", 50);
				if (array6 != null && !Bot.Instance.Status.Changed && Includes.ApplicationIsActivated())
				{
					InputMouse.LeftClick(array6[1], array6[2]);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
					Bot.Instance.Actions.NextPokeManual();
					Bot.Instance.Status.ThiefHelper = true;
				}
			}
			return;
		}
		int[] array7 = search.UseImageSearch("bin/img/Fight.png", 50);
		if (array7 == null)
		{
			return;
		}
		Bot.Instance.Actions.Potion();
		InputMouse.LeftClick(array7[1], array7[2]);
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		int[] array8 = search.UseImageSearch("bin/img/Back.png", 50);
		if (array8 == null)
		{
			return;
		}
		Bot.Instance.Actions.ScanMovePPStatus();
		Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
		if (Bot.Instance.Settings.AutoWalkFish && (Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 || Bot.Instance.Status.FourthMovePP0))
		{
			Bot.Instance.Actions.RunandGoBack();
		}
		else if (Bot.Instance.Status.ImprisonHelper || !Bot.Instance.Settings.Imprison || Bot.Instance.Status.SecondMovePP0)
		{
			if (!Bot.Instance.Status.FirstMovePP0)
			{
				ClickMoveButtonTwice(0);
			}
			else
			{
				InputMouse.LeftClick(array8[1], array8[2]);
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				int[] array9 = search.UseImageSearch("bin/img/Level.png", 50);
				if (array9 != null && !Bot.Instance.Status.Changed && Includes.ApplicationIsActivated())
				{
					InputMouse.LeftClick(array9[1], array9[2]);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
					Bot.Instance.Actions.NextPokeManual();
				}
			}
		}
		else
		{
			InputMouse.LeftClickHdSd(620, 700, 530, 450);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			InputMouse.LeftClickHdSd(630, 750, 530, 500);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			Bot.Instance.Status.ImprisonHelper = true;
		}
		Bot.Instance.Actions.ResetMovePPStatus();
	}

	public void PayDayCatchMixed(IntPtr h)
	{
		DispatchByPokemonType(h, delegate
		{
			PayDay(h);
		}, delegate
		{
			Bot.Instance.Actions.Catch();
		}, delegate
		{
			Bot.Instance.Actions.Catch();
		}, delegate
		{
			PayDay(h);
		});
	}

	public void PayDayThiefMixed(IntPtr h)
	{
		DispatchByPokemonType(h, delegate
		{
			ThiefWithOutPokemonCheck(h);
		}, delegate
		{
			ThiefWithOutPokemonCheck(h);
		}, delegate
		{
			ThiefWithOutPokemonCheck(h);
		}, delegate
		{
			PayDay(h);
		});
	}

	public void FightPokemonCheck(IntPtr h, int[] fightCoords)
	{
		DispatchByPokemonType(h, delegate
		{
			FightNoPokemonCheck(h, fightCoords);
		}, delegate
		{
			FightNoPokemonCheck(h, fightCoords);
		}, delegate
		{
			FightNoPokemonCheck(h, fightCoords);
		}, delegate
		{
			Run(h);
		});
	}

	public void CatchPokemonCheck(IntPtr h)
	{
		DispatchByPokemonType(h, delegate
		{
			Bot.Instance.Actions.Catch();
		}, delegate
		{
			Bot.Instance.Actions.Catch();
		}, delegate
		{
			Bot.Instance.Actions.Catch();
		}, delegate
		{
			Run(h);
		});
	}

	public void ThiefPokemonCheck(IntPtr h)
	{
		DispatchByPokemonType(h, delegate
		{
			ThiefMechanics(h);
		}, delegate
		{
			ThiefMechanics(h);
		}, delegate
		{
			ThiefMechanics(h);
		}, delegate
		{
			Run(h);
		});
	}

	public void Pickpocket(IntPtr h)
	{
		int[] array = search.UseImageSearch("bin/img/Battle.png", 50);
		if (array != null)
		{
			Bot.Instance.Actions.ResetAndUpdateWalkCycle();
			Bot.Instance.Status.IsInFight = true;
			Bot.Instance.Actions.ScanPokemonItemStatus();
			if (Bot.Instance.Check.CheckShiny())
			{
				Shiny();
			}
			else
			{
				PickpocketPokemonCheck(h);
			}
		}
		if (Bot.Instance.Check.NextPoke)
		{
			Bot.Instance.Actions.NextPoke();
		}
	}

	public void PickpocketPokemonCheck(IntPtr h)
	{
		DispatchByPokemonType(h, delegate
		{
			PickpocketMechanics(h);
		}, delegate
		{
			PickpocketMechanics(h);
		}, delegate
		{
			PickpocketMechanics(h);
		}, delegate
		{
			Run(h);
		});
	}

	public void PickpocketMechanics(IntPtr h)
	{
		if (Bot.Instance.Status.DetectedItem)
		{
			if (Bot.Instance.Status.ImprisonHelper)
			{
				if (Bot.Instance.Status.ThiefHelper)
				{
					Run(h);
					return;
				}
				int[] array = search.UseImageSearch("bin/img/Level.png", 50);
				if (array != null && Includes.ApplicationIsActivated())
				{
					InputMouse.LeftClick(array[1], array[2]);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
					Bot.Instance.Actions.NextPokeManual();
					Bot.Instance.Status.ThiefHelper = true;
				}
				return;
			}
			int[] array2 = search.UseImageSearch("bin/img/Fight.png", 50);
			if (array2 == null)
			{
				return;
			}
			InputMouse.LeftClick(array2[1], array2[2]);
			Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
			int[] array3 = search.UseImageSearch("bin/img/Back.png", 50);
			if (array3 != null)
			{
				Bot.Instance.Actions.ScanMovePPStatus();
				Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
				if (Bot.Instance.Settings.AutoWalkFish && (Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 || Bot.Instance.Status.FourthMovePP0))
				{
					Bot.Instance.Actions.RunandGoBack();
				}
				else if (!Bot.Instance.Status.SecondMovePP0)
				{
					InputMouse.LeftClickHdSd(620, 700, 530, 450);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
					InputMouse.LeftClickHdSd(630, 750, 530, 500);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
					Bot.Instance.Status.ImprisonHelper = true;
				}
				else
				{
					InputMouse.LeftClick(array3[1], array3[2]);
					Bot.Instance.Sleep(Bot.Instance.Settings.WaitTime);
					Run(h);
				}
				Bot.Instance.Actions.ResetMovePPStatus();
			}
		}
		else
		{
			Run(h);
		}
	}

	private void DispatchByPokemonType(IntPtr h, Action onAll, Action onUncaught, Action onSelected, Action onDefault)
	{
		string catchPokemon = MainViewModel.Instance.Home.CatchPokemon;
		if (!(catchPokemon == Pokemon.All.ToString()))
		{
			if (catchPokemon == Pokemon.Uncaught.ToString())
			{
				if (!Bot.Instance.Check.Catched)
				{
					onUncaught();
				}
				else
				{
					onDefault();
				}
			}
			else if (!(catchPokemon == Bot.Instance.Check.CheckSelectedPokemon()))
			{
				onDefault();
			}
			else
			{
				Bot.Instance.Actions.SelectedCatchPokemonCounter();
				onSelected();
			}
		}
		else
		{
			onAll();
		}
	}
}
