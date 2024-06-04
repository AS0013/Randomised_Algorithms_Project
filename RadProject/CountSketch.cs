namespace RadProject;
using System.Numerics;

public class CountSketch {
    readonly CountSketchHash g;
    readonly int l;
    readonly int arraySize;
    public int[] C { get; }

    public CountSketch(CountSketchHash g, int l) {
        this.g = g;
        this.l = l;
        this.arraySize = 1 << l; // Array size is 2^l or m idk
        C = new int[arraySize];
    }

    private void Init(IEnumerable<Tuple<ulong, int>> stream) {
        foreach (var tuple in stream) {
            var hHashPair = g.CSHash(tuple.Item1);
            ulong hHash = hHashPair.Item1;
            int sHash = hHashPair.Item2;

            C[hHash] += sHash * tuple.Item2;
        }
    }

    public long EstimateX(IEnumerable<Tuple<ulong, int>> stream) {
        this.Init(stream);

        long sum = 0;
        foreach (var value in C) {
            sum += (long)value * value;
        }
        return sum;
    }
}
