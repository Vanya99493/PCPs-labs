import java.util.concurrent.Semaphore;

public class Manager {
    private Semaphore _manager;
    private Boolean[] _isEating;
    private Integer _eatingPhilosophers;

    public Manager(Integer philosophersCount){
        _manager = new Semaphore(1);
        _isEating = new Boolean[philosophersCount];
        for (Integer i = 0; i < _isEating.length; i++){
            _isEating[i] = false;
        }
        _eatingPhilosophers = 0;
    }

    public Boolean canEating(Integer philosopherIndex){
        try {
            _manager.acquire();
            if(_eatingPhilosophers >= _isEating.length - 1 && !_isEating[philosopherIndex]){
                return false;
            }
            if(!_isEating[philosopherIndex]){
                _isEating[philosopherIndex] = true;
                _eatingPhilosophers++;
                return true;
            }
            return false;
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        } finally {
            _manager.release();
        }
    }

    public void stopEating(Integer philosopherIndex){
        try {
            _manager.acquire();
            _isEating[philosopherIndex] = false;
            _eatingPhilosophers--;
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        } finally{
            _manager.release();
        }
    }

    public Boolean get(Integer index) {
        return _isEating[index];
    }
}