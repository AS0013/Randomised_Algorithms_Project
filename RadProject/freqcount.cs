using System.Collections;
using System.Numerics;
namespace RadProject;
public class FreqCount {

    // Assuming Zeyu has implemnted
    // get(x): returns value of key x
    // set(x,v): sets value of x to v
    // increment(x,d): adds the value d to the value of x


    // initialise zeyus hash table

    public long FreqCount(IEnumerable<Tuple<ulong, int>> stream){

        // each tuple is (x_i, d_i)

        // for each x_i in the stream

        // check if the the key is already in the table

        // if it exists then increment by d
        
        // if it doesnt exists then set the value of x_i to d
        

    }

    public long Sum_x(ulong x) {
        
        // check if x is in the hash table
        
        // if it in the hash table then return the value of x

    }

    // When HashTable is implemented, set the correct types for input and foreach value type:)
    public long QuadraticSum(HashTable table) {
        long sum = 0;
        foreach ((ulong, ulong) entry in table)
        {
            sum += (long)Math.Pow(Sum_x(entry.Item1), 2);
        }
        return sum;
    }
}