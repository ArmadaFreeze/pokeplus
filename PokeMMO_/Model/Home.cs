// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Model.Home
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Botting;
using PokeMMO_.Classes;
using PokeMMO_.Mvvm;
using PokeMMO_.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace PokeMMO_.Model;

public class Home : BindableBase
{
  private bool _StartEnabled;
  private bool _StopEnabled;
  private bool _Walk;
  private bool _Fish;
  private bool _SweetScent;
  private bool _AutoWalkFish;
  private bool _AutoSweetScent;
  private bool _SafariAutoWalk;
  private bool _SafariAutoFish;
  private Visibility _SellBoxVisibility;
  private Visibility _AutoWalkFishRoutesVisibility;
  private Visibility _AutoSweetScentRoutesVisibility;
  private Visibility _SafariAutoWalkRoutesVisibility;
  private Visibility _SafariAutoFishRoutesVisibility;
  private Visibility _CatchPokemonVisibility;
  private Visibility _WalkOptionsVisibility;
  private string _CatchPokemon;
  private string _AutoWalkFishText;
  private bool _WalkDirection;
  private bool _LevelFirst;
  private bool _CatchWithSecondPokemon;
  private bool _LevelFirstEnabled;
  private bool _SquaresWalkPattern;
  private bool _CatchSpellsOrder;
  private bool _RandomWalkPattern;
  private bool _Login;
  private bool _OnlyKeepIV31;
  private Visibility _FightOptionVisibility;
  private Visibility _IV31Visibility;
  private Visibility _CatchSpellsVisbility;
  private Visibility _PayDayOptionVisibility;
  private Visibility _ThiefOptionVisibility;
  private Visibility _SafariOptionVisibility;
  private Visibility _CaughtVisible;
  private Visibility _AutoWalkFishVisibility;
  private Visibility _AutoSweetScentVisibility;
  private string _SellPrice;
  private bool _PayDayMultiTarget;
  private bool _CatchShiny;
  private bool _StopOnShiny;
  private bool _SkipDialog;
  private bool _SkipLearningNew;
  private bool _SkipEvolve;
  private bool _MoreThief;
  private bool _MorePayDay;
  private bool _Lure;
  private bool _Imprison;
  private bool _Rock;
  private bool _Bait;
  private bool _AutoLeppa;
  private bool _AutoEther;
  private BotMode _BotMode;
  private Pokemon _Pokemon;
  private int _CatchPokemonSelectedIndex;
  private int _AutoSweetScentRoutesSelectedIndex;
  private int _AutoWalkFishRoutesSelectedIndex;
  private int _SafariAutoWalkRoutesSelectedIndex;
  private int _SafariAutoFishRoutesSelectedIndex;
  private ComboBoxItem _RoutesSelectedItem;
  private string _FreeRoutes0;
  private string _FreeRoutes1;
  private ObservableCollection<string> _PremiumRoutes;
  private string _SafariWalkRoutes0;
  private string _SafariWalkRoutes1;
  private string _SafariWalkRoutes2;
  private string _SafariWalkRoutes3;
  private string _SafariFishRoutes0;
  private bool _PremiumEnabled;
  private bool _FreeEnabled;
  private string _LatestUploadLink;

  public ObservableCollection<ItemCatchSpells> Options { get; set; }

  public DelegateCommand ScreenshotPokemonNameCommand { get; }

  public DelegateCommand ChoosePokeBallCommand { get; }

