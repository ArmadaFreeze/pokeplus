// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.PokeMMOLogger
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System;
using System.IO;
using System.Text;

#nullable disable
namespace PokeMMO_.Classes;

public class PokeMMOLogger : IDisposable
{
  private static PokeMMOLogger instance;
  private readonly string logFilePath;
  private readonly long maxLogFileSize;
  private StreamWriter logStreamWriter;
  private FileStream logFileStream;

  private PokeMMOLogger(string logFileName, long maxFileSizeInBytes)
  {
    this.logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logFileName);
    this.maxLogFileSize = maxFileSizeInBytes;
    this.InitializeLogFile();
  }

  public static PokeMMOLogger Instance
  {
    get
    {
      if (PokeMMOLogger.instance == null)
        PokeMMOLogger.instance = new PokeMMOLogger("log.log", 1048576L /*0x100000*/);
      return PokeMMOLogger.instance;
    }
  }

  private void InitializeLogFile()
  {
    this.logFileStream = new FileStream(this.logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
    this.logStreamWriter = new StreamWriter((Stream) this.logFileStream, Encoding.UTF8);
  }

  public void Log(string message)
  {
    try
    {
      lock (this.logStreamWriter)
      {
        if (this.logFileStream.Length >= this.maxLogFileSize)
          this.RotateLogFile();
        this.logStreamWriter.WriteLine($"{DateTime.Now} - {message}");
        this.logStreamWriter.Flush();
      }
    }
    catch (Exception ex)
    {
    }
  }

  private void RotateLogFile()
  {
    this.logStreamWriter.Close();
    this.logFileStream.Close();
    File.Move(this.logFilePath, $"{this.logFilePath}_{DateTime.Now:yyyyMMddHHmmss}.log");
    this.InitializeLogFile();
  }

  public void Dispose()
  {
    this.logStreamWriter.Close();
    this.logFileStream.Close();
  }
}
