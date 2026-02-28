using System;
using PokeMMO_.Model;
using PokeMMO_.Mvvm;

namespace PokeMMO_.ViewModels;

public class MainViewModel : BindableBase
{
	private static readonly Lazy<MainViewModel> _instance = new Lazy<MainViewModel>(() => new MainViewModel());

	private Home _Home = new Home();

	private Premium _Premium = new Premium();

	private Security _Security = new Security();

	private Auth _Auth = new Auth();

	private Settings _Settings = new Settings();

	public static MainViewModel Instance => _instance.Value;

	public Home Home
	{
		get
		{
			return _Home;
		}
		set
		{
			SetProperty(ref _Home, value, "Home");
		}
	}

	public Premium Premium
	{
		get
		{
			return _Premium;
		}
		set
		{
			SetProperty(ref _Premium, value, "Premium");
		}
	}

	public Security Security
	{
		get
		{
			return _Security;
		}
		set
		{
			SetProperty(ref _Security, value, "Security");
		}
	}

	public Auth Auth
	{
		get
		{
			return _Auth;
		}
		set
		{
			SetProperty(ref _Auth, value, "Auth");
		}
	}

	public Settings Settings
	{
		get
		{
			return _Settings;
		}
		set
		{
			SetProperty(ref _Settings, value, "Settings");
		}
	}
}
