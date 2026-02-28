using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using PokeMMO_.Botting;
using PokeMMO_.Model;

namespace PokeMMO_.Classes;

public class Search
{
	private struct Region
	{
		public int X;

		public int Y;

		public int Right;

		public int Bottom;

		public Region(int x, int y, int right, int bottom)
		{
			X = x;
			Y = y;
			Right = right;
			Bottom = bottom;
		}
	}

	private string filename = "";

	private static readonly Dictionary<string, (Region hd, Region sd)> RegionMap = new Dictionary<string, (Region, Region)>
	{
		{
			"Captcha",
			(new Region(640, 374, 1278, 678), new Region(323, 197, 956, 496))
		},
		{
			"Captcha2",
			(new Region(640, 374, 1278, 678), new Region(323, 197, 956, 496))
		},
		{
			"Potion",
			(new Region(286, 529, 591, 788), new Region(190, 277, 495, 536))
		},
		{
			"SuperPotion",
			(new Region(286, 529, 591, 788), new Region(190, 277, 495, 536))
		},
		{
			"HyperPotion",
			(new Region(286, 529, 591, 788), new Region(190, 277, 495, 536))
		},
		{
			"PotionH",
			(new Region(286, 529, 591, 788), new Region(190, 277, 495, 536))
		},
		{
			"SuperPotionH",
			(new Region(286, 529, 591, 788), new Region(190, 277, 495, 536))
		},
		{
			"HyperPotionH",
			(new Region(286, 529, 591, 788), new Region(190, 277, 495, 536))
		},
		{
			"Battle",
			(new Region(1465, 662, 1631, 724), new Region(970, 410, 1087, 470))
		},
		{
			"Safari",
			(new Region(1465, 662, 1631, 724), new Region(970, 410, 1087, 470))
		},
		{
			"Repel",
			(new Region(550, 127, 900, 165), new Region(365, 52, 700, 90))
		},
		{
			"TimeOver",
			(new Region(550, 127, 900, 165), new Region(365, 52, 700, 90))
		},
		{
			"Lure",
			(new Region(550, 127, 900, 165), new Region(365, 52, 700, 90))
		},
		{
			"CantRun",
			(new Region(300, 680, 532, 715), new Region(205, 430, 437, 463))
		},
		{
			"NoEffect",
			(new Region(300, 680, 532, 715), new Region(205, 430, 437, 463))
		},
		{
			"Fight",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"Run",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"Bag",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"SafariC",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"Super",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"Effective",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"FalseSwipe",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"Spore",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"Substitute",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"Assist",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"Struggle",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"0PPS",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"0PPS2",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"0PPE",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"0PPE2",
			(new Region(295, 667, 715, 781), new Region(200, 417, 620, 530))
		},
		{
			"Cancel",
			(new Region(1530, 732, 1625, 800), new Region(985, 482, 1085, 550))
		},
		{
			"Back",
			(new Region(1530, 732, 1625, 800), new Region(985, 482, 1085, 550))
		},
		{
			"NextPoke",
			(new Region(295, 662, 925, 800), new Region(200, 412, 830, 550))
		},
		{
			"NextPoke2",
			(new Region(295, 662, 925, 800), new Region(200, 412, 830, 550))
		},
		{
			"NextPoke3",
			(new Region(295, 662, 925, 800), new Region(200, 412, 830, 550))
		},
		{
			"Level",
			(new Region(295, 720, 510, 785), new Region(200, 470, 415, 535))
		},
		{
			"Shiny",
			(new Region(316, 71, 1359, 285), new Region(223, 73, 1024, 253))
		},
		{
			"Horde",
			(new Region(316, 71, 1359, 285), new Region(223, 73, 1024, 253))
		},
		{
			"Horde2",
			(new Region(316, 71, 1359, 285), new Region(223, 73, 1024, 253))
		},
		{
			"Horde_old",
			(new Region(316, 71, 1359, 285), new Region(223, 73, 1024, 253))
		},
		{
			"Ball",
			(new Region(440, 540, 485, 573), new Region(345, 290, 390, 318))
		},
		{
			"Medicine",
			(new Region(440, 540, 485, 573), new Region(345, 290, 390, 318))
		},
		{
			"Login",
			(new Region(1010, 437, 1095, 610), new Region(685, 257, 800, 500))
		},
		{
			"Character",
			(new Region(650, 382, 1200, 420), new Region(330, 202, 960, 240))
		},
		{
			"DC",
			(new Region(770, 497, 1149, 522), new Region(443, 304, 831, 343))
		},
		{
			"DCLogin",
			(new Region(770, 497, 1149, 522), new Region(443, 304, 831, 343))
		},
		{
			"Session",
			(new Region(807, 490, 1112, 515), new Region(490, 309, 790, 333))
		},
		{
			"PM",
			(new Region(780, 387, 805, 442), new Region(463, 211, 484, 262))
		},
		{
			"PM2",
			(new Region(0, 889, 419, 1039), new Region(0, 529, 419, 679))
		},
		{
			"LearnMove",
			(new Region(845, 658, 950, 690), new Region(750, 407, 850, 455))
		},
		{
			"Evolve",
			(new Region(286, 725, 1529, 786), new Region(190, 465, 985, 533))
		},
		{
			"Disable",
			(new Region(286, 725, 1529, 786), new Region(190, 465, 985, 533))
		},
		{
			"Skip",
			(new Region(1330, 162, 1386, 254), new Region(892, 96, 940, 240))
		},
		{
			"HPOrange",
			(new Region(1410, 535, 1632, 574), new Region(866, 284, 1087, 323))
		},
		{
			"HPRed",
			(new Region(1410, 535, 1632, 574), new Region(866, 284, 1087, 323))
		},
		{
			"Sleep",
			(new Region(278, 137, 342, 199), new Region(189, 135, 251, 203))
		},
		{
			"Catched",
			(new Region(316, 143, 614, 170), new Region(223, 140, 519, 170))
		},
		{
			"Stats",
			(new Region(743, 329, 1265, 671), new Region(379, 176, 901, 516))
		},
		{
			"StatsIV",
			(new Region(743, 329, 1265, 671), new Region(379, 176, 901, 516))
		},
		{
			"31",
			(new Region(743, 329, 1265, 671), new Region(379, 176, 901, 516))
		},
		{
			"Release",
			(new Region(885, 369, 931, 403), new Region(562, 189, 613, 224))
		},
		{
			"CRelease",
			(new Region(722, 519, 913, 564), new Region(404, 341, 594, 383))
		},
		{
			"Error1",
			(new Region(880, 886, 1039, 937), new Region(560, 526, 719, 577))
		},
		{
			"Error2",
			(new Region(880, 886, 1039, 937), new Region(560, 526, 719, 577))
		},
		{
			"Error3",
			(new Region(880, 886, 1039, 937), new Region(560, 526, 719, 577))
		},
		{
			"GTLNoMoney",
			(new Region(880, 886, 1039, 937), new Region(560, 526, 719, 577))
		},
		{
			"Repel2",
			(new Region(0, 0, 1920, 1040), new Region(0, 0, 1280, 680))
		},
		{
			"Potion0",
			(new Region(0, 0, 1920, 1040), new Region(0, 0, 1280, 680))
		},
		{
			"SuperPotion0",
			(new Region(0, 0, 1920, 1040), new Region(0, 0, 1280, 680))
		},
		{
			"HyperPotion0",
			(new Region(0, 0, 1920, 1040), new Region(0, 0, 1280, 680))
		},
		{
			"Leppa0",
			(new Region(0, 0, 1920, 1040), new Region(0, 0, 1280, 680))
		},
		{
			"Disabled",
			(new Region(0, 0, 1920, 1040), new Region(0, 0, 1280, 680))
		},
		{
			"Item2",
			(new Region(0, 0, 1920, 1040), new Region(0, 0, 1280, 680))
		},
		{
			"SC0",
			(new Region(0, 0, 1920, 1040), new Region(0, 0, 1280, 680))
		}
	};

