using System;
using System.Threading;

namespace Semaphore_sharp
{
    public class Producer
    {
        private int _itemsCount;
        private int _pushDelay_ms;
        private Storage _storage;

        public Producer(int itemsCount, int pushDelay_ms, Storage storage)
        {
            _itemsCount = itemsCount;
            _pushDelay_ms = pushDelay_ms;
            _storage = storage;
        }

        public void Start()
        {
            for (int i = 0; i < _itemsCount; i++)
            {
                _storage.PushItemSemaphore.WaitOne();
                Thread.Sleep(_pushDelay_ms);
                _storage.AccessToStorageSemaphore.WaitOne();

                Program.CounterAccess.WaitOne();
                string item = $"Item {Program.ItemsCounter++}";
                Program.CounterAccess.Release();
                _storage.PushItem(item);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Push {item}");
                Console.ForegroundColor = ConsoleColor.Black;

                _storage.AccessToStorageSemaphore.Release();
                _storage.PickItemSemaphore.Release();
            }
        }
    }
}