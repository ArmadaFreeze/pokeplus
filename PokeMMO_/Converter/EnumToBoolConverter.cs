// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Converter.EnumToBoolConverter
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Botting;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace PokeMMO_.Converter;

public class EnumToBoolConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (Bot.Instance.Settings.BotMode == BotMode.Fight)
    {
      MainViewModel.Instance.Home.LevelFirstEnabled = true;
      MainViewModel.Instance.Home.FightOptionVisibility = Visibility.Visible;
      MainViewModel.Instance.Home.CatchPokemonVisibility = Visibility.Visible;
    }
    else
    {
      MainViewModel.Instance.Home.LevelFirstEnabled = false;
      MainViewModel.Instance.Home.FightOptionVisibility = Visibility.Hidden;
      MainViewModel.Instance.Home.CatchPokemonVisibility = Visibility.Hidden;
    }
    if (Bot.Instance.Settings.BotMode != BotMode.Run)
      ;
    MainViewModel.Instance.Home.CatchSpellsVisbility = Bot.Instance.Settings.BotMode != BotMode.Catch ? Visibility.Hidden : Visibility.Visible;
    if ((Bot.Instance.Settings.BotMode == BotMode.Catch ? 1 : (Bot.Instance.Settings.BotMode == BotMode.Safari ? 1 : 0)) == 0)
    {
      MainViewModel.Instance.Home.IV31Visibility = Visibility.Hidden;
      if ((Bot.Instance.Settings.BotMode == BotMode.Thief ? 0 : (Bot.Instance.Settings.BotMode != BotMode.Fight ? 1 : 0)) != 0)
        MainViewModel.Instance.Home.CatchPokemonVisibility = Visibility.Hidden;
    }
    else
    {
      MainViewModel.Instance.Home.IV31Visibility = Visibility.Visible;
      MainViewModel.Instance.Home.CatchPokemonVisibility = Visibility.Visible;
    }
    MainViewModel.Instance.Home.PayDayOptionVisibility = Bot.Instance.Settings.BotMode == BotMode.PayDay ? Visibility.Visible : Visibility.Hidden;
    if (Bot.Instance.Settings.BotMode == BotMode.Thief)
    {
      MainViewModel.Instance.Home.ThiefOptionVisibility = Visibility.Visible;
      MainViewModel.Instance.Home.CatchPokemonVisibility = Visibility.Visible;
    }
    else
    {
      MainViewModel.Instance.Home.ThiefOptionVisibility = Visibility.Hidden;
      if ((Bot.Instance.Settings.BotMode == BotMode.Catch || Bot.Instance.Settings.BotMode == BotMode.Safari ? 0 : (Bot.Instance.Settings.BotMode != BotMode.Fight ? 1 : 0)) != 0)
        MainViewModel.Instance.Home.CatchPokemonVisibility = Visibility.Hidden;
    }
    if (Bot.Instance.Settings.BotMode == BotMode.Safari)
    {
      MainViewModel.Instance.Home.AutoWalkFishVisibility = Visibility.Hidden;
      MainViewModel.Instance.Home.AutoSweetScentVisibility = Visibility.Hidden;
      MainViewModel.Instance.Home.AutoWalkFishRoutesVisibility = Visibility.Hidden;
      MainViewModel.Instance.Home.AutoSweetScentRoutesVisibility = Visibility.Hidden;
      MainViewModel.Instance.Home.SafariOptionVisibility = Visibility.Visible;
      if (MainViewModel.Instance.Home.SafariAutoWalk)
        MainViewModel.Instance.Home.SafariAutoWalkRoutesVisibility = Visibility.Visible;
      if (MainViewModel.Instance.Home.SafariAutoFish)
        MainViewModel.Instance.Home.SafariAutoFishRoutesVisibility = Visibility.Visible;
    }
    else
    {
      MainViewModel.Instance.Home.SafariOptionVisibility = Visibility.Hidden;
      MainViewModel.Instance.Home.SafariAutoWalkRoutesVisibility = Visibility.Hidden;
      MainViewModel.Instance.Home.SafariAutoFishRoutesVisibility = Visibility.Hidden;
      MainViewModel.Instance.Home.AutoWalkFishVisibility = Visibility.Visible;
      MainViewModel.Instance.Home.AutoSweetScentVisibility = Visibility.Visible;
      if (MainViewModel.Instance.Home.AutoWalkFish)
        MainViewModel.Instance.Home.AutoWalkFishRoutesVisibility = Visibility.Visible;
      if (MainViewModel.Instance.Home.AutoSweetScent)
        MainViewModel.Instance.Home.AutoSweetScentRoutesVisibility = Visibility.Visible;
    }
    MainViewModel.Instance.Home.SellBoxVisibility = Bot.Instance.Settings.BotMode != BotMode.SellBox ? Visibility.Hidden : Visibility.Visible;
    if (Bot.Instance.Settings.BotMode != BotMode.None)
      ;
    return (object) value?.Equals(parameter);
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return (bool) value ? parameter : Binding.DoNothing;
  }
}
