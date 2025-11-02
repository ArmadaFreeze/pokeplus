// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Botting.Check
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Classes;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;
using System;
using System.Windows;

#nullable disable
namespace PokeMMO_.Botting;

public class Check
{
  private Search search = new Search();
  public int[] _Coordinates;

  public bool Potion0 => this.CheckImage("bin/img/Potion0.png", Tolerance.Low);

  public bool SuperPotion0 => this.CheckImage("bin/img/SuperPotion0.png", Tolerance.Low);

  public bool HyperPotion0 => this.CheckImage("bin/img/HyperPotion0.png", Tolerance.Low);

  public bool Error1 => this.CheckImage("bin/img/Error1.png", Tolerance.Middle);

  public bool Error2 => this.CheckImage("bin/img/Error2.png", Tolerance.Middle);

  public bool Error3 => this.CheckImage("bin/img/Error3.png", Tolerance.Middle);

  public bool GTLNoMoney => this.CheckImage("bin/img/GTLNoMoney.png", Tolerance.Middle);

  public bool Lure => this.CheckImage("bin/img/Lure.png", Tolerance.High);

  public bool SafariTimeOver => this.CheckImage("bin/img/TimeOver.png", Tolerance.Middle);

  public bool HPOrange => this.CheckImage("bin/img/HPOrange.png", Tolerance.Low);

  public bool HPRed => this.CheckImage("bin/img/HPRed.png", Tolerance.Low);

  public bool Sleep => this.CheckImage("bin/img/Sleep.png", Tolerance.High);

  public bool Catched => this.CheckImage("bin/img/Catched.png", Tolerance.Middle);

  public bool Repel => this.CheckImage("bin/img/Repel.png", Tolerance.Low);

  public bool RepelInBattle => this.CheckImage("bin/img/Repel2.png", Tolerance.Low);

  public bool Dialogue => this.CheckImage("bin/img/Skip.png", Tolerance.Skip);

  public bool CantRun => this.CheckImage("bin/img/CantRun.png", Tolerance.High);

  public bool ThiefPokemonItem => this.CheckImage("bin/img/Item2.png", Tolerance.High);

  public bool Stats => this.CheckImage("bin/img/Stats.png", Tolerance.Middle);

  public bool Leppa0 => this.CheckImage("bin/img/Leppa0.png", Tolerance.Middle);

  public bool SweetCent0 => this.CheckSweetCent0();

  public bool Walk => this.CheckWalk();

  public bool PM => this.CheckPM();

  public bool Horde => this.CheckHorde();

  public bool NextPoke => this.CheckNextPoke();

  public bool PPEffective0 => this.Check0PPE();

  public bool PPSuperEffective0 => this.Check0PPS();

  public bool Login => this.CheckLogin();

  public bool Captcha => this.CheckCaptcha();

  public bool CheckImage(string imagePath, string tolerance)
  {
    this._Coordinates = this.search.UseImageSearch(imagePath, tolerance);
    return this._Coordinates != null;
  }

