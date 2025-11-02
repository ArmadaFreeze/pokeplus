// Decompiled with JetBrains decompiler
// Type: KeyAuth.api
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading;

#nullable disable
namespace KeyAuth;

public class api
{
  public string name;
  public string ownerid;
  public string version;
  public string path;
  public string seed;
  private static string sessionid;
  private static string enckey;
  private bool initialized;
  public api.app_data_class app_data = new api.app_data_class();
  public api.user_data_class user_data = new api.user_data_class();
  public api.response_class response = new api.response_class();
  private json_wrapper response_decoder = new json_wrapper((object) new api.response_structure());

  [DllImport("kernel32.dll", SetLastError = true)]
  private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

  [DllImport("kernel32.dll", SetLastError = true)]
  private static extern IntPtr GetCurrentProcess();

  [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern ushort GlobalAddAtom(string lpString);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern ushort GlobalFindAtom(string lpString);

  public api(string name, string ownerid, string version, string path = null)
  {
    if (ownerid.Length != 10)
    {
      Process.Start("https://youtube.com/watch?v=RfDTdiBq4_o");
      Process.Start("https://keyauth.cc/app/");
      Thread.Sleep(2000);
      api.error("Application not setup correctly. Please watch the YouTube video for setup.");
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
    }
    this.name = name;
    this.ownerid = ownerid;
    this.version = version;
    this.path = path;
  }

  public void init()
  {
    Random random = new Random();
    int capacity = random.Next(5, 51);
    StringBuilder stringBuilder = new StringBuilder(capacity);
    for (int index = 0; index < capacity; ++index)
    {
      char ch = (char) random.Next(32 /*0x20*/, (int) sbyte.MaxValue);
      stringBuilder.Append(ch);
    }
    this.seed = stringBuilder.ToString();
    NameValueCollection post_data = new NameValueCollection()
    {
      ["type"] = nameof (init),
      ["ver"] = this.version,
      ["hash"] = api.checksum(Process.GetCurrentProcess().MainModule.FileName),
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    };
    if (!string.IsNullOrEmpty(this.path))
    {
      post_data.Add("token", System.IO.File.ReadAllText(this.path));
      post_data.Add("thash", api.TokenHash(this.path));
    }
    string json = api.req(post_data);
    if (json == "KeyAuth_Invalid")
    {
      api.error("Application not found");
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
    }
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(json);
    if (!(generic.ownerid == this.ownerid))
    {
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
    }
    else
    {
      this.load_response_struct(generic);
      if (!generic.success)
      {
        if (!(generic.message == "invalidver"))
          return;
        this.app_data.downloadLink = generic.download;
      }
      else
      {
        api.sessionid = generic.sessionid;
        this.initialized = true;
      }
    }
  }

  private void checkAtom()
  {
    new Thread((ThreadStart) (() =>
    {
      while (true)
      {
        do
        {
          Thread.Sleep(60000);
        }
        while (api.GlobalFindAtom(this.seed) != (ushort) 0);
        api.TerminateProcess(api.GetCurrentProcess(), 1U);
      }
    }))
    {
      IsBackground = true
    }.Start();
  }

  public static string TokenHash(string tokenPath)
  {
    using (SHA256 shA256 = SHA256.Create())
    {
      using (FileStream inputStream = System.IO.File.OpenRead(tokenPath))
        return BitConverter.ToString(shA256.ComputeHash((Stream) inputStream)).Replace("-", string.Empty);
    }
  }

  public void CheckInit()
  {
    if (this.initialized)
      return;
    api.error("You must run the function KeyAuthApp.init(); first");
    api.TerminateProcess(api.GetCurrentProcess(), 1U);
  }

  public string expirydaysleft(string Type, int subscription)
  {
    this.CheckInit();
    TimeSpan timeSpan = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local).AddSeconds((double) long.Parse(this.user_data.subscriptions[subscription].expiry)).ToLocalTime() - DateTime.Now;
    string str;
    switch (Type.ToLower())
    {
      case "months":
        str = Convert.ToString(timeSpan.Days / 30);
        break;
      case "days":
        str = Convert.ToString(timeSpan.Days);
        break;
      case "hours":
        str = Convert.ToString(timeSpan.Hours);
        break;
      default:
        str = (string) null;
        break;
    }
    return str;
  }

