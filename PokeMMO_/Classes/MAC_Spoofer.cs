// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.MAC_Spoofer
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

#nullable disable
namespace PokeMMO_.Classes;

public class MAC_Spoofer
{
  private static RegistryKey NetworkClass = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}\\");
  private RegistryKey NetworkInterface;
  private ManagementObject NetworkAdapter;
  private string RegPath = "Computer\\HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}\\";
  public string Device;
  public string DriverDesc;

  private static string GenerateID(int i) => i.ToString().PadLeft(4, '0');

  public static string GenerateRandomMAC()
  {
    Random random = new Random((int) DateTime.Now.ToFileTimeUtc());
    string str = "0123456789ABCDEF";
    string randomMac = "";
    for (int index = 1; index < 12; ++index)
      randomMac += str[random.Next(0, 15)].ToString();
    return randomMac;
  }

  private bool DisableNetworkDriver()
  {
    bool flag;
    try
    {
      if ((uint) this.NetworkAdapter.InvokeMethod("Disable", (object[]) null) == 0U)
      {
        flag = true;
        goto label_5;
      }
    }
    catch (Exception ex)
    {
    }
    flag = false;
label_5:
    return flag;
  }

  private bool EnableNetworkDriver()
  {
    bool flag;
    try
    {
      if ((uint) this.NetworkAdapter.InvokeMethod("Enable", (object[]) null) == 0U)
      {
        flag = true;
        goto label_5;
      }
    }
    catch (Exception ex)
    {
    }
    flag = false;
label_5:
    return flag;
  }

  public static List<string> GetDeviceIDs()
  {
    List<string> deviceIds = new List<string>();
    for (int i = 0; i <= 9999; ++i)
    {
      string id = MAC_Spoofer.GenerateID(i);
      if (MAC_Spoofer.NetworkClass.OpenSubKey(id) != null)
        deviceIds.Add(id);
      else
        break;
    }
    return deviceIds;
  }

  public static string GetDriverDescByID(string id)
  {
    return MAC_Spoofer.NetworkClass.OpenSubKey(id).GetValue("DriverDesc").ToString();
  }

  public MAC_Spoofer(string DeviceID)
  {
    this.DriverDesc = MAC_Spoofer.GetDriverDescByID(DeviceID);
    this.Device = DeviceID;
    this.NetworkInterface = MAC_Spoofer.NetworkClass.OpenSubKey(DeviceID, true);
    this.NetworkAdapter = new ManagementObjectSearcher($"select * from win32_networkadapter where Name='{this.DriverDesc}'").Get().Cast<ManagementObject>().FirstOrDefault<ManagementObject>();
  }

  public bool Spoof(string MAC)
  {
    bool flag;
    if (!this.DisableNetworkDriver())
    {
      this.NetworkInterface.SetValue("NetworkAddress", (object) MAC, RegistryValueKind.String);
      flag = !this.EnableNetworkDriver();
    }
    else
      flag = false;
    return flag;
  }

  public bool Spoof()
  {
    bool flag;
    if (this.DisableNetworkDriver())
    {
      try
      {
        this.NetworkInterface.SetValue("NetworkAddress", (object) MAC_Spoofer.GenerateRandomMAC(), RegistryValueKind.String);
      }
      catch
      {
        flag = !this.EnableNetworkDriver() && false;
        goto label_5;
      }
      flag = this.EnableNetworkDriver();
    }
    else
      flag = false;
label_5:
    return flag;
  }

  public bool Reset()
  {
    bool flag;
    if (!this.DisableNetworkDriver())
    {
      flag = false;
    }
    else
    {
      try
      {
        this.NetworkInterface.DeleteValue("NetworkAddress");
      }
      catch
      {
        flag = this.EnableNetworkDriver() && false;
        goto label_5;
      }
      flag = this.EnableNetworkDriver();
    }
label_5:
    return flag;
  }
}
