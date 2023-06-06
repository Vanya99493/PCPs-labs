import java.util.concurrent.Semaphore;

public class Table {
    private Semaphore[] _forks;

    public Table(Integer forksCount){
        _forks = new Semaphore[forksCount];
        for (Integer i = 0; i < _forks.length; i++){
            _forks[i] = new Semaphore(1);
        }
    }

    public void acquireFork(Integer index){
        try {
            _forks[index].acquire();
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        }
    }

    public void releaseFork(Integer index){
        _forks[index].release();
    }
}