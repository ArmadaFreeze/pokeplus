// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.IniFile
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace PokeMMO_.Classes;

public class IniFile
{
  private string Path;
  private string EXE = Assembly.GetExecutingAssembly().GetName().Name;

  [DllImport("kernel32", CharSet = CharSet.Unicode)]
  private static extern long WritePrivateProfileString(
    string Section,
    string Key,
    string Value,
    string FilePath);

  [DllImport("kernel32", CharSet = CharSet.Unicode)]
  private static extern int GetPrivateProfileString(
    string Section,
    string Key,
    string Default,
    StringBuilder RetVal,
    int Size,
    string FilePath);

  public IniFile(string IniPath = null)
  {
    this.Path = new FileInfo(IniPath ?? this.EXE + ".ini").FullName;
  }

  public string Read(string Key, string Section = null)
  {
    StringBuilder RetVal = new StringBuilder((int) byte.MaxValue);
    IniFile.GetPrivateProfileString(Section ?? this.EXE, Key, "", RetVal, (int) byte.MaxValue, this.Path);
    return RetVal.ToString();
  }

  public void Write(string Key, string Value, string Section = null)
  {
    IniFile.WritePrivateProfileString(Section ?? this.EXE, Key, Value, this.Path);
  }

  public void DeleteKey(string Key, string Section = null)
  {
    this.Write(Key, (string) null, Section ?? this.EXE);
  }

  public void DeleteSection(string Section = null)
  {
    this.Write((string) null, (string) null, Section ?? this.EXE);
  }

  public bool KeyExists(string Key, string Section = null) => this.Read(Key, Section).Length > 0;
}
