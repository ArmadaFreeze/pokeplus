using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Microsoft.Win32;

namespace PokeMMO_.Classes;

public class MAC_Spoofer
{
	private static readonly string NetworkClassPath = "SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}\\";

	private readonly RegistryKey NetworkInterface;

	private readonly ManagementObject NetworkAdapter;

	public string Device;

	public string DriverDesc;

	private static string GenerateID(int i)
	{
		return i.ToString().PadLeft(4, '0');
	}

	public static string GenerateRandomMAC()
	{
		Random random = new Random();
		char[] array = new char[12];
		for (int i = 0; i < 12; i++)
		{
			array[i] = "0123456789ABCDEF"[random.Next(0, 16)];
		}
		return new string(array);
	}

	private bool DisableNetworkDriver()
	{
		try
		{
			return NetworkAdapter != null && (uint)NetworkAdapter.InvokeMethod("Disable", null) == 0;
		}
		catch
		{
			return false;
		}
	}

	private bool EnableNetworkDriver()
	{
		try
		{
			return NetworkAdapter != null && (uint)NetworkAdapter.InvokeMethod("Enable", null) == 0;
		}
		catch
		{
			return false;
		}
	}

	public static List<string> GetDeviceIDs()
	{
		List<string> list = new List<string>();
		using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(NetworkClassPath))
		{
			if (registryKey == null)
			{
				return list;
			}
			for (int i = 0; i <= 9999; i++)
			{
				string text = GenerateID(i);
				using RegistryKey registryKey2 = registryKey.OpenSubKey(text);
				if (registryKey2 == null)
				{
					break;
				}
				list.Add(text);
				continue;
			}
		}
		return list;
	}

	public static string GetDriverDescByID(string id)
	{
		using RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(NetworkClassPath);
		using RegistryKey registryKey2 = registryKey?.OpenSubKey(id);
		return registryKey2?.GetValue("DriverDesc")?.ToString();
	}

	public MAC_Spoofer(string DeviceID)
	{
		DriverDesc = GetDriverDescByID(DeviceID);
		Device = DeviceID;
		using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(NetworkClassPath))
		{
			NetworkInterface = registryKey?.OpenSubKey(DeviceID, writable: true);
		}
		string text = DriverDesc?.Replace("'", "\\'");
		NetworkAdapter = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE Name='" + text + "'").Get().Cast<ManagementObject>().FirstOrDefault();
	}

	public bool Spoof(string MAC)
	{
		if (NetworkInterface != null && DisableNetworkDriver())
		{
			NetworkInterface.SetValue("NetworkAddress", MAC, RegistryValueKind.String);
			if (!EnableNetworkDriver())
			{
				return false;
			}
			return true;
		}
		return false;
	}

	public bool Spoof()
	{
		if (NetworkInterface == null || !DisableNetworkDriver())
		{
			return false;
		}
		try
		{
			NetworkInterface.SetValue("NetworkAddress", GenerateRandomMAC(), RegistryValueKind.String);
		}
		catch
		{
			EnableNetworkDriver();
			return false;
		}
		if (!EnableNetworkDriver())
		{
			return false;
		}
		return true;
	}

	public bool Reset()
	{
		if (NetworkInterface == null || !DisableNetworkDriver())
		{
			return false;
		}
		try
		{
			NetworkInterface.DeleteValue("NetworkAddress");
		}
		catch
		{
			EnableNetworkDriver();
			return false;
		}
		if (EnableNetworkDriver())
		{
			return true;
		}
		return false;
	}
}
