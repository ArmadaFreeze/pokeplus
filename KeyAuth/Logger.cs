// Decompiled with JetBrains decompiler
// Type: KeyAuth.Logger
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace KeyAuth
{
    public static class Logger
    {
      //public static bool IsLoggingEnabled
      //{
      //  set => Logger.\u003CIsLoggingEnabled\u003Ek__BackingField = value;
      //}

      public static void LogEvent(string content)
      {
        // ISSUE: reference to a compiler-generated field
        //if (!Logger.\u003CIsLoggingEnabled\u003Ek__BackingField)
        //  return;
        string withoutExtension = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
        string str = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "KeyAuth", "debug", withoutExtension);
        if (!Directory.Exists(str))
          Directory.CreateDirectory(str);
        string path2 = $"{DateTime.Now:MMM_dd_yyyy}_logs.txt";
        string path = Path.Combine(str, path2);
        try
        {
          content = Logger.RedactField(content, "sessionid");
          content = Logger.RedactField(content, "ownerid");
          content = Logger.RedactField(content, "app");
          content = Logger.RedactField(content, "version");
          content = Logger.RedactField(content, "fileid");
          content = Logger.RedactField(content, "webhooks");
          content = Logger.RedactField(content, "nonce");
          using (StreamWriter streamWriter = File.AppendText(path))
            streamWriter.WriteLine($"[{DateTime.Now}] [{AppDomain.CurrentDomain.FriendlyName}] {content}");
        }
        catch (Exception ex)
        {
          Console.WriteLine("Error logging data: " + ex.Message);
        }
      }

      private static string RedactField(string content, string fieldName)
      {
        string pattern = $"\"{fieldName}\":\"[^\"]*\"";
        string replacement = $"\"{fieldName}\":\"REDACTED\"";
        return Regex.Replace(content, pattern, replacement);
      }
    }
}
