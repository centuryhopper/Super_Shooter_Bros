using System.Diagnostics;
using UnityEngine;
using Unity.Jobs;
using System.Threading;
using System;
using Unity.Collections;

/*
Entities => things
Components => data
Systems => logic
*/

public class JobExample
{
    static void p(object m) => UnityEngine.Debug.Log(m);
    static double[] data = new double[10000000];

    [RuntimeInitializeOnLoadMethod]
    static void MainMethod()
    {
        Stopwatch s = new Stopwatch();
        NativeArray<double> result = new NativeArray<double>(1, Allocator.Persistent);

        const int NUM_JOBS = 4;
        const int MAX_BOUND = 10000000;
        int upperBound = MAX_BOUND / NUM_JOBS;

        // initialize 4 jobs, each at different locations between 0 and 10^7
        MyJob[] jobs = new MyJob[NUM_JOBS];

        s.Start();
        for (int i = 0; i < NUM_JOBS; ++i)
        {
            // initialize each job thread and start them
            jobs[i] = new MyJob(i * upperBound, upperBound, result);

            JobHandle scheduleJob = jobs[i].Schedule();
            scheduleJob.Complete();
        }

        s.Stop();
        s.Reset();

        p($"(multi-threaded) time in milliseconds: {s.Elapsed.TotalMilliseconds}");

        s.Start();
        for (int i = 0; i < MAX_BOUND; ++i)
        {
            // we must use a temporary variable to update the native array
            double tmp = result[0];
            tmp += (Mathf.Sin(i) + Mathf.Cos(i) + Mathf.Tan(i)
                    + (float)Math.Sinh(i) + (float)Math.Cosh(i) + (float)Math.Tanh(i));
            result[0] = tmp;
        }
        s.Stop();

        p($"(single-threaded) time in milliseconds: {s.Elapsed.TotalMilliseconds}");

        // clean up after yourself
        result.Dispose();
    }

    // Job adding two floating point values together
    public struct MyJob : IJob
    {
        public int startingIndex;
        public int offset;
        public NativeArray<double> result;

        public MyJob(int st, int of, NativeArray<double> r)
        {
            startingIndex = st;
            offset = of;
            result = r;
        }

        private void LongTask(int startingIndex, int offset)
        {
            // p($"{startingIndex} {startingIndex + offset}");
            for (int i = startingIndex; i < startingIndex + offset; ++i)
            {
                // we must use a temporary variable to update the native array
                double tmp = result[0];
                tmp += (Mathf.Sin(i) + Mathf.Cos(i) + Mathf.Tan(i)
                        + (float)Math.Sinh(i) + (float)Math.Cosh(i) + (float)Math.Tanh(i));
                result[0] = tmp;
            }
        }

        public void Execute()
        {
            LongTask(startingIndex, offset);
        }
    }

    static void MainMethod2()
    {
        Thread[] threads = new Thread[4];
        int upperBound = data.Length / threads.Length;
        p(upperBound);

        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();

        for (int i = 0; i < threads.Length; ++i)
        {
            // the i passed into longtask isn't working, so we make a local tmp variable and make it work
            int j = i;
            threads[i] = new Thread(() => LongTask(j * upperBound, upperBound));
            threads[i].Start();
        }

        Array.ForEach<Thread>(threads, (Thread t) => t.Join());

        stopwatch.Stop();

        p("time in miliseconds: " + stopwatch.ElapsedMilliseconds);
    }


    static void LongTask(int startingIndex, int offset)
    {
        for (int idx = startingIndex; idx < startingIndex + offset; ++idx)
        {
            data[idx] = (int)(Math.Sin(idx) + Math.Cos(idx) + Math.Tan(idx)
                    + Math.Sinh(idx) + Math.Cosh(idx) + Math.Tanh(idx));
        }
    }
}
