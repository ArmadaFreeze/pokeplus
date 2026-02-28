using System;
using System.IO;
using System.Text;

namespace PokeMMO_.Classes;

public class PokeMMOLogger : IDisposable
{
	private static readonly Lazy<PokeMMOLogger> _instance = new Lazy<PokeMMOLogger>(() => new PokeMMOLogger("log.log", 1048576L));

	private readonly object _lock = new object();

	private readonly string logFilePath;

	private readonly long maxLogFileSize;

	private StreamWriter logStreamWriter;

	private FileStream logFileStream;

	public static PokeMMOLogger Instance => _instance.Value;

	private PokeMMOLogger(string logFileName, long maxFileSizeInBytes)
	{
		logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logFileName);
		maxLogFileSize = maxFileSizeInBytes;
		InitializeLogFile();
	}

	private void InitializeLogFile()
	{
		try
		{
			logFileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
			logStreamWriter = new StreamWriter(logFileStream, Encoding.UTF8);
		}
		catch
		{
		}
	}

	public void Log(string message)
	{
		lock (_lock)
		{
			try
			{
				if (logStreamWriter != null)
				{
					if (logFileStream.Length >= maxLogFileSize)
					{
						RotateLogFile();
					}
					logStreamWriter.WriteLine($"{DateTime.Now} - {message}");
					logStreamWriter.Flush();
				}
			}
			catch
			{
			}
		}
	}

	private void RotateLogFile()
	{
		try
		{
			logStreamWriter.Close();
			logFileStream.Close();
			string destFileName = $"{logFilePath}_{DateTime.Now:yyyyMMddHHmmss}.log";
			File.Move(logFilePath, destFileName);
		}
		catch
		{
		}
		InitializeLogFile();
	}

	public void Dispose()
	{
		logStreamWriter?.Close();
		logFileStream?.Close();
		GC.SuppressFinalize(this);
	}
}