  public void register(string username, string pass, string key, string email = "")
  {
    this.CheckInit();
    string str = WindowsIdentity.GetCurrent().User.Value;
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (register),
      [nameof (username)] = username,
      [nameof (pass)] = pass,
      [nameof (key)] = key,
      [nameof (email)] = email,
      ["hwid"] = str,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    if (generic.ownerid == this.ownerid)
    {
      int num1 = (int) api.GlobalAddAtom(this.seed);
      int num2 = (int) api.GlobalAddAtom(this.ownerid);
      this.load_response_struct(generic);
      if (!generic.success)
        return;
      this.load_user_data(generic.info);
    }
    else
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
  }

  public void forgot(string username, string email)
  {
    this.CheckInit();
    this.load_response_struct(this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (forgot),
      [nameof (username)] = username,
      [nameof (email)] = email,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    })));
  }

  public void login(string username, string pass, string code = null)
  {
    this.CheckInit();
    string str = WindowsIdentity.GetCurrent().User.Value;
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (login),
      [nameof (username)] = username,
      [nameof (pass)] = pass,
      ["hwid"] = str,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid,
      [nameof (code)] = code ?? string.Empty
    }));
    if (!(generic.ownerid == this.ownerid))
    {
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
    }
    else
    {
      int num1 = (int) api.GlobalAddAtom(this.seed);
      int num2 = (int) api.GlobalAddAtom(this.ownerid);
      this.load_response_struct(generic);
      if (generic.success)
        this.load_user_data(generic.info);
      this.checkAtom();
    }
  }

  public void logout()
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (logout),
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    if (!(generic.ownerid == this.ownerid))
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
    else
      this.load_response_struct(generic);
  }

  public void web_login()
  {
    this.CheckInit();
    string str1 = WindowsIdentity.GetCurrent().User.Value;
    HttpListenerResponse response;
    HttpListener httpListener;
    HttpListenerRequest request;
    while (true)
    {
      httpListener = new HttpListener();
      string uriPrefix = $"http://localhost:1337/handshake/";
      httpListener.Prefixes.Add(uriPrefix);
      httpListener.Start();
      HttpListenerContext context = httpListener.GetContext();
      request = context.Request;
      response = context.Response;
      response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
      response.AddHeader("Access-Control-Allow-Origin", "*");
      response.AddHeader("Via", "hugzho's big brain");
      response.AddHeader("Location", "your kernel ;)");
      response.AddHeader("Retry-After", "never lmao");
      response.Headers.Add("Server", "\r\n\r\n");
      if (request.HttpMethod == "OPTIONS")
      {
        response.StatusCode = 200;
        Thread.Sleep(1);
        httpListener.Stop();
      }
      else
        break;
    }
    httpListener.AuthenticationSchemes = AuthenticationSchemes.Negotiate;
    httpListener.UnsafeConnectionNtlmAuthentication = true;
    httpListener.IgnoreWriteExceptions = true;
    string str2 = request.RawUrl.Replace("/handshake?user=", "").Replace("&token=", " ");
    string str3 = str2.Split()[0];
    string str4 = str2.Split(' ')[1];
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = "login",
      ["username"] = str3,
      ["token"] = str4,
      ["hwid"] = str1,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    bool flag = true;
    if (generic.ownerid == this.ownerid)
    {
      int num1 = (int) api.GlobalAddAtom(this.seed);
      int num2 = (int) api.GlobalAddAtom(this.ownerid);
      this.load_response_struct(generic);
      if (!generic.success)
      {
        Console.WriteLine(generic.message);
        response.StatusCode = 200;
        response.StatusDescription = generic.message;
        flag = false;
      }
      else
      {
        this.load_user_data(generic.info);
        response.StatusCode = 420;
        response.StatusDescription = "SHEESH";
      }
    }
    else
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
    byte[] bytes = Encoding.UTF8.GetBytes("Complete");
    response.ContentLength64 = (long) bytes.Length;
    response.OutputStream.Write(bytes, 0, bytes.Length);
    Thread.Sleep(1);
    httpListener.Stop();
    if (flag)
      return;
    api.TerminateProcess(api.GetCurrentProcess(), 1U);
  }

  public void button(string button)
  {
    this.CheckInit();
    HttpListener httpListener = new HttpListener();
    string uriPrefix = $"http://localhost:1337/{button}/";
    httpListener.Prefixes.Add(uriPrefix);
    httpListener.Start();
    HttpListenerContext context = httpListener.GetContext();
    HttpListenerRequest request = context.Request;
    HttpListenerResponse response = context.Response;
    response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
    response.AddHeader("Access-Control-Allow-Origin", "*");
    response.AddHeader("Via", "hugzho's big brain");
    response.AddHeader("Location", "your kernel ;)");
    response.AddHeader("Retry-After", "never lmao");
    response.Headers.Add("Server", "\r\n\r\n");
    response.StatusCode = 420;
    response.StatusDescription = "SHEESH";
    httpListener.AuthenticationSchemes = AuthenticationSchemes.Negotiate;
    httpListener.UnsafeConnectionNtlmAuthentication = true;
    httpListener.IgnoreWriteExceptions = true;
    httpListener.Stop();
  }

  public void upgrade(string username, string key)
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (upgrade),
      [nameof (username)] = username,
      [nameof (key)] = key,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    if (!(generic.ownerid == this.ownerid))
    {
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
    }
    else
    {
      generic.success = false;
      this.load_response_struct(generic);
    }
  }

  public void license(string key, string code = null)
  {
    this.CheckInit();
    string str = WindowsIdentity.GetCurrent().User.Value;
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (license),
      [nameof (key)] = key,
      ["hwid"] = str,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid,
      [nameof (code)] = code ?? string.Empty
    }));
    if (!(generic.ownerid == this.ownerid))
    {
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
    }
    else
    {
      int num1 = (int) api.GlobalAddAtom(this.seed);
      int num2 = (int) api.GlobalAddAtom(this.ownerid);
      this.load_response_struct(generic);
      if (!generic.success)
        return;
      this.load_user_data(generic.info);
    }
  }

  public void check()
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (check),
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    if (!(generic.ownerid == this.ownerid))
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
    else
      this.load_response_struct(generic);
  }

  public void disable2fa(string code)
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = "2fadisable",
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid,
      [nameof (code)] = code
    }));
    this.load_response_struct(generic);
    Console.WriteLine(generic.message);
  }

  public void enable2fa(string code = null)
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = "2faenable",
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid,
      [nameof (code)] = code
    }));
    this.load_response_struct(generic);
    if (!generic.success)
    {
      Console.WriteLine("Error: " + generic.message);
      Thread.Sleep(3000);
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
    }
    else if (code != null)
    {
      Console.WriteLine("2FA has been successfully enabled!");
      Thread.Sleep(3000);
    }
    else
    {
      Console.WriteLine("Your 2FA Secret is: " + generic.twoFactor.SecretCode);
      Console.Write("Enter the 6 digit authentication code from your authentication app: ");
      this.enable2fa(Console.ReadLine());
    }
  }

  public void setvar(string var, string data)
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (setvar),
      [nameof (var)] = var,
      [nameof (data)] = data,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    if (generic.ownerid == this.ownerid)
      this.load_response_struct(generic);
    else
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
  }

  public string getvar(string var)
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (getvar),
      [nameof (var)] = var,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    string str;
    if (!(generic.ownerid == this.ownerid))
    {
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
    }
    else
    {
      this.load_response_struct(generic);
      if (generic.success)
      {
        str = generic.response;
        goto label_5;
      }
    }
    str = (string) null;