  public Home()
  {
    ObservableCollection<string> observableCollection = new ObservableCollection<string>();
    observableCollection.Add("Kanto_Premium");
    observableCollection.Add("Kanto_Premium");
    observableCollection.Add("Kanto_Premium");
    observableCollection.Add("Kanto_Premium");
    observableCollection.Add("Kanto_Premium");
    observableCollection.Add("Kanto_Premium");
    observableCollection.Add("Hoenn_Premium");
    observableCollection.Add("Hoenn_Premium");
    observableCollection.Add("Hoenn_Premium");
    observableCollection.Add("Hoenn_Premium");
    observableCollection.Add("Hoenn_Premium");
    observableCollection.Add("Hoenn_Premium");
    observableCollection.Add("Hoenn_Premium");
    observableCollection.Add("Sinnoh_Premium");
    observableCollection.Add("Sinnoh_Premium");
    observableCollection.Add("Sinnoh_Premium");
    observableCollection.Add("Sinnoh_Premium");
    observableCollection.Add("Sinnoh_Premium");
    observableCollection.Add("Kanto_Premium");
    observableCollection.Add("Kanto_Premium");
    observableCollection.Add("Kanto_Premium");
    observableCollection.Add("Kanto_Premium");
    observableCollection.Add("Kanto_Premium");
    observableCollection.Add("Kanto_Premium");
    observableCollection.Add("Hoenn_Premium");
    observableCollection.Add("Hoenn_Premium");
    observableCollection.Add("Hoenn_Premium");
    observableCollection.Add("Kanto_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Sinnoh_Premium");
    observableCollection.Add("Sinnoh_Premium");
    observableCollection.Add("Sinnoh_Premium");
    observableCollection.Add("Sinnoh_Premium");
    observableCollection.Add("Sinnoh_Premium");
    observableCollection.Add("Sinnoh_Premium");
    observableCollection.Add("Sinnoh_Premium");
    observableCollection.Add("Sinnoh_Premium");
    observableCollection.Add("Sinnoh_Premium");
    observableCollection.Add("Unova_Premium");
    observableCollection.Add("Unova_Premium");
    this._PremiumRoutes = observableCollection;
    this._SafariWalkRoutes0 = "Kanto_Safari_Walk_1";
    this._SafariWalkRoutes1 = "Hoenn_Safari_Walk_1";
    this._SafariWalkRoutes2 = "Hoenn_Safari_Walk_2_Repel";
    this._SafariWalkRoutes3 = "Hoenn_Safari_Walk_3";
    this._SafariFishRoutes0 = "Kanto_Safari_Fish_1";
    this._PremiumEnabled = MainWindow.DevelopmentMode;
    this._FreeEnabled = false;
    this._LatestUploadLink = "";
    // ISSUE: explicit constructor call
    this.ChoosePokeBallCommand = new DelegateCommand((Action) (() => Application.Current.Dispatcher.Invoke((Action) (() => Application.Current.Windows.OfType<ChosenPokeBallWindow>().SingleOrDefault<ChosenPokeBallWindow>().Show()))), (Func<bool>) (() => true));
    this.ScreenshotPokemonNameCommand = new DelegateCommand((Action) (() => ScreenCapture.PokemonName()), (Func<bool>) (() => true));
    string[] strArray = new string[4]
    {
      "Substitute",
      "False Swipe",
      "Spore",
      "Assist"
    };
    this.Options = new ObservableCollection<ItemCatchSpells>();
    foreach (string str in strArray)
      this.Options.Add(new ItemCatchSpells()
      {
        CatchSpells = str,
        Selected = false
      });
    this.StartCommand = new DelegateCommand((Action) (() => Bot.Instance.Start()), (Func<bool>) (() => true));
    this.StopCommand = new DelegateCommand((Action) (() => Bot.Instance.Stop()), (Func<bool>) (() => true));
    this.ChoosePokeBallCommand.RaiseCanExecuteChanged();
    this.ScreenshotPokemonNameCommand.RaiseCanExecuteChanged();
    this.StartCommand.RaiseCanExecuteChanged();
    this.StopCommand.RaiseCanExecuteChanged();
  }

  public bool StartEnabled
  {
    get => this._StartEnabled;
    set => this.SetProperty<bool>(ref this._StartEnabled, value, nameof (StartEnabled));
  }

  public bool StopEnabled
  {
    get => this._StopEnabled;
    set => this.SetProperty<bool>(ref this._StopEnabled, value, nameof (StopEnabled));
  }

  public bool Walk
  {
    get => this._Walk;
    set
    {
      this.SetProperty<bool>(ref this._Walk, value, nameof (Walk));
      if (!value)
        return;
      this.AutoSweetScentRoutesVisibility = Visibility.Hidden;
      this.AutoWalkFishRoutesVisibility = Visibility.Hidden;
      this.SafariAutoWalkRoutesVisibility = Visibility.Hidden;
      this.SafariAutoFishRoutesVisibility = Visibility.Hidden;
      this.WalkOptionsVisibility = Visibility.Visible;
    }
  }

