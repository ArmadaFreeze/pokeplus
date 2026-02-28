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
using Cryptographic;

namespace KeyAuth;

public class api
{
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
		public user_data_structure info { get; set; }

		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public app_data_structure appinfo { get; set; }

		[DataMember]
		public List<msg> messages { get; set; }

		[DataMember]
		public List<users> users { get; set; }

		[DataMember(Name = "2fa", IsRequired = false, EmitDefaultValue = false)]
		public TwoFactorData twoFactor { get; set; }
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
		public List<Data> subscriptions { get; set; }
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

		public List<Data> subscriptions { get; set; }
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

	public string name;

	public string ownerid;

	public string version;

	public string path;

	public string seed;

	private static string sessionid;

	private static string enckey;

	private bool initialized;

	public app_data_class app_data = new app_data_class();

	public user_data_class user_data = new user_data_class();

	public response_class response = new response_class();

	private json_wrapper response_decoder = new json_wrapper(new response_structure());

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
			error("Application not setup correctly. Please watch the YouTube video for setup.");
			TerminateProcess(GetCurrentProcess(), 1u);
		}
		this.name = name;
		this.ownerid = ownerid;
		this.version = version;
		this.path = path;
	}

	public void init()
	{
		Random random = new Random();
		int num = random.Next(5, 51);
		StringBuilder stringBuilder = new StringBuilder(num);
		for (int i = 0; i < num; i++)
		{
			char value = (char)random.Next(32, 127);
			stringBuilder.Append(value);
		}
		seed = stringBuilder.ToString();
		checkAtom();
		NameValueCollection nameValueCollection = new NameValueCollection
		{
			["type"] = "init",
			["ver"] = version,
			["hash"] = checksum(Process.GetCurrentProcess().MainModule.FileName),
			["name"] = name,
			["ownerid"] = ownerid
		};
		if (!string.IsNullOrEmpty(path))
		{
			nameValueCollection.Add("token", File.ReadAllText(path));
			nameValueCollection.Add("thash", TokenHash(path));
		}
		string text = req(nameValueCollection);
		if (text == "KeyAuth_Invalid")
		{
			error("Application not found");
			TerminateProcess(GetCurrentProcess(), 1u);
		}
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(text);
		if (!(response_structure.ownerid == ownerid))
		{
			TerminateProcess(GetCurrentProcess(), 1u);
			return;
		}
		load_response_struct(response_structure);
		if (response_structure.success)
		{
			sessionid = response_structure.sessionid;
			initialized = true;
		}
		else if (response_structure.message == "invalidver")
		{
			app_data.downloadLink = response_structure.download;
		}
	}

	private void checkAtom()
	{
		Thread thread = new Thread((ThreadStart)delegate
		{
			while (true)
			{
				Thread.Sleep(60000);
				if (GlobalFindAtom(seed) == 0)
				{
					TerminateProcess(GetCurrentProcess(), 1u);
				}
			}
		});
		thread.IsBackground = true;
		thread.Start();
	}

	public static string TokenHash(string tokenPath)
	{
		using SHA256 sHA = SHA256.Create();
		using FileStream inputStream = File.OpenRead(tokenPath);
		byte[] array = sHA.ComputeHash(inputStream);
		return BitConverter.ToString(array).Replace("-", string.Empty);
	}

	public void CheckInit()
	{
		if (!initialized)
		{
			error("You must run the function KeyAuthApp.init(); first");
			TerminateProcess(GetCurrentProcess(), 1u);
		}
	}

	public string expirydaysleft(string Type, int subscription)
	{
		CheckInit();
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local).AddSeconds(long.Parse(user_data.subscriptions[subscription].expiry)).ToLocalTime();
		TimeSpan timeSpan = dateTime - DateTime.Now;
		return Type.ToLower() switch
		{
			"days" => Convert.ToString(timeSpan.Days), 
			"hours" => Convert.ToString(timeSpan.Hours), 
			"months" => Convert.ToString(timeSpan.Days / 30), 
			_ => null, 
		};
	}

	public void register(string username, string pass, string key, string email = "")
	{
		CheckInit();
		string value = WindowsIdentity.GetCurrent().User.Value;
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "register",
			["username"] = username,
			["pass"] = pass,
			["key"] = key,
			["email"] = email,
			["hwid"] = value,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		if (response_structure.ownerid == ownerid)
		{
			GlobalAddAtom(seed);
			GlobalAddAtom(ownerid);
			load_response_struct(response_structure);
			if (response_structure.success)
			{
				load_user_data(response_structure.info);
			}
		}
		else
		{
			TerminateProcess(GetCurrentProcess(), 1u);
		}
	}

	public void forgot(string username, string email)
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "forgot",
			["username"] = username,
			["email"] = email,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure data = response_decoder.string_to_generic<response_structure>(json);
		load_response_struct(data);
	}

	public void login(string username, string pass, string code = null)
	{
		CheckInit();
		string value = WindowsIdentity.GetCurrent().User.Value;
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "login",
			["username"] = username,
			["pass"] = pass,
			["hwid"] = value,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid,
			["code"] = code ?? string.Empty
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		if (response_structure.ownerid == ownerid)
		{
			GlobalAddAtom(seed);
			GlobalAddAtom(ownerid);
			load_response_struct(response_structure);
			if (response_structure.success)
			{
				load_user_data(response_structure.info);
			}
		}
		else
		{
			TerminateProcess(GetCurrentProcess(), 1u);
		}
	}

	public void logout()
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "logout",
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		if (response_structure.ownerid == ownerid)
		{
			load_response_struct(response_structure);
		}
		else
		{
			TerminateProcess(GetCurrentProcess(), 1u);
		}
	}

	public void web_login()
	{
		CheckInit();
		string value = WindowsIdentity.GetCurrent().User.Value;
		HttpListener httpListener;
		HttpListenerRequest request;
		HttpListenerResponse httpListenerResponse;
		while (true)
		{
			httpListener = new HttpListener();
			string text = "handshake";
			text = "http://localhost:1337/" + text + "/";
			httpListener.Prefixes.Add(text);
			httpListener.Start();
			HttpListenerContext context = httpListener.GetContext();
			request = context.Request;
			httpListenerResponse = context.Response;
			httpListenerResponse.AddHeader("Access-Control-Allow-Methods", "GET, POST");
			httpListenerResponse.AddHeader("Access-Control-Allow-Origin", "*");
			httpListenerResponse.AddHeader("Via", "hugzho's big brain");
			httpListenerResponse.AddHeader("Location", "your kernel ;)");
			httpListenerResponse.AddHeader("Retry-After", "never lmao");
			httpListenerResponse.Headers.Add("Server", "\r\n\r\n");
			if (!(request.HttpMethod == "OPTIONS"))
			{
				break;
			}
			httpListenerResponse.StatusCode = 200;
			Thread.Sleep(1);
			httpListener.Stop();
		}
		httpListener.AuthenticationSchemes = AuthenticationSchemes.Negotiate;
		httpListener.UnsafeConnectionNtlmAuthentication = true;
		httpListener.IgnoreWriteExceptions = true;
		string rawUrl = request.RawUrl;
		string text2 = rawUrl.Replace("/handshake?user=", "");
		text2 = text2.Replace("&token=", " ");
		string text3 = text2;
		string value2 = text3.Split()[0];
		string value3 = text3.Split(' ')[1];
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "login",
			["username"] = value2,
			["token"] = value3,
			["hwid"] = value,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		bool flag = true;
		if (!(response_structure.ownerid == ownerid))
		{
			TerminateProcess(GetCurrentProcess(), 1u);
		}
		else
		{
			GlobalAddAtom(seed);
			GlobalAddAtom(ownerid);
			load_response_struct(response_structure);
			if (response_structure.success)
			{
				load_user_data(response_structure.info);
				httpListenerResponse.StatusCode = 420;
				httpListenerResponse.StatusDescription = "SHEESH";
			}
			else
			{
				Console.WriteLine(response_structure.message);
				httpListenerResponse.StatusCode = 200;
				httpListenerResponse.StatusDescription = response_structure.message;
				flag = false;
			}
		}
		byte[] bytes = Encoding.UTF8.GetBytes("Complete");
		httpListenerResponse.ContentLength64 = bytes.Length;
		Stream outputStream = httpListenerResponse.OutputStream;
		outputStream.Write(bytes, 0, bytes.Length);
		Thread.Sleep(1);
		httpListener.Stop();
		if (!flag)
		{
			TerminateProcess(GetCurrentProcess(), 1u);
		}
	}

	public void button(string button)
	{
		CheckInit();
		HttpListener httpListener = new HttpListener();
		string text = button;
		text = "http://localhost:1337/" + text + "/";
		httpListener.Prefixes.Add(text);
		httpListener.Start();
		HttpListenerContext context = httpListener.GetContext();
		_ = context.Request;
		HttpListenerResponse httpListenerResponse = context.Response;
		httpListenerResponse.AddHeader("Access-Control-Allow-Methods", "GET, POST");
		httpListenerResponse.AddHeader("Access-Control-Allow-Origin", "*");
		httpListenerResponse.AddHeader("Via", "hugzho's big brain");
		httpListenerResponse.AddHeader("Location", "your kernel ;)");
		httpListenerResponse.AddHeader("Retry-After", "never lmao");
		httpListenerResponse.Headers.Add("Server", "\r\n\r\n");
		httpListenerResponse.StatusCode = 420;
		httpListenerResponse.StatusDescription = "SHEESH";
		httpListener.AuthenticationSchemes = AuthenticationSchemes.Negotiate;
		httpListener.UnsafeConnectionNtlmAuthentication = true;
		httpListener.IgnoreWriteExceptions = true;
		httpListener.Stop();
	}

	public void upgrade(string username, string key)
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "upgrade",
			["username"] = username,
			["key"] = key,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		if (!(response_structure.ownerid == ownerid))
		{
			TerminateProcess(GetCurrentProcess(), 1u);
			return;
		}
		response_structure.success = false;
		load_response_struct(response_structure);
	}

	public void license(string key, string code = null)
	{
		CheckInit();
		string value = WindowsIdentity.GetCurrent().User.Value;
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "license",
			["key"] = key,
			["hwid"] = value,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid,
			["code"] = code ?? string.Empty
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		if (response_structure.ownerid == ownerid)
		{
			GlobalAddAtom(seed);
			GlobalAddAtom(ownerid);
			load_response_struct(response_structure);
			if (response_structure.success)
			{
				load_user_data(response_structure.info);
			}
		}
		else
		{
			TerminateProcess(GetCurrentProcess(), 1u);
		}
	}

	public void check()
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "check",
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		if (!(response_structure.ownerid == ownerid))
		{
			TerminateProcess(GetCurrentProcess(), 1u);
		}
		else
		{
			load_response_struct(response_structure);
		}
	}

	public void disable2fa(string code)
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "2fadisable",
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid,
			["code"] = code
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		load_response_struct(response_structure);
		Console.WriteLine(response_structure.message);
	}

	public void enable2fa(string code = null)
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "2faenable",
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid,
			["code"] = code
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		load_response_struct(response_structure);
		if (!response_structure.success)
		{
			Console.WriteLine("Error: " + response_structure.message);
			Thread.Sleep(3000);
			TerminateProcess(GetCurrentProcess(), 1u);
		}
		else if (code == null)
		{
			Console.WriteLine("Your 2FA Secret is: " + response_structure.twoFactor.SecretCode);
			Console.Write("Enter the 6 digit authentication code from your authentication app: ");
			string code2 = Console.ReadLine();
			enable2fa(code2);
		}
		else
		{
			Console.WriteLine("2FA has been successfully enabled!");
			Thread.Sleep(3000);
		}
	}

	public void setvar(string var, string data)
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "setvar",
			["var"] = var,
			["data"] = data,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		if (response_structure.ownerid == ownerid)
		{
			load_response_struct(response_structure);
		}
		else
		{
			TerminateProcess(GetCurrentProcess(), 1u);
		}
	}

	public string getvar(string var)
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "getvar",
			["var"] = var,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		if (response_structure.ownerid == ownerid)
		{
			load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.response;
			}
		}
		else
		{
			TerminateProcess(GetCurrentProcess(), 1u);
		}
		return null;
	}

	public void ban(string reason = null)
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "ban",
			["reason"] = reason,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		if (!(response_structure.ownerid == ownerid))
		{
			TerminateProcess(GetCurrentProcess(), 1u);
		}
		else
		{
			load_response_struct(response_structure);
		}
	}

	public string var(string varid)
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "var",
			["varid"] = varid,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		if (response_structure.ownerid == ownerid)
		{
			load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.message;
			}
		}
		else
		{
			TerminateProcess(GetCurrentProcess(), 1u);
		}
		return null;
	}

	public List<users> fetchOnline()
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "fetchOnline",
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		load_response_struct(response_structure);
		if (response_structure.success)
		{
			return response_structure.users;
		}
		return null;
	}

	public void fetchStats()
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "fetchStats",
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		load_response_struct(response_structure);
		if (response_structure.success)
		{
			load_app_data(response_structure.appinfo);
		}
	}

	public List<msg> chatget(string channelname)
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "chatget",
			["channel"] = channelname,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		load_response_struct(response_structure);
		if (!response_structure.success)
		{
			return null;
		}
		return response_structure.messages;
	}

	public bool chatsend(string msg, string channelname)
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "chatsend",
			["message"] = msg,
			["channel"] = channelname,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		load_response_struct(response_structure);
		if (response_structure.success)
		{
			return true;
		}
		return false;
	}

	public bool checkblack()
	{
		CheckInit();
		string value = WindowsIdentity.GetCurrent().User.Value;
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "checkblacklist",
			["hwid"] = value,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		if (!(response_structure.ownerid == ownerid))
		{
			TerminateProcess(GetCurrentProcess(), 1u);
			return true;
		}
		load_response_struct(response_structure);
		if (!response_structure.success)
		{
			return false;
		}
		return true;
	}

	public string webhook(string webid, string param, string body = "", string conttype = "")
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "webhook",
			["webid"] = webid,
			["params"] = param,
			["body"] = body,
			["conttype"] = conttype,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		if (!(response_structure.ownerid == ownerid))
		{
			TerminateProcess(GetCurrentProcess(), 1u);
		}
		else
		{
			load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.response;
			}
		}
		return null;
	}

	public byte[] download(string fileid)
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "file",
			["fileid"] = fileid,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure response_structure = response_decoder.string_to_generic<response_structure>(json);
		load_response_struct(response_structure);
		if (!response_structure.success)
		{
			return null;
		}
		return encryption.str_to_byte_arr(response_structure.contents);
	}

	public void log(string message)
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "log",
			["pcuser"] = Environment.UserName,
			["message"] = message,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		req(post_data);
	}

	public void changeUsername(string username)
	{
		CheckInit();
		NameValueCollection post_data = new NameValueCollection
		{
			["type"] = "changeUsername",
			["newUsername"] = username,
			["sessionid"] = sessionid,
			["name"] = name,
			["ownerid"] = ownerid
		};
		string json = req(post_data);
		response_structure data = response_decoder.string_to_generic<response_structure>(json);
		load_response_struct(data);
	}

	public static string checksum(string filename)
	{
		using MD5 mD = MD5.Create();
		using FileStream inputStream = File.OpenRead(filename);
		byte[] array = mD.ComputeHash(inputStream);
		return BitConverter.ToString(array).Replace("-", "").ToLowerInvariant();
	}

	public static void error(string message)
	{
		string path = "Logs";
		string text = Path.Combine(path, "ErrorLogs.txt");
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		if (!File.Exists(text))
		{
			using (File.Create(text))
			{
				File.AppendAllText(text, DateTime.Now.ToString() + " > This is the start of your error logs file");
			}
		}
		File.AppendAllText(text, DateTime.Now.ToString() + " > " + message + Environment.NewLine);
		Console.Error.WriteLine("Error: " + message);
		Console.Error.WriteLine("Press any key to exit");
		Console.ReadKey();
		Environment.Exit(0);
	}

	private static string req(NameValueCollection post_data)
	{
		try
		{
			using WebClient webClient = new WebClient();
			webClient.Proxy = null;
			ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(assertSSL));
			byte[] bytes = webClient.UploadValues("https://keyauth.win/api/1.3/", post_data);
			ServicePointManager.ServerCertificateValidationCallback = (object _003Cp0_003E, X509Certificate _003Cp1_003E, X509Chain _003Cp2_003E, SslPolicyErrors _003Cp3_003E) => true;
			sigCheck(Encoding.UTF8.GetString(bytes), webClient.ResponseHeaders, post_data.Get(0));
			Logger.LogEvent(Encoding.Default.GetString(bytes) + "\n");
			return Encoding.Default.GetString(bytes);
		}
		catch (WebException ex)
		{
			HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
			HttpStatusCode statusCode = httpWebResponse.StatusCode;
			HttpStatusCode httpStatusCode = statusCode;
			if (httpStatusCode == (HttpStatusCode)429)
			{
				error("You're connecting too fast to loader, slow down.");
				Logger.LogEvent("You're connecting too fast to loader, slow down.");
				TerminateProcess(GetCurrentProcess(), 1u);
				return "";
			}
			error("Connection failure. Please try again, or contact us for help.");
			Logger.LogEvent("Connection failure. Please try again, or contact us for help.");
			TerminateProcess(GetCurrentProcess(), 1u);
			return "";
		}
	}

	private static bool assertSSL(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	{
		if ((!certificate.Issuer.Contains("Google Trust Services") && !certificate.Issuer.Contains("Let's Encrypt")) || sslPolicyErrors != 0)
		{
			error("SSL assertion fail, make sure you're not debugging Network. Disable internet firewall on router if possible. & echo: & echo If not, ask the developer of the program to use custom domains to fix this.");
			Logger.LogEvent("SSL assertion fail, make sure you're not debugging Network. Disable internet firewall on router if possible. If not, ask the developer of the program to use custom domains to fix this.");
			return false;
		}
		return true;
	}

	private static void sigCheck(string resp, WebHeaderCollection headers, string type)
	{
		switch (type)
		{
		case "file":
			return;
		case "2faenable":
			return;
		}
		if (type == "2fadisable")
		{
			return;
		}
		try
		{
			string hex = headers["x-signature-ed25519"];
			string text = headers["x-signature-timestamp"];
			if (!long.TryParse(text, out var result))
			{
				error("Failed to parse the timestamp from the server. Please ensure your device's date and time settings are correct.");
				TerminateProcess(GetCurrentProcess(), 1u);
			}
			DateTime utcDateTime = DateTimeOffset.FromUnixTimeSeconds(result).UtcDateTime;
			DateTime utcNow = DateTime.UtcNow;
			if ((utcNow - utcDateTime).TotalSeconds > 20.0)
			{
				error("Date/Time settings aren't synced on your device, please sync them to use the program");
				TerminateProcess(GetCurrentProcess(), 1u);
			}
			byte[] signature = encryption.str_to_byte_arr(hex);
			byte[] publicKey = encryption.str_to_byte_arr("5586b4bc69c7a4b487e4563a4cd96afd39140f919bd31cea7d1c6a1e8439422b");
			string s = text + resp;
			byte[] bytes = Encoding.Default.GetBytes(s);
			Console.Write(" Authenticating");
			if (!Ed25519.CheckValid(signature, bytes, publicKey))
			{
				error("Signature checksum failed. Request was tampered with or session ended most likely. & echo: & echo Response: " + resp);
				Logger.LogEvent(resp + "\n");
				TerminateProcess(GetCurrentProcess(), 1u);
			}
		}
		catch
		{
			error("Signature checksum failed. Request was tampered with or session ended most likely. & echo: & echo Response: " + resp);
			Logger.LogEvent(resp + "\n");
			TerminateProcess(GetCurrentProcess(), 1u);
		}
	}

	private void load_app_data(app_data_structure data)
	{
		app_data.numUsers = data.numUsers;
		app_data.numOnlineUsers = data.numOnlineUsers;
		app_data.numKeys = data.numKeys;
		app_data.version = data.version;
		app_data.customerPanelLink = data.customerPanelLink;
	}

	private void load_user_data(user_data_structure data)
	{
		user_data.username = data.username;
		user_data.ip = data.ip;
		user_data.hwid = data.hwid;
		user_data.createdate = data.createdate;
		user_data.lastlogin = data.lastlogin;
		user_data.subscriptions = data.subscriptions;
	}

	private void load_response_struct(response_structure data)
	{
		response.success = data.success;
		response.message = data.message;
	}
}
