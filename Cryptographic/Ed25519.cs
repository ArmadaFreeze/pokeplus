using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace Cryptographic;

public class Ed25519
{
	private static readonly Dictionary<BigInteger, BigInteger> InverseCache = new Dictionary<BigInteger, BigInteger>();

	private const int BitLength = 256;

	private static readonly BigInteger TwoPowBitLengthMinusTwo = BigInteger.Pow(2, 254);

	private static readonly BigInteger[] TwoPowCache = (from i in Enumerable.Range(0, 512)
		select BigInteger.Pow(2, i)).ToArray();

	private static readonly BigInteger Q = BigInteger.Parse("57896044618658097711785492504343953926634992332820282019728792003956564819949");

	private static readonly BigInteger Qm2 = BigInteger.Parse("57896044618658097711785492504343953926634992332820282019728792003956564819947");

	private static readonly BigInteger Qp3 = BigInteger.Parse("57896044618658097711785492504343953926634992332820282019728792003956564819952");

	private static readonly BigInteger L = BigInteger.Parse("7237005577332262213973186563042994240857116359379907606001950938285454250989");

	private static readonly BigInteger D = BigInteger.Parse("-4513249062541557337682894930092624173785641285191125241628941591882900924598840740");

	private static readonly BigInteger I = BigInteger.Parse("19681161376707505956807079304988542015446066515923890162744021073123829784752");

	private static readonly BigInteger By = BigInteger.Parse("46316835694926478169428394003475163141307993866256225615783033603165251855960");

	private static readonly BigInteger Bx = BigInteger.Parse("15112221349535400772501151409588531511454012693041857206046113283949847762202");

	private static readonly Tuple<BigInteger, BigInteger> B = new Tuple<BigInteger, BigInteger>(Bx.Mod(Q), By.Mod(Q));

	private static readonly BigInteger Un = BigInteger.Parse("57896044618658097711785492504343953926634992332820282019728792003956564819967");

	private static readonly BigInteger Two = new BigInteger(2);

	private static readonly BigInteger Eight = new BigInteger(8);

	private static byte[] ComputeHash(byte[] m)
	{
		using SHA512 sHA = SHA512.Create();
		return sHA.ComputeHash(m);
	}

	private static BigInteger ExpMod(BigInteger number, BigInteger exponent, BigInteger modulo)
	{
		BigInteger bigInteger = BigInteger.One;
		BigInteger bigInteger2 = number.Mod(modulo);
		while (exponent > 0L)
		{
			if (!exponent.IsEven)
			{
				bigInteger = (bigInteger * bigInteger2).Mod(modulo);
			}
			bigInteger2 = (bigInteger2 * bigInteger2).Mod(modulo);
			exponent /= (BigInteger)2;
		}
		return bigInteger;
	}

	private static BigInteger Inv(BigInteger x)
	{
		if (!InverseCache.ContainsKey(x))
		{
			InverseCache[x] = ExpMod(x, Qm2, Q);
		}
		return InverseCache[x];
	}

	private static BigInteger RecoverX(BigInteger y)
	{
		BigInteger bigInteger = y * y;
		BigInteger bigInteger2 = (bigInteger - 1) * Inv(D * bigInteger + 1);
		BigInteger bigInteger3 = ExpMod(bigInteger2, Qp3 / Eight, Q);
		if (!(bigInteger3 * bigInteger3 - bigInteger2).Mod(Q).Equals(BigInteger.Zero))
		{
			bigInteger3 = (bigInteger3 * I).Mod(Q);
		}
		if (!bigInteger3.IsEven)
		{
			bigInteger3 = Q - bigInteger3;
		}
		return bigInteger3;
	}

