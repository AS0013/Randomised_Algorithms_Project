﻿using System.Diagnostics;
using System.Numerics;
namespace RadProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // //OPGAVE 3:
            // int max = 18;
            // int n = (int)Math.Pow(2,max);
            // Console.WriteLine("Opgave 3 quadratic sum:");
            // Console.WriteLine("VALUE OF n: n = ", n);
            // int[] lValues = {2,4,6,8,10,12,14,16,18};
            // foreach (int l in lValues)
            // {
            //     Console.WriteLine("TEST WITH l = " +l);
            //     //set parameters for Multiply shift
            //     ulong a = 1470686635362862957; // Generated using https://www.random.org/bytes/
            //     MulShiftHash mulShiftHash = new MulShiftHash(a,l);
            //     //set parameters for multiplymodprime
            //     BigInteger b = BigInteger.Parse("595679239539172459088339861");
            //     BigInteger c = BigInteger.Parse("165641934261307971454905931");
            //     MulModPriHash MulModPriHash = new MulModPriHash(b,c,l);

                
            //     // geneate test stream
            //     IEnumerable<Tuple<ulong, int>> stream = StreamTest.CreateStream(n, l);
            //     //Console.WriteLine("Stream generated");
            //     //initiate stopwatch
            //     var watch = new Stopwatch();
            //     ChainHashTable table1 = new ChainHashTable(l,mulShiftHash);
            //     ChainHashTable table2 = new ChainHashTable(l,MulModPriHash);
                
            //     watch.Reset();
            //     watch.Start();
            //     long table1_sum = table1.QuadraticSum(stream);
            //     Console.WriteLine("multiply shift hash Quadratic sum:" + table1_sum);
            //     watch.Stop();
            //     long time3 = watch.ElapsedMilliseconds;
            //     Console.WriteLine("multiply shift hash time taken (ms):" + time3);
            //     watch.Reset();
            //     watch.Start();
            //     long table2_sum = table2.QuadraticSum(stream);
            //     Console.WriteLine("multiply mod prime Quadratic sum:" + table2_sum);
            //     watch.Stop();
            //     long time4 = watch.ElapsedMilliseconds;
            //     Console.WriteLine("multiply mod prime time taken (ms):" + time4);
            //     Console.WriteLine("Time difference: " + Math.Abs(time4-time3));
               
            // }

            //OPGAVE 7:
            Console.WriteLine("Opgave 7:");

            int max7 = 17;
            int n7 = (int)Math.Pow(2,max7);
            int l7 = 16;
            // initiate fourhash
            BigInteger a0 = BigInteger.Parse("339176588342155335418149214"); // Generated using https://www.random.org/bytes/
            BigInteger a1 = BigInteger.Parse("545615294102633400098072839"); // Generated using https://www.random.org/bytes/
            BigInteger a2 = BigInteger.Parse("559758820152785102864000321"); // Generated using https://www.random.org/bytes/
            BigInteger a3 = BigInteger.Parse("189597206666064321536354137"); // Generated using https://www.random.org/bytes/
            FourHashFunction fourhashfunc = new FourHashFunction(a0,a1,a2,a3);
            // initiate countsketchhash
            CountSketchHash countSketchHash = new CountSketchHash(fourhashfunc ,l7);
    
            for(int i = 1; i <= 10; i++)
            {
                               
                // geneate test stream
                IEnumerable<Tuple<ulong, int>> stream = StreamTest.CreateStream(n7, l7);
                //Console.WriteLine("Stream generated");
                //hash table
                ChainHashTable table1 = new ChainHashTable(l7,countSketchHash);
                long table1_sum = table1.QuadraticSum(stream);
                Console.WriteLine("multiply shift hash Quadratic sum:" + table1_sum);

                //countsketch
                CountSketch countSketch = new CountSketch(countSketchHash,l7);
                long table1_estim = countSketch.EstimateX(stream);
                 Console.WriteLine("multiply shift hash Quadratic sum:" + table1_sum);
            }
            
            // //TESTS FOR HASH FUNCTIONS:
            // int testl = 16; 
            // //TEST MULSHIFT HASH:
            // // 2^30 *2^30 >> 48 = 4096
            // ulong a = 1<<30; 
            // MulShiftHash mulShiftTest = new MulShiftHash(a,testl);
            // Console.WriteLine(mulShiftTest.Hash(1<<30));
            // //TEST MULMODPRI HASH:
            // // ((559*300 + 156) mod (2^89-1)) mod 2^16 = 3674
            
            // BigInteger aTest1 = BigInteger.Parse("559");
            // BigInteger bTest1 = BigInteger.Parse("156");
            // MulModPriHash mulModPriTest1 = new MulModPriHash(aTest1,bTest1,testl);
            // Console.WriteLine(mulModPriTest1.Hash(300));

            // // 2^89 mod (2^89-1)) mod 2^16 = 1
            // BigInteger aTest2 = BigInteger.Pow(2,89);
            // BigInteger bTest2 = BigInteger.Parse("0");
            // MulModPriHash mulModPriTest2 = new MulModPriHash(aTest2,bTest2,testl);
            // Console.WriteLine(mulModPriTest2.Hash(1));

            // //TEST FOUR HASH
            // // (33+54*200+55*200^2+18*200^3) mod (2^89-1) = 146210833
            // BigInteger a0 = BigInteger.Parse("33"); 
            // BigInteger a1 = BigInteger.Parse("54");
            // BigInteger a2 = BigInteger.Parse("55"); 
            // BigInteger a3 = BigInteger.Parse("18"); 

            // FourHashFunction fourhashTest = new FourHashFunction(a0,a1,a2,a3);
            // Console.WriteLine(fourhashTest.Hash(200));

            // // test countsketchhash
            // //g(x): (33+54*20+55*20^2+18*20^3) mod (2^89-1) = 167113
            // ulong input = 20;
            // FourHashFunction fourhash = new FourHashFunction(a0,a1,a2,a3);
            // Console.WriteLine("TEST "+fourhash.Hash(input));
            // //h(x): 167113 mod 2^16 = 36041
            // //s(x): 1-2* floor(36041/2^89) = 1
            // CountSketchHash G = new CountSketchHash(fourhash ,testl);
            // Console.WriteLine("TEST COUNTSKETCH:" + G.CSHash(input));
        }
    }
}