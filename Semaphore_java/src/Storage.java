import java.util.LinkedList;
import java.util.concurrent.Semaphore;

public class Storage {
    public final Semaphore AccessToStorageSemaphore;
    public final Semaphore PushItemSemaphore;
    public final Semaphore PickItemSemaphore;

    private LinkedList<String> _storage;

    public Storage(int storageSize){
        AccessToStorageSemaphore = new Semaphore(1);
        PushItemSemaphore = new Semaphore(storageSize);
        PickItemSemaphore = new Semaphore(0);

        _storage = new LinkedList<>();
    }

    public void pushItem(String itemName){
        _storage.add(itemName);
    }

    public String pickItem(){
        String itemName = _storage.get(0);
        _storage.removeFirst();
        return itemName;
    }
}