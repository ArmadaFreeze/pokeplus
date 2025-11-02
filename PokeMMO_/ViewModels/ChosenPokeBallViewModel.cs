// Decompiled with JetBrains decompiler
// Type: PokeMMO_.ViewModels.ChosenPokeBallViewModel
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Model;
using PokeMMO_.Mvvm;

#nullable disable
namespace PokeMMO_.ViewModels;

public class ChosenPokeBallViewModel : BindableBase
{
  private static readonly object padlock = new object();
  private static ChosenPokeBallViewModel instance = (ChosenPokeBallViewModel) null;
  private ChosenPokeBall _ChosenPokeBall = ChosenPokeBall.PokeBall;

  public static ChosenPokeBallViewModel Instance
  {
    get
    {
      lock (ChosenPokeBallViewModel.padlock)
      {
        if (ChosenPokeBallViewModel.instance == null)
          ChosenPokeBallViewModel.instance = new ChosenPokeBallViewModel();
        return ChosenPokeBallViewModel.instance;
      }
    }
  }

  public ChosenPokeBall ChosenPokeBall
  {
    get => this._ChosenPokeBall;
    set
    {
      this.SetProperty<ChosenPokeBall>(ref this._ChosenPokeBall, value, nameof (ChosenPokeBall));
    }
  }
}
