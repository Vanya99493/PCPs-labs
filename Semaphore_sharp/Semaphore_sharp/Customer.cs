using System;
using System.Threading;

namespace Semaphore_sharp
{
    public class Customer
    {
        private int _itemsCount;
        private int _pickDelay_ms;
        private Storage _storage;

        public Customer(int itemsCount, int pickDelay_ms, Storage storage)
        {
            _itemsCount = itemsCount;
            _pickDelay_ms = pickDelay_ms;
            _storage = storage;
        }

        public void Start()
        {
            for (int i = 0; i < _itemsCount; i++)
            {
                _storage.PickItemSemaphore.WaitOne();
                Thread.Sleep(_pickDelay_ms);
                _storage.AccessToStorageSemaphore.WaitOne();

                string item = _storage.PickItem();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Pick {item}");
                Console.ForegroundColor = ConsoleColor.Black;

                _storage.AccessToStorageSemaphore.Release();
                _storage.PushItemSemaphore.Release();
            }
        }
    }
}