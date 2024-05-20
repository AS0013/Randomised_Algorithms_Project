using System.Collections;
using System.Numerics;

namespace RadProject;
public class MulShiftHash {
    readonly Hashtable hashtable = new();
    
    public short MultiplyShift(long x)
    {
        long a = -4347435114166313097; // Generated using https://www.random.org/bytes/
        int l = 16; // hashing to short (16-bit int)
        short hash = (short)Math.Floor(
            (decimal)((int)Math.BigMul(a, x, out long low)>>(64 - l))
        );
        return hash;
    }
    public long MultiplyModPrime(long x){
        BigInteger a = BigInteger.Parse("595679239539172459088339861");
        BigInteger b = BigInteger.Parse("165641934261307971454905931");
         
        // long p = 2^89 -1;
        BigInteger p = new BigInteger(2^89 -1);
        
        long l = 16;
        BigInteger x_big = new BigInteger(x);

        // short hash = ((a * x + b) % p) % 2^16;
        // BigInteger y = new BigInteger(((BigInteger.Multiply(a,x_big)) + b) & p) + ((a*x + b) >> 89));

        BigInteger y =  ((BigInteger.Multiply(a,x_big) + b) & p) + ((BigInteger.Multiply(a,x_big) + b) >> 89);

        if (y>=p){
            y -= p;
        }

        // long hash = (y&2^l) + (y>>l);
        long hash = (long)((y & (2^l)) + (y >> (Int32)l));
        if (hash >= (2^l)){
            hash -= 2^l;
        }
        return hash;
    }

    // public void AddValue(long x)
    // {
    //     hashtable.Add(x, MultiplyShift(x));
    // }

    // public long GetHashSum()
    // {
    //     long sum = 0;

    //     foreach (DictionaryEntry d in hashtable)
    //     {   
    //         if (!(d.Value == null))
    //         {
    //             if (d.Value.GetType() == typeof(short))
    //             {
    //                 sum += (short)d.Value;
    //             } 
    //             else
    //             {
    //                 throw new Exception("Hash is not of type short");
    //             }
    //         } 
    //         else
    //         {
    //             throw new Exception("Hash is null");
    //         }
    //     }

    //     return sum;
    // }
}