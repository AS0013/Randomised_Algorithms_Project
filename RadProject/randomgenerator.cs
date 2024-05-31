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
    public static BigInteger GenerateRandomNumber()
    {
        // 12 bytes is 96 bits
        byte[] bytes = new byte[12];
        // filling the byte array with random numbers
        rng.GetBytes(bytes);

        // currently it is 96 bits, so we need to mask it to a 89 bit number

        // bytes[11] &= 0x01; // masking the most significant bit to 0


        BigInteger randomnumber = new BigInteger(bytes);


        randomnumber >>= 7;



        return randomnumber;
    }
}