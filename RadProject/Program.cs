﻿using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Collections.Generic;
using ScottPlot;

namespace RadProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //OPGAVE 1:
                int max1=16;
                int n1=(int)Math.Pow(2,max1);
                int l1=16;
                ulong d1 = 1470686635362862957; // Generated using https://www.random.org/bytes/
                MulShiftHash mulShiftHash1 = new MulShiftHash(d1,l1);
                //set parameters for multiplymodprime
                BigInteger b1 = BigInteger.Parse("595679239539172459088339861");
                BigInteger c1 = BigInteger.Parse("165641934261307971454905931");
                MulModPriHash mulModPriHash1 = new MulModPriHash(b1,c1,l1);

                
                // geneate test stream
                IEnumerable<Tuple<ulong, int>> stream1 = StreamTest.CreateStream(n1, l1);
                //Console.WriteLine("Stream generated");
                //initiate stopwatch
                var watch1 = new Stopwatch();

                //multiply shift runtime
                watch1.Reset();
                watch1.Start();
                ulong shiftsum=0;
                foreach (Tuple<ulong, int> x in stream1){
                    shiftsum += mulShiftHash1.Hash(x.Item1);
                }
                watch1.Stop();
                long time1 = watch1.ElapsedMilliseconds;
                Console.WriteLine("Opgave 1:");
                Console.WriteLine("multiply shift sum: "+shiftsum+ " hash time taken (ms):" + time1);


                //multiply mod prime runtime
                watch1.Reset();
                watch1.Start();
                ulong modsum=0;
                foreach (Tuple<ulong, int> x in stream1){
                    modsum += mulModPriHash1.Hash(x.Item1);
                }
                watch1.Stop();
                long time2 = watch1.ElapsedMilliseconds;
                Console.WriteLine("multiply mod prime  sum: "+modsum+ " hash time taken (ms): "+ time2);
                Console.WriteLine("Time difference: " + Math.Abs(time2-time1));

            //OPGAVE 3:
            int max = 18;
            int n = (int)Math.Pow(2,max);
            Console.WriteLine("Opgave 3 quadratic sum:");
            Console.WriteLine("VALUE OF n: n = ", n);
            int[] lValues = {2,4,6,8,10,12,14,16,17,18};
            foreach (int l in lValues)
            {
                Console.WriteLine("TEST WITH l = " +l);
                //set parameters for Multiply shift
                ulong a = 1470686635362862957; // Generated using https://www.random.org/bytes/
                MulShiftHash mulShiftHash = new MulShiftHash(a,l);
                //set parameters for multiplymodprime
                BigInteger b = BigInteger.Parse("595679239539172459088339861");
                BigInteger c = BigInteger.Parse("165641934261307971454905931");
                MulModPriHash MulModPriHash = new MulModPriHash(b,c,l);

                
                // geneate test stream
                IEnumerable<Tuple<ulong, int>> stream = StreamTest.CreateStream(n, l);
                //Console.WriteLine("Stream generated");
                //initiate stopwatch
                var watch = new Stopwatch();
                ChainHashTable table1 = new ChainHashTable(l,mulShiftHash);
                ChainHashTable table2 = new ChainHashTable(l,MulModPriHash);
                
                watch.Reset();
                watch.Start();
                long table1_sum = table1.QuadraticSum(stream);
                watch.Stop();
                Console.WriteLine("multiply shift hash Quadratic sum:" + table1_sum);
                long time3 = watch.ElapsedMilliseconds;
                Console.WriteLine("multiply shift hash time taken (ms):" + time3);
                watch.Reset();
                watch.Start();
                long table2_sum = table2.QuadraticSum(stream);
                watch.Stop();
                Console.WriteLine("multiply mod prime Quadratic sum:" + table2_sum);
                long time4 = watch.ElapsedMilliseconds;
                Console.WriteLine("multiply mod prime time taken (ms):" + time4);
                Console.WriteLine("Time difference: " + Math.Abs(time4-time3));
               
            }

            //OPGAVE 7 & 8:
            Console.WriteLine("Opgave 7 & 8:");
            int l7 = 17;
            // geneate test stream
            int max7 = 18;
            int n7 = (int)Math.Pow(2,max7);
            IEnumerable<Tuple<ulong, int>> stream7 = StreamTest.CreateStream(n7, l7);
            
            //repeat for m = 2^9,2^12.2^16
            int[] tValues = {8,12,16};
            foreach (int t in tValues){                
                // Calculate Quadratic Sum
                // initiate fourhash
                BigInteger a0 = BigInteger.Parse("339176588342155335418149214"); // Generated using https://www.random.org/bytes/
                BigInteger a1 = BigInteger.Parse("545615294102633400098072839"); // Generated using https://www.random.org/bytes/
                BigInteger a2 = BigInteger.Parse("559758820152785102864000321"); // Generated using https://www.random.org/bytes/
                BigInteger a3 = BigInteger.Parse("189597206666064321536354137"); // Generated using https://www.random.org/bytes/
                FourHashFunction fourhashfunc = new FourHashFunction(a0,a1,a2,a3);
                // initiate countsketchhash
                CountSketchHash countSketchHash = new CountSketchHash(fourhashfunc ,t);
                ChainHashTable table1 = new ChainHashTable(t,countSketchHash);
                long quadraticSum = table1.QuadraticSum(stream7);
                Console.WriteLine("OPG8 Experiment: Number of elements n : 2^" +max7+"  Number of different elements 2^l: 2^" +l7+ " Range m = 2^t of hash function h: 2^" + t+ " Quadratic sum:" + quadraticSum );
                // initiate results list for estimates:
                List<long> QSTable = new List<long>();
                long avgRuntime = 0;
                int testCount=100;
                for(int i = 1; i <= testCount; i++)
                {   
                    Random random = new Random();  
                    //random values for four hash
                    BigInteger[] A = new BigInteger[4];
                    //Console.WriteLine("Random values: ");
                    for (int j=0; j<A.Length;j++){
                    byte[] bytes = new byte[12];
                        random.NextBytes(bytes);
                        A[j] = new BigInteger(bytes);
                        A[j]= A[j] & (BigInteger.Pow(2, 89) - 1);
                        //Console.WriteLine(A[j]);
                    }
                    //hash functions:
                    FourHashFunction fourHash= new FourHashFunction(A[0],A[1],A[2],A[3]);
                    CountSketchHash CShash = new CountSketchHash(fourHash ,t);
                    //countsketch estimate for QS
                    var watch7 = new Stopwatch();
                    CountSketch countSketch = new CountSketch(CShash,t);
                    watch7.Reset();
                    watch7.Start();
                    long QSEstim = countSketch.EstimateX(stream7);
                    watch7.Stop();
                    long time = watch7.ElapsedMilliseconds;
                    avgRuntime +=time;
                    Console.WriteLine("Test" + i + ": random values: " + A[0] + " , " + A[1] + " , " +A[2] + " , " + A[3] +" Estimate: " + QSEstim + "Time taken (ms):" + time);
                    QSTable.Add(QSEstim);
                }
                //average time
                avgRuntime = avgRuntime/testCount;

                //results list sorted after estimates
                List<long>QSTableSorted = new List<long>(QSTable);
                QSTableSorted.Sort();

                //mean square error, Expected value, variance.
                long meanSquareError = 0;
                long mean = 0;
                long variance = 0;
                long QSEstimSum = 0;
                foreach (long x in QSTable)
                {          
                    meanSquareError += (long)Math.Pow(x - quadraticSum,2);
                    mean += x;
                }
                meanSquareError = meanSquareError/testCount;
                mean = mean/testCount;
                foreach (long x in QSTable)
                {          
                    variance += (long)Math.Pow(mean-x,2);
                    QSEstimSum += x;
                }
                variance = variance/testCount;
                Console.WriteLine("Average Runtime: " +avgRuntime);
                Console.WriteLine("Mean square error: " +meanSquareError);
                Console.WriteLine("Anticipated Expected value: " + quadraticSum + " Mean: " + mean);
                Console.WriteLine("Anticipated variance: " + Math.Round(2*Math.Pow(quadraticSum,2)/Math.Pow(2,t)) + " Variance: " + variance);

                // groups and median:
                // divide into groups:
                List<List<long>> QSTableGroups = new List<List<long>>();
                for (int i = 0; i < (testCount-1)/11; i++)
                {
                    QSTableGroups.Add(QSTable.Skip(i * 11).Take(11).ToList());
                }
                //medians of each group:
                List<long> QSTablemedians = new List<long>();
                foreach (var group in QSTableGroups)
                {
                    long median;
                    group.Sort();
                    median = group[group.Count / 2];
                    QSTablemedians.Add(median);
                }
                QSTablemedians.Sort();
                Console.WriteLine("Medians: " + string.Join(", ", QSTablemedians));

                // Create a Plot object
                var plt = new ScottPlot.Plot();
                // Add a scatter plot
                plt.Add.Line(0, quadraticSum,100, quadraticSum);
                List<int> numbers = Enumerable.Range(1, 100).ToList();
                plt.Add.ScatterPoints(numbers, QSTableSorted);
                plt.Axes.SetLimits(0, 100, QSTableSorted[0]-0.1*(QSTableSorted[QSTableSorted.Count-1]-QSTableSorted[0]), QSTableSorted[QSTableSorted.Count-1]+0.1*(QSTableSorted[QSTableSorted.Count-1]-QSTableSorted[0]));
                string filepath1=$"100ResultsPlotT{t}.png";
                plt.SavePng(filepath1, 1024, 768);
                Console.WriteLine("100 points Plot saved in directory");

                var plt2 = new ScottPlot.Plot();
                // Add a scatter plot
                plt2.Add.Line(0, quadraticSum,10, quadraticSum);
                List<int> numbers2 = Enumerable.Range(1, 9).ToList();
                plt2.Add.ScatterPoints(numbers2, QSTablemedians);
                plt2.Axes.SetLimits(0, 10,QSTableSorted[0]-0.1*(QSTableSorted[QSTableSorted.Count-1]-QSTableSorted[0]), QSTableSorted[QSTableSorted.Count-1]+0.1*(QSTableSorted[QSTableSorted.Count-1]-QSTableSorted[0]));
                string filepath2=$"MediansPlotT{t}.png";
                plt2.SavePng(filepath2, 1024, 768);
                Console.WriteLine("medians Plot saved in directory");
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