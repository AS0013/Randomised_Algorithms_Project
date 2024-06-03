
namespace RadProject;
using System.Numerics;
public class CountSketch {
    readonly CountSketchHash g;
    public SortedList<ulong , int> C {
            get;
    }
    public CountSketch(CountSketchHash g, int l) {
        this.g = g;
        C = new SortedList<ulong , int>();
    }


    private SortedList<ulong , int> Init(IEnumerable<Tuple<ulong, int>> stream) {
        foreach (var tuple in stream) {
            ulong hHash = g.CSHash(tuple.Item1).Item1;
            int sHash = g.CSHash(tuple.Item1).Item2;
                if (C.ContainsKey(hHash))
                {
                    C[hHash] += sHash*tuple.Item2;
                }
                else {
                    C.Add(hHash, sHash*tuple.Item2);
                }  
        }
        return C;
    }

    public long EstimateX(IEnumerable<Tuple<ulong, int>> stream) {
        this.Init(stream);
        long sum = 0;
        foreach (var x in C) {
            sum += (long)x.Value * x.Value;
        }
        return sum;
    }
}