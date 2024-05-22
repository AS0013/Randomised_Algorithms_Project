using System.Diagnostics;
namespace RadProject
{
    class Program
    {
        static void Main(string[] args)
        {
            MulShiftHash mulShiftHash = new MulShiftHash();
            short sum = 0;
            var watch = new Stopwatch();
            // geneate test stream

            //  StreamTest.CreateStream(10,10);
            IEnumerable<Tuple<ulong, int>> stream = StreamTest.CreateStream(10, 1);

            // test MulShiftHash
            watch.Start();
            foreach (var tuple in stream)
            {
                // Console.WriteLine("Hash: " + mulShiftHash.MultiplyShift((long)tuple.Item1));
                Console.WriteLine("Hash tuple: " + tuple.Item1 + " " + tuple.Item2);
                sum += mulShiftHash.MultiplyShift((long)tuple.Item1);
                
            }
            watch.Stop();
            long time1 = watch.ElapsedMilliseconds;
            // Console.WriteLine("Time elapsed: " + time1);
            Console.WriteLine("Sum: " + sum);

            watch.Reset();
            watch.Start();
            long sum2 = 0;
            foreach (var tuple in stream)
            {
                // Console.WriteLine("Hash: " + mulShiftHash.MultiplyModPrime((long)tuple.Item1));
                sum2 += mulShiftHash.MultiplyModPrime((long)tuple.Item1);
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