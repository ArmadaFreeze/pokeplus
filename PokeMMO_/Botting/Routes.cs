// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Botting.Routes
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Classes;
using PokeMMO_.Input;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;

#nullable disable
namespace PokeMMO_.Botting;

public class Routes
{
  private static readonly object padlock = new object();
  private static Routes instance = (Routes) null;
  public string[] SafariAutoWalkRoutesString = new string[4]
  {
    "Kanto_Safari_Walk_1",
    "Hoenn_Safari_Walk_1",
    "Hoenn_Safari_Walk_2_Repel",
    "Hoenn_Safari_Walk_3"
  };
  public string[] SafariAutoFishRoutesString = new string[1]
  {
    "Kanto_Safari_Fish_1"
  };
  public string[] FreeAutoSweetScentRoutesString = new string[2]
  {
    "Kanto_Route_4",
    "Unova_Mistralton_City_1"
  };
  public List<string> PremiumAutoSweetScentRoutesString = new List<string>();

  public static Routes Instance
  {
    get
    {
      lock (Routes.padlock)
      {
        if (Routes.instance == null)
          Routes.instance = new Routes();
        return Routes.instance;
      }
    }
  }

  public void FillPremiumRoutes()
  {
    try
    {
      this.PremiumAutoSweetScentRoutesString.Add("Kanto_Cerulean_City");
      this.PremiumAutoSweetScentRoutesString.Add("Kanto_Virdian_City");
      this.PremiumAutoSweetScentRoutesString.Add("Kanto_Cinnabar_Island");
      this.PremiumAutoSweetScentRoutesString.Add("Kanto_Vermilion_City");
      this.PremiumAutoSweetScentRoutesString.Add("Kanto_Indigo_Plateau");
      this.PremiumAutoSweetScentRoutesString.Add("Kanto_Fuchsia_City");
      this.PremiumAutoSweetScentRoutesString.Add("Hoenn_Petalburg_City");
      this.PremiumAutoSweetScentRoutesString.Add("Hoenn_Verdanturf_Town");
      this.PremiumAutoSweetScentRoutesString.Add("Hoenn_Slateport_City");
      this.PremiumAutoSweetScentRoutesString.Add("Hoenn_Fallarbor_Town");
      this.PremiumAutoSweetScentRoutesString.Add("Hoenn_Fallarbor_Town_2");
      this.PremiumAutoSweetScentRoutesString.Add("Hoenn_Pacifidlog_Town");
      this.PremiumAutoSweetScentRoutesString.Add("Hoenn_Sootopolis_City");
      this.PremiumAutoSweetScentRoutesString.Add("Sinnoh_Celestic_Town_1");
      this.PremiumAutoSweetScentRoutesString.Add("Sinnoh_Celestic_Town_2");
      this.PremiumAutoSweetScentRoutesString.Add("Sinnoh_Eterna_City");
      this.PremiumAutoSweetScentRoutesString.Add("Sinnoh_Solaceon_Town");
      this.PremiumAutoSweetScentRoutesString.Add("Sinnoh_Solaceon_Town_2");
      this.PremiumAutoSweetScentRoutesString.Add("Kanto_Island_One");
      this.PremiumAutoSweetScentRoutesString.Add("Kanto_Island_Two_1");
      this.PremiumAutoSweetScentRoutesString.Add("Kanto_Island_Two_2");
      this.PremiumAutoSweetScentRoutesString.Add("Kanto_Island_Six_1");
      this.PremiumAutoSweetScentRoutesString.Add("Kanto_Island_Six_2");
      this.PremiumAutoSweetScentRoutesString.Add("Kanto_Celadon_City");
      this.PremiumAutoSweetScentRoutesString.Add("Hoenn_Battle_Frontier");
      this.PremiumAutoSweetScentRoutesString.Add("Hoenn_Ever_Grande_City_1");
      this.PremiumAutoSweetScentRoutesString.Add("Hoenn_Ever_Grande_City_2_Fish");
      this.PremiumAutoSweetScentRoutesString.Add("Kanto_Route_10");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Undella_Town_Fish");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Icirrus_City_Fish");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Black_City_1");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Black_City_2");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Undella_Town_1");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Undella_Town_2");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Lacunosa_Town_1");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Lacunosa_Town_2");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Lacunosa_Town_3");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Lacunosa_Town_4");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Lacunosa_Town_5");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Opelucid_City_1");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Opelucid_City_2");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Opelucid_City_3");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Opelucid_City_4");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Icirrus_City_1");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Icirrus_City_2");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Icirrus_City_3");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Icirrus_City_4");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Mistralton_City_2");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Driftveil_City_1");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Driftveil_City_2");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Driftveil_City_3");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Nimbasa_City_1");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Nimbasa_City_2");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Nimbasa_City_3");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Nimbasa_City_4");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Nacrene_City_1");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Nacrene_City_2");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Striaton_City");
      this.PremiumAutoSweetScentRoutesString.Add("Sinnoh_Floaroma_Town");
      this.PremiumAutoSweetScentRoutesString.Add("Sinnoh_Sunyshore_City");
      this.PremiumAutoSweetScentRoutesString.Add("Sinnoh_Canalave_City");
      this.PremiumAutoSweetScentRoutesString.Add("Sinnoh_Pokemon_League");
      this.PremiumAutoSweetScentRoutesString.Add("Sinnoh_Veilstone_City");
      this.PremiumAutoSweetScentRoutesString.Add("Sinnoh_Celestic_Town_3");
      this.PremiumAutoSweetScentRoutesString.Add("Sinnoh_Snowpoint_City");
      this.PremiumAutoSweetScentRoutesString.Add("Sinnoh_Fight_Area");
      this.PremiumAutoSweetScentRoutesString.Add("Sinnoh_Resort_Area");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Undella_Town");
      this.PremiumAutoSweetScentRoutesString.Add("Unova_Pokemon_League");
    }
    catch (Exception ex)
    {
      if ((ex.Message.Contains("401") ? 1 : (ex.Message.Contains("connection") ? 1 : 0)) != 0)
        Process.GetCurrentProcess().Kill();
      PokeMMOLogger.Instance.Log(ex.Message);
    }
    if ((MainWindow.KeyAuthApp.user_data.username == null || MainWindow.KeyAuthApp.user_data.hwid == null ? 0 : (MainWindow.KeyAuthApp.user_data.ip != null ? 1 : 0)) == 0)
      return;
    for (int index = 0; index < this.PremiumAutoSweetScentRoutesString.Count; ++index)
      MainViewModel.Instance.Home.PremiumRoutes[index] = this.PremiumAutoSweetScentRoutesString[index];
  }

  public string GetSelectedRoute()
  {
    string selectedRoute = "";
    int num = 0;
    if ((!Bot.Instance.Settings.AutoWalkFish ? 0 : (Bot.Instance.Settings.BotMode != BotMode.Safari ? 1 : 0)) != 0)
      num = Bot.Instance.Settings.AutoWalkFishRoutesSelectedIndex;
    else if ((!Bot.Instance.Settings.AutoSweetScent ? 0 : (Bot.Instance.Settings.BotMode != BotMode.Safari ? 1 : 0)) == 0)
    {
      if ((!Bot.Instance.Settings.SafariAutoWalk ? 0 : (Bot.Instance.Settings.BotMode == BotMode.Safari ? 1 : 0)) != 0)
        num = Bot.Instance.Settings.SafariAutoWalkRoutesSelectedIndex;
      else if ((!Bot.Instance.Settings.SafariAutoFish ? 0 : (Bot.Instance.Settings.BotMode == BotMode.Safari ? 1 : 0)) != 0)
        num = Bot.Instance.Settings.SafariAutoFishRoutesSelectedIndex;
    }
    else
      num = Bot.Instance.Settings.AutoSweetScentRoutesSelectedIndex;
    if ((!Bot.Instance.Settings.AutoWalkFish ? 0 : (Bot.Instance.Settings.BotMode != BotMode.Safari ? 1 : 0)) != 0)
    {
      switch (num)
      {
        case 0:
          selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[0];
          break;
        case 1:
          selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[27];
          break;
        case 2:
          selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[9];
          break;
        case 3:
          selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[26];
          break;
        case 4:
          selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[67];
          break;
        case 5:
          selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[28];
          break;
        case 6:
          selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[29];
          break;
      }
    }
    else if ((!Bot.Instance.Settings.SafariAutoWalk ? 0 : (Bot.Instance.Settings.BotMode == BotMode.Safari ? 1 : 0)) == 0)
    {
      if ((!Bot.Instance.Settings.SafariAutoFish ? 0 : (Bot.Instance.Settings.BotMode == BotMode.Safari ? 1 : 0)) == 0)
      {
        if ((!Bot.Instance.Settings.AutoSweetScent ? 0 : (Bot.Instance.Settings.BotMode != BotMode.Safari ? 1 : 0)) != 0)
        {
          switch (num)
          {
            case 0:
              selectedRoute = MainViewModel.Instance.Home.FreeRoutes0;
              break;
            case 1:
              selectedRoute = MainViewModel.Instance.Home.FreeRoutes1;
              break;
            case 2:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[0];
              break;
            case 3:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[27];
              break;
            case 4:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[1];
              break;
            case 5:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[2];
              break;
            case 6:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[3];
              break;
            case 7:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[4];
              break;
            case 8:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[5];
              break;
            case 9:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[18];
              break;
            case 10:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[19];
              break;
            case 11:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[20];
              break;
            case 12:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[21];
              break;
            case 13:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[22];
              break;
            case 14:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[23];
              break;
            case 15:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[6];
              break;
            case 16 /*0x10*/:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[7];
              break;
            case 17:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[8];
              break;
            case 18:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[9];
              break;
            case 19:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[10];
              break;
            case 20:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[11];
              break;
            case 21:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[12];
              break;
            case 22:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[24];
              break;
            case 23:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[25];
              break;
            case 24:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[13];
              break;
            case 25:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[14];
              break;
            case 26:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[15];
              break;
            case 27:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[16 /*0x10*/];
              break;
            case 28:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[17];
              break;
            case 29:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[58];
              break;
            case 30:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[59];
              break;
            case 31 /*0x1F*/:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[60];
              break;
            case 32 /*0x20*/:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[61];
              break;
            case 33:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[62];
              break;
            case 34:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[63 /*0x3F*/];
              break;
            case 35:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[64 /*0x40*/];
              break;
            case 36:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[65];
              break;
            case 37:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[66];
              break;
            case 38:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[30];
              break;
            case 39:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[31 /*0x1F*/];
              break;
            case 40:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[32 /*0x20*/];
              break;
            case 41:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[33];
              break;
            case 42:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[34];
              break;
            case 43:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[35];
              break;
            case 44:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[36];
              break;
            case 45:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[37];
              break;
            case 46:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[38];
              break;
            case 47:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[39];
              break;
            case 48 /*0x30*/:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[40];
              break;
            case 49:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[41];
              break;
            case 50:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[42];
              break;
            case 51:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[43];
              break;
            case 52:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[44];
              break;
            case 53:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[45];
              break;
            case 54:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[46];
              break;
            case 55:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[47];
              break;
            case 56:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[48 /*0x30*/];
              break;
            case 57:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[49];
              break;
            case 58:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[50];
              break;
            case 59:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[51];
              break;
            case 60:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[52];
              break;
            case 61:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[53];
              break;
            case 62:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[54];
              break;
            case 63 /*0x3F*/:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[55];
              break;
            case 64 /*0x40*/:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[56];
              break;
            case 65:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[57];
              break;
            case 66:
              selectedRoute = MainViewModel.Instance.Home.PremiumRoutes[68];
              break;
          }
        }
      }
      else if (num == 0)
        selectedRoute = MainViewModel.Instance.Home.SafariFishRoutes0;
    }
    else
    {
      switch (num)
      {
        case 0:
          selectedRoute = MainViewModel.Instance.Home.SafariWalkRoutes0;
          break;
        case 1:
          selectedRoute = MainViewModel.Instance.Home.SafariWalkRoutes1;
          break;
        case 2:
          selectedRoute = MainViewModel.Instance.Home.SafariWalkRoutes2;
          break;
        case 3:
          selectedRoute = MainViewModel.Instance.Home.SafariWalkRoutes3;
          break;
      }
    }
    return selectedRoute;
  }

  public void Router(string route, string action)
  {
    if (route == this.SafariAutoWalkRoutesString[0])
      this.Kanto_Safari_Walk_1(action);
    else if (route == this.SafariAutoWalkRoutesString[1])
      this.Hoenn_Safari_Walk_1(action);
    else if (!(route == this.SafariAutoWalkRoutesString[2]))
    {
      if (!(route == this.SafariAutoWalkRoutesString[3]))
      {
        if (route == this.SafariAutoFishRoutesString[0])
          this.Kanto_Safari_Fish_1(action);
        else if (!(route == this.FreeAutoSweetScentRoutesString[0]))
        {
          if (!(route == this.FreeAutoSweetScentRoutesString[1]))
          {
            if (route == this.PremiumAutoSweetScentRoutesString[0])
              this.Kanto_Cerulean_City(action);
            else if (route == this.PremiumAutoSweetScentRoutesString[27])
              this.Kanto_Route_10(action);
            else if (!(route == this.PremiumAutoSweetScentRoutesString[1]))
            {
              if (!(route == this.PremiumAutoSweetScentRoutesString[2]))
              {
                if (route == this.PremiumAutoSweetScentRoutesString[3])
                  this.Kanto_Vermilion_City(action);
                else if (route == this.PremiumAutoSweetScentRoutesString[4])
                  this.Kanto_Indigo_Plateau(action);
                else if (!(route == this.PremiumAutoSweetScentRoutesString[5]))
                {
                  if (!(route == this.PremiumAutoSweetScentRoutesString[18]))
                  {
                    if (route == this.PremiumAutoSweetScentRoutesString[19])
                      this.Kanto_Island_Two_1(action);
                    else if (!(route == this.PremiumAutoSweetScentRoutesString[20]))
                    {
                      if (!(route == this.PremiumAutoSweetScentRoutesString[21]))
                      {
                        if (route == this.PremiumAutoSweetScentRoutesString[22])
                          this.Kanto_Island_Six_2(action);
                        else if (!(route == this.PremiumAutoSweetScentRoutesString[23]))
                        {
                          if (!(route == this.PremiumAutoSweetScentRoutesString[6]))
                          {
                            if (route == this.PremiumAutoSweetScentRoutesString[7])
                              this.Hoenn_Verdanturf_Town(action);
                            else if (route == this.PremiumAutoSweetScentRoutesString[8])
                              this.Hoenn_Slateport_City(action);
                            else if (!(route == this.PremiumAutoSweetScentRoutesString[9]))
                            {
                              if (!(route == this.PremiumAutoSweetScentRoutesString[10]))
                              {
                                if (!(route == this.PremiumAutoSweetScentRoutesString[11]))
                                {
                                  if (!(route == this.PremiumAutoSweetScentRoutesString[12]))
                                  {
                                    if (route == this.PremiumAutoSweetScentRoutesString[24])
                                      this.Hoenn_Battle_Frontier(action);
                                    else if (route == this.PremiumAutoSweetScentRoutesString[25])
                                      this.Hoenn_Ever_Grande_City_1(action);
                                    else if (!(route == this.PremiumAutoSweetScentRoutesString[26]))
                                    {
                                      if (route == this.PremiumAutoSweetScentRoutesString[13])
                                        this.Sinnoh_Celestic_Town_1(action);
                                      else if (route == this.PremiumAutoSweetScentRoutesString[14])
                                        this.Sinnoh_Celestic_Town_2(action);
                                      else if (route == this.PremiumAutoSweetScentRoutesString[15])
                                        this.Sinnoh_Eterna_City(action);
                                      else if (!(route == this.PremiumAutoSweetScentRoutesString[16 /*0x10*/]))
                                      {
                                        if (route == this.PremiumAutoSweetScentRoutesString[17])
                                          this.Sinnoh_Solaceon_Town_2(action);
                                        else if (route == this.PremiumAutoSweetScentRoutesString[58])
                                          this.Sinnoh_Floaroma_Town(action);
                                        else if (!(route == this.PremiumAutoSweetScentRoutesString[59]))
                                        {
                                          if (!(route == this.PremiumAutoSweetScentRoutesString[60]))
                                          {
                                            if (route == this.PremiumAutoSweetScentRoutesString[61])
                                              this.Sinnoh_Pokemon_League(action);
                                            else if (route == this.PremiumAutoSweetScentRoutesString[62])
                                              this.Sinnoh_Veilstone_City(action);
                                            else if (route == this.PremiumAutoSweetScentRoutesString[63 /*0x3F*/])
                                              this.Sinnoh_Celestic_Town_3(action);
                                            else if (!(route == this.PremiumAutoSweetScentRoutesString[64 /*0x40*/]))
                                            {
                                              if (!(route == this.PremiumAutoSweetScentRoutesString[65]))
                                              {
                                                if (!(route == this.PremiumAutoSweetScentRoutesString[66]))
                                                {
                                                  if (route == this.PremiumAutoSweetScentRoutesString[28])
                                                    this.Unova_Undella_Town_Fish(action);
                                                  else if (!(route == this.PremiumAutoSweetScentRoutesString[29]))
                                                  {
                                                    if (route == this.PremiumAutoSweetScentRoutesString[30])
                                                      this.Unova_Black_City_1(action);
                                                    else if (route == this.PremiumAutoSweetScentRoutesString[31 /*0x1F*/])
                                                      this.Unova_Black_City_2(action);
                                                    else if (!(route == this.PremiumAutoSweetScentRoutesString[32 /*0x20*/]))
                                                    {
                                                      if (route == this.PremiumAutoSweetScentRoutesString[33])
                                                        this.Unova_Undella_Town_2(action);
                                                      else if (route == this.PremiumAutoSweetScentRoutesString[34])
                                                        this.Unova_Lacunosa_Town_1(action);
                                                      else if (route == this.PremiumAutoSweetScentRoutesString[35])
                                                        this.Unova_Lacunosa_Town_2(action);
                                                      else if (!(route == this.PremiumAutoSweetScentRoutesString[36]))
                                                      {
                                                        if (!(route == this.PremiumAutoSweetScentRoutesString[37]))
                                                        {
                                                          if (!(route == this.PremiumAutoSweetScentRoutesString[38]))
                                                          {
                                                            if (!(route == this.PremiumAutoSweetScentRoutesString[39]))
                                                            {
                                                              if (route == this.PremiumAutoSweetScentRoutesString[40])
                                                                this.Unova_Opelucid_City_2(action);
                                                              else if (!(route == this.PremiumAutoSweetScentRoutesString[41]))
                                                              {
                                                                if (!(route == this.PremiumAutoSweetScentRoutesString[42]))
                                                                {
                                                                  if (route == this.PremiumAutoSweetScentRoutesString[43])
                                                                    this.Unova_Icirrus_City_1(action);
                                                                  else if (route == this.PremiumAutoSweetScentRoutesString[44])
                                                                    this.Unova_Icirrus_City_2(action);
                                                                  else if (!(route == this.PremiumAutoSweetScentRoutesString[45]))
                                                                  {
                                                                    if (!(route == this.PremiumAutoSweetScentRoutesString[46]))
                                                                    {
                                                                      if (!(route == this.PremiumAutoSweetScentRoutesString[47]))
                                                                      {
                                                                        if (route == this.PremiumAutoSweetScentRoutesString[48 /*0x30*/])
                                                                          this.Unova_Driftveil_City_1(action);
                                                                        else if (route == this.PremiumAutoSweetScentRoutesString[49])
                                                                          this.Unova_Driftveil_City_2(action);
                                                                        else if (!(route == this.PremiumAutoSweetScentRoutesString[50]))
                                                                        {
                                                                          if (!(route == this.PremiumAutoSweetScentRoutesString[51]))
                                                                          {
                                                                            if (route == this.PremiumAutoSweetScentRoutesString[52])
                                                                              this.Unova_Nimbasa_City_2(action);
                                                                            else if (!(route == this.PremiumAutoSweetScentRoutesString[53]))
                                                                            {
                                                                              if (route == this.PremiumAutoSweetScentRoutesString[54])
                                                                                this.Unova_Nimbasa_City_4(action);
                                                                              else if (route == this.PremiumAutoSweetScentRoutesString[55])
                                                                                this.Unova_Nacrene_City_1(action);
                                                                              else if (!(route == this.PremiumAutoSweetScentRoutesString[56]))
                                                                              {
                                                                                if (route == this.PremiumAutoSweetScentRoutesString[57])
                                                                                  this.Unova_Striaton_City(action);
                                                                                else if (route == this.PremiumAutoSweetScentRoutesString[67])
                                                                                  this.Unova_Undella_Town(action);
                                                                                else if (!(route == this.PremiumAutoSweetScentRoutesString[68]))
                                                                                {
                                                                                  if (route == "N/A")
                                                                                  {
                                                                                    Bot.Instance.Stop();
                                                                                    int num = (int) MessageBox.Show("This Route is only for PREMIUM Users!", "PREMIUM", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                                                                                  }
                                                                                  else
                                                                                  {
                                                                                    Bot.Instance.Stop();
                                                                                    int num = (int) MessageBox.Show("This Route is only for PREMIUM Users!", "PREMIUM", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                                                                                  }
                                                                                }
                                                                                else
                                                                                  this.Unova_Pokemon_League(action);
                                                                              }
                                                                              else
                                                                                this.Unova_Nacrene_City_2(action);
                                                                            }
                                                                            else
                                                                              this.Unova_Nimbasa_City_3(action);
                                                                          }
                                                                          else
                                                                            this.Unova_Nimbasa_City_1(action);
                                                                        }
                                                                        else
                                                                          this.Unova_Driftveil_City_3(action);
                                                                      }
                                                                      else
                                                                        this.Unova_Mistralton_City_2(action);
                                                                    }
                                                                    else
                                                                      this.Unova_Icirrus_City_4(action);
                                                                  }
                                                                  else
                                                                    this.Unova_Icirrus_City_3(action);
                                                                }
                                                                else
                                                                  this.Unova_Opelucid_City_4(action);
                                                              }
                                                              else
                                                                this.Unova_Opelucid_City_3(action);
                                                            }
                                                            else
                                                              this.Unova_Opelucid_City_1(action);
                                                          }
                                                          else
                                                            this.Unova_Lacunosa_Town_5(action);
                                                        }
                                                        else
                                                          this.Unova_Lacunosa_Town_4(action);
                                                      }
                                                      else
                                                        this.Unova_Lacunosa_Town_3(action);
                                                    }
                                                    else
                                                      this.Unova_Undella_Town_1(action);
                                                  }
                                                  else
                                                    this.Unova_Icirrus_City_Fish(action);
                                                }
                                                else
                                                  this.Sinnoh_Resort_Area(action);
                                              }
                                              else
                                                this.Sinnoh_Fight_Area(action);
                                            }
                                            else
                                              this.Sinnoh_Snowpoint_City(action);
                                          }
                                          else
                                            this.Sinnoh_Canalave_City(action);
                                        }
                                        else
                                          this.Sinnoh_Sunyshore_City(action);
                                      }
                                      else
                                        this.Sinnoh_Solaceon_Town_1(action);
                                    }
                                    else
                                      this.Hoenn_Ever_Grande_City_2_Fish(action);
                                  }
                                  else
                                    this.Hoenn_Sootopolis_City(action);
                                }
                                else
                                  this.Hoenn_Pacifidlog_Town(action);
                              }
                              else
                                this.Hoenn_Fallarbor_Town_2(action);
                            }
                            else
                              this.Hoenn_Fallarbor_Town_1(action);
                          }
                          else
                            this.Hoenn_Petalburg_City(action);
                        }
                        else
                          this.Kanto_Celadon_City(action);
                      }
                      else
                        this.Kanto_Island_Six_1(action);
                    }
                    else
                      this.Kanto_Island_Two_2(action);
                  }
                  else
                    this.Kanto_Island_One(action);
                }
                else
                  this.Kanto_Fuchsia_City(action);
              }
              else
                this.Kanto_Cinnabar_Island(action);
            }
            else
              this.Kanto_Virdian_City(action);
          }
          else
            this.Unova_Mistralton_City_1(action);
        }
        else
          this.Kanto_Route_4(action);
      }
      else
        this.Hoenn_Safari_Walk_3(action);
    }
    else
      this.Hoenn_Safari_Walk_2_Repel(action);
  }

  public void HealPokemon()
  {
    string selectedRoute = this.GetSelectedRoute();
    if (selectedRoute.Contains("Kanto"))
    {
      if (!Includes.ApplicationIsActivated())
        return;
      if (!Bot.Instance.Settings.SlowMode)
        InputKeyboard.PressKeyA(5500);
      else
        InputKeyboard.PressKeyA(6000);
    }
    else if (selectedRoute.Contains("Johto"))
    {
      if (!Includes.ApplicationIsActivated())
        return;
      if (Bot.Instance.Settings.SlowMode)
        InputKeyboard.PressKeyA(6000);
      else
        InputKeyboard.PressKeyA(5500);
    }
    else if (!selectedRoute.Contains("Hoenn"))
    {
      if (selectedRoute.Contains("Sinnoh"))
      {
        if (!Includes.ApplicationIsActivated())
          return;
        if (Bot.Instance.Settings.SlowMode)
          InputKeyboard.PressKeyA(10000);
        else
          InputKeyboard.PressKeyA(9500);
      }
      else
      {
        if (!selectedRoute.Contains("Unova") || !Includes.ApplicationIsActivated())
          return;
        if (!Bot.Instance.Settings.SlowMode)
          InputKeyboard.PressKeyA(9500);
        else
          InputKeyboard.PressKeyA(10000);
      }
    }
    else
    {
      if (!Includes.ApplicationIsActivated())
        return;
      if (!Bot.Instance.Settings.SlowMode)
        InputKeyboard.PressKeyA(5600);
      else
        InputKeyboard.PressKeyA(6100);
    }
  }

  public void WaitAfterBump()
  {
    string selectedRoute = this.GetSelectedRoute();
    if (selectedRoute.Contains("Kanto"))
    {
      if (!Includes.ApplicationIsActivated())
        return;
      if (Bot.Instance.Settings.SlowMode)
        Thread.Sleep(1500);
      else
        Thread.Sleep(1200);
    }
    else if (selectedRoute.Contains("Johto"))
    {
      if (!Includes.ApplicationIsActivated())
        return;
      if (!Bot.Instance.Settings.SlowMode)
        Thread.Sleep(1200);
      else
        Thread.Sleep(1700);
    }
    else if (!selectedRoute.Contains("Hoenn"))
    {
      if (!selectedRoute.Contains("Sinnoh"))
      {
        if (!selectedRoute.Contains("Unova") || !Includes.ApplicationIsActivated())
          return;
        if (!Bot.Instance.Settings.SlowMode)
          Thread.Sleep(1200);
        else
          Thread.Sleep(1700);
      }
      else
      {
        if (!Includes.ApplicationIsActivated())
          return;
        if (Bot.Instance.Settings.SlowMode)
          Thread.Sleep(1700);
        else
          Thread.Sleep(1200);
      }
    }
    else
    {
      if (!Includes.ApplicationIsActivated())
        return;
      if (Bot.Instance.Settings.SlowMode)
        Thread.Sleep(1700);
      else
        Thread.Sleep(1200);
    }
  }

  public void WaitAfterEntrace()
  {
    string selectedRoute = this.GetSelectedRoute();
    if (!selectedRoute.Contains("Kanto"))
    {
      if (selectedRoute.Contains("Johto"))
      {
        if (!Includes.ApplicationIsActivated())
          return;
        if (!Bot.Instance.Settings.SlowMode)
          Thread.Sleep(2500);
        else
          Thread.Sleep(3500);
      }
      else if (!selectedRoute.Contains("Hoenn"))
      {
        if (selectedRoute.Contains("Sinnoh"))
        {
          if (!Includes.ApplicationIsActivated())
            return;
          if (!Bot.Instance.Settings.SlowMode)
            Thread.Sleep(2500);
          else
            Thread.Sleep(3500);
        }
        else
        {
          if (!selectedRoute.Contains("Unova") || !Includes.ApplicationIsActivated())
            return;
          if (!Bot.Instance.Settings.SlowMode)
            Thread.Sleep(3000);
          else
            Thread.Sleep(4000);
        }
      }
      else
      {
        if (!Includes.ApplicationIsActivated())
          return;
        if (Bot.Instance.Settings.SlowMode)
          Thread.Sleep(3500);
        else
          Thread.Sleep(2500);
      }
    }
    else
    {
      if (!Includes.ApplicationIsActivated())
        return;
      if (!Bot.Instance.Settings.SlowMode)
        Thread.Sleep(2000);
      else
        Thread.Sleep(3000);
    }
  }

  public void UseSurfer()
  {
    string selectedRoute = this.GetSelectedRoute();
    if (!selectedRoute.Contains("Kanto"))
    {
      if (!selectedRoute.Contains("Hoenn"))
      {
        if (!selectedRoute.Contains("Sinnoh"))
        {
          if (!selectedRoute.Contains("Unova") || !Includes.ApplicationIsActivated())
            return;
          Thread.Sleep(100);
          InputKeyboard.PressKeyA(2800);
        }
        else
        {
          if (!Includes.ApplicationIsActivated())
            return;
          Thread.Sleep(100);
          InputKeyboard.PressKeyA(2800);
        }
      }
      else
      {
        if (!Includes.ApplicationIsActivated())
          return;
        Thread.Sleep(100);
        InputKeyboard.PressKeyA(2800);
      }
    }
    else
    {
      if (!Includes.ApplicationIsActivated())
        return;
      Thread.Sleep(100);
      InputKeyboard.PressKeyA(2800);
    }
  }

  public void WaitBeforeTurn()
  {
    Bot.Instance.Actions.HumanizeActions();
    if (!Includes.ApplicationIsActivated())
      return;
    Thread.Sleep(300);
  }

  public void UseBike()
  {
    if (!Includes.ApplicationIsActivated())
      return;
    InputKeyboard.PressHotkey2(100);
    Thread.Sleep(250);
  }

  public void UseRepel()
  {
    if (!Includes.ApplicationIsActivated())
      return;
    InputKeyboard.PressHotkey5(100);
    Thread.Sleep(250);
  }

  public void UseDefog()
  {
    if (!Includes.ApplicationIsActivated())
      return;
    InputKeyboard.PressHotkey7(100);
    Thread.Sleep(250);
  }

  public void UseCut()
  {
    if (!Includes.ApplicationIsActivated())
      return;
    InputKeyboard.PressKeyA(3200);
    Thread.Sleep(1800);
  }

  public void UseEscapeRope()
  {
    if (!Includes.ApplicationIsActivated() || !Bot.Instance.Settings.EscapeRope)
      return;
    InputKeyboard.PressHotkey3(Bot.Instance.Settings.HoldTime);
    this.WaitAfterTeleport();
  }

  public void UseDig()
  {
    if (!Includes.ApplicationIsActivated())
      return;
    InputKeyboard.PressHotkey3(Bot.Instance.Settings.HoldTime);
    this.WaitAfterTeleport();
  }

  public void UseTeleport()
  {
    if (!Includes.ApplicationIsActivated())
      return;
    InputKeyboard.PressHotkey8(Bot.Instance.Settings.HoldTime);
    this.WaitAfterTeleport();
  }

  public void WaitAfterTeleport()
  {
    if (!Includes.ApplicationIsActivated())
      return;
    if (Bot.Instance.Settings.SlowMode)
      Thread.Sleep(5000);
    else
      Thread.Sleep(4000);
  }

  public void Unova_Icirrus_City_Fish(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(400);
        InputKeyboard.PressKeyUp(2700);
        InputKeyboard.PressKeyRight(200);
        InputKeyboard.PressKeyUp(2100);
        InputKeyboard.PressKeyLeft(200);
        InputKeyboard.PressKeyUp(700);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(700);
        InputKeyboard.PressKeyRight(200);
        break;
      case "goback":
        InputKeyboard.PressKeyDown(2000);
        this.WaitAfterEntrace();
        this.UseTeleport();
        break;
      case "teleportback":
        InputKeyboard.PressKeyDown(2000);
        this.WaitAfterEntrace();
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Undella_Town(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyLeft(1000);
        InputKeyboard.PressKeyDown(1000);
        InputKeyboard.PressKeyRight(3700);
        InputKeyboard.PressKeyUp(4000);
        InputKeyboard.PressKeyRight(200);
        this.UseSurfer();
        InputKeyboard.PressKeyRight(300);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Undella_Town_Fish(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyLeft(1000);
        InputKeyboard.PressKeyDown(1000);
        InputKeyboard.PressKeyRight(3700);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Hoenn_Ever_Grande_City_1(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(250);
        InputKeyboard.PressKeyLeft(750);
        InputKeyboard.PressKeyUp(1000);
        InputKeyboard.PressKeyLeft(150);
        InputKeyboard.PressKeyUp(300);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(50);
        break;
      case "goback":
        InputKeyboard.PressKeyDown(250);
        this.WaitAfterEntrace();
        this.UseEscapeRope();
        this.UseTeleport();
        break;
      case "teleportback":
        InputKeyboard.PressKeyDown(250);
        this.WaitAfterEntrace();
        this.UseEscapeRope();
        this.UseTeleport();
        break;
    }
  }

  public void Hoenn_Ever_Grande_City_2_Fish(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(250);
        InputKeyboard.PressKeyLeft(750);
        InputKeyboard.PressKeyDown(800);
        break;
      case "goback":
        this.UseEscapeRope();
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Kanto_Route_10(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(1000);
        InputKeyboard.PressKeyLeft(1000);
        InputKeyboard.PressKeyUp(1000);
        InputKeyboard.PressKeyRight(600);
        InputKeyboard.PressKeyUp(150);
        this.WaitAfterEntrace();
        if (Bot.Instance.Settings.AutoWalkFish)
        {
          InputKeyboard.PressKeyDown(300);
          break;
        }
        InputKeyboard.PressKeyDown(150);
        break;
      case "goback":
        if (Bot.Instance.Settings.AutoWalkFish)
        {
          InputKeyboard.PressHotkey3(Bot.Instance.Settings.HoldTime);
        }
        else
        {
          InputKeyboard.PressKeyUp(150);
          this.WaitAfterEntrace();
        }
        this.UseTeleport();
        break;
      case "teleportback":
        if (Bot.Instance.Settings.AutoWalkFish)
        {
          InputKeyboard.PressHotkey3(Bot.Instance.Settings.HoldTime);
        }
        else
        {
          InputKeyboard.PressKeyUp(150);
          this.WaitAfterEntrace();
        }
        this.UseTeleport();
        break;
    }
  }

  public void Hoenn_Battle_Frontier(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(300);
        InputKeyboard.PressKeyRight(2500);
        InputKeyboard.PressKeyDown(300);
        InputKeyboard.PressKeyRight(500);
        InputKeyboard.PressKeyDown(300);
        this.UseSurfer();
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Kanto_Celadon_City(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(2000);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Kanto_Safari_Fish_1(string action)
  {
    if (!(action == "goto"))
      return;
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyUp(150);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyA(7500);
    this.WaitAfterEntrace();
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyUp(1600);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyRight(1200);
    if (!Includes.ApplicationIsActivated())
      return;
    InputKeyboard.PressKeyUp(400);
  }

  public void Kanto_Safari_Walk_1(string action)
  {
    if (!(action == "goto"))
      return;
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyUp(150);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyA(7500);
    this.WaitAfterEntrace();
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyUp(150);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyLeft(1650);
    if (!Includes.ApplicationIsActivated())
      return;
    InputKeyboard.PressKeyUp(500);
  }

  public void Hoenn_Safari_Template()
  {
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyDown(150);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyLeft(1250);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyUp(1800);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyRight(3200);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyUp(400);
    if (!Includes.ApplicationIsActivated())
      return;
    InputKeyboard.PressKeyRight(650);
  }

  public void Hoenn_Safari_Walk_1(string action)
  {
    if (!(action == "goto"))
      return;
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyLeft(150);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyA(9000);
    this.WaitAfterEntrace();
    this.UseBike();
    this.Hoenn_Safari_Template();
    if (!Includes.ApplicationIsActivated())
      return;
    InputKeyboard.PressKeyDown(750);
  }

  public void RepelWalk(string direction, int holdtime)
  {
    switch (direction)
    {
      case "left":
        if (!Includes.ApplicationIsActivated())
          break;
        for (int index = 0; index < holdtime; index += 50)
        {
          InputKeyboard.PressKeyLeft(50);
          Thread.Sleep(250);
          if (Bot.Instance.Check.Repel)
            InputKeyboard.PressKeyA(150);
        }
        break;
      case "right":
        if (!Includes.ApplicationIsActivated())
          break;
        for (int index = 0; index < holdtime; index += 50)
        {
          InputKeyboard.PressKeyRight(50);
          Thread.Sleep(250);
          if (Bot.Instance.Check.Repel)
            InputKeyboard.PressKeyA(150);
        }
        break;
      case "up":
        if (!Includes.ApplicationIsActivated())
          break;
        for (int index = 0; index < holdtime; index += 50)
        {
          InputKeyboard.PressKeyUp(50);
          Thread.Sleep(250);
          if (Bot.Instance.Check.Repel)
            InputKeyboard.PressKeyA(150);
        }
        break;
      case "down":
        if (!Includes.ApplicationIsActivated())
          break;
        for (int index = 0; index < holdtime; index += 50)
        {
          InputKeyboard.PressKeyDown(50);
          Thread.Sleep(250);
          if (Bot.Instance.Check.Repel)
            InputKeyboard.PressKeyA(150);
        }
        break;
    }
  }

  public void Hoenn_Safari_Walk_2_Repel(string action)
  {
    switch (action)
    {
      case "goto":
        if (Bot.Instance.Check.Repel)
          InputKeyboard.PressKeyA(150);
        this.RepelWalk("left", 100);
        if (Includes.ApplicationIsActivated())
          InputKeyboard.PressKeyA(9000);
        this.WaitAfterEntrace();
        if (Bot.Instance.Check.Repel)
          InputKeyboard.PressKeyA(150);
        this.UseBike();
        this.RepelWalk("down", 100);
        this.RepelWalk("left", 800);
        this.RepelWalk("up", 1100);
        this.RepelWalk("right", 2000);
        this.RepelWalk("up", 250);
        this.RepelWalk("right", 550);
        this.RepelWalk("down", 250);
        this.RepelWalk("right", 50);
        this.UseRepel();
        this.UseSurfer();
        this.RepelWalk("down", 100);
        this.WaitAfterBump();
        this.RepelWalk("down", 200);
        break;
      case "goto2":
        if (Includes.ApplicationIsActivated())
          InputKeyboard.PressKeyLeft(150);
        if (Includes.ApplicationIsActivated())
          InputKeyboard.PressKeyA(9000);
        this.WaitAfterEntrace();
        this.UseBike();
        this.Hoenn_Safari_Template();
        if (Includes.ApplicationIsActivated())
          InputKeyboard.PressKeyRight(200);
        if (Includes.ApplicationIsActivated())
          InputKeyboard.PressKeyDown(300);
        if (Includes.ApplicationIsActivated())
          InputKeyboard.PressKeyRight(50);
        this.UseSurfer();
        if (Includes.ApplicationIsActivated())
          InputKeyboard.PressKeyDown(150);
        this.WaitAfterBump();
        if (!Includes.ApplicationIsActivated())
          break;
        InputKeyboard.PressKeyDown(200);
        break;
    }
  }

  public void Hoenn_Safari_Walk_3(string action)
  {
    if (!(action == "goto"))
      return;
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyLeft(150);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyA(9000);
    this.WaitAfterEntrace();
    this.UseBike();
    this.Hoenn_Safari_Template();
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyRight(200);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyUp(1000);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyRight(800);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyUp(1000);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyLeft(700);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyUp(1100);
    if (Includes.ApplicationIsActivated())
      InputKeyboard.PressKeyRight(650);
    if (!Includes.ApplicationIsActivated())
      return;
    InputKeyboard.PressKeyDown(500);
  }

  public void Kanto_Island_One(string action)
  {
    switch (action)
    {
      case "goto":
        if (Bot.Instance.Check.Repel)
          InputKeyboard.PressKeyA(150);
        if (Includes.ApplicationIsActivated())
        {
          for (int index = 0; index < 100; index += 50)
          {
            InputKeyboard.PressKeyDown(50);
            Thread.Sleep(250);
            if (Bot.Instance.Check.Repel)
              InputKeyboard.PressKeyA(150);
          }
        }
        if (Includes.ApplicationIsActivated())
        {
          for (int index = 0; index < 250; index += 50)
          {
            InputKeyboard.PressKeyRight(50);
            Thread.Sleep(250);
            if (Bot.Instance.Check.Repel)
              InputKeyboard.PressKeyA(150);
          }
        }
        if (Includes.ApplicationIsActivated())
        {
          for (int index = 0; index < 400; index += 50)
          {
            InputKeyboard.PressKeyDown(50);
            Thread.Sleep(250);
            if (Bot.Instance.Check.Repel)
              InputKeyboard.PressKeyA(150);
          }
        }
        this.WaitAfterEntrace();
        this.UseBike();
        if (Includes.ApplicationIsActivated())
        {
          for (int index = 0; index < 200; index += 50)
          {
            InputKeyboard.PressKeyDown(50);
            Thread.Sleep(250);
            if (Bot.Instance.Check.Repel)
              InputKeyboard.PressKeyA(150);
          }
        }
        if (Includes.ApplicationIsActivated())
        {
          for (int index = 0; index < 350; index += 50)
          {
            InputKeyboard.PressKeyRight(50);
            Thread.Sleep(250);
            if (Bot.Instance.Check.Repel)
              InputKeyboard.PressKeyA(150);
          }
        }
        if (Includes.ApplicationIsActivated())
        {
          for (int index = 0; index < 250; index += 50)
          {
            InputKeyboard.PressKeyDown(50);
            Thread.Sleep(250);
            if (Bot.Instance.Check.Repel)
              InputKeyboard.PressKeyA(150);
          }
        }
        if (Includes.ApplicationIsActivated())
        {
          for (int index = 0; index < 550; index += 50)
          {
            InputKeyboard.PressKeyRight(50);
            Thread.Sleep(250);
            if (Bot.Instance.Check.Repel)
              InputKeyboard.PressKeyA(150);
          }
        }
        this.UseRepel();
        this.UseSurfer();
        if (Includes.ApplicationIsActivated())
        {
          for (int index = 0; index < 350; index += 50)
          {
            InputKeyboard.PressKeyRight(50);
            Thread.Sleep(250);
            if (Bot.Instance.Check.Repel)
              InputKeyboard.PressKeyA(150);
          }
        }
        if (!Includes.ApplicationIsActivated())
          break;
        for (int index = 0; index < 1100; index += 50)
        {
          InputKeyboard.PressKeyUp(50);
          Thread.Sleep(250);
          if (Bot.Instance.Check.Repel)
            InputKeyboard.PressKeyA(150);
        }
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Kanto_Island_Two_Template()
  {
    InputKeyboard.PressKeyDown(1450);
    this.WaitAfterEntrace();
    this.UseBike();
    InputKeyboard.PressKeyRight(600);
    InputKeyboard.PressKeyUp(500);
    InputKeyboard.PressKeyRight(900);
    InputKeyboard.PressKeyUp(1000);
  }

  public void Kanto_Island_Two_1(string action)
  {
    switch (action)
    {
      case "goto":
        this.Kanto_Island_Two_Template();
        InputKeyboard.PressKeyLeft(550);
        InputKeyboard.PressKeyUp(500);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Kanto_Island_Two_2(string action)
  {
    switch (action)
    {
      case "goto":
        this.Kanto_Island_Two_Template();
        this.UseSurfer();
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Kanto_Island_Six_1(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(2000);
        InputKeyboard.PressKeyDown(660);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Kanto_Island_Six_2(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(2000);
        InputKeyboard.PressKeyDown(300);
        InputKeyboard.PressKeyRight(300);
        this.UseSurfer();
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Hoenn_Petalburg_City(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(300);
        InputKeyboard.PressKeyUp(1000);
        this.UseSurfer();
        break;
      case "goback":
        InputKeyboard.PressKeyDown(800);
        InputKeyboard.PressKeyLeft(350);
        InputKeyboard.PressKeyUp(250);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1400);
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Hoenn_Verdanturf_Town(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(550);
        InputKeyboard.PressKeyRight(2100);
        this.UseSurfer();
        break;
      case "goback":
        InputKeyboard.PressKeyLeft(2100);
        InputKeyboard.PressKeyUp(800);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1400);
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Hoenn_Slateport_City(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(250);
        InputKeyboard.PressKeyUp(2150);
        InputKeyboard.PressKeyLeft(300);
        break;
      case "goback":
        InputKeyboard.PressKeyRight(300);
        InputKeyboard.PressKeyDown(2150);
        InputKeyboard.PressKeyRight(250);
        InputKeyboard.PressKeyUp(400);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1400);
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Hoenn_Pacifidlog_Town(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyDown(100);
        InputKeyboard.PressKeyLeft(150);
        InputKeyboard.PressKeyDown(200);
        this.UseSurfer();
        break;
      case "goback":
        InputKeyboard.PressKeyUp(150);
        InputKeyboard.PressKeyRight(150);
        InputKeyboard.PressKeyUp(350);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1400);
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Hoenn_Sootopolis_City(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(1000);
        this.UseSurfer();
        break;
      case "goback":
        InputKeyboard.PressKeyUp(1200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1450);
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Sinnoh_Celestic_Town_1(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(400);
        InputKeyboard.PressKeyUp(650);
        InputKeyboard.PressKeyRight(650);
        InputKeyboard.PressKeyDown(140);
        this.UseDefog();
        break;
      case "goback":
        InputKeyboard.PressKeyUp(150);
        InputKeyboard.PressKeyLeft(650);
        InputKeyboard.PressKeyDown(650);
        InputKeyboard.PressKeyLeft(400);
        InputKeyboard.PressKeyUp(150);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1450);
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Sinnoh_Celestic_Town_2(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(1000);
        this.UseSurfer();
        break;
      case "goback":
        InputKeyboard.PressKeyRight(1000);
        InputKeyboard.PressKeyUp(150);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1450);
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Hoenn_Fallarbor_Town_1(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(120);
        InputKeyboard.PressKeyLeft(2150);
        InputKeyboard.PressKeyUp(1250);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1700);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(3350);
        InputKeyboard.PressKeyLeft(200);
        InputKeyboard.PressKeyUp(700);
        InputKeyboard.PressKeyRight(200);
        InputKeyboard.PressKeyUp(300);
        this.WaitAfterEntrace();
        if (Bot.Instance.Settings.AutoWalkFish)
        {
          InputKeyboard.PressKeyUp(300);
          this.UseBike();
          break;
        }
        InputKeyboard.PressKeyUp(50);
        break;
      case "goback":
        if (Bot.Instance.Settings.AutoWalkFish)
          this.UseDig();
        else
          InputKeyboard.PressKeyDown(300);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyLeft(200);
        InputKeyboard.PressKeyDown(600);
        InputKeyboard.PressKeyRight(200);
        InputKeyboard.PressKeyDown(3650);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyDown(1200);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(300);
        InputKeyboard.PressKeyRight(2150);
        InputKeyboard.PressKeyUp(400);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1450);
        break;
      case "teleportback":
        if (Bot.Instance.Settings.AutoWalkFish)
          this.UseDig();
        else
          InputKeyboard.PressKeyDown(300);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyLeft(200);
        InputKeyboard.PressKeyDown(600);
        InputKeyboard.PressKeyRight(200);
        InputKeyboard.PressKeyDown(3650);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyDown(1200);
        this.WaitAfterEntrace();
        this.UseTeleport();
        break;
    }
  }

  public void Hoenn_Fallarbor_Town_2(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(110);
        InputKeyboard.PressKeyLeft(2850);
        InputKeyboard.PressKeyDown(2550);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Sinnoh_Eterna_City(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(50);
        InputKeyboard.PressKeyLeft(2050);
        InputKeyboard.PressKeyUp(250);
        break;
      case "goback":
        InputKeyboard.PressKeyDown(250);
        InputKeyboard.PressKeyRight(2000);
        InputKeyboard.PressKeyUp(400);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1450);
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Sinnoh_Solaceon_Town_1(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(400);
        InputKeyboard.PressKeyDown(2600);
        InputKeyboard.PressKeyRight(300);
        break;
      case "goback":
        InputKeyboard.PressKeyLeft(300);
        InputKeyboard.PressKeyUp(2600);
        InputKeyboard.PressKeyRight(400);
        InputKeyboard.PressKeyUp(200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1450);
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Sinnoh_Solaceon_Town_2(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(500);
        InputKeyboard.PressKeyUp(2600);
        this.UseBike();
        InputKeyboard.PressKeyUp(100);
        break;
      case "goback":
        InputKeyboard.PressKeyDown(100);
        this.UseBike();
        InputKeyboard.PressKeyDown(2300);
        InputKeyboard.PressKeyRight(500);
        InputKeyboard.PressKeyUp(200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1450);
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  private void Sinnoh_Resort_Area(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(50);
        InputKeyboard.PressKeyRight(550);
        InputKeyboard.PressKeyUp(1700);
        InputKeyboard.PressKeyLeft(150);
        InputKeyboard.PressKeyUp(600);
        InputKeyboard.PressKeyLeft(400);
        InputKeyboard.PressKeyUp(1300);
        InputKeyboard.PressKeyLeft(500);
        InputKeyboard.PressKeyUp(500);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  private void Sinnoh_Fight_Area(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(150);
        InputKeyboard.PressKeyRight(4500);
        InputKeyboard.PressKeyUp(150);
        InputKeyboard.PressKeyRight(150);
        this.UseSurfer();
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  private void Sinnoh_Snowpoint_City(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyLeft(900);
        InputKeyboard.PressKeyUp(1500);
        InputKeyboard.PressKeyLeft(1400);
        InputKeyboard.PressKeyDown(2800);
        InputKeyboard.PressKeyLeft(2500);
        InputKeyboard.PressKeyUp(700);
        InputKeyboard.PressKeyLeft(3700);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  private void Sinnoh_Celestic_Town_3(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(1000);
        InputKeyboard.PressKeyDown(500);
        InputKeyboard.PressKeyLeft(1000);
        InputKeyboard.PressKeyUp(500);
        InputKeyboard.PressKeyLeft(300);
        InputKeyboard.PressKeyUp(850);
        InputKeyboard.PressKeyLeft(1250);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  private void Sinnoh_Veilstone_City(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(300);
        InputKeyboard.PressKeyLeft(550);
        InputKeyboard.PressKeyDown(400);
        InputKeyboard.PressKeyRight(1000);
        InputKeyboard.PressKeyDown(1200);
        InputKeyboard.PressKeyLeft(400);
        InputKeyboard.PressKeyDown(800);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyDown(1200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyDown(1400);
        InputKeyboard.PressKeyRight(950);
        InputKeyboard.PressKeyDown(600);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  private void Sinnoh_Pokemon_League(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(850);
        InputKeyboard.PressKeyUp(300);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(50);
        break;
      case "goback":
        InputKeyboard.PressKeyDown(300);
        this.WaitAfterEntrace();
        this.UseEscapeRope();
        this.UseTeleport();
        break;
      case "teleportback":
        InputKeyboard.PressKeyDown(300);
        this.WaitAfterEntrace();
        this.UseEscapeRope();
        this.UseTeleport();
        break;
    }
  }

  public void Sinnoh_Canalave_City(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(400);
        InputKeyboard.PressKeyDown(2700);
        InputKeyboard.PressKeyRight(300);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyRight(1200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyRight(800);
        InputKeyboard.PressKeyDown(150);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Sinnoh_Sunyshore_City(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(100);
        InputKeyboard.PressKeyLeft(2000);
        InputKeyboard.PressKeyDown(400);
        InputKeyboard.PressKeyLeft(800);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyLeft(1200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyLeft(400);
        InputKeyboard.PressKeyUp(400);
        InputKeyboard.PressKeyLeft(900);
        InputKeyboard.PressKeyUp(900);
        InputKeyboard.PressKeyLeft(2600);
        InputKeyboard.PressKeyUp(600);
        InputKeyboard.PressKeyRight(1900);
        InputKeyboard.PressKeyDown(400);
        InputKeyboard.PressKeyLeft(500);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Sinnoh_Floaroma_Town(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(1300);
        InputKeyboard.PressKeyUp(500);
        InputKeyboard.PressKeyRight(3400);
        InputKeyboard.PressKeyUp(300);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Striaton_City(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(200);
        InputKeyboard.PressKeyLeft(1750);
        InputKeyboard.PressKeyUp(300);
        this.UseSurfer();
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Nacrene_City_1(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(2900);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyRight(1200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyRight(550);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Nacrene_City_2(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(600);
        InputKeyboard.PressKeyUp(800);
        InputKeyboard.PressKeyLeft(3000);
        InputKeyboard.PressKeyUp(400);
        InputKeyboard.PressKeyLeft(900);
        InputKeyboard.PressKeyDown(900);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Icirrus_City_1(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(600);
        InputKeyboard.PressKeyDown(800);
        InputKeyboard.PressKeyRight(1700);
        this.UseSurfer();
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Icirrus_City_2(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(600);
        InputKeyboard.PressKeyDown(1070);
        InputKeyboard.PressKeyLeft(750);
        this.WaitAfterEntrace();
        break;
      case "goback":
        InputKeyboard.PressKeyRight(150);
        this.WaitAfterEntrace();
        this.UseEscapeRope();
        this.UseTeleport();
        break;
      case "teleportback":
        InputKeyboard.PressKeyRight(150);
        this.WaitAfterEntrace();
        this.UseEscapeRope();
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Icirrus_City_3(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(450);
        InputKeyboard.PressKeyUp(3650);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Icirrus_City_4(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(450);
        InputKeyboard.PressKeyUp(3300);
        InputKeyboard.PressKeyRight(400);
        InputKeyboard.PressKeyUp(1300);
        InputKeyboard.PressKeyLeft(250);
        InputKeyboard.PressKeyUp(300);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(2000);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(100);
        break;
      case "goback":
        InputKeyboard.PressKeyDown(350);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(1500);
        this.WaitAfterEntrace();
        this.UseTeleport();
        break;
      case "teleportback":
        InputKeyboard.PressKeyDown(350);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(1500);
        this.WaitAfterEntrace();
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Pokemon_League(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(50);
        InputKeyboard.PressKeyRight(1000);
        InputKeyboard.PressKeyDown(1000);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyDown(200);
        InputKeyboard.PressKeyLeft(850);
        InputKeyboard.PressKeyDown(4300);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Opelucid_City_1(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(100);
        InputKeyboard.PressKeyLeft(3500);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyLeft(1200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyLeft(500);
        InputKeyboard.PressKeyDown(400);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Opelucid_City_2(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(2200);
        InputKeyboard.PressKeyDown(150);
        InputKeyboard.PressKeyRight(250);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyRight(1200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyRight(550);
        InputKeyboard.PressKeyUp(500);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Opelucid_City_3(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(750);
        InputKeyboard.PressKeyUp(4000);
        InputKeyboard.PressKeyLeft(150);
        InputKeyboard.PressKeyUp(250);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyRight(300);
        InputKeyboard.PressKeyUp(900);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Opelucid_City_4(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(750);
        InputKeyboard.PressKeyUp(4000);
        InputKeyboard.PressKeyLeft(150);
        InputKeyboard.PressKeyUp(250);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(450);
        InputKeyboard.PressKeyRight(4000);
        InputKeyboard.PressKeyUp(300);
        InputKeyboard.PressKeyRight(900);
        InputKeyboard.PressKeyUp(200);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Lacunosa_Town_1(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(50);
        InputKeyboard.PressKeyLeft(3000);
        InputKeyboard.PressKeyUp(300);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Lacunosa_Town_2(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(900);
        InputKeyboard.PressKeyUp(900);
        InputKeyboard.PressKeyRight(900);
        InputKeyboard.PressKeyDown(800);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Lacunosa_Town_3(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(900);
        InputKeyboard.PressKeyUp(900);
        InputKeyboard.PressKeyRight(2000);
        InputKeyboard.PressKeyUp(200);
        InputKeyboard.PressKeyRight(300);
        InputKeyboard.PressKeyUp(300);
        InputKeyboard.PressKeyRight(250);
        InputKeyboard.PressKeyUp(500);
        InputKeyboard.PressKeyLeft(300);
        InputKeyboard.PressKeyUp(300);
        InputKeyboard.PressKeyLeft(300);
        InputKeyboard.PressKeyUp(200);
        InputKeyboard.PressKeyLeft(300);
        InputKeyboard.PressKeyUp(600);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Lacunosa_Town_4(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(900);
        InputKeyboard.PressKeyUp(900);
        InputKeyboard.PressKeyRight(2000);
        InputKeyboard.PressKeyUp(200);
        InputKeyboard.PressKeyRight(300);
        InputKeyboard.PressKeyUp(300);
        InputKeyboard.PressKeyRight(250);
        InputKeyboard.PressKeyUp(500);
        InputKeyboard.PressKeyLeft(300);
        InputKeyboard.PressKeyUp(300);
        InputKeyboard.PressKeyLeft(300);
        InputKeyboard.PressKeyUp(200);
        InputKeyboard.PressKeyLeft(200);
        InputKeyboard.PressKeyUp(600);
        InputKeyboard.PressKeyRight(900);
        InputKeyboard.PressKeyUp(300);
        InputKeyboard.PressKeyRight(200);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Lacunosa_Town_5(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(900);
        InputKeyboard.PressKeyUp(900);
        InputKeyboard.PressKeyRight(2000);
        InputKeyboard.PressKeyUp(200);
        InputKeyboard.PressKeyRight(300);
        InputKeyboard.PressKeyUp(300);
        InputKeyboard.PressKeyRight(250);
        InputKeyboard.PressKeyUp(500);
        InputKeyboard.PressKeyLeft(300);
        InputKeyboard.PressKeyUp(300);
        InputKeyboard.PressKeyLeft(300);
        InputKeyboard.PressKeyUp(200);
        InputKeyboard.PressKeyLeft(200);
        InputKeyboard.PressKeyUp(600);
        InputKeyboard.PressKeyRight(900);
        InputKeyboard.PressKeyUp(400);
        InputKeyboard.PressKeyLeft(500);
        InputKeyboard.PressKeyUp(150);
        InputKeyboard.PressKeyLeft(600);
        InputKeyboard.PressKeyUp(250);
        this.WaitAfterEntrace();
        break;
      case "goback":
        InputKeyboard.PressKeyDown(150);
        this.WaitAfterEntrace();
        this.UseEscapeRope();
        this.UseTeleport();
        break;
      case "teleportback":
        InputKeyboard.PressKeyDown(150);
        this.WaitAfterEntrace();
        this.UseEscapeRope();
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Undella_Town_1(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(250);
        InputKeyboard.PressKeyLeft(750);
        InputKeyboard.PressKeyDown(800);
        InputKeyboard.PressKeyLeft(2550);
        InputKeyboard.PressKeyDown(1000);
        InputKeyboard.PressKeyRight(2200);
        InputKeyboard.PressKeyDown(850);
        InputKeyboard.PressKeyLeft(450);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Undella_Town_2(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(250);
        InputKeyboard.PressKeyLeft(750);
        InputKeyboard.PressKeyDown(800);
        InputKeyboard.PressKeyLeft(2550);
        InputKeyboard.PressKeyDown(1000);
        InputKeyboard.PressKeyRight(2150);
        InputKeyboard.PressKeyDown(2300);
        this.UseSurfer();
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Black_City_1(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(750);
        InputKeyboard.PressKeyUp(2400);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1100);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(350);
        InputKeyboard.PressKeyRight(900);
        InputKeyboard.PressKeyUp(300);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Black_City_2(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(750);
        InputKeyboard.PressKeyDown(1350);
        InputKeyboard.PressKeyLeft(3600);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyLeft(1200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyLeft(300);
        InputKeyboard.PressKeyDown(900);
        InputKeyboard.PressKeyLeft(300);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Nimbasa_City_1(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(2000);
        InputKeyboard.PressKeyDown(1100);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyDown(1200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyDown(4000);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Nimbasa_City_2(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(2300);
        InputKeyboard.PressKeyUp(2500);
        InputKeyboard.PressKeyRight(1300);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyRight(1200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyRight(1200);
        InputKeyboard.PressKeyDown(1200);
        InputKeyboard.PressKeyRight(500);
        InputKeyboard.PressKeyUp(1200);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Nimbasa_City_3(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(2300);
        InputKeyboard.PressKeyUp(2500);
        InputKeyboard.PressKeyRight(1300);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyRight(1200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyRight(1200);
        InputKeyboard.PressKeyDown(1200);
        InputKeyboard.PressKeyRight(1250);
        InputKeyboard.PressKeyUp(1200);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Nimbasa_City_4(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(500);
        InputKeyboard.PressKeyUp(2650);
        InputKeyboard.PressKeyLeft(2300);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyLeft(1200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyLeft(550);
        InputKeyboard.PressKeyUp(950);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Driftveil_City_1(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(500);
        InputKeyboard.PressKeyUp(700);
        InputKeyboard.PressKeyLeft(3600);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Driftveil_City_2(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(500);
        InputKeyboard.PressKeyUp(150);
        InputKeyboard.PressKeyRight(1200);
        this.UseSurfer();
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Driftveil_City_3(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(500);
        InputKeyboard.PressKeyLeft(600);
        InputKeyboard.PressKeyDown(1500);
        InputKeyboard.PressKeyLeft(500);
        InputKeyboard.PressKeyDown(2000);
        InputKeyboard.PressKeyLeft(400);
        InputKeyboard.PressKeyDown(1100);
        InputKeyboard.PressKeyRight(300);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Mistralton_City_1(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(500);
        InputKeyboard.PressKeyDown(550);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyDown(50);
        break;
      case "goback":
        InputKeyboard.PressKeyUp(350);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(400);
        InputKeyboard.PressKeyRight(500);
        InputKeyboard.PressKeyUp(200);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1450);
        break;
      case "teleportback":
        InputKeyboard.PressKeyUp(400);
        this.WaitAfterEntrace();
        this.UseEscapeRope();
        this.UseTeleport();
        break;
    }
  }

  public void Unova_Mistralton_City_2(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(500);
        InputKeyboard.PressKeyUp(700);
        InputKeyboard.PressKeyRight(600);
        InputKeyboard.PressKeyUp(400);
        InputKeyboard.PressKeyRight(800);
        InputKeyboard.PressKeyUp(1500);
        this.UseBike();
        InputKeyboard.PressKeyUp(100);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Kanto_Route_4(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(660);
        InputKeyboard.PressKeyUp(400);
        break;
      case "goback":
        InputKeyboard.PressKeyDown(400);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyLeft(650);
        InputKeyboard.PressKeyUp(400);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyUp(1450);
        break;
      case "teleportback":
        InputKeyboard.PressKeyDown(400);
        this.WaitAfterEntrace();
        this.UseEscapeRope();
        this.UseTeleport();
        break;
    }
  }

  public void Kanto_Fuchsia_City(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyRight(2300);
        InputKeyboard.PressKeyUp(1000);
        InputKeyboard.PressKeyRight(1400);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyRight(1300);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyRight(500);
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Kanto_Indigo_Plateau(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(700);
        InputKeyboard.PressKeyLeft(400);
        InputKeyboard.PressKeyDown(400);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyDown(1200);
        InputKeyboard.PressKeyRight(400);
        InputKeyboard.PressKeyDown(800);
        InputKeyboard.PressKeyRight(150);
        InputKeyboard.PressKeyDown(750);
        InputKeyboard.PressKeyRight(600);
        InputKeyboard.PressKeyDown(900);
        InputKeyboard.PressKeyLeft(300);
        InputKeyboard.PressKeyUp(200);
        this.WaitAfterEntrace();
        break;
      case "goback":
        InputKeyboard.PressKeyDown(200);
        this.WaitAfterEntrace();
        this.UseEscapeRope();
        this.UseTeleport();
        break;
      case "teleportback":
        InputKeyboard.PressKeyDown(200);
        this.WaitAfterEntrace();
        this.UseEscapeRope();
        this.UseTeleport();
        break;
    }
  }

  public void Kanto_Cerulean_City(string action)
  {
    switch (action)
    {
      case "goto":
        int num = 0;
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        switch (RandomNumber.Between(1, 2))
        {
          case 1:
            InputKeyboard.PressKeyDown(200);
            InputKeyboard.PressKeyLeft(400);
            break;
          case 2:
            InputKeyboard.PressKeyDown(100);
            InputKeyboard.PressKeyLeft(400);
            InputKeyboard.PressKeyDown(100);
            break;
        }
        InputKeyboard.PressKeyDown(800);
        InputKeyboard.PressKeyRight(750);
        InputKeyboard.PressKeyDown(200);
        this.UseCut();
        switch (RandomNumber.Between(1, 3))
        {
          case 1:
            InputKeyboard.PressKeyDown(100);
            num = 100;
            break;
          case 2:
            InputKeyboard.PressKeyDown(200);
            num = 200;
            break;
          case 3:
            InputKeyboard.PressKeyDown(300);
            num = 300;
            break;
        }
        switch (RandomNumber.Between(1, 3))
        {
          case 1:
            InputKeyboard.PressKeyLeft(RandomNumber.Between(160 /*0xA0*/, 500));
            break;
          case 2:
            InputKeyboard.PressKeyRight(160 /*0xA0*/);
            break;
        }
        InputKeyboard.PressKeyDown(1950 - num);
        switch (RandomNumber.Between(1, 4))
        {
          case 1:
            InputKeyboard.PressKeyDown(50);
            return;
          case 2:
            InputKeyboard.PressKeyLeft(RandomNumber.Between(160 /*0xA0*/, 400));
            return;
          case 3:
            InputKeyboard.PressKeyRight(RandomNumber.Between(160 /*0xA0*/, 400));
            return;
          case 4:
            return;
          default:
            return;
        }
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Kanto_Virdian_City(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        this.UseBike();
        InputKeyboard.PressKeyLeft(800);
        this.UseSurfer();
        break;
      case "goback":
        InputKeyboard.PressKeyRight(900);
        InputKeyboard.PressKeyUp(2000);
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Kanto_Cinnabar_Island(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyDown(500);
        this.UseSurfer();
        break;
      case "goback":
        this.UseTeleport();
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }

  public void Kanto_Cinnabar_Island_GoBack()
  {
    InputKeyboard.PressKeyUp(700);
    InputKeyboard.PressKeyUp(2000);
  }

  public void Kanto_Vermilion_City(string action)
  {
    switch (action)
    {
      case "goto":
        InputKeyboard.PressKeyDown(1450);
        this.WaitAfterEntrace();
        InputKeyboard.PressKeyDown(800);
        this.UseSurfer();
        break;
      case "goback":
        InputKeyboard.PressKeyUp(900);
        InputKeyboard.PressKeyUp(2000);
        break;
      case "teleportback":
        this.UseTeleport();
        break;
    }
  }
}
