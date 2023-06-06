public class Calculator extends Thread {
    private Double _sum;
    private Integer _count;
    private Breaker _breaker;
    private Integer _step;
    private Integer _calculationTime;
    private Integer _id;

    public Calculator(Breaker breaker, CalculationInfo info) {
        _breaker = breaker;
        _step = info.GetStep();
        _calculationTime = info.GetCalculationTime_ms();
        _id = info.GetId();
    }

    @Override
    public void run(){
        Calculate();
    }

    public void Calculate(){
        _sum = 0d;
        _count = 0;

        do{
            _sum += _step;
            _count++;
        }while(!_breaker.GetStopper(_id));

        System.out.println("Thread Id: " + _id + "; Sum: " + _sum + "; Step: " + _step + "; Count: " + _count + "; Calculation time: " + _calculationTime + " ms");
    }

    public Double GetSum(){
        return _sum;
    }

    public Integer GetCount(){
        return _count;
    }
}