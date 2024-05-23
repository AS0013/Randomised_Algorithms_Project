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
        ulong hash = (ulong)Math.Floor(
            (decimal)(Math.BigMul(a, x, out ulong low)>>(64 - l))
        );
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
        return hash;
    }

    // internal string MultiplyShift(long item1)
    // {
    //     throw new NotImplementedException();
    // }

}