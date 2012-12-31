using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EfCrutches
{
    public class Timer
    {
        public static void MeasureGarbage(Action action)
        {
            GC.Collect(2, GCCollectionMode.Forced, true);
            var gen0 = GC.CollectionCount(0);
            var gen1 = GC.CollectionCount(1);
            var gen2 = GC.CollectionCount(2);
            var memoryBefore = GC.GetTotalMemory(false) / 1024.0f / 1024.0f;

            action();

            Trace.WriteLine(string.Format("Memory before: {0,5}MB, after {1,5}MB\nCollections gen0: {2,2}, gen1: {3,2}, gen2: {4,2}",
                memoryBefore,
                GC.GetTotalMemory(false) / 1024.0f / 1024.0f,
                GC.CollectionCount(0) - gen0,
                GC.CollectionCount(1) - gen1,
                GC.CollectionCount(2) - gen2
                ));
        }

        public static TimeSpan Time(Action action, int tries = 1)
        {
            var times = new List<long>(tries);
            var timer = new Stopwatch();
            for (int i = 0; i < tries; i++)
            {
                timer.Start();
                MeasureGarbage(action);
                times.Add(timer.ElapsedMilliseconds);
                timer.Reset();
            }
            return TimeSpan.FromMilliseconds(times.Average());
        } 
    }
}