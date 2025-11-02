// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Input.AgentClient
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

#nullable disable
namespace PokeMMO_.Input;

public class AgentClient : IDisposable
{
  private readonly object lockObj = new object();
  private readonly string host;
  private readonly int port;
  private TcpClient client;
  private StreamWriter writer;

  private bool IsConnectedStable
  {
    get
    {
      if (this.client == null || !this.client.Connected)
        return false;
      return !this.client.Client.Poll(0, SelectMode.SelectRead) || this.client.Client.Available != 0;
    }
  }

  public bool IsConnected => this.IsConnectedStable;

  public AgentClient(string host = "127.0.0.1", int port = 18888)
  {
    this.host = host;
    this.port = port;
  }

  public void Start()
  {
    lock (this.lockObj)
    {
      if (!this.IsConnectedStable)
      {
        this.SafeClose();
        try
        {
          this.client = new TcpClient();
          this.client.NoDelay = true;
          this.client.Connect(this.host, this.port);
          this.writer = new StreamWriter((Stream) this.client.GetStream(), Encoding.UTF8)
          {
            AutoFlush = true
          };
          Console.WriteLine("[C#] Connected to Java Agent.");
        }
        catch (Exception ex)
        {
          this.SafeClose();
          Console.WriteLine("[C#] Failed to connect: " + ex.Message);
        }
      }
      else
        Console.WriteLine("[C#] Already connected; reusing session.");
    }
  }

  public void Stop()
  {
    lock (this.lockObj)
    {
      this.SafeClose();
      Console.WriteLine("[C#] Disconnected from Java Agent.");
    }
  }

  public void SendRaw(string command)
  {
    if (string.IsNullOrWhiteSpace(command))
      return;
    lock (this.lockObj)
    {
      if (!this.EnsureConnected())
        return;
      try
      {
        this.writer.WriteLine(command);
      }
      catch (Exception ex1)
      {
        Console.WriteLine("[C#] Write failed: " + ex1.Message);
        if (!this.Reconnect())
          return;
        try
        {
          this.writer.WriteLine(command);
        }
        catch (Exception ex2)
        {
          Console.WriteLine("[C#] Write failed after reconnect: " + ex2.Message);
        }
      }
    }
  }

  public void SendKeyDown(string key) => this.SendRaw("keydown " + key);

  public void SendKeyUp(string key) => this.SendRaw("keyup " + key);

  public void SendMouseDown(string button) => this.SendRaw("mousedown " + button);

  public void SendMouseUp(string button) => this.SendRaw("mouseup " + button);

  public void SendMouseMove(double x, double y) => this.SendRaw($"mousemove {x} {y}");

  public void Dispose() => this.Stop();

  private bool EnsureConnected() => this.IsConnectedStable || this.Reconnect();

  private bool Reconnect()
  {
    this.SafeClose();
    try
    {
      this.client = new TcpClient();
      this.client.NoDelay = true;
      this.client.Connect(this.host, this.port);
      this.writer = new StreamWriter((Stream) this.client.GetStream(), Encoding.UTF8)
      {
        AutoFlush = true
      };
      Console.WriteLine("[C#] Reconnected to Java Agent.");
      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine("[C#] Reconnect failed: " + ex.Message);
      this.SafeClose();
      return false;
    }
  }

  private void SafeClose()
  {
    try
    {
      ((AgentClient) this.writer)?.method_0();
    }
    catch
    {
    }
    try
    {
      this.client?.Close();
    }
    catch
    {
    }
    this.writer = (StreamWriter) null;
    this.client = (TcpClient) null;
  }

  void method_0() => __nonvirtual (((TextWriter) this).Dispose());
}
