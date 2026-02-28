using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace PokeMMO_.Classes;

public class IniFile
{
	private const int MaxValueLength = 255;

	private readonly string _path;

	private readonly string _exeName = Assembly.GetExecutingAssembly().GetName().Name;

	[DllImport("kernel32", CharSet = CharSet.Unicode)]
	private static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

	[DllImport("kernel32", CharSet = CharSet.Unicode)]
	private static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

	public IniFile(string IniPath = null)
	{
		_path = new FileInfo(IniPath ?? (_exeName + ".ini")).FullName;
	}

	public string Read(string Key, string Section = null)
	{
		StringBuilder stringBuilder = new StringBuilder(255);
		GetPrivateProfileString(Section ?? _exeName, Key, "", stringBuilder, 255, _path);
		return stringBuilder.ToString();
	}

	public void Write(string Key, string Value, string Section = null)
	{
		WritePrivateProfileString(Section ?? _exeName, Key, Value, _path);
	}

	public void Write<T>(string Key, T Value, string Section = null)
	{
		WritePrivateProfileString(Section ?? _exeName, Key, Value.ToString(), _path);
	}

	public void DeleteKey(string Key, string Section = null)
	{
		Write(Key, null, Section ?? _exeName);
	}

	public void DeleteSection(string Section = null)
	{
		Write(null, null, Section ?? _exeName);
	}

	public bool KeyExists(string Key, string Section = null)
	{
		return Read(Key, Section).Length > 0;
	}
}
