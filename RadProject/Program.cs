using System.Diagnostics;
using System.Numerics;
namespace RadProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //set parameters for Multiply shift
            long a = -4347435114166313097; // Generated using https://www.random.org/bytes/
            int l = 16; // hashing to short (16-bit int) 
            MulShiftHash mulShiftHash = new MulShiftHash(a,l);

            long sum = 0;
            var watch = new Stopwatch();
            // geneate test stream

            //  StreamTest.CreateStream(10,10);
            IEnumerable<Tuple<ulong, int>> stream = StreamTest.CreateStream(1000, 100);
            Console.WriteLine("Stream generated");
            // print out the stream
            foreach (var tuple in stream)
            {
                Console.WriteLine("Item1: " + tuple.Item1 + " Item2: " + tuple.Item2);
            }

            // test MulShiftHash
            watch.Start();
            foreach (var tuple in stream)
            {
                // Console.WriteLine("Hash: " + mulShiftHash.Hash((long)tuple.Item1));
                sum += mulShiftHash.Hash((long)tuple.Item1);
            }
            watch.Stop();
            long time1 = watch.ElapsedMilliseconds;
            // Console.WriteLine("Time elapsed: " + time1);
            

            watch.Reset();
            watch.Start();
            //set parameters for multiplymodprime
            BigInteger b = BigInteger.Parse("595679239539172459088339861");
            BigInteger c = BigInteger.Parse("165641934261307971454905931");
            MulModPriHash MulModPriHash = new MulModPriHash(b,c,l);
            long sum2 = 0;
            foreach (var tuple in stream)
            {
                // Console.WriteLine("Hash: " + MulModPriHash.Hash((long)tuple.Item1));
                sum2 +=  MulModPriHash.Hash((long)tuple.Item1);
            }
            watch.Stop();
            long time2 = watch.ElapsedMilliseconds;
            // Console.WriteLine("Time elapsed: " + time2);

            // Console.WriteLine("mulshift sum: " + sum);
            // Console.WriteLine("mulmodprime sum: " + sum2);

            // Console.WriteLine("Time elapsed for MultiplyShift: " + time1);
            // Console.WriteLine("Time elapsed for MultiplyModPrime: " + time2);
            // Console.WriteLine("Difference: " + (time1 - time2));


            // test chain hash table for each hash function 

            ChainHashTable table1 = new ChainHashTable(l,mulShiftHash);
            // ChainHashTable table2 = new ChainHashTable(l,MulModPriHash);

            table1.InitializeTable(stream);
            // table2.InitializeTable(stream);
            watch.Reset();
            watch.Start();
            long table1_sum = table1.QuadraticSum();
            watch.Stop();
            long time3 = watch.ElapsedMilliseconds;

            // watch.Reset();
            // watch.Start();
            // long table2_sum = table2.QuadraticSum();
            // watch.Stop();
            // long time4 = watch.ElapsedMilliseconds;

            
            Console.WriteLine("Table1 sum: " + table1_sum);
            // Console.WriteLine("Table2 sum: " + table2_sum);
            Console.WriteLine("Time elapsed for MultiplyShift    (Quadratic Sum): " + time3);
            // Console.WriteLine("Time elapsed for MultiplyModPrime (Quadratic Sum): " + time4);
            // Console.WriteLine("Difference: " + (time3 - time4));
        }
    }
}