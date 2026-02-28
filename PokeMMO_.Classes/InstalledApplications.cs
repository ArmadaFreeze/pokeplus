using System;
using System.IO;
using Microsoft.Win32;

namespace PokeMMO_.Classes;

public static class InstalledApplications
{
	private static readonly RegistryHive[] Hives = new RegistryHive[2]
	{
		RegistryHive.CurrentUser,
		RegistryHive.LocalMachine
	};

	private static readonly RegistryView[] Views = new RegistryView[2]
	{
		RegistryView.Registry32,
		RegistryView.Registry64
	};

	private const string UninstallKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";

	private static readonly string[] CommonPaths = new string[4]
	{
		"C:\\Program Files\\PokeMMO",
		"C:\\Program Files (x86)\\PokeMMO",
		Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "PokeMMO"),
		Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PokeMMO")
	};

	public static string GetApplicationInstallPath(string nameOfAppToFind)
	{
		RegistryHive[] hives = Hives;
		foreach (RegistryHive hKey in hives)
		{
			RegistryView[] views = Views;
			foreach (RegistryView view in views)
			{
				using RegistryKey root = RegistryKey.OpenBaseKey(hKey, view);
				string text = FindInSubKey(root, nameOfAppToFind);
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
		}
		string[] commonPaths = CommonPaths;
		foreach (string text2 in commonPaths)
		{
			if (File.Exists(Path.Combine(text2, "config\\main.properties")))
			{
				return text2;
			}
		}
		return string.Empty;
	}

	private static string FindInSubKey(RegistryKey root, string nameOfAppToFind)
	{
		using (RegistryKey registryKey = root.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall"))
		{
			if (registryKey == null)
			{
				return string.Empty;
			}
			string[] subKeyNames = registryKey.GetSubKeyNames();
			foreach (string name in subKeyNames)
			{
				using RegistryKey registryKey2 = registryKey.OpenSubKey(name);
				if (registryKey2 != null)
				{
					string value = registryKey2.GetValue("DisplayName") as string;
					if (nameOfAppToFind.Equals(value, StringComparison.OrdinalIgnoreCase))
					{
						return (registryKey2.GetValue("InstallLocation") as string) ?? string.Empty;
					}
				}
			}
		}
		return string.Empty;
	}
}
