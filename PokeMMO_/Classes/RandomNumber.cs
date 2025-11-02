// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.RandomNumber
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System;
using System.Security.Cryptography;

#nullable disable
namespace PokeMMO_.Classes;

public class RandomNumber
{
  private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

  public static int Between(int minimumValue, int maximumValue)
  {
    byte[] data = new byte[1];
    RandomNumber._generator.GetBytes(data);
    double num = Math.Floor(Math.Max(0.0, Convert.ToDouble(data[0]) / (double) byte.MaxValue - 1E-11) * (double) (maximumValue - minimumValue + 1));
    return (int) ((double) minimumValue + num);
  }
}
