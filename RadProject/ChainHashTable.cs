using System.Collections;
using System.Numerics;

namespace RadProject;
public class ChainHashTable {
    HashFunction h;
    int l;
    private SortedList<ulong , LinkedList<KeyValuePair<ulong, int>>> table;
    public ChainHashTable (int l, HashFunction h){
        this.l = l;
        this.table = new SortedList<ulong , LinkedList<KeyValuePair<ulong, int>>>();
        this.h = h;
    }
    public int Get (ulong x){
        ulong index = h.Hash(x);
        if (table.ContainsKey(index))
        {
            foreach (var pair in table[index])
            {
                if (pair.Key == x)
                {
                    return pair.Value;
                }
            }
        }
            return 0; 
    }
    public void Set (ulong x, int v){
        ulong index = h.Hash(x);
        if (table.ContainsKey(index))
        {
            foreach (var pair in table[index])
            {
                if (pair.Key == x)
                {
                    table[index].Find(pair).Value = new KeyValuePair<ulong, int> (x,v);
                    return;
                }
            }
            table[index].AddLast(new KeyValuePair<ulong, int> (x,v));
        }
        else {
            table.Add(index, new LinkedList<KeyValuePair<ulong, int>>());
            table[index].AddLast(new KeyValuePair<ulong, int> (x,v));
        }
    }
    public void Increment (ulong x, int v){
        ulong index = h.Hash(x);
        // Console.WriteLine("X: "+ x +"Index: " + index);
        if (table.ContainsKey(index))
        {
            foreach (var pair in table[index])
            {
                if (pair.Key == x)
                {
                    table[index].Find(pair).Value = new KeyValuePair<ulong, int> (x,v+pair.Value);
                    return;
                }
            }
            table[index].AddLast(new KeyValuePair<ulong, int> (x,v));
        }
        else {
            table.Add(index, new LinkedList<KeyValuePair<ulong, int>>());
            table[index].AddLast(new KeyValuePair<ulong, int> (x,v));
        }
    }


    public void InitializeTable(IEnumerable<Tuple<ulong, int>> stream){
        foreach (var tuple in stream) {
            // Console.WriteLine("Tuple: " + tuple.Item1 + " " + tuple.Item2);
            Increment(tuple.Item1, tuple.Item2);
        }
    }

    //sum of squares.
    public long QuadraticSum(IEnumerable<Tuple<ulong, int>> stream) {
        long sum = 0;

        this.InitializeTable(stream);

        // Console.WriteLine("starting sum");
        foreach (LinkedList<KeyValuePair<ulong, int>> list in table.Values){
            // Console.WriteLine("LinkedList Count: " + list.Count);
            foreach (KeyValuePair<ulong, int> pair in list){
                // Console.WriteLine("Pair: " + pair.Key + " " + pair.Value);
                sum += (long)Math.Pow(pair.Value, 2);

            }
        }
        return sum;
    }
}