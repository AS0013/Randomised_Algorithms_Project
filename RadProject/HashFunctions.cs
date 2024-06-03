using System.Numerics;
namespace RadProject;
public class HashFunction{
    public int l;
    public virtual ulong Hash(ulong x){
        return x;
    }

    // public virtual BigInteger Hash(ulong){
    //     return x;
    // }

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
        // Console.WriteLine("input: " + x + " hashed: " + hashed); 
        return hashed;
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
        BigInteger p = BigInteger.Pow(2,89)-1;
        BigInteger x_big = new BigInteger(x);
        // Console.WriteLine("a " + (BigInteger.Multiply(a,x_big) + b));
        BigInteger y =  ((BigInteger.Multiply(a,x_big) + b) & p) + ((BigInteger.Multiply(a,x_big) + b) >> 89);

        if (y>=p){
            y -= p;
        }
        // Console.WriteLine("y " + (y));
        // long hash = (y&2^l) + (y>>l);
        ulong hash = (ulong)(y & ((1UL << 16) -1));
        // Console.WriteLine("hash " + (hash));
        // Console.WriteLine("input: " + x + " hashed: " + hash); 

        return hash;
    }

}

public class FourHashFunction{

    // 4-universal hashfunction g(x) = a0 + a1x + a2x^2 + a3x^3 mod p

    List<BigInteger> a_values = new List<BigInteger>();

    public FourHashFunction (BigInteger a0, BigInteger a1,BigInteger a2,BigInteger a3 ){
        a_values.Add(a0);
        a_values.Add(a1);
        a_values.Add(a2);
        a_values.Add(a3);
    }

    public BigInteger Hash(ulong x)
    {

        BigInteger p;

        p = BigInteger.Pow(2, 89) - 1;

        BigInteger x_big = new BigInteger(x);

        BigInteger y = a_values[3];

        // ulong k = 4;

        for (int i = 2; i >= 0; i--)
        {
            BigInteger temp = BigInteger.Multiply(y, x_big);
            y = temp + a_values[i];
            y = (y & p) + (y >> 89);
            // y = (y * x_big + a_values[i]) % p;
        }

        if (y>=p){
            y -= p;
        }

        // ulong y_k = (ulong)( y & (k-1));

//         Console.WriteLine("Y: " + y + " X: " + x);
        
//         return y;

        // ulong y_k = (ulong)( y & (k-1));

        // Console.WriteLine("Y: " + y + " X: " + x);
        
        return y;
    
    }
}
public class CountSketchHash : HashFunction{
    FourHashFunction g;
    int b = 89;
    public CountSketchHash (FourHashFunction g, int l){
        this.g = g;
        this.l = l;
    }

    public (ulong,int) CSHash(ulong x)
    {
        //h(x)
        //Console.WriteLine("adafafddsf " + (g.Hash(x) & ((1UL<<l)-1)));
        ulong h = (ulong)(g.Hash(x) & ((1UL<<l)-1));
        //s(x)
        int s = (int)(1-2*((BigInteger)g.Hash(x) >> (b-1)));
        return (h,s);
    }
}