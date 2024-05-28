namespace RadProject;

public class CountSketch {
    readonly HashFunction h;
    readonly HashFunction s;
    readonly ulong[] C;
    readonly ulong X;
    public CountSketch(HashFunction h, HashFunction s, IEnumerable<Tuple<ulong, int>> stream) {
        this.h = h;
        this.s = s;
        C = Init(stream);
        X = EstimateX();
    }

    // TODO: Find out how to input w as a constant
    private ulong[] Init(IEnumerable<Tuple<ulong, int>> stream) {
        const int size; // length of image of h(x) = 2^t <= 2^64
        ulong[] C = new ulong[size] {};
        foreach (var tuple in stream) {
            ulong hHash = h.Hash(tuple.Item1);
            ulong sHash = s.Hash(tuple.Item1);
            C[hHash] += sHash;
        }
        return C;
    }

    private ulong EstimateX() {
        ulong sum = 0;
        for (int i = 0; i < C.Length; i++) {
            sum += (ulong)Math.Pow(C[i], 2);
        }
        return sum;
    }

}