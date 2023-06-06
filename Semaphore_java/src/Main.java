import java.util.concurrent.Semaphore;

public class Main {
    public static Integer Counter = 1;
    public static Semaphore CounterAccess = new Semaphore(1);

    private static Integer _storageSize = 4;
    private static Integer _itemsCount = 15;
    private static Integer _pickDelay_ms = 1000;
    private static Integer _pushDelay_ms = 0;

    private static Integer _producersCount = 2;
    private static Integer _customersCount = 5;

    public static void main(String[] args) {
        Storage storage = new Storage(_storageSize);

        Integer itemsOfOneProducer = _itemsCount / _producersCount;
        Integer itemsOfOneCustomer = _itemsCount / _customersCount;

        for(Integer i = 0; i < _producersCount - 1; i++){
            new Thread(new Producer(itemsOfOneProducer, _pushDelay_ms, storage)).start();
        }
        new Thread(new Producer(_itemsCount - (itemsOfOneProducer * (_producersCount - 1)), _pushDelay_ms, storage)).start();

        for(Integer i = 0; i < _customersCount - 1; i++){
            new Thread(new Customer(itemsOfOneCustomer, _pickDelay_ms, storage)).start();
        }
        new Thread(new Customer(_itemsCount - (itemsOfOneCustomer * (_customersCount - 1)), _pickDelay_ms, storage)).start();
    }
}