public class Restaurant {
    private Philosopher[] _philosophers;
    private Table _table;
    private Manager _manager;

    public Restaurant(Integer philosophersCount){
        _philosophers = new Philosopher[philosophersCount];
        _table = new Table(philosophersCount);
        _manager = new Manager(philosophersCount);
    }

    public void start(Integer lapsCount, Integer thinkingTime, Integer eatingTime){
        for (Integer i = 0; i < _philosophers.length; i++){
            _philosophers[i] = new Philosopher(
                i,
                (i+1) % _philosophers.length,
                i,
                _table,
                _manager,
                lapsCount,
                thinkingTime,
                eatingTime
                );

            new Thread(_philosophers[i]).start();
        }
        Print();
    }

    public void Print(){
        while(true){
            for(Integer i = 0; i < Main.Counter.length; i++){
                System.out.print(_manager.get(i) ? "T\t" : "F\t");
            }
            System.out.println();
            for(Integer i = 0; i < Main.Counter.length; i++){
                System.out.print(Main.Counter[i] + "\t");
            }
            System.out.println();
            System.out.println("----------------------------------");
            try {
                Thread.sleep(200);
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
        }
    }
}