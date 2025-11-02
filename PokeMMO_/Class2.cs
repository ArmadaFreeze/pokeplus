// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Class2
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Classes;
using System.Security.Cryptography;
using System.Text;

namespace PokeMMO_
{
    internal class Class2
    {
      public static string randomTitle()
      {
        char[] chArray = new char[62];
        char[] charArray = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();
        byte[] data = new byte[80 /*0x50*/];
        cryptoServiceProvider.GetNonZeroBytes(data);
        StringBuilder stringBuilder1 = new StringBuilder(80 /*0x50*/);
        foreach (byte num in data)
          stringBuilder1.Append(charArray[(int) num % (charArray.Length - 1)]);
        StringBuilder stringBuilder2 = stringBuilder1;
        string str;
        return stringBuilder2 == null || (str = stringBuilder2.ToString()) == null ? "" : str.Substring(RandomNumber.Between(68, 70));
      }
    }
}
