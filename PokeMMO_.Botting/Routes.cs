using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows;
using PokeMMO_.Classes;
using PokeMMO_.Input;
using PokeMMO_.Model;

namespace PokeMMO_.Botting;

public class Routes
{
	private static readonly object padlock = new object();

	private static Routes instance = null;

	private static readonly (string region, int slow, int normal)[] HealTimings = new(string, int, int)[5]
	{
		("Kanto", 6000, 5500),
		("Johto", 6000, 5500),
		("Hoenn", 6100, 5600),
		("Sinnoh", 10000, 9500),
		("Unova", 10000, 9500)
	};

	private static readonly (string region, int slow, int normal)[] BumpTimings = new(string, int, int)[5]
	{
		("Kanto", 1500, 1200),
		("Johto", 1700, 1200),
		("Hoenn", 1700, 1200),
		("Sinnoh", 1700, 1200),
		("Unova", 1700, 1200)
	};

	private static readonly (string region, int slow, int normal)[] EntranceTimings = new(string, int, int)[5]
	{
		("Kanto", 3000, 2000),
		("Johto", 3500, 2500),
		("Hoenn", 3500, 2500),
		("Sinnoh", 3500, 2500),
		("Unova", 4000, 3000)
	};

	private static readonly Dictionary<string, Action<int>> DirectionKeys = new Dictionary<string, Action<int>>
	{
		{
			"left",
			InputKeyboard.PressKeyLeft
		},
		{
			"right",
			InputKeyboard.PressKeyRight
		},
		{
			"up",
			InputKeyboard.PressKeyUp
		},
		{
			"down",
			InputKeyboard.PressKeyDown
		}
	};

	public static Routes Instance
	{
		get
		{
			lock (padlock)
			{
				if (instance == null)
				{
					instance = new Routes();
				}
				return instance;
			}
		}
	}

	public string GetSelectedRoute()
	{
		BotSettings settings = Bot.Instance.Settings;
		bool flag = settings.BotMode == BotMode.Safari;
		if (!settings.AutoWalkFish || flag)
		{
			if (settings.AutoSweetScent && !flag)
			{
				return settings.SelectedAutoSweetScentRoute;
			}
			if (!(settings.SafariAutoWalk && flag))
			{
				if (!(settings.SafariAutoFish && flag))
				{
					return "";
				}
				return settings.SelectedSafariAutoFishRoute;
			}
			return settings.SelectedSafariAutoWalkRoute;
		}
		return settings.SelectedAutoWalkFishRoute;
	}

