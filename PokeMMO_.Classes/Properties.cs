using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PokeMMO_.Classes;

public class Properties
{
	private Dictionary<string, string> list;

	private string filename;

	public Properties(string file)
	{
		reload(file);
	}

	public string get(string field, string defValue)
	{
		return get(field) ?? defValue;
	}

	public string get(string field)
	{
		string value;
		return list.TryGetValue(field, out value) ? value : null;
	}

	public void set(string field, object value)
	{
		list[field] = value.ToString();
	}

	public void Save()
	{
		Save(filename);
	}

	public void Save(string filename)
	{
		this.filename = filename;
		using StreamWriter streamWriter = new StreamWriter(filename);
		string[] array = list.Keys.ToArray();
		foreach (string text in array)
		{
			if (!string.IsNullOrWhiteSpace(list[text]))
			{
				streamWriter.WriteLine(text + "=" + list[text]);
			}
		}
	}

	public void reload()
	{
		reload(filename);
	}

	public void reload(string filename)
	{
		this.filename = filename;
		list = new Dictionary<string, string>();
		if (File.Exists(filename))
		{
			loadFromFile(filename);
		}
	}

	private void loadFromFile(string file)
	{
		string[] array = File.ReadAllLines(file);
		foreach (string text in array)
		{
			if (!string.IsNullOrEmpty(text) && !text.StartsWith(";") && !text.StartsWith("#") && !text.StartsWith("'") && text.Contains('='))
			{
				int num = text.IndexOf('=');
				string key = text.Substring(0, num).Trim();
				string text2 = text.Substring(num + 1).Trim();
				if ((text2.StartsWith("\"") && text2.EndsWith("\"")) || (text2.StartsWith("'") && text2.EndsWith("'")))
				{
					text2 = text2.Substring(1, text2.Length - 2);
				}
				if (!list.ContainsKey(key))
				{
					list.Add(key, text2);
				}
			}
		}
	}
}
