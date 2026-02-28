using System;
using System.Security.Cryptography;

namespace PokeMMO_.Classes;

public class RandomNumber
{
	private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

	public static int Between(int minimumValue, int maximumValue)
	{
		byte[] array = new byte[4];
		_generator.GetBytes(array);
		int num = BitConverter.ToInt32(array, 0) & 0x7FFFFFFF;
		int num2 = maximumValue - minimumValue + 1;
		return minimumValue + num % num2;
	}
}
