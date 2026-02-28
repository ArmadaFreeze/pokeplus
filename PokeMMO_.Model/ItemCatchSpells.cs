using PokeMMO_.Mvvm;

namespace PokeMMO_.Model;

public class ItemCatchSpells : BindableBase
{
	private bool _selected;

	private string _catchspells;

	public bool Selected
	{
		get
		{
			return _selected;
		}
		set
		{
			SetProperty(ref _selected, value, "Selected");
		}
	}

	public string CatchSpells
	{
		get
		{
			return _catchspells;
		}
		set
		{
			SetProperty(ref _catchspells, value, "CatchSpells");
		}
	}
}
