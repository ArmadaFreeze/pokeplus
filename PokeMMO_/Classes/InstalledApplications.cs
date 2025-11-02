// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.InstalledApplications
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using Microsoft.Win32;
using System;

#nullable disable
namespace PokeMMO_.Classes;

public static class InstalledApplications
{
  public static string GetApplictionInstallPath(string nameOfAppToFind)
  {
    string subKeyName1 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
    string str1 = InstalledApplications.ExistsInSubKey(RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32), subKeyName1, "DisplayName", nameOfAppToFind);
    string applictionInstallPath;
    if (!string.IsNullOrEmpty(str1))
    {
      applictionInstallPath = str1;
    }
    else
    {
      string subKeyName2 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
      string str2 = InstalledApplications.ExistsInSubKey(RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64), subKeyName2, "DisplayName", nameOfAppToFind);
      if (!string.IsNullOrEmpty(str2))
      {
        applictionInstallPath = str2;
      }
      else
      {
        string subKeyName3 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
        string str3 = InstalledApplications.ExistsInSubKey(RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32), subKeyName3, "DisplayName", nameOfAppToFind);
        if (!string.IsNullOrEmpty(str3))
        {
          applictionInstallPath = str3;
        }
        else
        {
          string subKeyName4 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
          string str4 = InstalledApplications.ExistsInSubKey(RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64), subKeyName4, "DisplayName", nameOfAppToFind);
          applictionInstallPath = !string.IsNullOrEmpty(str4) ? str4 : string.Empty;
        }
      }
    }
    return applictionInstallPath;
  }

  private static string ExistsInSubKey(
    RegistryKey root,
    string subKeyName,
    string attributeName,
    string nameOfAppToFind)
  {
    string empty;
    using (RegistryKey registryKey1 = root.OpenSubKey(subKeyName))
    {
      if (registryKey1 != null)
      {
        foreach (string subKeyName1 in registryKey1.GetSubKeyNames())
        {
          RegistryKey registryKey2;
          using (registryKey2 = registryKey1.OpenSubKey(subKeyName1))
          {
            string str = registryKey2.GetValue(attributeName) as string;
            if (nameOfAppToFind.Equals(str, StringComparison.OrdinalIgnoreCase))
            {
              empty = registryKey2.GetValue("InstallLocation") as string;
              goto label_15;
            }
          }
        }
      }
    }
    empty = string.Empty;
label_15:
    return empty;
  }
}
