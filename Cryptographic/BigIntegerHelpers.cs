using System.Numerics;

namespace Cryptographic;

internal static class BigIntegerHelpers
{
	public static BigInteger Mod(this BigInteger num, BigInteger modulo)
	{
		BigInteger bigInteger = num % modulo;
		return (bigInteger < 0L) ? (bigInteger + modulo) : bigInteger;
	}
}
