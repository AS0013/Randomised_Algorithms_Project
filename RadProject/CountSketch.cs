
// namespace RadProject;
// using System.Numerics;
// public class CountSketch {
//     readonly CountSketchHash g;
//     public SortedList<ulong , int> C {
//             get;
//     }
//     public CountSketch(CountSketchHash g, int l) {
//         this.g = g;
//         C = new SortedList<ulong , int>();
//     }


//     private SortedList<ulong , int> Init(IEnumerable<Tuple<ulong, int>> stream) {
//         foreach (var tuple in stream) {
//             ulong hHash = g.CSHash(tuple.Item1).Item1;
//             int sHash = g.CSHash(tuple.Item1).Item2;
//                 if (C.ContainsKey(hHash))
//                 {
//                     C[hHash] += sHash*tuple.Item2;
//                 }
//                 else {
//                     C.Add(hHash, sHash*tuple.Item2);
//                 }  
//         }
//         return C;
//     }

//     public long EstimateX(IEnumerable<Tuple<ulong, int>> stream) {
//         this.Init(stream);
//         long sum = 0;
//         foreach (var x in C) {
//             sum += (long)x.Value * x.Value;
//         }
//         return sum;
//     }
// }

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

            // to ensure the hash index fits within the array bounds, and also the hhash value is a ulong so we get it to int
            int index = (int)(hHash % (ulong)arraySize);
            C[index] += sHash * tuple.Item2;
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
