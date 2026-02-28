using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using PokeMMO_.Botting;
using PokeMMO_.Classes;
using WindowsInput.Native;

namespace PokeMMO_.Input;

public static class InputKeyboard
{
	private static readonly Dictionary<int, VirtualKeyCode> GdxToVirtualKey = new Dictionary<int, VirtualKeyCode>
	{
		{
			7,
			(VirtualKeyCode)48
		},
		{
			8,
			(VirtualKeyCode)49
		},
		{
			9,
			(VirtualKeyCode)50
		},
		{
			10,
			(VirtualKeyCode)51
		},
		{
			11,
			(VirtualKeyCode)52
		},
		{
			12,
			(VirtualKeyCode)53
		},
		{
			13,
			(VirtualKeyCode)54
		},
		{
			14,
			(VirtualKeyCode)55
		},
		{
			15,
			(VirtualKeyCode)56
		},
		{
			16,
			(VirtualKeyCode)57
		},
		{
			19,
			(VirtualKeyCode)38
		},
		{
			20,
			(VirtualKeyCode)40
		},
		{
			21,
			(VirtualKeyCode)37
		},
		{
			22,
			(VirtualKeyCode)39
		},
		{
			29,
			(VirtualKeyCode)65
		},
		{
			30,
			(VirtualKeyCode)66
		},
		{
			31,
			(VirtualKeyCode)67
		},
		{
			32,
			(VirtualKeyCode)68
		},
		{
			33,
			(VirtualKeyCode)69
		},
		{
			34,
			(VirtualKeyCode)70
		},
		{
			35,
			(VirtualKeyCode)71
		},
		{
			36,
			(VirtualKeyCode)72
		},
		{
			37,
			(VirtualKeyCode)73
		},
		{
			38,
			(VirtualKeyCode)74
		},
		{
			39,
			(VirtualKeyCode)75
		},
		{
			40,
			(VirtualKeyCode)76
		},
		{
			41,
			(VirtualKeyCode)77
		},
		{
			42,
			(VirtualKeyCode)78
		},
		{
			43,
			(VirtualKeyCode)79
		},
		{
			44,
			(VirtualKeyCode)80
		},
		{
			45,
			(VirtualKeyCode)81
		},
		{
			46,
			(VirtualKeyCode)82
		},
		{
			47,
			(VirtualKeyCode)83
		},
		{
			48,
			(VirtualKeyCode)84
		},
		{
			49,
			(VirtualKeyCode)85
		},
		{
			50,
			(VirtualKeyCode)86
		},
		{
			51,
			(VirtualKeyCode)87
		},
		{
			52,
			(VirtualKeyCode)88
		},
		{
			62,
			(VirtualKeyCode)32
		},
		{
			131,
			(VirtualKeyCode)112
		},
		{
			132,
			(VirtualKeyCode)113
		},
		{
			133,
			(VirtualKeyCode)114
		},
		{
			134,
			(VirtualKeyCode)115
		},
		{
			135,
			(VirtualKeyCode)116
		},
		{
			136,
			(VirtualKeyCode)117
		},
		{
			137,
			(VirtualKeyCode)118
		},
		{
			138,
			(VirtualKeyCode)119
		},
		{
			139,
			(VirtualKeyCode)120
		},
		{
			140,
			(VirtualKeyCode)121
		},
		{
			141,
			(VirtualKeyCode)122
		},
		{
			142,
			(VirtualKeyCode)123
		},
		{
			144,
			(VirtualKeyCode)96
		},
		{
			145,
			(VirtualKeyCode)97
		},
		{
			146,
			(VirtualKeyCode)98
		},
		{
			147,
			(VirtualKeyCode)99
		},
		{
			148,
			(VirtualKeyCode)100
		},
		{
			149,
			(VirtualKeyCode)101
		},
		{
			150,
			(VirtualKeyCode)102
		},
		{
			151,
			(VirtualKeyCode)103
		},
		{
			152,
			(VirtualKeyCode)104
		},
		{
			153,
			(VirtualKeyCode)105
		}
	};

	[DllImport("user32.dll")]
	private static extern long GetKeyboardLayoutName(StringBuilder pwszKLID);