	private static Tuple<BigInteger, BigInteger> Edwards(BigInteger px, BigInteger py, BigInteger qx, BigInteger qy)
	{
		BigInteger bigInteger = px * qx;
		BigInteger bigInteger2 = py * qy;
		BigInteger bigInteger3 = D * bigInteger * bigInteger2;
		BigInteger num = (px * qy + qx * py) * Inv(1 + bigInteger3);
		BigInteger num2 = (py * qy + bigInteger) * Inv(1 - bigInteger3);
		return new Tuple<BigInteger, BigInteger>(num.Mod(Q), num2.Mod(Q));
	}

	private static Tuple<BigInteger, BigInteger> EdwardsSquare(BigInteger x, BigInteger y)
	{
		BigInteger bigInteger = x * x;
		BigInteger bigInteger2 = y * y;
		BigInteger bigInteger3 = D * bigInteger * bigInteger2;
		BigInteger num = 2 * x * y * Inv(1 + bigInteger3);
		BigInteger num2 = (bigInteger2 + bigInteger) * Inv(1 - bigInteger3);
		return new Tuple<BigInteger, BigInteger>(num.Mod(Q), num2.Mod(Q));
	}

	private static Tuple<BigInteger, BigInteger> ScalarMul(Tuple<BigInteger, BigInteger> point, BigInteger scalar)
	{
		Tuple<BigInteger, BigInteger> tuple = new Tuple<BigInteger, BigInteger>(BigInteger.Zero, BigInteger.One);
		Tuple<BigInteger, BigInteger> tuple2 = point;
		while (scalar > 0L)
		{
			if (!scalar.IsEven)
			{
				tuple = Edwards(tuple.Item1, tuple.Item2, tuple2.Item1, tuple2.Item2);
			}
			tuple2 = EdwardsSquare(tuple2.Item1, tuple2.Item2);
			scalar >>= 1;
		}
		return tuple;
	}

	public static byte[] EncodeInt(BigInteger y)
	{
		byte[] array = y.ToByteArray();
		byte[] array2 = new byte[Math.Max(array.Length, 32)];
		Array.Copy(array, array2, array.Length);
		return array2;
	}

	public static byte[] EncodePoint(BigInteger x, BigInteger y)
	{
		byte[] array = EncodeInt(y);
		array[array.Length - 1] |= (byte)((!x.IsEven) ? 128 : 0);
		return array;
	}

	private static int GetBit(byte[] h, int i)
	{
		return (h[i / 8] >> i % 8) & 1;
	}

	public static byte[] PublicKey(byte[] signingKey)
	{
		byte[] h = ComputeHash(signingKey);
		BigInteger twoPowBitLengthMinusTwo = TwoPowBitLengthMinusTwo;
		for (int i = 3; i < 254; i++)
		{
			if (GetBit(h, i) != 0)
			{
				twoPowBitLengthMinusTwo += TwoPowCache[i];
			}
		}
		Tuple<BigInteger, BigInteger> tuple = ScalarMul(B, twoPowBitLengthMinusTwo);
		return EncodePoint(tuple.Item1, tuple.Item2);
	}

	private static BigInteger HashInt(byte[] m)
	{
		byte[] h = ComputeHash(m);
		BigInteger zero = BigInteger.Zero;
		for (int i = 0; i < 512; i++)
		{
			if (GetBit(h, i) != 0)
			{
				zero += TwoPowCache[i];
			}
		}
		return zero;
	}

