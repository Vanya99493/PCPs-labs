public class Customer extends Thread{
    private final Integer _itemsCount;
    private final Integer _pushDelay_ms;
    private final Storage _storage;

    public Customer(int itemsCount, int pushDelay_ms, Storage storage) {
        _itemsCount = itemsCount;
        _pushDelay_ms = pushDelay_ms;
        _storage = storage;
    }

    @Override
    public void run(){
        for (Integer i = 0; i < _itemsCount; i++)
        {
            try {
                _storage.PickItemSemaphore.acquire();
                Thread.sleep(_pushDelay_ms);
                _storage.AccessToStorageSemaphore.acquire();

                String item = _storage.pickItem();
                System.out.println("Pick " + item);

                _storage.AccessToStorageSemaphore.release();
                _storage.PushItemSemaphore.release();
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
        }
    }
}