	public static int GetKeyFromProperties(string key)
	{
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected I4, but got Unknown
		StringBuilder stringBuilder = new StringBuilder(9);
		GetKeyboardLayoutName(stringBuilder);
		string key2 = "client.controls.gdx." + key;
		if (Bot.Instance.Settings.Data.TryGetValue(key2, out var value) && int.TryParse(value, out var result))
		{
			bool flag = stringBuilder.ToString().Contains("407");
			switch (result)
			{
			case 53:
				return flag ? 90 : 89;
			case 54:
				return flag ? 89 : 90;
			default:
			{
				if (!GdxToVirtualKey.TryGetValue(result, out var value2))
				{
					TopMostMessageBox.Show("If you set a key in PokeMMO to a key that is not supported, the bot will not work properly.\nPlease change your controls in PokeMMO.\nRefer below to our supported controls!.\nSupported Controls: A-Z, 0-9, F1-F12, ARROW KEYS, SPACE", "ERROR Controls", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
					Bot.Instance.Stop();
					return -1;
				}
				return (int)value2;
			}
			}
		}
		TopMostMessageBox.Show("Key binding not found for: " + key + "\nPlease check your PokeMMO controls configuration.", "ERROR Controls", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
		Bot.Instance.Stop();
		return -1;
	}

	private static void PressKey(VirtualKeyCode keyCode, int holdtime)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (Includes.ApplicationIsActivated())
		{
			Bot.Instance.Sim.get_Keyboard().KeyDown(keyCode);
			Thread.Sleep(holdtime);
			Bot.Instance.Sim.get_Keyboard().KeyUp(keyCode);
		}
	}

	private static void PressPropertyKey(string propertyKey, int holdtime)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (Includes.ApplicationIsActivated())
		{
			int keyFromProperties = GetKeyFromProperties(propertyKey);
			if (keyFromProperties >= 0)
			{
				VirtualKeyCode val = (VirtualKeyCode)keyFromProperties;
				Bot.Instance.Sim.get_Keyboard().KeyDown(val);
				Thread.Sleep(holdtime);
				Bot.Instance.Sim.get_Keyboard().KeyUp(val);
			}
		}
	}

	private static void PressDirectionKey(string direction, string propertyKey, int holdtime)
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		if (Includes.ApplicationIsActivated())
		{
			if ((Bot.Instance.Settings.AutoSweetScent || Bot.Instance.Settings.AutoWalkFish || Bot.Instance.Settings.SafariAutoWalk || Bot.Instance.Settings.SafariAutoFish) && Bot.Instance.Status.LastWalkDirection != direction)
			{
				Bot.Instance.Routes.WaitBeforeTurn();
			}
			Bot.Instance.Status.LastWalkDirection = direction;
			int keyFromProperties = GetKeyFromProperties(propertyKey);
			if (keyFromProperties >= 0)
			{
				VirtualKeyCode val = (VirtualKeyCode)keyFromProperties;
				Bot.Instance.Sim.get_Keyboard().KeyDown(val);
				Thread.Sleep(holdtime);
				Bot.Instance.Sim.get_Keyboard().KeyUp(val);
			}
		}
	}

	public static void PressKeyEscape(int holdtime)
	{
		PressKey((VirtualKeyCode)27, holdtime);
	}

	public static void PressKeyA(int holdtime)
	{
		PressPropertyKey("key_a", holdtime);
	}

	public static void PressKeyB(int holdtime)
	{
		PressPropertyKey("key_b", holdtime);
	}

	public static void PressKeyUp(int holdtime)
	{
		PressDirectionKey("Up", "key_up", holdtime);
	}

	public static void PressKeyDown(int holdtime)
	{
		PressDirectionKey("Down", "key_down", holdtime);
	}

	public static void PressKeyLeft(int holdtime)
	{
		PressDirectionKey("Left", "key_left", holdtime);
	}

	public static void PressKeyRight(int holdtime)
	{
		PressDirectionKey("Right", "key_right", holdtime);
	}

	public static void PressHotkey(int index, int holdtime)
	{
		PressPropertyKey("hotbar" + index, holdtime);
	}
}