label_5:
    return str;
  }

  public void ban(string reason = null)
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (ban),
      [nameof (reason)] = reason,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    if (generic.ownerid == this.ownerid)
      this.load_response_struct(generic);
    else
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
  }

  public string var(string varid)
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (var),
      [nameof (varid)] = varid,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    string str;
    if (!(generic.ownerid == this.ownerid))
    {
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
    }
    else
    {
      this.load_response_struct(generic);
      if (generic.success)
      {
        str = generic.message;
        goto label_5;
      }
    }
    str = (string) null;
label_5:
    return str;
  }

  public List<api.users> fetchOnline()
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (fetchOnline),
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    this.load_response_struct(generic);
    return !generic.success ? (List<api.users>) null : generic.users;
  }

  public void fetchStats()
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (fetchStats),
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    this.load_response_struct(generic);
    if (!generic.success)
      return;
    this.load_app_data(generic.appinfo);
  }

  public List<api.msg> chatget(string channelname)
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (chatget),
      ["channel"] = channelname,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    this.load_response_struct(generic);
    return generic.success ? generic.messages : (List<api.msg>) null;
  }

  public bool chatsend(string msg, string channelname)
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (chatsend),
      ["message"] = msg,
      ["channel"] = channelname,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    this.load_response_struct(generic);
    return generic.success;
  }

  public bool checkblack()
  {
    this.CheckInit();
    string str = WindowsIdentity.GetCurrent().User.Value;
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = "checkblacklist",
      ["hwid"] = str,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    bool flag;
    if (!(generic.ownerid == this.ownerid))
    {
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
      flag = true;
    }
    else
    {
      this.load_response_struct(generic);
      flag = generic.success;
    }
    return flag;
  }

  public string webhook(string webid, string param, string body = "", string conttype = "")
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (webhook),
      [nameof (webid)] = webid,
      ["params"] = param,
      [nameof (body)] = body,
      [nameof (conttype)] = conttype,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    string str;
    if (generic.ownerid == this.ownerid)
    {
      this.load_response_struct(generic);
      if (generic.success)
      {
        str = generic.response;
        goto label_5;
      }
    }
    else
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
    str = (string) null;