	public void Router(string route, string action)
	{
		MethodInfo method = GetType().GetMethod(route, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[1] { typeof(string) }, null);
		if (method != null)
		{
			method.Invoke(this, new object[1] { action });
		}
		else
		{
			Bot.Instance.Stop();
			TopMostMessageBox.Show("This Route is only for PREMIUM Users!", "PREMIUM", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
		}
	}

	private void RegionTimedAction(Action<int> action, (string region, int slow, int normal)[] timings)
	{
		string selectedRoute = GetSelectedRoute();
		for (int i = 0; i < timings.Length; i++)
		{
			(string, int, int) tuple = timings[i];
			if (selectedRoute.Contains(tuple.Item1) && Includes.ApplicationIsActivated())
			{
				action(Bot.Instance.Settings.SlowMode ? tuple.Item2 : tuple.Item3);
				break;
			}
		}
	}

	public void HealPokemon()
	{
		RegionTimedAction(delegate(int t)
		{
			InputKeyboard.PressKeyA(t);
		}, HealTimings);
	}

	public void WaitAfterBump()
	{
		RegionTimedAction(delegate(int t)
		{
			Thread.Sleep(t);
		}, BumpTimings);
	}

	public void WaitAfterEntrace()
	{
		RegionTimedAction(delegate(int t)
		{
			Thread.Sleep(t);
		}, EntranceTimings);
	}

	public void UseSurfer()
	{
		string selectedRoute = GetSelectedRoute();
		if ((selectedRoute.Contains("Kanto") || selectedRoute.Contains("Hoenn") || selectedRoute.Contains("Sinnoh") || selectedRoute.Contains("Unova")) && Includes.ApplicationIsActivated())
		{
			Thread.Sleep(100);
			InputKeyboard.PressKeyA(2800);
			Thread.Sleep(1800);
		}
	}

	public void WaitBeforeTurn()
	{
		Bot.Instance.Actions.HumanizeActions();
		if (Includes.ApplicationIsActivated())
		{
			Thread.Sleep(300);
		}
	}

	private void UseHotkeyItem(Action<int> hotkeyPress, int holdTime = 100, int sleepTime = 250)
	{
		if (Includes.ApplicationIsActivated())
		{
			hotkeyPress(holdTime);
			Thread.Sleep(sleepTime);
		}
	}

	public void UseBike()
	{
		UseHotkeyItem(delegate(int h)
		{
			InputKeyboard.PressHotkey(2, h);
		});
	}

	public void UseRepel()
	{
		UseHotkeyItem(delegate(int h)
		{
			InputKeyboard.PressHotkey(5, h);
		});
	}

	public void UseDefog()
	{
		UseHotkeyItem(delegate(int h)
		{
			InputKeyboard.PressHotkey(7, h);
		});
	}

	public void UseCut()
	{
		if (Includes.ApplicationIsActivated())
		{
			InputKeyboard.PressKeyA(3300);
			Thread.Sleep(1800);
		}
	}

	public void UseEscapeRope()
	{
		if (Includes.ApplicationIsActivated() && Bot.Instance.Settings.EscapeRope)
		{
			InputKeyboard.PressHotkey(3, Bot.Instance.Settings.HoldTime);
			WaitAfterTeleport();
		}
	}

	public void UseDig()
	{
		if (Includes.ApplicationIsActivated())
		{
			InputKeyboard.PressHotkey(3, Bot.Instance.Settings.HoldTime);
			WaitAfterTeleport();
		}
	}

	public void UseTeleport()
	{
		if (Includes.ApplicationIsActivated())
		{
			InputKeyboard.PressHotkey(8, Bot.Instance.Settings.HoldTime);
			WaitAfterTeleport();
		}
	}

	public void WaitAfterTeleport()
	{
		if (Includes.ApplicationIsActivated())
		{
			Thread.Sleep(Bot.Instance.Settings.SlowMode ? 5000 : 4000);
		}
	}

	public void Unova_Icirrus_City_Fish(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				InputKeyboard.PressKeyDown(2000);
				WaitAfterEntrace();
				UseTeleport();
			}
			return;
		}
		InputKeyboard.PressKeyDown(1450);
		WaitAfterEntrace();
		UseBike();
		InputKeyboard.PressKeyLeft(400);
		InputKeyboard.PressKeyUp(2700);
		InputKeyboard.PressKeyRight(200);
		InputKeyboard.PressKeyUp(2100);
		InputKeyboard.PressKeyLeft(200);
		InputKeyboard.PressKeyUp(700);
		WaitAfterEntrace();
		InputKeyboard.PressKeyUp(700);
		InputKeyboard.PressKeyRight(200);
	}

	public void Unova_Undella_Town(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			InputKeyboard.PressKeyLeft(1000);
			InputKeyboard.PressKeyDown(1000);
			InputKeyboard.PressKeyRight(3700);
			InputKeyboard.PressKeyUp(4000);
			InputKeyboard.PressKeyRight(200);
			UseSurfer();
			InputKeyboard.PressKeyRight(300);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Undella_Town_Fish(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			InputKeyboard.PressKeyLeft(1000);
			InputKeyboard.PressKeyDown(1000);
			InputKeyboard.PressKeyRight(3700);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Hoenn_Ever_Grande_City_1(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				InputKeyboard.PressKeyDown(250);
				WaitAfterEntrace();
				UseEscapeRope();
				UseTeleport();
			}
		}
		else
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(250);
			InputKeyboard.PressKeyLeft(750);
			InputKeyboard.PressKeyUp(1000);
			InputKeyboard.PressKeyLeft(150);
			InputKeyboard.PressKeyUp(300);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(50);
		}
	}

	public void Hoenn_Ever_Grande_City_2_Fish(string action)
	{
		switch (action)
		{
		case "teleportback":
			UseTeleport();
			break;
		case "goback":
			UseEscapeRope();
			UseTeleport();
			break;
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(250);
			InputKeyboard.PressKeyLeft(750);
			InputKeyboard.PressKeyDown(800);
			break;
		}
	}

	public void Kanto_Route_10(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(1000);
			InputKeyboard.PressKeyLeft(1000);
			InputKeyboard.PressKeyUp(1000);
			InputKeyboard.PressKeyRight(600);
			InputKeyboard.PressKeyUp(150);
			WaitAfterEntrace();
			if (Bot.Instance.Settings.AutoWalkFish)
			{
				InputKeyboard.PressKeyDown(300);
			}
			else
			{
				InputKeyboard.PressKeyDown(150);
			}
		}
		else if (action == "goback" || action == "teleportback")
		{
			if (Bot.Instance.Settings.AutoWalkFish)
			{
				InputKeyboard.PressHotkey(3, Bot.Instance.Settings.HoldTime);
			}
			else
			{
				InputKeyboard.PressKeyUp(150);
				WaitAfterEntrace();
			}
			UseTeleport();
		}
	}

	public void Hoenn_Battle_Frontier(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(300);
			InputKeyboard.PressKeyRight(2500);
			InputKeyboard.PressKeyDown(300);
			InputKeyboard.PressKeyRight(500);
			InputKeyboard.PressKeyDown(300);
			UseSurfer();
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Kanto_Celadon_City(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(2000);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Kanto_Safari_Fish_1(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyUp(150);
			InputKeyboard.PressKeyA(7500);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1600);
			InputKeyboard.PressKeyRight(1200);
			InputKeyboard.PressKeyUp(400);
		}
	}

	public void Kanto_Safari_Walk_1(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyUp(150);
			InputKeyboard.PressKeyA(7500);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(150);
			InputKeyboard.PressKeyLeft(1650);
			InputKeyboard.PressKeyUp(500);
		}
	}

	public void Hoenn_Safari_Template()
	{
		InputKeyboard.PressKeyDown(150);
		InputKeyboard.PressKeyLeft(1250);
		InputKeyboard.PressKeyUp(1800);
		InputKeyboard.PressKeyRight(3200);
		InputKeyboard.PressKeyUp(400);
		InputKeyboard.PressKeyRight(650);
	}

	public void Hoenn_Safari_Walk_1(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyLeft(150);
			InputKeyboard.PressKeyA(9000);
			WaitAfterEntrace();
			UseBike();
			Hoenn_Safari_Template();
			InputKeyboard.PressKeyDown(750);
		}
	}

	public void RepelWalk(string direction, int holdtime)
	{
		if (!DirectionKeys.TryGetValue(direction, out var value) || !Includes.ApplicationIsActivated())
		{
			return;
		}
		for (int i = 0; i < holdtime; i += 50)
		{
			value(50);
			Thread.Sleep(250);
			if (Bot.Instance.Check.Repel)
			{
				InputKeyboard.PressKeyA(150);
			}
		}
	}

	public void Hoenn_Safari_Walk_2_Repel(string action)
	{
		if (action == "goto")
		{
			if (Bot.Instance.Check.Repel)
			{
				InputKeyboard.PressKeyA(150);
			}
			RepelWalk("left", 100);
			InputKeyboard.PressKeyA(9000);
			WaitAfterEntrace();
			if (Bot.Instance.Check.Repel)
			{
				InputKeyboard.PressKeyA(150);
			}
			UseBike();
			RepelWalk("down", 100);
			RepelWalk("left", 800);
			RepelWalk("up", 1100);
			RepelWalk("right", 2000);
			RepelWalk("up", 250);
			RepelWalk("right", 550);
			RepelWalk("down", 250);
			RepelWalk("right", 50);
			UseRepel();
			UseSurfer();
			RepelWalk("down", 100);
			WaitAfterBump();
			RepelWalk("down", 200);
		}
		else if (action == "goto2")
		{
			InputKeyboard.PressKeyLeft(150);
			InputKeyboard.PressKeyA(9000);
			WaitAfterEntrace();
			UseBike();
			Hoenn_Safari_Template();
			InputKeyboard.PressKeyRight(200);
			InputKeyboard.PressKeyDown(300);
			InputKeyboard.PressKeyRight(50);
			UseSurfer();
			InputKeyboard.PressKeyDown(150);
			WaitAfterBump();
			InputKeyboard.PressKeyDown(200);
		}
	}

	public void Hoenn_Safari_Walk_3(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyLeft(150);
			InputKeyboard.PressKeyA(9000);
			WaitAfterEntrace();
			UseBike();
			Hoenn_Safari_Template();
			InputKeyboard.PressKeyRight(200);
			InputKeyboard.PressKeyUp(1000);
			InputKeyboard.PressKeyRight(800);
			InputKeyboard.PressKeyUp(1000);
			InputKeyboard.PressKeyLeft(700);
			InputKeyboard.PressKeyUp(1100);
			InputKeyboard.PressKeyRight(650);
			InputKeyboard.PressKeyDown(500);
		}
	}

	public void Kanto_Island_One(string action)
	{
		if (action == "goto")
		{
			if (Bot.Instance.Check.Repel)
			{
				InputKeyboard.PressKeyA(150);
			}
			RepelWalk("down", 100);
			RepelWalk("right", 250);
			RepelWalk("down", 400);
			WaitAfterEntrace();
			UseBike();
			RepelWalk("down", 200);
			RepelWalk("right", 350);
			RepelWalk("down", 250);
			RepelWalk("right", 550);
			UseRepel();
			UseSurfer();
			RepelWalk("right", 350);
			RepelWalk("up", 1100);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Kanto_Island_Two_Template()
	{
		InputKeyboard.PressKeyDown(1450);
		WaitAfterEntrace();
		UseBike();
		InputKeyboard.PressKeyRight(600);
		InputKeyboard.PressKeyUp(500);
		InputKeyboard.PressKeyRight(900);
		InputKeyboard.PressKeyUp(1000);
	}

	public void Kanto_Island_Two_1(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				UseTeleport();
			}
		}
		else
		{
			Kanto_Island_Two_Template();
			InputKeyboard.PressKeyLeft(550);
			InputKeyboard.PressKeyUp(500);
		}
	}

	public void Kanto_Island_Two_2(string action)
	{
		if (action == "goto")
		{
			Kanto_Island_Two_Template();
			UseSurfer();
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Kanto_Island_Six_1(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(2000);
			InputKeyboard.PressKeyDown(660);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Kanto_Island_Six_2(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(2000);
			InputKeyboard.PressKeyDown(300);
			InputKeyboard.PressKeyRight(300);
			UseSurfer();
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Hoenn_Petalburg_City(string action)
	{
		switch (action)
		{
		case "goback":
			InputKeyboard.PressKeyDown(800);
			InputKeyboard.PressKeyLeft(350);
			InputKeyboard.PressKeyUp(250);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1400);
			break;
		case "teleportback":
			UseTeleport();
			break;
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(300);
			InputKeyboard.PressKeyUp(1000);
			UseSurfer();
			break;
		}
	}

	public void Hoenn_Verdanturf_Town(string action)
	{
		switch (action)
		{
		case "goback":
			InputKeyboard.PressKeyLeft(2100);
			InputKeyboard.PressKeyUp(800);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1400);
			break;
		case "teleportback":
			UseTeleport();
			break;
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(550);
			InputKeyboard.PressKeyRight(2100);
			UseSurfer();
			break;
		}
	}

	public void Hoenn_Slateport_City(string action)
	{
		switch (action)
		{
		case "teleportback":
			UseTeleport();
			break;
		case "goback":
			InputKeyboard.PressKeyRight(300);
			InputKeyboard.PressKeyDown(2150);
			InputKeyboard.PressKeyRight(250);
			InputKeyboard.PressKeyUp(400);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1400);
			break;
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyLeft(250);
			InputKeyboard.PressKeyUp(2150);
			InputKeyboard.PressKeyLeft(300);
			break;
		}
	}

	public void Hoenn_Pacifidlog_Town(string action)
	{
		switch (action)
		{
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			InputKeyboard.PressKeyDown(100);
			InputKeyboard.PressKeyLeft(150);
			InputKeyboard.PressKeyDown(200);
			UseSurfer();
			break;
		case "teleportback":
			UseTeleport();
			break;
		case "goback":
			InputKeyboard.PressKeyUp(150);
			InputKeyboard.PressKeyRight(150);
			InputKeyboard.PressKeyUp(350);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1400);
			break;
		}
	}

	public void Hoenn_Sootopolis_City(string action)
	{
		switch (action)
		{
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(1000);
			UseSurfer();
			break;
		case "goback":
			InputKeyboard.PressKeyUp(1200);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1450);
			break;
		case "teleportback":
			UseTeleport();
			break;
		}
	}

	public void Sinnoh_Celestic_Town_1(string action)
	{
		switch (action)
		{
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(400);
			InputKeyboard.PressKeyUp(650);
			InputKeyboard.PressKeyRight(650);
			InputKeyboard.PressKeyDown(140);
			UseDefog();
			break;
		case "teleportback":
			UseTeleport();
			break;
		case "goback":
			InputKeyboard.PressKeyUp(150);
			InputKeyboard.PressKeyLeft(650);
			InputKeyboard.PressKeyDown(650);
			InputKeyboard.PressKeyLeft(400);
			InputKeyboard.PressKeyUp(150);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1450);
			break;
		}
	}

	public void Sinnoh_Celestic_Town_2(string action)
	{
		switch (action)
		{
		case "goback":
			InputKeyboard.PressKeyRight(1000);
			InputKeyboard.PressKeyUp(150);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1450);
			break;
		case "teleportback":
			UseTeleport();
			break;
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyLeft(1000);
			UseSurfer();
			break;
		}
	}

	public void Hoenn_Fallarbor_Town_1(string action)
	{
		switch (action)
		{
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(120);
			InputKeyboard.PressKeyLeft(2150);
			InputKeyboard.PressKeyUp(1250);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1700);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(3350);
			InputKeyboard.PressKeyLeft(200);
			InputKeyboard.PressKeyUp(700);
			InputKeyboard.PressKeyRight(200);
			InputKeyboard.PressKeyUp(300);
			WaitAfterEntrace();
			if (!Bot.Instance.Settings.AutoWalkFish)
			{
				InputKeyboard.PressKeyUp(50);
				break;
			}
			InputKeyboard.PressKeyUp(300);
			UseBike();
			break;
		case "goback":
			if (!Bot.Instance.Settings.AutoWalkFish)
			{
				InputKeyboard.PressKeyDown(300);
			}
			else
			{
				UseDig();
			}
			WaitAfterEntrace();
			InputKeyboard.PressKeyLeft(200);
			InputKeyboard.PressKeyDown(600);
			InputKeyboard.PressKeyRight(200);
			InputKeyboard.PressKeyDown(3650);
			WaitAfterEntrace();
			InputKeyboard.PressKeyDown(1200);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(300);
			InputKeyboard.PressKeyRight(2150);
			InputKeyboard.PressKeyUp(400);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1450);
			break;
		case "teleportback":
			if (!Bot.Instance.Settings.AutoWalkFish)
			{
				InputKeyboard.PressKeyDown(300);
			}
			else
			{
				UseDig();
			}
			WaitAfterEntrace();
			InputKeyboard.PressKeyLeft(200);
			InputKeyboard.PressKeyDown(600);
			InputKeyboard.PressKeyRight(200);
			InputKeyboard.PressKeyDown(3650);
			WaitAfterEntrace();
			InputKeyboard.PressKeyDown(1200);
			WaitAfterEntrace();
			UseTeleport();
			break;
		}
	}

	public void Hoenn_Fallarbor_Town_2(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				UseTeleport();
			}
			return;
		}
		InputKeyboard.PressKeyDown(1450);
		WaitAfterEntrace();
		UseBike();
		InputKeyboard.PressKeyDown(110);
		InputKeyboard.PressKeyLeft(2850);
		InputKeyboard.PressKeyDown(2550);
	}

	public void Sinnoh_Eterna_City(string action)
	{
		switch (action)
		{
		case "goback":
			InputKeyboard.PressKeyDown(250);
			InputKeyboard.PressKeyRight(2000);
			InputKeyboard.PressKeyUp(400);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1450);
			break;
		case "teleportback":
			UseTeleport();
			break;
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(50);
			InputKeyboard.PressKeyLeft(2050);
			InputKeyboard.PressKeyUp(250);
			break;
		}
	}

	public void Sinnoh_Solaceon_Town_1(string action)
	{
		switch (action)
		{
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyLeft(400);
			InputKeyboard.PressKeyDown(2600);
			InputKeyboard.PressKeyRight(300);
			break;
		case "teleportback":
			UseTeleport();
			break;
		case "goback":
			InputKeyboard.PressKeyLeft(300);
			InputKeyboard.PressKeyUp(2600);
			InputKeyboard.PressKeyRight(400);
			InputKeyboard.PressKeyUp(200);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1450);
			break;
		}
	}

	public void Sinnoh_Solaceon_Town_2(string action)
	{
		switch (action)
		{
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyLeft(500);
			InputKeyboard.PressKeyUp(2600);
			UseBike();
			InputKeyboard.PressKeyUp(100);
			break;
		case "goback":
			InputKeyboard.PressKeyDown(100);
			UseBike();
			InputKeyboard.PressKeyDown(2300);
			InputKeyboard.PressKeyRight(500);
			InputKeyboard.PressKeyUp(200);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1450);
			break;
		case "teleportback":
			UseTeleport();
			break;
		}
	}

	public void Sinnoh_Resort_Area(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(50);
			InputKeyboard.PressKeyRight(550);
			InputKeyboard.PressKeyUp(1700);
			InputKeyboard.PressKeyLeft(150);
			InputKeyboard.PressKeyUp(600);
			InputKeyboard.PressKeyLeft(400);
			InputKeyboard.PressKeyUp(1300);
			InputKeyboard.PressKeyLeft(500);
			InputKeyboard.PressKeyUp(500);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Sinnoh_Fight_Area(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				UseTeleport();
			}
			return;
		}
		InputKeyboard.PressKeyDown(1450);
		WaitAfterEntrace();
		UseBike();
		InputKeyboard.PressKeyDown(150);
		InputKeyboard.PressKeyRight(4500);
		InputKeyboard.PressKeyUp(150);
		InputKeyboard.PressKeyRight(150);
		UseSurfer();
	}

	public void Sinnoh_Snowpoint_City(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			InputKeyboard.PressKeyLeft(900);
			InputKeyboard.PressKeyUp(1500);
			InputKeyboard.PressKeyLeft(1400);
			InputKeyboard.PressKeyDown(2800);
			InputKeyboard.PressKeyLeft(2500);
			InputKeyboard.PressKeyUp(700);
			InputKeyboard.PressKeyLeft(3700);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Sinnoh_Celestic_Town_3(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyLeft(1000);
			InputKeyboard.PressKeyDown(500);
			InputKeyboard.PressKeyLeft(1000);
			InputKeyboard.PressKeyUp(500);
			InputKeyboard.PressKeyLeft(300);
			InputKeyboard.PressKeyUp(850);
			InputKeyboard.PressKeyLeft(1250);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Sinnoh_Veilstone_City(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				UseTeleport();
			}
			return;
		}
		InputKeyboard.PressKeyDown(1450);
		WaitAfterEntrace();
		UseBike();
		InputKeyboard.PressKeyDown(300);
		InputKeyboard.PressKeyLeft(550);
		InputKeyboard.PressKeyDown(400);
		InputKeyboard.PressKeyRight(1000);
		InputKeyboard.PressKeyDown(1200);
		InputKeyboard.PressKeyLeft(400);
		InputKeyboard.PressKeyDown(800);
		WaitAfterEntrace();
		InputKeyboard.PressKeyDown(1200);
		WaitAfterEntrace();
		InputKeyboard.PressKeyDown(1400);
		InputKeyboard.PressKeyRight(950);
		InputKeyboard.PressKeyDown(600);
	}

	public void Sinnoh_Pokemon_League(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(850);
			InputKeyboard.PressKeyUp(300);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(50);
		}
		else if (action == "goback" || action == "teleportback")
		{
			InputKeyboard.PressKeyDown(300);
			WaitAfterEntrace();
			UseEscapeRope();
			UseTeleport();
		}
	}

	public void Sinnoh_Canalave_City(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				UseTeleport();
			}
			return;
		}
		InputKeyboard.PressKeyDown(1450);
		WaitAfterEntrace();
		UseBike();
		InputKeyboard.PressKeyRight(400);
		InputKeyboard.PressKeyDown(2700);
		InputKeyboard.PressKeyRight(300);
		WaitAfterEntrace();
		InputKeyboard.PressKeyRight(1200);
		WaitAfterEntrace();
		InputKeyboard.PressKeyRight(800);
		InputKeyboard.PressKeyDown(150);
	}

	public void Sinnoh_Sunyshore_City(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(100);
			InputKeyboard.PressKeyLeft(2000);
			InputKeyboard.PressKeyDown(400);
			InputKeyboard.PressKeyLeft(800);
			WaitAfterEntrace();
			InputKeyboard.PressKeyLeft(1200);
			WaitAfterEntrace();
			InputKeyboard.PressKeyLeft(400);
			InputKeyboard.PressKeyUp(400);
			InputKeyboard.PressKeyLeft(900);
			InputKeyboard.PressKeyUp(900);
			InputKeyboard.PressKeyLeft(2600);
			InputKeyboard.PressKeyUp(600);
			InputKeyboard.PressKeyRight(1900);
			InputKeyboard.PressKeyDown(400);
			InputKeyboard.PressKeyLeft(500);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Sinnoh_Floaroma_Town(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(1300);
			InputKeyboard.PressKeyUp(500);
			InputKeyboard.PressKeyRight(3400);
			InputKeyboard.PressKeyUp(300);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Striaton_City(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(200);
			InputKeyboard.PressKeyLeft(1750);
			InputKeyboard.PressKeyUp(300);
			UseSurfer();
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Nacrene_City_1(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				UseTeleport();
			}
			return;
		}
		InputKeyboard.PressKeyDown(1450);
		WaitAfterEntrace();
		UseBike();
		InputKeyboard.PressKeyRight(2900);
		WaitAfterEntrace();
		InputKeyboard.PressKeyRight(1200);
		WaitAfterEntrace();
		InputKeyboard.PressKeyRight(550);
	}

	public void Unova_Nacrene_City_2(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyLeft(600);
			InputKeyboard.PressKeyUp(800);
			InputKeyboard.PressKeyLeft(3000);
			InputKeyboard.PressKeyUp(400);
			InputKeyboard.PressKeyLeft(900);
			InputKeyboard.PressKeyDown(900);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Icirrus_City_1(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyLeft(600);
			InputKeyboard.PressKeyDown(800);
			InputKeyboard.PressKeyRight(1700);
			UseSurfer();
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Icirrus_City_2(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyLeft(600);
			InputKeyboard.PressKeyDown(1070);
			InputKeyboard.PressKeyLeft(750);
			WaitAfterEntrace();
		}
		else if (action == "goback" || action == "teleportback")
		{
			InputKeyboard.PressKeyRight(150);
			WaitAfterEntrace();
			UseEscapeRope();
			UseTeleport();
		}
	}

	public void Unova_Icirrus_City_3(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyLeft(450);
			InputKeyboard.PressKeyUp(3650);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Icirrus_City_4(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				InputKeyboard.PressKeyDown(350);
				WaitAfterEntrace();
				UseBike();
				InputKeyboard.PressKeyDown(1500);
				WaitAfterEntrace();
				UseTeleport();
			}
		}
		else
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyLeft(450);
			InputKeyboard.PressKeyUp(3300);
			InputKeyboard.PressKeyRight(400);
			InputKeyboard.PressKeyUp(1300);
			InputKeyboard.PressKeyLeft(250);
			InputKeyboard.PressKeyUp(300);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(2000);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(100);
		}
	}

	public void Unova_Pokemon_League(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(50);
			InputKeyboard.PressKeyRight(1000);
			InputKeyboard.PressKeyDown(1000);
			WaitAfterEntrace();
			InputKeyboard.PressKeyDown(200);
			InputKeyboard.PressKeyLeft(850);
			InputKeyboard.PressKeyDown(4300);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Opelucid_City_1(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(100);
			InputKeyboard.PressKeyLeft(3500);
			WaitAfterEntrace();
			InputKeyboard.PressKeyLeft(1200);
			WaitAfterEntrace();
			InputKeyboard.PressKeyLeft(500);
			InputKeyboard.PressKeyDown(400);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Opelucid_City_2(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(2200);
			InputKeyboard.PressKeyDown(150);
			InputKeyboard.PressKeyRight(250);
			WaitAfterEntrace();
			InputKeyboard.PressKeyRight(1200);
			WaitAfterEntrace();
			InputKeyboard.PressKeyRight(550);
			InputKeyboard.PressKeyUp(500);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Opelucid_City_3(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				UseTeleport();
			}
			return;
		}
		InputKeyboard.PressKeyDown(1450);
		WaitAfterEntrace();
		UseBike();
		InputKeyboard.PressKeyRight(750);
		InputKeyboard.PressKeyUp(4000);
		InputKeyboard.PressKeyLeft(150);
		InputKeyboard.PressKeyUp(250);
		WaitAfterEntrace();
		InputKeyboard.PressKeyUp(1200);
		WaitAfterEntrace();
		InputKeyboard.PressKeyRight(300);
		InputKeyboard.PressKeyUp(900);
	}

	public void Unova_Opelucid_City_4(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(750);
			InputKeyboard.PressKeyUp(4000);
			InputKeyboard.PressKeyLeft(150);
			InputKeyboard.PressKeyUp(250);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1200);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(450);
			InputKeyboard.PressKeyRight(4000);
			InputKeyboard.PressKeyUp(300);
			InputKeyboard.PressKeyRight(900);
			InputKeyboard.PressKeyUp(200);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Lacunosa_Town_1(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(50);
			InputKeyboard.PressKeyLeft(3000);
			InputKeyboard.PressKeyUp(300);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Lacunosa_Town_2(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(900);
			InputKeyboard.PressKeyUp(900);
			InputKeyboard.PressKeyRight(900);
			InputKeyboard.PressKeyDown(800);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Lacunosa_Town_3(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				UseTeleport();
			}
			return;
		}
		InputKeyboard.PressKeyDown(1450);
		WaitAfterEntrace();
		UseBike();
		InputKeyboard.PressKeyRight(900);
		InputKeyboard.PressKeyUp(900);
		InputKeyboard.PressKeyRight(2000);
		InputKeyboard.PressKeyUp(200);
		InputKeyboard.PressKeyRight(300);
		InputKeyboard.PressKeyUp(300);
		InputKeyboard.PressKeyRight(250);
		InputKeyboard.PressKeyUp(500);
		InputKeyboard.PressKeyLeft(300);
		InputKeyboard.PressKeyUp(300);
		InputKeyboard.PressKeyLeft(300);
		InputKeyboard.PressKeyUp(200);
		InputKeyboard.PressKeyLeft(300);
		InputKeyboard.PressKeyUp(600);
	}

	public void Unova_Lacunosa_Town_4(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(900);
			InputKeyboard.PressKeyUp(900);
			InputKeyboard.PressKeyRight(2000);
			InputKeyboard.PressKeyUp(200);
			InputKeyboard.PressKeyRight(300);
			InputKeyboard.PressKeyUp(300);
			InputKeyboard.PressKeyRight(250);
			InputKeyboard.PressKeyUp(500);
			InputKeyboard.PressKeyLeft(300);
			InputKeyboard.PressKeyUp(300);
			InputKeyboard.PressKeyLeft(300);
			InputKeyboard.PressKeyUp(200);
			InputKeyboard.PressKeyLeft(200);
			InputKeyboard.PressKeyUp(600);
			InputKeyboard.PressKeyRight(900);
			InputKeyboard.PressKeyUp(300);
			InputKeyboard.PressKeyRight(200);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Lacunosa_Town_5(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(900);
			InputKeyboard.PressKeyUp(900);
			InputKeyboard.PressKeyRight(2000);
			InputKeyboard.PressKeyUp(200);
			InputKeyboard.PressKeyRight(300);
			InputKeyboard.PressKeyUp(300);
			InputKeyboard.PressKeyRight(250);
			InputKeyboard.PressKeyUp(500);
			InputKeyboard.PressKeyLeft(300);
			InputKeyboard.PressKeyUp(300);
			InputKeyboard.PressKeyLeft(300);
			InputKeyboard.PressKeyUp(200);
			InputKeyboard.PressKeyLeft(200);
			InputKeyboard.PressKeyUp(600);
			InputKeyboard.PressKeyRight(900);
			InputKeyboard.PressKeyUp(400);
			InputKeyboard.PressKeyLeft(500);
			InputKeyboard.PressKeyUp(150);
			InputKeyboard.PressKeyLeft(600);
			InputKeyboard.PressKeyUp(250);
			WaitAfterEntrace();
		}
		else if (action == "goback" || action == "teleportback")
		{
			InputKeyboard.PressKeyDown(150);
			WaitAfterEntrace();
			UseEscapeRope();
			UseTeleport();
		}
	}

	public void Unova_Undella_Town_1(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(250);
			InputKeyboard.PressKeyLeft(750);
			InputKeyboard.PressKeyDown(800);
			InputKeyboard.PressKeyLeft(2550);
			InputKeyboard.PressKeyDown(1000);
			InputKeyboard.PressKeyRight(2200);
			InputKeyboard.PressKeyDown(850);
			InputKeyboard.PressKeyLeft(450);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Undella_Town_2(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(250);
			InputKeyboard.PressKeyLeft(750);
			InputKeyboard.PressKeyDown(800);
			InputKeyboard.PressKeyLeft(2550);
			InputKeyboard.PressKeyDown(1000);
			InputKeyboard.PressKeyRight(2150);
			InputKeyboard.PressKeyDown(2300);
			UseSurfer();
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Black_City_1(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				UseTeleport();
			}
			return;
		}
		InputKeyboard.PressKeyDown(1450);
		WaitAfterEntrace();
		UseBike();
		InputKeyboard.PressKeyRight(750);
		InputKeyboard.PressKeyUp(2400);
		WaitAfterEntrace();
		InputKeyboard.PressKeyUp(1100);
		WaitAfterEntrace();
		InputKeyboard.PressKeyUp(350);
		InputKeyboard.PressKeyRight(900);
		InputKeyboard.PressKeyUp(300);
	}

	public void Unova_Black_City_2(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(750);
			InputKeyboard.PressKeyDown(1350);
			InputKeyboard.PressKeyLeft(3600);
			WaitAfterEntrace();
			InputKeyboard.PressKeyLeft(1200);
			WaitAfterEntrace();
			InputKeyboard.PressKeyLeft(300);
			InputKeyboard.PressKeyDown(900);
			InputKeyboard.PressKeyLeft(300);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Nimbasa_City_1(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(2000);
			InputKeyboard.PressKeyDown(1100);
			WaitAfterEntrace();
			InputKeyboard.PressKeyDown(1200);
			WaitAfterEntrace();
			InputKeyboard.PressKeyDown(4000);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Nimbasa_City_2(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(2300);
			InputKeyboard.PressKeyUp(2500);
			InputKeyboard.PressKeyRight(1300);
			WaitAfterEntrace();
			InputKeyboard.PressKeyRight(1200);
			WaitAfterEntrace();
			InputKeyboard.PressKeyRight(1200);
			InputKeyboard.PressKeyDown(1200);
			InputKeyboard.PressKeyRight(500);
			InputKeyboard.PressKeyUp(1200);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Nimbasa_City_3(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				UseTeleport();
			}
			return;
		}
		InputKeyboard.PressKeyDown(1450);
		WaitAfterEntrace();
		UseBike();
		InputKeyboard.PressKeyRight(2300);
		InputKeyboard.PressKeyUp(2500);
		InputKeyboard.PressKeyRight(1300);
		WaitAfterEntrace();
		InputKeyboard.PressKeyRight(1200);
		WaitAfterEntrace();
		InputKeyboard.PressKeyRight(1200);
		InputKeyboard.PressKeyDown(1200);
		InputKeyboard.PressKeyRight(1250);
		InputKeyboard.PressKeyUp(1200);
	}

	public void Unova_Nimbasa_City_4(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(500);
			InputKeyboard.PressKeyUp(2650);
			InputKeyboard.PressKeyLeft(2300);
			WaitAfterEntrace();
			InputKeyboard.PressKeyLeft(1200);
			WaitAfterEntrace();
			InputKeyboard.PressKeyLeft(550);
			InputKeyboard.PressKeyUp(950);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Driftveil_City_1(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				UseTeleport();
			}
			return;
		}
		InputKeyboard.PressKeyDown(1450);
		WaitAfterEntrace();
		UseBike();
		InputKeyboard.PressKeyLeft(500);
		InputKeyboard.PressKeyUp(700);
		InputKeyboard.PressKeyLeft(3600);
	}

	public void Unova_Driftveil_City_2(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(500);
			InputKeyboard.PressKeyUp(150);
			InputKeyboard.PressKeyRight(1200);
			UseSurfer();
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Driftveil_City_3(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(500);
			InputKeyboard.PressKeyLeft(600);
			InputKeyboard.PressKeyDown(1500);
			InputKeyboard.PressKeyLeft(500);
			InputKeyboard.PressKeyDown(2000);
			InputKeyboard.PressKeyLeft(400);
			InputKeyboard.PressKeyDown(1100);
			InputKeyboard.PressKeyRight(300);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Unova_Mistralton_City_1(string action)
	{
		switch (action)
		{
		case "goback":
			InputKeyboard.PressKeyUp(350);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(400);
			InputKeyboard.PressKeyRight(500);
			InputKeyboard.PressKeyUp(200);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1450);
			break;
		case "teleportback":
			InputKeyboard.PressKeyUp(400);
			WaitAfterEntrace();
			UseEscapeRope();
			UseTeleport();
			break;
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyLeft(500);
			InputKeyboard.PressKeyDown(550);
			WaitAfterEntrace();
			InputKeyboard.PressKeyDown(50);
			break;
		}
	}

	public void Unova_Mistralton_City_2(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyLeft(500);
			InputKeyboard.PressKeyUp(700);
			InputKeyboard.PressKeyRight(600);
			InputKeyboard.PressKeyUp(400);
			InputKeyboard.PressKeyRight(800);
			InputKeyboard.PressKeyUp(1500);
			UseBike();
			InputKeyboard.PressKeyUp(100);
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Kanto_Route_4(string action)
	{
		switch (action)
		{
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyRight(660);
			InputKeyboard.PressKeyUp(400);
			break;
		case "teleportback":
			InputKeyboard.PressKeyDown(400);
			WaitAfterEntrace();
			UseEscapeRope();
			UseTeleport();
			break;
		case "goback":
			InputKeyboard.PressKeyDown(400);
			WaitAfterEntrace();
			InputKeyboard.PressKeyLeft(650);
			InputKeyboard.PressKeyUp(400);
			WaitAfterEntrace();
			InputKeyboard.PressKeyUp(1450);
			break;
		}
	}

	public void Kanto_Fuchsia_City(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				UseTeleport();
			}
			return;
		}
		InputKeyboard.PressKeyDown(1450);
		WaitAfterEntrace();
		UseBike();
		InputKeyboard.PressKeyRight(2300);
		InputKeyboard.PressKeyUp(1000);
		InputKeyboard.PressKeyRight(1400);
		WaitAfterEntrace();
		InputKeyboard.PressKeyRight(1300);
		WaitAfterEntrace();
		InputKeyboard.PressKeyRight(500);
	}

	public void Kanto_Indigo_Plateau(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(700);
			InputKeyboard.PressKeyLeft(400);
			InputKeyboard.PressKeyDown(400);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyDown(1200);
			InputKeyboard.PressKeyRight(400);
			InputKeyboard.PressKeyDown(800);
			InputKeyboard.PressKeyRight(150);
			InputKeyboard.PressKeyDown(750);
			InputKeyboard.PressKeyRight(600);
			InputKeyboard.PressKeyDown(900);
			InputKeyboard.PressKeyLeft(300);
			InputKeyboard.PressKeyUp(200);
			WaitAfterEntrace();
		}
		else if (action == "goback" || action == "teleportback")
		{
			InputKeyboard.PressKeyDown(200);
			WaitAfterEntrace();
			UseEscapeRope();
			UseTeleport();
		}
	}

	public void Kanto_Cerulean_City(string action)
	{
		if (!(action == "goto"))
		{
			if (action == "goback" || action == "teleportback")
			{
				UseTeleport();
			}
			return;
		}
		int num = 0;
		int num2 = 0;
		InputKeyboard.PressKeyDown(1450);
		WaitAfterEntrace();
		UseBike();
		switch (RandomNumber.Between(1, 2))
		{
		case 1:
			InputKeyboard.PressKeyDown(200);
			InputKeyboard.PressKeyLeft(400);
			break;
		case 2:
			InputKeyboard.PressKeyDown(100);
			InputKeyboard.PressKeyLeft(400);
			InputKeyboard.PressKeyDown(100);
			break;
		}
		InputKeyboard.PressKeyDown(800);
		InputKeyboard.PressKeyRight(750);
		InputKeyboard.PressKeyDown(200);
		UseCut();
		switch (RandomNumber.Between(1, 3))
		{
		case 2:
			InputKeyboard.PressKeyDown(200);
			num2 = 200;
			break;
		case 3:
			InputKeyboard.PressKeyDown(300);
			num2 = 300;
			break;
		case 1:
			InputKeyboard.PressKeyDown(100);
			num2 = 100;
			break;
		}
		switch (RandomNumber.Between(1, 3))
		{
		case 1:
			InputKeyboard.PressKeyLeft(RandomNumber.Between(160, 500));
			break;
		case 2:
			InputKeyboard.PressKeyRight(160);
			break;
		}
		InputKeyboard.PressKeyDown(1950 - num2);
		switch (RandomNumber.Between(1, 4))
		{
		case 1:
			InputKeyboard.PressKeyDown(50);
			break;
		case 3:
			InputKeyboard.PressKeyRight(RandomNumber.Between(160, 400));
			break;
		case 2:
			InputKeyboard.PressKeyLeft(RandomNumber.Between(160, 400));
			break;
		}
	}

	public void Kanto_Virdian_City(string action)
	{
		switch (action)
		{
		case "teleportback":
			UseTeleport();
			break;
		case "goback":
			InputKeyboard.PressKeyRight(900);
			InputKeyboard.PressKeyUp(2000);
			break;
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			UseBike();
			InputKeyboard.PressKeyLeft(800);
			UseSurfer();
			break;
		}
	}

	public void Kanto_Cinnabar_Island(string action)
	{
		if (action == "goto")
		{
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			InputKeyboard.PressKeyDown(500);
			UseSurfer();
		}
		else if (action == "goback" || action == "teleportback")
		{
			UseTeleport();
		}
	}

	public void Kanto_Cinnabar_Island_GoBack()
	{
		InputKeyboard.PressKeyUp(700);
		InputKeyboard.PressKeyUp(2000);
	}

	public void Kanto_Vermilion_City(string action)
	{
		switch (action)
		{
		case "goto":
			InputKeyboard.PressKeyDown(1450);
			WaitAfterEntrace();
			InputKeyboard.PressKeyDown(800);
			UseSurfer();
			break;
		case "goback":
			InputKeyboard.PressKeyUp(900);
			InputKeyboard.PressKeyUp(2000);
			break;
		case "teleportback":
			UseTeleport();
			break;
		}
	}
}