  public bool Fish
  {
    get => this._Fish;
    set
    {
      this.SetProperty<bool>(ref this._Fish, value, nameof (Fish));
      if (!value)
        return;
      this.AutoSweetScentRoutesVisibility = Visibility.Hidden;
      this.AutoWalkFishRoutesVisibility = Visibility.Hidden;
      this.SafariAutoWalkRoutesVisibility = Visibility.Hidden;
      this.SafariAutoFishRoutesVisibility = Visibility.Hidden;
      this.WalkOptionsVisibility = Visibility.Hidden;
    }
  }

  public bool SweetScent
  {
    get => this._SweetScent;
    set
    {
      this.SetProperty<bool>(ref this._SweetScent, value, nameof (SweetScent));
      if (!value)
        return;
      this.AutoSweetScentRoutesVisibility = Visibility.Hidden;
      this.AutoWalkFishRoutesVisibility = Visibility.Hidden;
      this.SafariAutoWalkRoutesVisibility = Visibility.Hidden;
      this.SafariAutoFishRoutesVisibility = Visibility.Hidden;
      this.WalkOptionsVisibility = Visibility.Hidden;
    }
  }

  public bool AutoWalkFish
  {
    get => this._AutoWalkFish;
    set
    {
      this.SetProperty<bool>(ref this._AutoWalkFish, value, nameof (AutoWalkFish));
      if (!value)
        return;
      this.WalkOptionsVisibility = Visibility.Hidden;
      this.SafariAutoWalkRoutesVisibility = Visibility.Hidden;
      this.SafariAutoFishRoutesVisibility = Visibility.Hidden;
      this.AutoSweetScentRoutesVisibility = Visibility.Hidden;
      this.AutoWalkFishRoutesVisibility = Visibility.Visible;
    }
  }

  public bool AutoSweetScent
  {
    get => this._AutoSweetScent;
    set
    {
      this.SetProperty<bool>(ref this._AutoSweetScent, value, nameof (AutoSweetScent));
      if (!value)
        return;
      this.WalkOptionsVisibility = Visibility.Hidden;
      this.SafariAutoWalkRoutesVisibility = Visibility.Hidden;
      this.SafariAutoFishRoutesVisibility = Visibility.Hidden;
      this.AutoWalkFishRoutesVisibility = Visibility.Hidden;
      this.AutoSweetScentRoutesVisibility = Visibility.Visible;
    }
  }

  public bool SafariAutoWalk
  {
    get => this._SafariAutoWalk;
    set
    {
      this.SetProperty<bool>(ref this._SafariAutoWalk, value, nameof (SafariAutoWalk));
      if (!value)
        return;
      this.AutoWalkFishRoutesVisibility = Visibility.Hidden;
      this.AutoSweetScentRoutesVisibility = Visibility.Hidden;
      this.WalkOptionsVisibility = Visibility.Hidden;
      this.SafariAutoFishRoutesVisibility = Visibility.Hidden;
      this.SafariAutoWalkRoutesVisibility = Visibility.Visible;
    }
  }

  public bool SafariAutoFish
  {
    get => this._SafariAutoFish;
    set
    {
      this.SetProperty<bool>(ref this._SafariAutoFish, value, nameof (SafariAutoFish));
      if (!value)
        return;
      this.AutoWalkFishRoutesVisibility = Visibility.Hidden;
      this.AutoSweetScentRoutesVisibility = Visibility.Hidden;
      this.WalkOptionsVisibility = Visibility.Hidden;
      this.SafariAutoWalkRoutesVisibility = Visibility.Hidden;
      this.SafariAutoFishRoutesVisibility = Visibility.Visible;
    }
  }

  public Visibility SellBoxVisibility
  {
    get => this._SellBoxVisibility;
    set
    {
      this.SetProperty<Visibility>(ref this._SellBoxVisibility, value, nameof (SellBoxVisibility));
    }
  }

  public Visibility AutoWalkFishRoutesVisibility
  {
    get => this._AutoWalkFishRoutesVisibility;
    set
    {
      this.SetProperty<Visibility>(ref this._AutoWalkFishRoutesVisibility, value, nameof (AutoWalkFishRoutesVisibility));
    }
  }

  public Visibility AutoSweetScentRoutesVisibility
  {
    get => this._AutoSweetScentRoutesVisibility;
    set
    {
      this.SetProperty<Visibility>(ref this._AutoSweetScentRoutesVisibility, value, nameof (AutoSweetScentRoutesVisibility));
    }
  }