label_5:
    return str;
  }

  public byte[] download(string fileid)
  {
    this.CheckInit();
    api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = "file",
      [nameof (fileid)] = fileid,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    }));
    this.load_response_struct(generic);
    return generic.success ? encryption.str_to_byte_arr(generic.contents) : (byte[]) null;
  }

  public void log(string message)
  {
    this.CheckInit();
    api.req(new NameValueCollection()
    {
      ["type"] = nameof (log),
      ["pcuser"] = Environment.UserName,
      [nameof (message)] = message,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    });
  }

  public void changeUsername(string username)
  {
    this.CheckInit();
    this.load_response_struct(this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
    {
      ["type"] = nameof (changeUsername),
      ["newUsername"] = username,
      ["sessionid"] = api.sessionid,
      ["name"] = this.name,
      ["ownerid"] = this.ownerid
    })));
  }

  public static string checksum(string filename)
  {
    string lowerInvariant;
    using (MD5 md5 = MD5.Create())
    {
      using (FileStream inputStream = System.IO.File.OpenRead(filename))
        lowerInvariant = BitConverter.ToString(md5.ComputeHash((Stream) inputStream)).Replace("-", "").ToLowerInvariant();
    }
    return lowerInvariant;
  }

  public static void error(string message)
  {
    string str = "Logs";
    string path = Path.Combine(str, "ErrorLogs.txt");
    if (!Directory.Exists(str))
      Directory.CreateDirectory(str);
    if (!System.IO.File.Exists(path))
    {
      using (System.IO.File.Create(path))
        System.IO.File.AppendAllText(path, DateTime.Now.ToString() + " > This is the start of your error logs file");
    }
    System.IO.File.AppendAllText(path, $"{DateTime.Now.ToString()} > {message}{Environment.NewLine}");
    Console.Error.WriteLine("Error: " + message);
    Console.Error.WriteLine("Press any key to exit");
    Console.ReadKey();
    Environment.Exit(0);
  }

  private static string req(NameValueCollection post_data)
  {
    try
    {
      using (WebClient webClient = new WebClient())
      {
        webClient.Proxy = (IWebProxy) null;
        ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(api.assertSSL);
        byte[] bytes = webClient.UploadValues("https://keyauth.win/api/1.3/", post_data);
        ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback) ((_param1, _param2, _param3, _param4) => true);
        api.sigCheck(Encoding.UTF8.GetString(bytes), webClient.ResponseHeaders, post_data.Get(0));
        Logger.LogEvent(Encoding.Default.GetString(bytes) + "\n");
        return Encoding.Default.GetString(bytes);
      }
    }
    catch (WebException ex)
    {
      if (((HttpWebResponse) ex.Response).StatusCode == (HttpStatusCode) 429)
      {
        api.error("You're connecting too fast to loader, slow down.");
        Logger.LogEvent("You're connecting too fast to loader, slow down.");
        api.TerminateProcess(api.GetCurrentProcess(), 1U);
        return "";
      }
      api.error("Connection failure. Please try again, or contact us for help.");
      Logger.LogEvent("Connection failure. Please try again, or contact us for help.");
      api.TerminateProcess(api.GetCurrentProcess(), 1U);
      return "";
    }
  }

  private static bool assertSSL(
    object sender,
    X509Certificate certificate,
    X509Chain chain,
    SslPolicyErrors sslPolicyErrors)
  {
    bool flag;
    if ((certificate.Issuer.Contains("Google Trust Services") || certificate.Issuer.Contains("Let's Encrypt") ? (sslPolicyErrors != 0 ? 1 : 0) : 1) == 0)
    {
      flag = true;
    }
    else
    {
      api.error("SSL assertion fail, make sure you're not debugging Network. Disable internet firewall on router if possible. & echo: & echo If not, ask the developer of the program to use custom domains to fix this.");
      Logger.LogEvent("SSL assertion fail, make sure you're not debugging Network. Disable internet firewall on router if possible. If not, ask the developer of the program to use custom domains to fix this.");
      flag = false;
    }
    return flag;
  }

  private static void sigCheck(string resp, WebHeaderCollection headers, string type)
  {
    if ((type == "log" || type == "file" || type == "2faenable" ? 1 : (type == "2fadisable" ? 1 : 0)) != 0)
      return;
    try
    {
    }
    catch
    {
    }
  }

  private void load_app_data(api.app_data_structure data)
  {
    this.app_data.numUsers = data.numUsers;
    this.app_data.numOnlineUsers = data.numOnlineUsers;
    this.app_data.numKeys = data.numKeys;
    this.app_data.version = data.version;
    this.app_data.customerPanelLink = data.customerPanelLink;
  }

  private void load_user_data(api.user_data_structure data)
  {
    this.user_data.username = data.username;
    this.user_data.ip = data.ip;
    this.user_data.hwid = data.hwid;
    this.user_data.createdate = data.createdate;
    this.user_data.lastlogin = data.lastlogin;
    this.user_data.subscriptions = data.subscriptions;
  }

  private void load_response_struct(api.response_structure data)
  {
    this.response.success = data.success;
    this.response.message = data.message;
  }

  [DataContract]
  private class response_structure
  {
    [DataMember]
    public bool success { get; set; }

    [DataMember]
    public bool newSession { get; set; }

    [DataMember]
    public string sessionid { get; set; }

    [DataMember]
    public string contents { get; set; }

    [DataMember]
    public string response { get; set; }

    [DataMember]
    public string message { get; set; }

    [DataMember]
    public string ownerid { get; set; }

    [DataMember]
    public string download { get; set; }

    [DataMember(IsRequired = false, EmitDefaultValue = false)]
    public api.user_data_structure info { get; set; }

    [DataMember(IsRequired = false, EmitDefaultValue = false)]
    public api.app_data_structure appinfo { get; set; }

    [DataMember]
    public List<api.msg> messages { get; set; }

    [DataMember]
    public List<api.users> users { get; set; }

    [DataMember(Name = "2fa", IsRequired = false, EmitDefaultValue = false)]
    public api.TwoFactorData twoFactor { get; set; }
  }

  public class msg
  {
    public string message { get; set; }

    public string author { get; set; }

    public string timestamp { get; set; }
  }

  public class users
  {
    public string credential { get; set; }
  }

  [DataContract]
  private class user_data_structure
  {
    [DataMember]
    public string username { get; set; }

    [DataMember]
    public string ip { get; set; }

    [DataMember]
    public string hwid { get; set; }

    [DataMember]
    public string createdate { get; set; }

    [DataMember]
    public string lastlogin { get; set; }

    [DataMember]
    public List<api.Data> subscriptions { get; set; }
  }

  [DataContract]
  private class app_data_structure
  {
    [DataMember]
    public string numUsers { get; set; }

    [DataMember]
    public string numOnlineUsers { get; set; }

    [DataMember]
    public string numKeys { get; set; }

    [DataMember]
    public string version { get; set; }

    [DataMember]
    public string customerPanelLink { get; set; }

    [DataMember]
    public string downloadLink { get; set; }
  }

  public class app_data_class
  {
    public string numUsers { get; set; }

    public string numOnlineUsers { get; set; }

    public string numKeys { get; set; }

    public string version { get; set; }

    public string customerPanelLink { get; set; }

    public string downloadLink { get; set; }
  }

  public class user_data_class
  {
    public string username { get; set; }

    public string ip { get; set; }

    public string hwid { get; set; }

    public string createdate { get; set; }

    public string lastlogin { get; set; }

    public List<api.Data> subscriptions { get; set; }
  }

  public class Data
  {
    public string subscription { get; set; }

    public string expiry { get; set; }

    public string timeleft { get; set; }

    public string key { get; set; }
  }

  [DataContract]
  private class TwoFactorData
  {
    [DataMember(Name = "secret_code")]
    public string SecretCode { get; set; }

    [DataMember(Name = "QRCode")]
    public string QRCode { get; set; }
  }

  public class response_class
  {
    public bool success { get; set; }

    public string message { get; set; }
  }
}
