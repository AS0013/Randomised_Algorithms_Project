using System.Collections;
using System.Numerics;

namespace RadProject;
public class HashFunction{
    public int l;
    public virtual long Hash(long x){
        return x;
    }

}
public class MulShiftHash : HashFunction {
    long a;
    public MulShiftHash (long a,int l){
        this.a = a;
        this.l = l;
    }
    public override long Hash(long x){
        long hash = (long)Math.Floor(
            (decimal)((int)Math.BigMul(a, x, out long low)>>(64 - l))
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
    public override long Hash(long x){
        BigInteger p = new BigInteger(2^89 -1);
        BigInteger x_big = new BigInteger(x);

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

    internal string MultiplyShift(long item1)
    {
        throw new NotImplementedException();
    }

}