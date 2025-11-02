// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.Search
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Botting;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;

#nullable disable
namespace PokeMMO_.Classes;

public class Search
{
  private string filename = "";

  [DllImport("Search.dll")]
  public static extern IntPtr ImageSearch(int x, int y, int right, int bottom, [MarshalAs(UnmanagedType.LPStr)] string imagePath);

  public int[] UseImageSearch(string path, string tolerance)
  {
    string str = "0";
    int num = 8;
    this.filename = path.Substring(8).Replace(".png", "");
    if (this.filename.Contains("mon/"))
      this.filename.Replace("mon/", "");
    Bitmap bitmap = new Bitmap(path);
    path = $"*{tolerance} {path}";
    if (BotSettings.Settings.ResolutionMode != ResolutionMode.HD)
    {
      if (BotSettings.Settings.ResolutionMode == ResolutionMode.SD)
      {
        if ((this.filename.Equals("Captcha") ? 1 : (this.filename.Equals("Captcha2") ? 1 : 0)) != 0)
          str = Marshal.PtrToStringAnsi(Search.ImageSearch(323, 205 - num, 956, 496, path));
        else if ((this.filename.Equals("Potion") || this.filename.Equals("SuperPotion") || this.filename.Equals("HyperPotion") || this.filename.Equals("PotionH") || this.filename.Equals("SuperPotionH") || this.filename.Equals("HyperPotionH") ? 1 : (this.filename.Equals(Bot.Instance.Settings.ChosenPokeBall.ToString()) ? 1 : 0)) != 0)
          str = Marshal.PtrToStringAnsi(Search.ImageSearch(190, 277, 495, 528 + num, path));
        else if ((this.filename.Equals("Battle") ? 1 : (this.filename.Equals("Safari") ? 1 : 0)) != 0)
          str = Marshal.PtrToStringAnsi(Search.ImageSearch(970, 418 - num, 1087, 470, path));
        else if ((this.filename.Equals("Repel") || this.filename.Equals("TimeOver") ? 1 : (this.filename.Equals("Lure") ? 1 : 0)) == 0)
        {
          if ((this.filename.Equals("CantRun") ? 1 : (this.filename.Equals("NoEffect") ? 1 : 0)) == 0)
          {
            if ((this.filename.Equals("Fight") || this.filename.Equals("Run") || this.filename.Equals("Bag") || this.filename.Equals("SafariC") || this.filename.Contains("0PPS") || this.filename.Contains("0PPE") || this.filename.Equals("Super") ? 1 : (this.filename.Equals("Effective") ? 1 : 0)) == 0)
            {
              if ((this.filename.Equals("ZeroPP") ? 1 : (this.filename.Equals("ZeroPPH") ? 1 : 0)) == 0)
              {
                if (this.filename.Equals("Item"))
                {
                  str = Marshal.PtrToStringAnsi(Search.ImageSearch(181, 364 - num, 211, 394, path));
                  if (str[0] != '0')
                    Bot.Instance.Status.DetectedItem = true;
                }
                else
                  str = (this.filename.Equals("FalseSwipe") || this.filename.Equals("Spore") || this.filename.Equals("Substitute") || this.filename.Equals("Assist") ? 1 : (this.filename.Equals("Struggle") ? 1 : 0)) == 0 ? ((this.filename.Equals("Cancel") ? 1 : (this.filename.Equals("Back") ? 1 : 0)) == 0 ? (this.filename.Contains("NextPoke") ? Marshal.PtrToStringAnsi(Search.ImageSearch(200, 420 - num, 830, 550, path)) : (this.filename.Equals("Level") ? Marshal.PtrToStringAnsi(Search.ImageSearch(265, 482 - num, 345, 530, path)) : ((this.filename.Equals("Shiny") ? 1 : (Enum.IsDefined(typeof (Pokemon), (object) this.filename) ? 1 : 0)) != 0 ? Marshal.PtrToStringAnsi(Search.ImageSearch(223, 81 - num, 1024 /*0x0400*/, 253, path)) : (this.filename.Contains("Horde") ? Marshal.PtrToStringAnsi(Search.ImageSearch(223, 81 - num, 1024 /*0x0400*/, 253, path)) : ((this.filename.Equals("Ball") ? 1 : (this.filename.Equals("Medicine") ? 1 : 0)) == 0 ? (this.filename.Equals("Login") ? Marshal.PtrToStringAnsi(Search.ImageSearch(685, 265 - num, 800, 500, path)) : (this.filename.Equals("Character") ? Marshal.PtrToStringAnsi(Search.ImageSearch(330, 210 - num, 960, 240 /*0xF0*/, path)) : (this.filename.Equals("PM") ? Marshal.PtrToStringAnsi(Search.ImageSearch(463, 219 - num, 484, 262, path)) : (this.filename.Equals("PM2") ? Marshal.PtrToStringAnsi(Search.ImageSearch(0, 529, 419, 671 + num, path)) : (!this.filename.Equals("LearnMove") ? (!this.filename.Equals("Skip") ? ((this.filename.Equals("HPOrange") ? 1 : (this.filename.Equals("HPRed") ? 1 : 0)) != 0 ? Marshal.PtrToStringAnsi(Search.ImageSearch(866, 292 - num, 1087, 323, path)) : (this.filename.Equals("Sleep") ? Marshal.PtrToStringAnsi(Search.ImageSearch(189, 143 - num, 251, 203, path)) : (!this.filename.Equals("Catched") ? ((this.filename.Equals("DC") ? 1 : (this.filename.Equals("DCLogin") ? 1 : 0)) == 0 ? (!this.filename.Equals("Session") ? ((this.filename.Equals("Evolve") ? 1 : (this.filename.Equals("Disable") ? 1 : 0)) == 0 ? (!this.filename.Equals("Stats") ? ((this.filename.Equals("StatsIV") ? 1 : (this.filename.Equals("31") ? 1 : 0)) != 0 ? Marshal.PtrToStringAnsi(Search.ImageSearch(379, 184 - num, 901, 516, path)) : (this.filename.Equals("Release") ? Marshal.PtrToStringAnsi(Search.ImageSearch(562, 197 - num, 613, 224 /*0xE0*/, path)) : (!this.filename.Equals("CRelease") ? ((this.filename.Equals("Error1") || this.filename.Equals("Error2") || this.filename.Equals("Error3") ? 1 : (this.filename.Equals("GTLNoMoney") ? 1 : 0)) != 0 ? Marshal.PtrToStringAnsi(Search.ImageSearch(560, 534 - num, 719, 577, path)) : Marshal.PtrToStringAnsi(Search.ImageSearch(0, 0, 1280 /*0x0500*/, 680, path))) : Marshal.PtrToStringAnsi(Search.ImageSearch(404, 349 - num, 594, 383, path))))) : Marshal.PtrToStringAnsi(Search.ImageSearch(379, 184 - num, 901, 516, path))) : Marshal.PtrToStringAnsi(Search.ImageSearch(190, 473 - num, 985, 533, path))) : Marshal.PtrToStringAnsi(Search.ImageSearch(490, 317 - num, 790, 333, path))) : Marshal.PtrToStringAnsi(Search.ImageSearch(443, 312 - num, 831, 343, path))) : Marshal.PtrToStringAnsi(Search.ImageSearch(223, 148 - num, 519, 170, path))))) : Marshal.PtrToStringAnsi(Search.ImageSearch(892, 104 - num, 940, 240 /*0xF0*/, path))) : Marshal.PtrToStringAnsi(Search.ImageSearch(750, 415 - num, 850, 455, path))))))) : Marshal.PtrToStringAnsi(Search.ImageSearch(345, 290, 390, 310 + num, path))))))) : Marshal.PtrToStringAnsi(Search.ImageSearch(985, 490 - num, 1085, 550, path))) : Marshal.PtrToStringAnsi(Search.ImageSearch(200, 425 - num, 620, 530, path));
              }
              else
              {
                Bot.Instance.Status.FirstMovePP0 = Marshal.PtrToStringAnsi(Search.ImageSearch(326, 452 - num, 379, 467, path))[0] != '0';
                Bot.Instance.Status.SecondMovePP0 = Marshal.PtrToStringAnsi(Search.ImageSearch(538, 452 - num, 591, 467, path))[0] != '0';
                Bot.Instance.Status.ThirdMovePP0 = Marshal.PtrToStringAnsi(Search.ImageSearch(326, 506 - num, 379, 521, path))[0] != '0';
                str = Marshal.PtrToStringAnsi(Search.ImageSearch(538, 506 - num, 591, 521, path));
                Bot.Instance.Status.FourthMovePP0 = str[0] != '0';
                if ((Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 ? 1 : (Bot.Instance.Status.FourthMovePP0 ? 1 : 0)) != 0)
                  Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: One of 4 Moves has 0PP"));
              }
            }
            else
              str = Marshal.PtrToStringAnsi(Search.ImageSearch(200, 425 - num, 620, 530, path));
          }
          else
            str = Marshal.PtrToStringAnsi(Search.ImageSearch(205, 430, 437, 455 + num, path));
        }
        else
          str = Marshal.PtrToStringAnsi(Search.ImageSearch(365, 60 - num, 700, 90, path));
      }
    }
    else if ((this.filename.Equals("Captcha") ? 1 : (this.filename.Equals("Captcha2") ? 1 : 0)) != 0)
      str = Marshal.PtrToStringAnsi(Search.ImageSearch(640, 382 - num, 1278, 678, path));
    else if ((this.filename.Equals("Potion") || this.filename.Equals("SuperPotion") || this.filename.Equals("HyperPotion") || this.filename.Equals("PotionH") || this.filename.Equals("SuperPotionH") || this.filename.Equals("HyperPotionH") ? 1 : (this.filename.Equals(Bot.Instance.Settings.ChosenPokeBall.ToString()) ? 1 : 0)) == 0)
    {
      if ((this.filename.Equals("Battle") ? 1 : (this.filename.Equals("Safari") ? 1 : 0)) != 0)
        str = Marshal.PtrToStringAnsi(Search.ImageSearch(1465, 670 - num, 1631, 724, path));
      else if ((this.filename.Equals("Repel") || this.filename.Equals("TimeOver") ? 1 : (this.filename.Equals("Lure") ? 1 : 0)) == 0)
      {
        if ((this.filename.Equals("CantRun") ? 1 : (this.filename.Equals("NoEffect") ? 1 : 0)) != 0)
          str = Marshal.PtrToStringAnsi(Search.ImageSearch(300, 680, 532, 707 + num, path));
        else if ((this.filename.Equals("Fight") || this.filename.Equals("Run") || this.filename.Equals("Bag") || this.filename.Equals("SafariC") || this.filename.Contains("0PPS") || this.filename.Contains("0PPE") || this.filename.Equals("Super") ? 1 : (this.filename.Equals("Effective") ? 1 : 0)) == 0)
        {
          if ((this.filename.Equals("ZeroPP") ? 1 : (this.filename.Equals("ZeroPPH") ? 1 : 0)) != 0)
          {
            Bot.Instance.Status.FirstMovePP0 = Marshal.PtrToStringAnsi(Search.ImageSearch(422, 704 - num, 475, 719, path))[0] != '0';
            Bot.Instance.Status.SecondMovePP0 = Marshal.PtrToStringAnsi(Search.ImageSearch(634, 704 - num, 687, 719, path))[0] != '0';
            Bot.Instance.Status.ThirdMovePP0 = Marshal.PtrToStringAnsi(Search.ImageSearch(422, 758 - num, 475, 773, path))[0] != '0';
            str = Marshal.PtrToStringAnsi(Search.ImageSearch(634, 758 - num, 687, 773, path));
            Bot.Instance.Status.FourthMovePP0 = str[0] != '0';
            if ((Bot.Instance.Status.FirstMovePP0 || Bot.Instance.Status.SecondMovePP0 || Bot.Instance.Status.ThirdMovePP0 ? 1 : (Bot.Instance.Status.FourthMovePP0 ? 1 : 0)) != 0)
              Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: One of 4 Moves has 0PP"));
          }
          else if (this.filename.Equals("Item"))
          {
            str = Marshal.PtrToStringAnsi(Search.ImageSearch(280, 620 - num, 307, 644, path));
            if (str[0] != '0')
              Bot.Instance.Status.DetectedItem = true;
          }
          else
            str = (this.filename.Equals("FalseSwipe") || this.filename.Equals("Spore") || this.filename.Equals("Substitute") || this.filename.Equals("Assist") ? 1 : (this.filename.Equals("Struggle") ? 1 : 0)) == 0 ? ((this.filename.Equals("Cancel") ? 1 : (this.filename.Equals("Back") ? 1 : 0)) != 0 ? Marshal.PtrToStringAnsi(Search.ImageSearch(1530, 740 - num, 1625, 800, path)) : (!this.filename.Contains("NextPoke") ? (!this.filename.Equals("Level") ? ((this.filename.Equals("Shiny") ? 1 : (Enum.IsDefined(typeof (Pokemon), (object) this.filename) ? 1 : 0)) != 0 ? Marshal.PtrToStringAnsi(Search.ImageSearch(316, 79 - num, 1359, 285, path)) : (this.filename.Contains("Horde") ? Marshal.PtrToStringAnsi(Search.ImageSearch(316, 79 - num, 1359, 285, path)) : ((this.filename.Equals("Ball") ? 1 : (this.filename.Equals("Medicine") ? 1 : 0)) == 0 ? (this.filename.Equals("Login") ? Marshal.PtrToStringAnsi(Search.ImageSearch(1010, 445 - num, 1095, 610, path)) : (this.filename.Equals("Character") ? Marshal.PtrToStringAnsi(Search.ImageSearch(650, 390 - num, 1200, 420, path)) : (this.filename.Equals("PM") ? Marshal.PtrToStringAnsi(Search.ImageSearch(780, 395 - num, 805, 442, path)) : (!this.filename.Equals("PM2") ? (!this.filename.Equals("LearnMove") ? (!this.filename.Equals("Skip") ? ((this.filename.Equals("HPOrange") ? 1 : (this.filename.Equals("HPRed") ? 1 : 0)) != 0 ? Marshal.PtrToStringAnsi(Search.ImageSearch(1410, 543 - num, 1632, 574, path)) : (!this.filename.Equals("Sleep") ? (!this.filename.Equals("Catched") ? ((this.filename.Equals("DC") ? 1 : (this.filename.Equals("DCLogin") ? 1 : 0)) != 0 ? Marshal.PtrToStringAnsi(Search.ImageSearch(770, 505 - num, 1149, 522, path)) : (!this.filename.Equals("Session") ? ((this.filename.Equals("Evolve") ? 1 : (this.filename.Equals("Disable") ? 1 : 0)) != 0 ? Marshal.PtrToStringAnsi(Search.ImageSearch(286, 733 - num, 1529, 786, path)) : (this.filename.Equals("Stats") ? Marshal.PtrToStringAnsi(Search.ImageSearch(743, 337 - num, 1265, 671, path)) : ((this.filename.Equals("StatsIV") ? 1 : (this.filename.Equals("31") ? 1 : 0)) != 0 ? Marshal.PtrToStringAnsi(Search.ImageSearch(743, 337 - num, 1265, 671, path)) : (this.filename.Equals("Release") ? Marshal.PtrToStringAnsi(Search.ImageSearch(885, 377 - num, 931, 403, path)) : (this.filename.Equals("CRelease") ? Marshal.PtrToStringAnsi(Search.ImageSearch(722, 527 - num, 913, 564, path)) : ((this.filename.Equals("Error1") || this.filename.Equals("Error2") || this.filename.Equals("Error3") ? 1 : (this.filename.Equals("GTLNoMoney") ? 1 : 0)) != 0 ? Marshal.PtrToStringAnsi(Search.ImageSearch(880, 894 - num, 1039, 937, path)) : Marshal.PtrToStringAnsi(Search.ImageSearch(0, 0, 1920, 1040, path)))))))) : Marshal.PtrToStringAnsi(Search.ImageSearch(807, 498 - num, 1112, 515, path)))) : Marshal.PtrToStringAnsi(Search.ImageSearch(316, 151 - num, 614, 170, path))) : Marshal.PtrToStringAnsi(Search.ImageSearch(278, 145 - num, 342, 199, path)))) : Marshal.PtrToStringAnsi(Search.ImageSearch(1330, 170 - num, 1386, 254, path))) : Marshal.PtrToStringAnsi(Search.ImageSearch(845, 666 - num, 950, 690, path))) : Marshal.PtrToStringAnsi(Search.ImageSearch(0, 889, 419, 1031 + num, path)))))) : Marshal.PtrToStringAnsi(Search.ImageSearch(440, 540, 485, 565 + num, path))))) : Marshal.PtrToStringAnsi(Search.ImageSearch(355, 731 - num, 440, 780, path))) : Marshal.PtrToStringAnsi(Search.ImageSearch(295, 670 - num, 925, 800, path)))) : Marshal.PtrToStringAnsi(Search.ImageSearch(295, 675 - num, 715, 781, path));
        }
        else
          str = Marshal.PtrToStringAnsi(Search.ImageSearch(295, 675 - num, 715, 781, path));
      }
      else
        str = Marshal.PtrToStringAnsi(Search.ImageSearch(550, 135 - num, 900, 165, path));
    }
    else
      str = Marshal.PtrToStringAnsi(Search.ImageSearch(286, 529, 591, 780 + num, path));
    int[] numArray1;
    if (str[0] == '0')
    {
      numArray1 = (int[]) null;
    }
    else
    {
      Application.Current.Dispatcher.Invoke((Action) (() => SubViewModel.Instance.Status = "Status: Detected " + this.filename));
      string[] strArray = str.Split('|');
      int[] numArray2 = new int[3];
      int[] numArray3 = new int[3];
      int.TryParse(strArray[1], out int _);
      int.TryParse(strArray[2], out int _);
      numArray2[1] = Convert.ToInt32(strArray[1]);
      numArray2[2] = Convert.ToInt32(strArray[2]);
      int width = bitmap.Width;
      int height = bitmap.Height;
      numArray3[1] = RandomNumber.Between(numArray2[1] + 3, numArray2[1] + width - 3);
      numArray3[2] = RandomNumber.Between(numArray2[2] + 3, numArray2[2] + height - 3);
      numArray1 = numArray3;
    }
    return numArray1;
  }
}
