public class Philosopher extends Thread{
    private Integer _id;
    private Integer _leftForkId;
    private Integer _rightForkId;
    private Table _table;
    private Manager _manager;
    private Integer _lapsCount;
    private Integer _thinkingTime;
    private Integer _eatingTime;

    public Philosopher(Integer id,
                       Integer leftForkId,
                       Integer rightForkId,
                       Table table,
                       Manager manager,
                       Integer lapsCount,
                       Integer thinkingTime,
                       Integer eatingTime){
        _id = id;
        _leftForkId = leftForkId;
        _rightForkId = rightForkId;
        _table = table;
        _manager = manager;
        _lapsCount = lapsCount;
        _thinkingTime = thinkingTime;
        _eatingTime = eatingTime;
    }

    @Override
    public void run(){
        for (Integer i = 0; i < _lapsCount; i++){
            try {
                //System.out.println("Philosopher " + _id + " thinking");
                Thread.sleep(_thinkingTime);

                while(true){
                    Boolean canEating = _manager.canEating(_id);
                    if(canEating){
                        break;
                    }
                }
                _table.acquireFork(_leftForkId);
                _table.acquireFork(_rightForkId);

                //System.out.println("Philosopher " + _id + " eating");
                Thread.sleep(_eatingTime);
                Main.Counter[_id]++;

                _manager.stopEating(_id);
                _table.releaseFork(_leftForkId);
                _table.releaseFork(_rightForkId);
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
        }
    }
}