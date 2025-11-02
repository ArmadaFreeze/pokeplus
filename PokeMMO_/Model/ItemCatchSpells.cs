// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Model.ItemCatchSpells
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System.ComponentModel;

#nullable disable
namespace PokeMMO_.Model;

public class ItemCatchSpells : INotifyPropertyChanged
{
  private bool _selected;
  private string _catchspells;

  public string CatchSpells
  {
    get => this._catchspells;
    set
    {
      this._catchspells = value;
      this.EmitChange(nameof (CatchSpells));
    }
  }

  public bool Selected
  {
    get => this._selected;
    set
    {
      this._selected = value;
      this.EmitChange(nameof (Selected));
    }
  }

  private void EmitChange(params string[] catchspells)
  {
    if (this.PropertyChanged == null)
      return;
    foreach (string catchspell in catchspells)
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(catchspell));
  }

  public event PropertyChangedEventHandler PropertyChanged;
}
