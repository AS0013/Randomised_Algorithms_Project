namespace RadProject;

public class CountSketch {
    readonly CountSketchHash g;
    public long[] C {
            get;
    }
    public long X {
        get;
    }
    readonly int l;
    public CountSketch(CountSketchHash g, IEnumerable<Tuple<ulong, int>> stream) {
        this.g = g;
        l = g.l;
        C = Init(stream);
        X = EstimateX();
    }


    private long[] Init(IEnumerable<Tuple<ulong, int>> stream) {
        long[] C = new long[(int)Math.Pow(2, l)];
        foreach (var tuple in stream) {
            ulong hHash = g.CSHash(tuple.Item1).Item1;
            long sHash = g.CSHash(tuple.Item1).Item2;
            C[hHash] += sHash;
        }
        return C;
    }

    private long EstimateX() {
        long sum = 0;
        for (int i = 0; i < C.Length; i++) {
            sum += (long)Math.Pow(C[i], 2);
        }
        return sum;
    }

}