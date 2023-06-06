using System;
using System.Collections.Generic;
using System.Threading;

namespace Semaphore_sharp
{
    internal class Program
    {
        public static Semaphore CounterAccess = new Semaphore(1, 1);
        public static int ItemsCounter = 1;

        private static int _storageSize = 5;
        private static int _itemsCount = 41;

        private static int _pickDelay_ms = 0;
        private static int _pushDelay_ms = 0;

        private static int _producersCount = 2;
        private static int _customersCount = 5;

        static void Main(string[] args)
        {
            Storage storage = new Storage(_storageSize);

            int itemsOfOneProducer = _itemsCount / _producersCount;
            int itemsOfOneCustomer = _itemsCount / _customersCount;

            for (int i = 0; i < _producersCount - 1; i++)
            {
                new Thread(new Producer(itemsOfOneProducer, _pushDelay_ms, storage).Start).Start();
            }
            new Thread(new Producer(_itemsCount - (itemsOfOneProducer * (_producersCount - 1)), _pushDelay_ms, storage).Start).Start();

            for (int i = 0; i < _customersCount - 1; i++)
            {
                new Thread(new Customer(itemsOfOneCustomer, _pickDelay_ms, storage).Start).Start();
            }
            new Thread(new Customer(_itemsCount - (itemsOfOneCustomer * (_customersCount - 1)), _pickDelay_ms, storage).Start).Start();

            Console.ReadKey();
        }
    }
}