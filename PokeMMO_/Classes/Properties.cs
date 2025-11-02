// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.Properties
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace PokeMMO_.Classes;

public class Properties
{
  private Dictionary<string, string> list;
  private string filename;

  public Properties(string file) => this.reload(file);

  public string get(string field, string defValue)
  {
    return this.get(field) == null ? defValue : this.get(field);
  }

  public string get(string field)
  {
    return this.list.ContainsKey(field) ? this.list[field] : (string) null;
  }

  public void set(string field, object value)
  {
    if (this.list.ContainsKey(field))
      this.list[field] = value.ToString();
    else
      this.list.Add(field, value.ToString());
  }

  public void Save() => this.Save(this.filename);

  public void Save(string filename)
  {
    this.filename = filename;
    if (!File.Exists(filename))
      File.Create(filename);
    StreamWriter streamWriter = new StreamWriter(filename);
    foreach (string key in this.list.Keys.ToArray<string>())
    {
      if (!string.IsNullOrWhiteSpace(this.list[key]))
        streamWriter.WriteLine($"{key}={this.list[key]}");
    }
    streamWriter.Close();
  }

  public void reload() => this.reload(this.filename);

  public void reload(string filename)
  {
    this.filename = filename;
    this.list = new Dictionary<string, string>();
    if (!File.Exists(filename))
      File.Create(filename);
    else
      this.loadFromFile(filename);
  }

  private void loadFromFile(string file)
  {
    foreach (string readAllLine in File.ReadAllLines(file))
    {
      if ((string.IsNullOrEmpty(readAllLine) || readAllLine.StartsWith(";") || readAllLine.StartsWith("#") || readAllLine.StartsWith("'") ? 0 : (readAllLine.Contains<char>('=') ? 1 : 0)) != 0)
      {
        int length = readAllLine.IndexOf('=');
        string key = readAllLine.Substring(0, length).Trim();
        string str = readAllLine.Substring(length + 1).Trim();
        if ((!str.StartsWith("\"") || !str.EndsWith("\"") ? (!str.StartsWith("'") ? 0 : (str.EndsWith("'") ? 1 : 0)) : 1) != 0)
          str = str.Substring(1, str.Length - 2);
        try
        {
          this.list.Add(key, str);
        }
        catch
        {
        }
      }
    }
  }
}
