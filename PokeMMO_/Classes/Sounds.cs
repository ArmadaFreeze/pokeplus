// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.Sounds
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Botting;
using System.Media;

#nullable disable
namespace PokeMMO_.Classes;

public class Sounds
{
  public static async void PlayShinySound()
  {
    SoundPlayer Sound = new SoundPlayer("bin/snd/Shiny.wav");
    Sound.Play();
    await Bot.Instance.AsyncSleep(2500);
    Sound = (SoundPlayer) null;
  }

  public static async void PlayAlertSound()
  {
    SoundPlayer Sound = new SoundPlayer("bin/snd/Alert.wav");
    Sound.Play();
    await Bot.Instance.AsyncSleep(2500);
    Sound = (SoundPlayer) null;
  }

  public static async void PlayPMSound()
  {
    SoundPlayer Sound = new SoundPlayer("bin/snd/PM.wav");
    Sound.Play();
    await Bot.Instance.AsyncSleep(2500);
    Sound = (SoundPlayer) null;
  }

  public static async void PlayNotificationSound()
  {
    SoundPlayer Sound = new SoundPlayer("bin/snd/Notification.wav");
    Sound.Play();
    await Bot.Instance.AsyncSleep(2500);
    Sound = (SoundPlayer) null;
  }
}