	private static readonly (Region hd, Region sd) PokemonRegion = (new Region(316, 71, 1359, 285), new Region(223, 73, 1024, 253));

	private static readonly (Region hd, Region sd) PotionRegion = (new Region(286, 529, 591, 788), new Region(190, 277, 495, 536));

	[DllImport("Search.dll")]
	public static extern IntPtr ImageSearch(int x, int y, int right, int bottom, [MarshalAs(UnmanagedType.LPStr)] string imagePath);

	private string SearchRegion(string path, Region r)
	{
		IntPtr ptr = ImageSearch(r.X, r.Y, r.Right, r.Bottom, path);
		return Marshal.PtrToStringAnsi(ptr) ?? "0";
	}

	private Region? GetRegion(string fname, bool isHD)
	{
		if (RegionMap.TryGetValue(fname, out var value))
		{
			return isHD ? value.Item1 : value.Item2;
		}
		if (fname.Equals(Bot.Instance.Settings.ChosenPokeBall.ToString()))
		{
			return isHD ? PotionRegion.hd : PotionRegion.sd;
		}
		if (!Enum.IsDefined(typeof(Pokemon), fname))
		{
			return null;
		}
		return isHD ? PokemonRegion.hd : PokemonRegion.sd;
	}

	private string CheckZeroPP(string path, bool isHD)
	{
		int num = 8;
		Region[] array = ((!isHD) ? new Region[4]
		{
			new Region(326, 452 - num, 379, 467),
			new Region(538, 452 - num, 591, 467),
			new Region(326, 506 - num, 379, 521),
			new Region(538, 506 - num, 591, 521)
		} : new Region[4]
		{
			new Region(422, 704 - num, 475, 719),
			new Region(634, 704 - num, 687, 719),
			new Region(422, 758 - num, 475, 773),
			new Region(634, 758 - num, 687, 773)
		});
		string text = SearchRegion(path, array[0]);
		Bot.Instance.Status.FirstMovePP0 = !string.IsNullOrEmpty(text) && text[0] != '0';
		text = SearchRegion(path, array[1]);
		Bot.Instance.Status.SecondMovePP0 = !string.IsNullOrEmpty(text) && text[0] != '0';
		text = SearchRegion(path, array[2]);
		Bot.Instance.Status.ThirdMovePP0 = !string.IsNullOrEmpty(text) && text[0] != '0';
		text = SearchRegion(path, array[3]);
		Bot.Instance.Status.FourthMovePP0 = !string.IsNullOrEmpty(text) && text[0] != '0';
		if (Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 || Bot.Instance.Status.FourthMovePP0)
		{
			UIHelper.SetStatus("Status: One of 4 Moves has 0PP");
		}
		return text;
	}

