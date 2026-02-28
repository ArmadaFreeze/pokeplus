using System;
using PokeMMO_.Model;
using PokeMMO_.Mvvm;

namespace PokeMMO_.ViewModels;

public class ChosenPokeBallViewModel : BindableBase
{
	private static readonly Lazy<ChosenPokeBallViewModel> _instance = new Lazy<ChosenPokeBallViewModel>(() => new ChosenPokeBallViewModel());

	private ChosenPokeBall _ChosenPokeBall = ChosenPokeBall.PokeBall;

	public static ChosenPokeBallViewModel Instance => _instance.Value;

	public ChosenPokeBall ChosenPokeBall
	{
		get
		{
			return _ChosenPokeBall;
		}
		set
		{
			SetProperty(ref _ChosenPokeBall, value, "ChosenPokeBall");
		}
	}
}
