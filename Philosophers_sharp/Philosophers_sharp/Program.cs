using System;
using System.Threading;

namespace Philosophers_sharp
{
    public static class Program
    {
        public const int PHILOSOPHERS_COUNT = 5;

        private static int _waitersCount = 2;
        private static int _lapsCount = 10;
        private static int _thinkingTime = 1000;
        private static int _eatingTime = 1000;

        static void Main(string[] args)
        {
            Restaurant restaurant = new Restaurant(PHILOSOPHERS_COUNT, _waitersCount);
            restaurant.Start(_lapsCount, _thinkingTime, _eatingTime);

            Console.ReadKey();
        }
    }

    public static class Counter
    {
        public static int[] Counters = new int[Program.PHILOSOPHERS_COUNT];

        public static void Print()
        {
            for (int i = 0; i < Counters.Length; i++)
            {
                Console.Write(Counters[i] + "\t");
            }
        }
    }
}