	public static byte[] Signature(byte[] message, byte[] signingKey, byte[] publicKey)
	{
		byte[] array = ComputeHash(signingKey);
		BigInteger twoPowBitLengthMinusTwo = TwoPowBitLengthMinusTwo;
		for (int i = 3; i < 254; i++)
		{
			if (GetBit(array, i) != 0)
			{
				twoPowBitLengthMinusTwo += TwoPowCache[i];
			}
		}
		BigInteger bigInteger;
		using (MemoryStream memoryStream = new MemoryStream(32 + message.Length))
		{
			memoryStream.Write(array, 32, 32);
			memoryStream.Write(message, 0, message.Length);
			bigInteger = HashInt(memoryStream.ToArray());
		}
		Tuple<BigInteger, BigInteger> tuple = ScalarMul(B, bigInteger);
		byte[] array2 = EncodePoint(tuple.Item1, tuple.Item2);
		BigInteger y;
		using (MemoryStream memoryStream2 = new MemoryStream(32 + publicKey.Length + message.Length))
		{
			memoryStream2.Write(array2, 0, array2.Length);
			memoryStream2.Write(publicKey, 0, publicKey.Length);
			memoryStream2.Write(message, 0, message.Length);
			y = (bigInteger + HashInt(memoryStream2.ToArray()) * twoPowBitLengthMinusTwo).Mod(L);
		}
		using MemoryStream memoryStream3 = new MemoryStream(64);
		memoryStream3.Write(array2, 0, array2.Length);
		byte[] array3 = EncodeInt(y);
		memoryStream3.Write(array3, 0, array3.Length);
		return memoryStream3.ToArray();
	}

	private static bool IsOnCurve(BigInteger x, BigInteger y)
	{
		BigInteger bigInteger = x * x;
		BigInteger bigInteger2 = y * y;
		BigInteger bigInteger3 = D * bigInteger2 * bigInteger;
		return (bigInteger2 - bigInteger - bigInteger3 - 1).Mod(Q).Equals(BigInteger.Zero);
	}

	private static BigInteger DecodeInt(byte[] s)
	{
		return new BigInteger(s) & Un;
	}

	private static Tuple<BigInteger, BigInteger> DecodePoint(byte[] pointBytes)
	{
		BigInteger bigInteger = new BigInteger(pointBytes) & Un;
		BigInteger bigInteger2 = RecoverX(bigInteger);
		if (((!bigInteger2.IsEven) ? 1 : 0) != GetBit(pointBytes, 255))
		{
			bigInteger2 = Q - bigInteger2;
		}
		Tuple<BigInteger, BigInteger> result = new Tuple<BigInteger, BigInteger>(bigInteger2, bigInteger);
		if (!IsOnCurve(bigInteger2, bigInteger))
		{
			throw new ArgumentException("Decoding point that is not on curve");
		}
		return result;
	}

	public static bool CheckValid(byte[] signature, byte[] message, byte[] publicKey)
	{
		Console.Write(".");
		if (signature.Length != 64)
		{
			throw new ArgumentException("Signature length is wrong");
		}
		if (publicKey.Length != 32)
		{
			throw new ArgumentException("Public key length is wrong");
		}
		byte[] pointBytes = Arrays.CopyOfRange(signature, 0, 32);
		Tuple<BigInteger, BigInteger> tuple = DecodePoint(pointBytes);
		Tuple<BigInteger, BigInteger> point = DecodePoint(publicKey);
		byte[] s = Arrays.CopyOfRange(signature, 32, 64);
		BigInteger scalar = DecodeInt(s);
		BigInteger scalar2;
		using (MemoryStream memoryStream = new MemoryStream(32 + publicKey.Length + message.Length))
		{
			byte[] array = EncodePoint(tuple.Item1, tuple.Item2);
			memoryStream.Write(array, 0, array.Length);
			memoryStream.Write(publicKey, 0, publicKey.Length);
			memoryStream.Write(message, 0, message.Length);
			scalar2 = HashInt(memoryStream.ToArray());
		}
		Console.Write(".");
		Tuple<BigInteger, BigInteger> tuple2 = ScalarMul(B, scalar);
		Console.Write(".");
		Tuple<BigInteger, BigInteger> tuple3 = ScalarMul(point, scalar2);
		Tuple<BigInteger, BigInteger> tuple4 = Edwards(tuple.Item1, tuple.Item2, tuple3.Item1, tuple3.Item2);
		if (!tuple2.Item1.Equals(tuple4.Item1) || !tuple2.Item2.Equals(tuple4.Item2))
		{
			return false;
		}
		return true;
	}
}
