using System.Collections;

namespace RadProject;
public class MulShiftHash {
    readonly Hashtable hashtable = new();
    private static short MultiplyShift(long x)
    {
        long a = 249; // Generated using https://www.random.org/bytes/
        int l = 16; // hashing to short (16-bit int)
        short hash = (short)Math.Floor(
            (decimal)((int)Math.BigMul(a, x, out long low)>>(64 - l))
        );
        return hash;
    }
    public void AddValue(long x)
    {
        hashtable.Add(x, MultiplyShift(x));
    }

    public long GetHashSum()
    {
        long sum = 0;

        foreach (DictionaryEntry d in hashtable)
        {   
            if (!(d.Value == null))
            {
                if (d.Value.GetType() == typeof(short))
                {
                    sum += (short)d.Value;
                } 
                else
                {
                    throw new Exception("Hash is not of type short");
                }
            } 
            else
            {
                throw new Exception("Hash is null");
            }
        }

        return sum;
    }
}