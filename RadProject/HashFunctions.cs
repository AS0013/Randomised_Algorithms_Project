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
        return hashed;
    }
}
public class MulModPriHash : HashFunction{
    BigInteger a;
    BigInteger b;

    BigInteger p;

    public MulModPriHash(BigInteger a, BigInteger b,int l){
        this.a = a; 
        this.b = b; 
        this.l = l; 
        p = BigInteger.Pow(2,89)-1;
    }
    public override ulong Hash(ulong x){
        BigInteger x_big = new BigInteger(x);
        BigInteger y =  ((BigInteger.Multiply(a,x_big) + b) & p) + ((BigInteger.Multiply(a,x_big) + b) >> 89);

        if (y>=p){
            y -= p;
        }
        ulong hash = (ulong)(y & ((1UL << l) -1));

        return hash;
    }

}

public class FourHashFunction{

    // 4-universal hashfunction g(x) = a0 + a1x + a2x^2 + a3x^3 mod p

    List<BigInteger> a_values = new List<BigInteger>();

    BigInteger p;
    BigInteger temp;
    BigInteger x_big;

    BigInteger y;

    public FourHashFunction (BigInteger a0, BigInteger a1,BigInteger a2,BigInteger a3 ){
        a_values.Add(a0);
        a_values.Add(a1);
        a_values.Add(a2);
        a_values.Add(a3);
        p = BigInteger.Pow(2, 89) - 1;   
    }

    public BigInteger Hash(ulong x)
    {


        x_big = (BigInteger)x;

        y = a_values[3];
        // BigInteger temp;

        for (int i = 2; i >= 0; i--)
        {
            temp = BigInteger.Multiply(y, x_big);
            y = temp + a_values[i];
            y = (y & p) + (y >> 89);
        }

        if (y>=p){
            y -= p;
        }
        
        return y;
    
    }
}
public class CountSketchHash : HashFunction{
    FourHashFunction g;
    int b = 89;
    BigInteger x_Big;
    public CountSketchHash (FourHashFunction g, int l){
        this.g = g;
        this.l = l;
    }

    public (ulong,int) CSHash(ulong x)
    {
        x_Big = g.Hash(x);
        //h(x)
        ulong h = (ulong)(x_Big & ((1UL<<l)-1));
        //s(x)
        int s = (int)(1-2*(x_Big >> (b-1)));
        return (h,s);
    }
}