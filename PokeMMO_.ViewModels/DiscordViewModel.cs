using System;
using PokeMMO_.Mvvm;

namespace PokeMMO_.ViewModels;

public class DiscordViewModel : BindableBase
{
	private static readonly Lazy<DiscordViewModel> _instance = new Lazy<DiscordViewModel>(() => new DiscordViewModel());

	private bool _DiscordDMThief = true;

	private bool _DiscordDMIV31 = true;

	private bool _DiscordDMPayDay = true;

	private bool _DiscordDMThrowBall = true;

	public static DiscordViewModel Instance => _instance.Value;

	public bool DiscordDMThief
	{
		get
		{
			return _DiscordDMThief;
		}
		set
		{
			SetProperty(ref _DiscordDMThief, value, "DiscordDMThief");
		}
	}

	public bool DiscordDMIV31
	{
		get
		{
			return _DiscordDMIV31;
		}
		set
		{
			SetProperty(ref _DiscordDMIV31, value, "DiscordDMIV31");
		}
	}

	public bool DiscordDMPayDay
	{
		get
		{
			return _DiscordDMPayDay;
		}
		set
		{
			SetProperty(ref _DiscordDMPayDay, value, "DiscordDMPayDay");
		}
	}

	public bool DiscordDMThrowBall
	{
		get
		{
			return _DiscordDMThrowBall;
		}
		set
		{
			SetProperty(ref _DiscordDMThrowBall, value, "DiscordDMThrowBall");
		}
	}
}
