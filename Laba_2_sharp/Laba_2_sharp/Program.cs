using System;
using System.Threading;

namespace Laba_2_sharp
{
    internal class Program
    {
        private const int ThreadsCount = 8;
        private const int ArrayLength = 10000;
        private const int MinValue = 0;
        private const int MaxValue = 100;

        private static readonly object _lockObject = new object();

        private static int _minimalValue = int.MaxValue;
        private static int _minimalValueIndex = -1;
        private static int _threadsEnded = 0;
        private static int[] _array;

        static void Main(string[] args)
        {
            _array = CreateArr(ArrayLength, new Random(), MinValue, MaxValue);
            Thread[] threads = new Thread[ThreadsCount];

            int step = ArrayLength / ThreadsCount,
                lastIndex = 0;

            for (int i = 0; i < ThreadsCount - 1; i++)
            {
                threads[i] = new Thread(FindMinimalElement);
                threads[i].Start(new Range(lastIndex, lastIndex + step));
                lastIndex += step;
            }
            threads[threads.Length - 1] = new Thread(FindMinimalElement);
            threads[threads.Length - 1].Start(new Range(lastIndex, _array.Length));

            lock (_lockObject)
            {
                while(_threadsEnded < ThreadsCount)
                {
                    Monitor.Wait(_lockObject);
                }
            }

            Console.WriteLine($"Minimal element: {_minimalValue}; minimal element`s index: {_minimalValueIndex}");
            Console.ReadKey();
        }

        static int[] CreateArr(int upperArrayBorder, Random rand, int minValue, int maxValue)
        {
            int[] array = new int[upperArrayBorder];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(minValue, maxValue);
            }

            array[rand.Next(0, array.Length)] = int.MinValue;

            return array;
        }

        static void FindMinimalElement(object range)
        {
            if(range is Range)
            {
                int localMinimalElement = int.MaxValue;
                int localMinimalElementIndex = -1;

                for (int i = (range as Range).LowerBorder; i < (range as Range).UpperBorder; i++)
                {
                    if (_array[i] < localMinimalElement)
                    {
                        localMinimalElement = _array[i];
                        localMinimalElementIndex = i;
                    }
                }

                lock (_lockObject)
                {
                    if (localMinimalElement < _minimalValue)
                    {
                        _minimalValue = localMinimalElement;
                        _minimalValueIndex = localMinimalElementIndex;
                    }
                    _threadsEnded++;
                    Monitor.Pulse(_lockObject);
                }
            }
        }
    }
}