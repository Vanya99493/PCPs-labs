public class Producer extends Thread {
    private final Integer _itemsCount;
    private final Integer _pushDelay_ms;
    private final Storage _storage;

    public Producer(int itemsCount, int pushDelay_ms, Storage storage) {
        _itemsCount = itemsCount;
        _pushDelay_ms = pushDelay_ms;
        _storage = storage;
    }

    @Override
    public void run(){
        for (Integer i = 0; i < _itemsCount; i++)
        {
            try {
                _storage.PushItemSemaphore.acquire();
                Thread.sleep(_pushDelay_ms);
                _storage.AccessToStorageSemaphore.acquire();

                Main.CounterAccess.acquire();
                String item = "item " + Main.Counter++;
                Main.CounterAccess.release();
                _storage.pushItem(item);
                System.out.println("Push " + item);

                _storage.AccessToStorageSemaphore.release();
                _storage.PickItemSemaphore.release();
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
        }
    }
}