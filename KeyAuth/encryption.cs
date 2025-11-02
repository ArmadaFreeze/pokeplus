// Decompiled with JetBrains decompiler
// Type: KeyAuth.encryption
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace KeyAuth;

public static class encryption
{
  [DllImport("kernel32.dll", SetLastError = true)]
  private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

  [DllImport("kernel32.dll", SetLastError = true)]
  private static extern IntPtr GetCurrentProcess();

  public static string HashHMAC(string enckey, string resp)
  {
    return encryption.byte_arr_to_str(new HMACSHA256(Encoding.UTF8.GetBytes(enckey)).ComputeHash(Encoding.UTF8.GetBytes(resp)));
  }

  public static string byte_arr_to_str(byte[] ba)
  {
    StringBuilder stringBuilder = new StringBuilder(ba.Length * 2);
    foreach (byte num in ba)
      stringBuilder.AppendFormat("{0:x2}", (object) num);
    return stringBuilder.ToString();
  }

  public static byte[] str_to_byte_arr(string hex)
  {
    try
    {
      int length = hex.Length;
      byte[] byteArr = new byte[length / 2];
      for (int startIndex = 0; startIndex < length; startIndex += 2)
        byteArr[startIndex / 2] = Convert.ToByte(hex.Substring(startIndex, 2), 16 /*0x10*/);
      return byteArr;
    }
    catch
    {
      api.error("The session has ended, open program again.");
      encryption.TerminateProcess(encryption.GetCurrentProcess(), 1U);
      return (byte[]) null;
    }
  }

  public static string iv_key() => Guid.NewGuid().ToString().Substring(0, 16 /*0x10*/);
}
