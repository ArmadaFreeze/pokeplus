using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;

namespace PokeMMO_.Classes;

internal class CEDetection
{
	private static readonly string[] CheatEngineFiles = new string[9] { "Cheat Engine.exe", ".CETRAINER", ".CT", "lua53-32.dll", "lua53-64.dll", "speedhack-x86_64.dll", "vehdebug-x86_64.dll", "winhook-x86_64.dll", "cheatengine-x86_64" };

	public static List<string> GetProcessesWithWindow()
	{
		List<string> list = new List<string>();
		using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT ExecutablePath FROM Win32_Process"))
		{
			using ManagementObjectCollection source = managementObjectSearcher.Get();
			foreach (ManagementObject item in source.Cast<ManagementObject>())
			{
				string text = item["ExecutablePath"] as string;
				if (!string.IsNullOrEmpty(text))
				{
					list.Add(text);
				}
			}
		}
		return list;
	}

	public static bool FindCEExe(List<string> paths)
	{
		foreach (string path in paths)
		{
			try
			{
				string directoryName = Path.GetDirectoryName(path);
				if (directoryName == null)
				{
					continue;
				}
				string[] files = Directory.GetFiles(directoryName);
				foreach (string f in files)
				{
					if (CheatEngineFiles.Any((string ce) => f.IndexOf(ce, StringComparison.OrdinalIgnoreCase) >= 0))
					{
						return true;
					}
				}
			}
			catch (Exception)
			{
			}
		}
		return false;
	}
}
