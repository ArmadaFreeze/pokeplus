using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace KeyAuth;

public static class Logger
{
	public static bool IsLoggingEnabled
	{
		[CompilerGenerated]
		set
		{
			_003CIsLoggingEnabled_003Ek__BackingField = value;
		}
	}

	public static void LogEvent(string content)
	{
		if (!_003CIsLoggingEnabled_003Ek__BackingField)
		{
			return;
		}
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
		string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "KeyAuth", "debug", fileNameWithoutExtension);
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string path = $"{DateTime.Now:MMM_dd_yyyy}_logs.txt";
		string path2 = Path.Combine(text, path);
		try
		{
			content = RedactField(content, "sessionid");
			content = RedactField(content, "ownerid");
			content = RedactField(content, "app");
			content = RedactField(content, "version");
			content = RedactField(content, "fileid");
			content = RedactField(content, "webhooks");
			content = RedactField(content, "nonce");
			using StreamWriter streamWriter = File.AppendText(path2);
			streamWriter.WriteLine($"[{DateTime.Now}] [{AppDomain.CurrentDomain.FriendlyName}] {content}");
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error logging data: " + ex.Message);
		}
	}

	private static string RedactField(string content, string fieldName)
	{
		string pattern = "\"" + fieldName + "\":\"[^\"]*\"";
		string replacement = "\"" + fieldName + "\":\"REDACTED\"";
		return Regex.Replace(content, pattern, replacement);
	}
}
