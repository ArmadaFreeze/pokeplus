using System.Security.Cryptography;
using System.Text;

namespace PokeMMO_;

internal static class RandomTitle
{
	public static string Generate()
	{
		byte[] array = new byte[12];
		using (RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider())
		{
			rNGCryptoServiceProvider.GetNonZeroBytes(array);
		}
		StringBuilder stringBuilder = new StringBuilder(12);
		byte[] array2 = array;
		foreach (byte b in array2)
		{
			stringBuilder.Append("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"[(int)b % "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".Length]);
		}
		return stringBuilder.ToString();
	}
}
