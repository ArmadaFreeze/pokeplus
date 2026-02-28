using PokeMMO_.Mvvm;

namespace PokeMMO_.Model;

public class Auth : BindableBase
{
	private string _Status = "Status: FREE";

	private string _ID = "Premium ID: ";

	private string _Username = "Username: ";

	private string _Email = "Email: ";

	private string _Rank = "Rank: ";

	private string _HWID = "HWID: ";

	private string _UserVariable = "User Variable: ";

	private string _IP = "IP: ";

	private string _Expiry = "Expiry: ";

	private string _LastLogin = "Last Login: ";

	private string _RegisterDate = "Register Date: ";

	public string Status
	{
		get
		{
			return _Status;
		}
		set
		{
			SetProperty(ref _Status, value, "Status");
		}
	}

	public string ID
	{
		get
		{
			return _ID;
		}
		set
		{
			SetProperty(ref _ID, value, "ID");
		}
	}

	public string Username
	{
		get
		{
			return _Username;
		}
		set
		{
			SetProperty(ref _Username, value, "Username");
		}
	}

	public string Email
	{
		get
		{
			return _Email;
		}
		set
		{
			SetProperty(ref _Email, value, "Email");
		}
	}

	public string Rank
	{
		get
		{
			return _Rank;
		}
		set
		{
			SetProperty(ref _Rank, value, "Rank");
		}
	}

	public string HWID
	{
		get
		{
			return _HWID;
		}
		set
		{
			SetProperty(ref _HWID, value, "HWID");
		}
	}

	public string UserVariable
	{
		get
		{
			return _UserVariable;
		}
		set
		{
			SetProperty(ref _UserVariable, value, "UserVariable");
		}
	}

	public string IP
	{
		get
		{
			return _IP;
		}
		set
		{
			SetProperty(ref _IP, value, "IP");
		}
	}

	public string Expiry
	{
		get
		{
			return _Expiry;
		}
		set
		{
			SetProperty(ref _Expiry, value, "Expiry");
		}
	}

	public string LastLogin
	{
		get
		{
			return _LastLogin;
		}
		set
		{
			SetProperty(ref _LastLogin, value, "LastLogin");
		}
	}

	public string RegisterDate
	{
		get
		{
			return _RegisterDate;
		}
		set
		{
			SetProperty(ref _RegisterDate, value, "RegisterDate");
		}
	}
}
