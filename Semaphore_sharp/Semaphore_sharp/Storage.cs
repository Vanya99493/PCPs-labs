using System.Collections.Generic;
using System.Threading;

namespace Semaphore_sharp
{
    public class Storage
    {
        public Semaphore AccessToStorageSemaphore;
        public Semaphore PushItemSemaphore;
        public Semaphore PickItemSemaphore;

        private List<string> _storage;

        public Storage(int storageSize)
        {
            AccessToStorageSemaphore = new Semaphore(1 ,1);
            PushItemSemaphore = new Semaphore(storageSize, storageSize);
            PickItemSemaphore = new Semaphore(0, storageSize);

            _storage = new List<string>(storageSize);
        }

        public void PushItem(string itemName)
        {
            _storage.Add(itemName);
        }

        public string PickItem()
        {
            string itemName = _storage[0];
            _storage.RemoveAt(0);
            return itemName;
        }
    }
}