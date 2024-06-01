using System;
using System.Numerics;
using System.Security.Cryptography;

namespace RadProject;

// making a public static class to generate random numbers

public static class RandomGenerator
{
    // making a static random number generator
    private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();

    // making a method to generate a random number
    public static BigInteger Generate89bitRandomNumber(){

        // 12 bytes is 96 bits
        byte[] bytes = new byte[12];

        // filling the byte array with random numbers
        rng.GetBytes(bytes);

        // currently it is 96 bits, so we need to mask it to a 89 bit number
        BigInteger randomnumber = new BigInteger(bytes);

        BigInteger mask = BigInteger.One << (7); // 96 - 89 = 7

        randomnumber &= ~mask;

        return randomnumber;
    }

    public static ulong Generate64bitRandomNumber(){

        // 8 bytes is 64 bits
        byte[] bytes = new byte[8];

        // filling the byte array with random numbers
        rng.GetBytes(bytes);

        // converting the byte array to a ulong, since it cant be BigInt/BigInteger for part a
        ulong randomnumber = BitConverter.ToUInt64(bytes, 0);

        return randomnumber;
    }
}