  public bool CheckShiny()
  {
    bool flag;
    if ((Bot.Instance.Settings.CatchShiny ? 0 : (!Bot.Instance.Settings.StopOnShiny ? 1 : 0)) != 0)
    {
      flag = false;
    }
    else
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/Shiny.png", Tolerance.Shiny);
      flag = this._Coordinates != null;
    }
    return flag;
  }

  public bool CheckCaptcha()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/Captcha.png", Tolerance.Middle);
    bool flag;
    if (this._Coordinates != null)
    {
      flag = true;
    }
    else
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/Captcha2.png", Tolerance.Middle);
      flag = this._Coordinates != null;
    }
    return flag;
  }

  public bool CheckDisabled()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/Disabled.png", Tolerance.High);
    bool flag;
    if (this._Coordinates != null)
    {
      Bot.Instance.Status.MoveDisabled = true;
      flag = true;
    }
    else
      flag = false;
    return flag;
  }

  public bool Check0PPE()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/0PPE.png", Tolerance.VeryHigh);
    bool flag;
    if (this._Coordinates != null)
    {
      flag = true;
    }
    else
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/0PPE2.png", Tolerance.VeryHigh);
      flag = this._Coordinates != null;
    }
    return flag;
  }

  public bool Check0PPS()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/0PPS.png", Tolerance.VeryHigh);
    bool flag;
    if (this._Coordinates == null)
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/0PPS2.png", Tolerance.VeryHigh);
      flag = this._Coordinates != null;
    }
    else
      flag = true;
    return flag;
  }

  public bool CheckSweetCent0()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/SC0.png", Tolerance.Middle);
    return this._Coordinates != null;
  }

  public bool CheckLogin()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/DC.png", Tolerance.Middle);
    bool flag;
    if (this._Coordinates != null)
    {
      flag = true;
    }
    else
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/DCLogin.png", Tolerance.Middle);
      if (this._Coordinates == null)
      {
        this._Coordinates = this.search.UseImageSearch("bin/img/Session.png", Tolerance.Middle);
        if (this._Coordinates == null)
        {
          this._Coordinates = this.search.UseImageSearch("bin/img/Login.png", Tolerance.Middle);
          if (this._Coordinates != null)
          {
            flag = true;
          }
          else
          {
            this._Coordinates = this.search.UseImageSearch("bin/img/Character.png", Tolerance.Middle);
            flag = this._Coordinates != null;
          }
        }
        else
          flag = true;
      }
      else
        flag = true;
    }
    return flag;
  }

  public bool CheckWalk()
  {
    bool flag;
    if (Bot.Instance.Settings.BotMode == BotMode.Safari)
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/Safari.png", Tolerance.Low);
      if ((this._Coordinates != null ? 0 : (!this.CheckLogin() ? 1 : 0)) != 0)
      {
        Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Not in Battle"));
        flag = true;
      }
      else
        flag = false;
    }
    else
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/Battle.png", Tolerance.Low);
      if ((this._Coordinates != null ? 0 : (!this.CheckLogin() ? 1 : 0)) == 0)
      {
        flag = false;
      }
      else
      {
        Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Not in Battle"));
        flag = true;
      }
    }
    return flag;
  }

  public bool CheckPokemon(Pokemon pokemon)
  {
    try
    {
      this._Coordinates = this.search.UseImageSearch($"bin/img/{pokemon.ToString()}.png", Tolerance.Middle);
      return this._Coordinates != null;
    }
    catch (Exception ex)
    {
      Bot.Instance.Stop();
      int num = (int) MessageBox.Show("No image was found of the Pokemon you are trying to catch.\nPlease take a screenshot of the Pokemon name and place it in the bin/img folder.", "Bot stopped", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
      return false;
    }
  }

  public string CheckSelectedPokemon()
  {
    try
    {
      this._Coordinates = this.search.UseImageSearch($"bin/pokemon/{MainViewModel.Instance.Home.CatchPokemon.ToString()}.png", Tolerance.High);
      if (this._Coordinates == null)
        return "";
      Bot.Instance.Status.EncounteredSelectedPokemon = true;
      return MainViewModel.Instance.Home.CatchPokemon.ToString();
    }
    catch (Exception ex)
    {
      Bot.Instance.Stop();
      int num = (int) MessageBox.Show("No image was found of the Pokemon you are trying to catch.\nPlease choose the Pokemon you want to catch and press the Take Screenshot button while in an encounter with the Pokemon.", "Bot stopped", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
      return "";
    }
  }

  public bool CheckHorde()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/Horde.png", Tolerance.Middle);
    bool flag;
    if (this._Coordinates != null)
    {
      flag = true;
    }
    else
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/Horde2.png", Tolerance.Middle);
      if (this._Coordinates == null)
      {
        this._Coordinates = this.search.UseImageSearch("bin/img/Horde_old.png", Tolerance.Middle);
        flag = this._Coordinates != null;
      }
      else
        flag = true;
    }
    return flag;
  }

  public bool CheckNextPoke()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/NextPoke.png", Tolerance.Low);
    bool flag;
    if (this._Coordinates != null)
    {
      flag = true;
    }
    else
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/NextPoke2.png", Tolerance.Low);
      if (this._Coordinates != null)
      {
        flag = true;
      }
      else
      {
        this._Coordinates = this.search.UseImageSearch("bin/img/NextPoke3.png", Tolerance.Low);
        flag = this._Coordinates != null;
      }
    }
    return flag;
  }

  public bool CheckPM()
  {
    this._Coordinates = this.search.UseImageSearch("bin/img/PM.png", Tolerance.Middle);
    bool flag;
    if (this._Coordinates != null)
    {
      flag = true;
    }
    else
    {
      this._Coordinates = this.search.UseImageSearch("bin/img/PM2.png", Tolerance.Middle);
      flag = this._Coordinates != null;
    }
    return flag;
  }
}
