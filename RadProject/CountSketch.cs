namespace RadProject;
using System.Numerics;
public class CountSketch {
    readonly CountSketchHash g;
    public SortedList<BigInteger , int> C {
            get;
    }
    readonly int l;
    public CountSketch(CountSketchHash g, int l) {
        this.g = g;
        l = g.l;
        C = new SortedList<BigInteger , int>();
    }


    private SortedList<BigInteger , int> Init(IEnumerable<Tuple<ulong, int>> stream) {
        Console.WriteLine("monke");
        foreach (var tuple in stream) {
        BigInteger hHash = g.CSHash(tuple.Item1).Item1;
        int sHash = g.CSHash(tuple.Item1).Item2;
            if (C.ContainsKey(hHash))
            {
                C[hHash] += sHash;
                //Console.WriteLine("aaa"+C[hHash]);
            }
            else {
                C.Add(hHash, sHash);
                //Console.WriteLine("aaa"+C[hHash]);
            }  
        }
        return C;
    }

    public long EstimateX(IEnumerable<Tuple<ulong, int>> stream) {
        this.Init(stream);
        long sum = 0;
        foreach (var x in C) {
            sum += (long)Math.Pow(x.Value, 2);
        }
        return sum;
    }

}