using System.Collections;
using System.Numerics;

namespace RadProject;
public class ChainHashTable {
    HashFunction h;
    int l;
    private SortedList<long , LinkedList<KeyValuePair<int, int>>> table;
    public ChainHashTable (int l, HashFunction h){
        this.l = l;
        this.table = new  SortedList<long , LinkedList<KeyValuePair<int, int>>>();
        this.h = h;
    }
    public int Get (int x){
        long index = h.Hash(x);
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
    public void Set (int x, int v){
        long index = h.Hash(x);
        if (table.ContainsKey(index))
        {
            foreach (var pair in table[index])
            {
                if (pair.Key == x)
                {
                    table[index].Find(pair).Value = new KeyValuePair<int, int> (x,v);
                    return;
                }
            }
            table[index].Append(new KeyValuePair<int, int> (x,v));
        }
        else {
            table.Add(index, new LinkedList<KeyValuePair<int, int>>());
            table[index].Append(new KeyValuePair<int, int> (x,v));
        }
    }
    public void Increment (int x, int v){
        long index = h.Hash(x);
        if (table.ContainsKey(index))
        {
            foreach (var pair in table[index])
            {
                if (pair.Key == x)
                {
                    table[index].Find(pair).Value = new KeyValuePair<int, int> (x,v+pair.Key);
                    return;
                }
            }
            table[index].Append(new KeyValuePair<int, int> (x,v));
        }
        else {
            table.Add(index, new LinkedList<KeyValuePair<int, int>>());
            table[index].Append(new KeyValuePair<int, int> (x,v));
        }
    }
}