	private string CheckItem(string path, bool isHD)
	{
		int num = 8;
		Region r = (isHD ? new Region(280, 620 - num, 307, 644) : new Region(181, 364 - num, 211, 394));
		string text = SearchRegion(path, r);
		if (!string.IsNullOrEmpty(text) && text[0] != '0')
		{
			Bot.Instance.Status.DetectedItem = true;
		}
		return text;
	}

	public int[] UseImageSearch(string path, int tolerance)
	{
		string text = "0";
		filename = path.Substring(8).Replace(".png", "");
		if (filename.Contains("mon/"))
		{
			filename = filename.Replace("mon/", "");
		}
		string text2 = path;
		path = "*" + tolerance + " " + path;
		bool flag = BotSettings.Settings.ResolutionMode == ResolutionMode.HD;
		if (!filename.Equals("ZeroPP") && !filename.Equals("ZeroPPH"))
		{
			if (!filename.Equals("Item"))
			{
				Region? region = GetRegion(filename, flag);
				if (!region.HasValue)
				{
					region = (flag ? new Region(0, 0, 1920, 1040) : new Region(0, 0, 1280, 680));
				}
				text = SearchRegion(path, region.Value);
			}
			else
			{
				text = CheckItem(path, flag);
			}
		}
		else
		{
			text = CheckZeroPP(path, flag);
		}
		if (!string.IsNullOrEmpty(text) && text[0] != '0')
		{
			UIHelper.SetStatus("Status: Detected " + filename);
			string[] array = text.Split('|');
			if (array.Length < 3)
			{
				return null;
			}
			int[] array2 = new int[3];
			int[] array3 = new int[3];
			array2[1] = Convert.ToInt32(array[1]);
			array2[2] = Convert.ToInt32(array[2]);
			int width;
			int height;
			using (Bitmap bitmap = new Bitmap(text2))
			{
				width = bitmap.Width;
				height = bitmap.Height;
			}
			array3[1] = RandomNumber.Between(array2[1] + 3, array2[1] + width - 3);
			array3[2] = RandomNumber.Between(array2[2] + 3, array2[2] + height - 3);
			return array3;
		}
		return null;
	}
}
