using System.Collections;
using System.Numerics;
namespace RadProject;
public class FreqCount {

    // Assuming Zeyu has implemnted
    // get(x): returns value of key x
    // set(x,v): sets value of x to v
    // increment(x,d): adds the value d to the value of x



    static int l = 16;

    //  constructor to define which has to use
    static HashFunction ModPriHash = new MulModPriHash(BigInteger.Parse("595679239539172459088339861"), BigInteger.Parse("165641934261307971454905931"),l);
    ChainHashTable table1 = new ChainHashTable(l,ModPriHash);
    private List<ulong> list_x = new List<ulong>();
    // initialise zeyus hash table

    // (x_1, d_1), (x_2, d_2), ... , (x_n, d_n)

    // (7,17),(3,0), (7,-3), (3,1)
    // 7 -> 14

    public void InitializeTable(IEnumerable<Tuple<ulong, int>> stream){

        foreach (var tuple in stream) {
            table1.Increment((int)tuple.Item1, tuple.Item2);
        }
        // each tuple is (x_i, d_i)

        // for each x_i in the stream

        // check if the the key is already in the table

        // if it exists then increment by d
        
        // if it doesnt exists then set the value of x_i to d
        

    }


    // could just use the get(x) function from zeyu's hash table instead ??

    // public long Sum_x(ulong x) {
        
    //     // check if x is in the hash table
        
    //     // if it in the hash table then return the value of x

    // }

    // public long QuadraticSum() {
    //     long sum = 0;
        
    //     foreach (ulong x in list_x) {
    //         if (table1.Get((int)x) != 0)    
    //             sum += (long)Math.Pow(table1.Get((int)x), 2);
    //     }

    //     return sum;
    // }
}