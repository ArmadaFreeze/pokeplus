using System;
using PokeMMO_.Mvvm;

namespace PokeMMO_.ViewModels;

public class AuthViewModel : BindableBase
{
	private static readonly Lazy<AuthViewModel> _instance = new Lazy<AuthViewModel>(() => new AuthViewModel());

	private bool _AutoLogin = false;

	public static AuthViewModel Instance => _instance.Value;

	public bool AutoLogin
	{
		get
		{
			return _AutoLogin;
		}
		set
		{
			SetProperty(ref _AutoLogin, value, "AutoLogin");
		}
	}
}
