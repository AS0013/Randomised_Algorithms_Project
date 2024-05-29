using System.Collections;
using System.Numerics;

namespace RadProject;
public class HashFunction{
    public int l;
    public virtual ulong Hash(ulong x){
        return x;
    }

}
public class MulShiftHash : HashFunction {
    ulong a;
    public MulShiftHash (ulong a,int l){
        this.a = a;
        this.l = l;
    }
    public override ulong Hash(ulong x){
        ulong low;
        ulong hash = Math.BigMul(a,x, out low);
        ulong hashed = low >> (64 - l);
        Console.WriteLine("input: " + x + " hashed: " + hashed); 
        return hash;
    }
}
public class MulModPriHash : HashFunction{
    BigInteger a;
    BigInteger b;

    public MulModPriHash(BigInteger a, BigInteger b,int l){
        this.a = a; 
        this.b = b; 
        this.l = l; 
    }
    public override ulong Hash(ulong x){
        BigInteger p = new BigInteger(2^89 -1);
        BigInteger x_big = new BigInteger(x);

        BigInteger y =  ((BigInteger.Multiply(a,x_big) + b) & p) + ((BigInteger.Multiply(a,x_big) + b) >> 89);

        if (y>=p){
            y -= p;
        }

        // long hash = (y&2^l) + (y>>l);
        ulong hash = (ulong)((y & (2^l)) + (y >> (Int32)l));
        if (hash >= (ulong)(2^l)){
            hash -= (ulong)(2^l);
        }

        Console.WriteLine("input: " + x + " hashed: " + hash); 

        return hash;
    }

    // internal string MultiplyShift(long item1)
    // {
    //     throw new NotImplementedException();
    // }

}

public class FourHashFunction : HashFunction{

    // 4-universal hashfunction g(x) = a0 + a1x + a2x^2 + a3x^3 mod p

    List<BigInteger> a_values = new List<BigInteger>();

    public FourHashFunction (BigInteger a0, BigInteger a1,BigInteger a2,BigInteger a3 ){
        a_values.Add(a0);
        a_values.Add(a1);
        a_values.Add(a2);
        a_values.Add(a3);
    }

    public override ulong Hash(ulong x)
    {

        BigInteger p = new BigInteger(2^89 -1);

        BigInteger x_big = new BigInteger(x);

        BigInteger y = a_values[3];

        for (int i = 2; i >= 0; i--)
        {
            BigInteger temp = BigInteger.Multiply(y, x_big);
            y = temp + a_values[i];
            y = (y & p) + (y >> 89);
        }

        if (y>=p){
            y -= p;
        }
        Console.WriteLine("Y: " + y);

        return (ulong) y;


      
    }
}
public class CountSketchHash : HashFunction{
    HashFunction g;
    int b = 89;
    public CountSketchHash (HashFunction g, int l){
        this.g = g;
        this.l = l;
    }

    public (ulong,long) CSHash(ulong x)
    {
        //h(x)
        ulong h = g.Hash(x) & ((1UL<<l)-1);
        //s(x)
        long s = 1-2*((long)g.Hash(x) >> (b-1));
        return (h,s);
    }
}