  public Visibility SafariAutoWalkRoutesVisibility
  {
    get => this._SafariAutoWalkRoutesVisibility;
    set
    {
      this.SetProperty<Visibility>(ref this._SafariAutoWalkRoutesVisibility, value, nameof (SafariAutoWalkRoutesVisibility));
    }
  }

  public Visibility SafariAutoFishRoutesVisibility
  {
    get => this._SafariAutoFishRoutesVisibility;
    set
    {
      this.SetProperty<Visibility>(ref this._SafariAutoFishRoutesVisibility, value, nameof (SafariAutoFishRoutesVisibility));
    }
  }

  public Visibility CatchPokemonVisibility
  {
    get => this._CatchPokemonVisibility;
    set
    {
      this.SetProperty<Visibility>(ref this._CatchPokemonVisibility, value, nameof (CatchPokemonVisibility));
    }
  }

  public Visibility WalkOptionsVisibility
  {
    get => this._WalkOptionsVisibility;
    set
    {
      this.SetProperty<Visibility>(ref this._WalkOptionsVisibility, value, nameof (WalkOptionsVisibility));
    }
  }

  public string CatchPokemon
  {
    get => this._CatchPokemon;
    set
    {
      this.SetProperty<string>(ref this._CatchPokemon, value, nameof (CatchPokemon));
      Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.EncountersCounter = $"Encounters: {Bot.Instance.Status.EncountersCounter.ToString()} - {value}'s {Bot.Instance.Status.SelectedCatchPokemonCounter.ToString()}"));
    }
  }

  public string AutoWalkFishText
  {
    get => this._AutoWalkFishText;
    set => this.SetProperty<string>(ref this._AutoWalkFishText, value, nameof (AutoWalkFishText));
  }

  public bool WalkDirection
  {
    get => this._WalkDirection;
    set => this.SetProperty<bool>(ref this._WalkDirection, value, nameof (WalkDirection));
  }

  public bool LevelFirst
  {
    get => this._LevelFirst;
    set => this.SetProperty<bool>(ref this._LevelFirst, value, nameof (LevelFirst));
  }

  public bool CatchWithSecondPokemon
  {
    get => this._CatchWithSecondPokemon;
    set
    {
      this.SetProperty<bool>(ref this._CatchWithSecondPokemon, value, nameof (CatchWithSecondPokemon));
    }
  }

  public bool LevelFirstEnabled
  {
    get => this._LevelFirstEnabled;
    set => this.SetProperty<bool>(ref this._LevelFirstEnabled, value, nameof (LevelFirstEnabled));
  }

  public bool SquaresWalkPattern
  {
    get => this._SquaresWalkPattern;
    set => this.SetProperty<bool>(ref this._SquaresWalkPattern, value, nameof (SquaresWalkPattern));
  }

  public bool CatchSpellsOrder
  {
    get => this._CatchSpellsOrder;
    set => this.SetProperty<bool>(ref this._CatchSpellsOrder, value, nameof (CatchSpellsOrder));
  }

  public bool RandomWalkPattern
  {
    get => this._RandomWalkPattern;
    set => this.SetProperty<bool>(ref this._RandomWalkPattern, value, nameof (RandomWalkPattern));
  }

  public bool Login
  {
    get => this._Login;
    set => this.SetProperty<bool>(ref this._Login, value, nameof (Login));
  }

  public bool OnlyKeepIV31
  {
    get => this._OnlyKeepIV31;
    set => this.SetProperty<bool>(ref this._OnlyKeepIV31, value, nameof (OnlyKeepIV31));
  }

  public Visibility FightOptionVisibility
  {
    get => this._FightOptionVisibility;
    set
    {
      this.SetProperty<Visibility>(ref this._FightOptionVisibility, value, nameof (FightOptionVisibility));
    }
  }

  public Visibility IV31Visibility
  {
    get => this._IV31Visibility;
    set => this.SetProperty<Visibility>(ref this._IV31Visibility, value, nameof (IV31Visibility));
  }

  public Visibility CatchSpellsVisbility
  {
    get => this._CatchSpellsVisbility;
    set
    {
      this.SetProperty<Visibility>(ref this._CatchSpellsVisbility, value, nameof (CatchSpellsVisbility));
    }
  }

  public Visibility PayDayOptionVisibility
  {
    get => this._PayDayOptionVisibility;
    set
    {
      this.SetProperty<Visibility>(ref this._PayDayOptionVisibility, value, nameof (PayDayOptionVisibility));
    }
  }

  public Visibility ThiefOptionVisibility
  {
    get => this._ThiefOptionVisibility;
    set
    {
      this.SetProperty<Visibility>(ref this._ThiefOptionVisibility, value, nameof (ThiefOptionVisibility));
    }
  }

  public Visibility SafariOptionVisibility
  {
    get => this._SafariOptionVisibility;
    set
    {
      this.SetProperty<Visibility>(ref this._SafariOptionVisibility, value, nameof (SafariOptionVisibility));
    }
  }

  public Visibility CaughtVisible
  {
    get => this._CaughtVisible;
    set => this.SetProperty<Visibility>(ref this._CaughtVisible, value, nameof (CaughtVisible));
  }

  public Visibility AutoWalkFishVisibility
  {
    get => this._AutoWalkFishVisibility;
    set
    {
      this.SetProperty<Visibility>(ref this._AutoWalkFishVisibility, value, nameof (AutoWalkFishVisibility));
    }
  }

  public Visibility AutoSweetScentVisibility
  {
    get => this._AutoSweetScentVisibility;
    set
    {
      this.SetProperty<Visibility>(ref this._AutoSweetScentVisibility, value, nameof (AutoSweetScentVisibility));
    }
  }

  public string SellPrice
  {
    get => this._SellPrice;
    set => this.SetProperty<string>(ref this._SellPrice, value, nameof (SellPrice));
  }

  public bool PayDayMultiTarget
  {
    get => this._PayDayMultiTarget;
    set => this.SetProperty<bool>(ref this._PayDayMultiTarget, value, nameof (PayDayMultiTarget));
  }

  public bool CatchShiny
  {
    get => this._CatchShiny;
    set => this.SetProperty<bool>(ref this._CatchShiny, value, nameof (CatchShiny));
  }

  public bool StopOnShiny
  {
    get => this._StopOnShiny;
    set => this.SetProperty<bool>(ref this._StopOnShiny, value, nameof (StopOnShiny));
  }

  public bool SkipDialog
  {
    get => this._SkipDialog;
    set => this.SetProperty<bool>(ref this._SkipDialog, value, nameof (SkipDialog));
  }

  public bool SkipLearningNew
  {
    get => this._SkipLearningNew;
    set => this.SetProperty<bool>(ref this._SkipLearningNew, value, nameof (SkipLearningNew));
  }

  public bool SkipEvolve
  {
    get => this._SkipEvolve;
    set => this.SetProperty<bool>(ref this._SkipEvolve, value, nameof (SkipEvolve));
  }

  public bool MoreThief
  {
    get => this._MoreThief;
    set => this.SetProperty<bool>(ref this._MoreThief, value, nameof (MoreThief));
  }

  public bool MorePayDay
  {
    get => this._MorePayDay;
    set => this.SetProperty<bool>(ref this._MorePayDay, value, nameof (MorePayDay));
  }

  public bool Lure
  {
    get => this._Lure;
    set => this.SetProperty<bool>(ref this._Lure, value, nameof (Lure));
  }

  public bool Imprison
  {
    get => this._Imprison;
    set => this.SetProperty<bool>(ref this._Imprison, value, nameof (Imprison));
  }

  public bool Rock
  {
    get => this._Rock;
    set => this.SetProperty<bool>(ref this._Rock, value, nameof (Rock));
  }

  public bool Bait
  {
    get => this._Bait;
    set => this.SetProperty<bool>(ref this._Bait, value, nameof (Bait));
  }

  public bool AutoLeppa
  {
    get => this._AutoLeppa;
    set => this.SetProperty<bool>(ref this._AutoLeppa, value, nameof (AutoLeppa));
  }

  public bool AutoEther
  {
    get => this._AutoEther;
    set => this.SetProperty<bool>(ref this._AutoEther, value, nameof (AutoEther));
  }

  public DelegateCommand StartCommand { get; }

  public DelegateCommand StopCommand { get; }

  public BotMode BotMode
  {
    get => this._BotMode;
    set => this.SetProperty<BotMode>(ref this._BotMode, value, nameof (BotMode));
  }

  public Pokemon Pokemon
  {
    get => this._Pokemon;
    set => this.SetProperty<Pokemon>(ref this._Pokemon, value, nameof (Pokemon));
  }

  public int CatchPokemonSelectedIndex
  {
    get => this._CatchPokemonSelectedIndex;
    set
    {
      this.SetProperty<int>(ref this._CatchPokemonSelectedIndex, value, nameof (CatchPokemonSelectedIndex));
    }
  }

  public int AutoSweetScentRoutesSelectedIndex
  {
    get => this._AutoSweetScentRoutesSelectedIndex;
    set
    {
      this.SetProperty<int>(ref this._AutoSweetScentRoutesSelectedIndex, value, nameof (AutoSweetScentRoutesSelectedIndex));
    }
  }

  public int AutoWalkFishRoutesSelectedIndex
  {
    get => this._AutoWalkFishRoutesSelectedIndex;
    set
    {
      this.SetProperty<int>(ref this._AutoWalkFishRoutesSelectedIndex, value, nameof (AutoWalkFishRoutesSelectedIndex));
    }
  }

  public int SafariAutoWalkRoutesSelectedIndex
  {
    get => this._SafariAutoWalkRoutesSelectedIndex;
    set
    {
      this.SetProperty<int>(ref this._SafariAutoWalkRoutesSelectedIndex, value, nameof (SafariAutoWalkRoutesSelectedIndex));
    }
  }

  public int SafariAutoFishRoutesSelectedIndex
  {
    get => this._SafariAutoFishRoutesSelectedIndex;
    set
    {
      this.SetProperty<int>(ref this._SafariAutoFishRoutesSelectedIndex, value, nameof (SafariAutoFishRoutesSelectedIndex));
    }
  }

  public ComboBoxItem RoutesSelectedItem
  {
    get => this._RoutesSelectedItem;
    set
    {
      this.SetProperty<ComboBoxItem>(ref this._RoutesSelectedItem, value, nameof (RoutesSelectedItem));
    }
  }

  public string FreeRoutes0
  {
    get => this._FreeRoutes0;
    set => this.SetProperty<string>(ref this._FreeRoutes0, value, nameof (FreeRoutes0));
  }

  public string FreeRoutes1
  {
    get => this._FreeRoutes1;
    set => this.SetProperty<string>(ref this._FreeRoutes1, value, nameof (FreeRoutes1));
  }

  public ObservableCollection<string> PremiumRoutes
  {
    get => this._PremiumRoutes;
    set
    {
      this.SetProperty<ObservableCollection<string>>(ref this._PremiumRoutes, value, nameof (PremiumRoutes));
    }
  }

  public string SafariWalkRoutes0
  {
    get => this._SafariWalkRoutes0;
    set => this.SetProperty<string>(ref this._SafariWalkRoutes0, value, nameof (SafariWalkRoutes0));
  }

  public string SafariWalkRoutes1
  {
    get => this._SafariWalkRoutes1;
    set => this.SetProperty<string>(ref this._SafariWalkRoutes1, value, nameof (SafariWalkRoutes1));
  }

  public string SafariWalkRoutes2
  {
    get => this._SafariWalkRoutes2;
    set => this.SetProperty<string>(ref this._SafariWalkRoutes2, value, nameof (SafariWalkRoutes2));
  }

  public string SafariWalkRoutes3
  {
    get => this._SafariWalkRoutes3;
    set => this.SetProperty<string>(ref this._SafariWalkRoutes3, value, nameof (SafariWalkRoutes3));
  }

  public string SafariFishRoutes0
  {
    get => this._SafariFishRoutes0;
    set => this.SetProperty<string>(ref this._SafariFishRoutes0, value, nameof (SafariFishRoutes0));
  }

  public bool PremiumEnabled
  {
    get => this._PremiumEnabled;
    set => this.SetProperty<bool>(ref this._PremiumEnabled, value, nameof (PremiumEnabled));
  }

  public bool FreeEnabled
  {
    get => this._FreeEnabled;
    set => this.SetProperty<bool>(ref this._FreeEnabled, value, nameof (FreeEnabled));
  }

  public string LatestUploadLink
  {
    get => this._LatestUploadLink;
    set => this.SetProperty<string>(ref this._LatestUploadLink, value, nameof (LatestUploadLink));
  }
}
