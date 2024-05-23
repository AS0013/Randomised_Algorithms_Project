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
            IEnumerable<Tuple<ulong, int>> stream = StreamTest.CreateStream(100000, 100);

            // test MulShiftHash
            watch.Start();
            foreach (var tuple in stream)
            {
                Console.WriteLine("Hash: " + mulShiftHash.Hash((long)tuple.Item1));
                sum += mulShiftHash.Hash((long)tuple.Item1);
            }
            watch.Stop();
            long time1 = watch.ElapsedMilliseconds;
            // Console.WriteLine("Time elapsed: " + time1);
            Console.WriteLine("Sum: " + sum);

            watch.Reset();
            watch.Start();
            //set parameters for multiplymodprime
            BigInteger b = BigInteger.Parse("595679239539172459088339861");
            BigInteger c = BigInteger.Parse("165641934261307971454905931");
            MulModPriHash MulModPriHash = new MulModPriHash(b,c,l);
            long sum2 = 0;
            foreach (var tuple in stream)
            {
                Console.WriteLine("Hash: " + MulModPriHash.Hash((long)tuple.Item1));
                sum2 +=  MulModPriHash.Hash((long)tuple.Item1);
            }
            watch.Stop();
            long time2 = watch.ElapsedMilliseconds;
            // Console.WriteLine("Time elapsed: " + time2);
            Console.WriteLine("Sum: " + sum2);

            Console.WriteLine("Time elapsed for MultiplyShift: " + time1);
            Console.WriteLine("Time elapsed for MultiplyModPrime: " + time2);
            Console.WriteLine("Difference: " + (time1 - time2));
        }
    }
}