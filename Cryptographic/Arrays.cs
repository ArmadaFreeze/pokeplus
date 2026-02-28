using System;

namespace Cryptographic;

internal static class Arrays
{
	public static byte[] CopyOfRange(byte[] original, int from, int to)
	{
		int num = to - from;
		byte[] array = new byte[num];
		Array.Copy(original, from, array, 0, num);
		return array;
	}
}
