public class BreakerElementInformation implements Comparable<BreakerElementInformation> {
    private Integer _calculationTime_ms;
    private Integer _id;
    private Boolean _isStop;

    public BreakerElementInformation(Integer calculationTime_ms, Integer id){
        _calculationTime_ms = calculationTime_ms;
        _id = id;
        _isStop = false;
    }

    public void Stop(){
        _isStop = true;
    }

    public Integer GetCalculationTime_ms(){
        return _calculationTime_ms;
    }

    public Integer GetId(){
        return _id;
    }

    public Boolean GetIsStop(){
        return _isStop;
    }

    @Override
    public int compareTo(BreakerElementInformation el) {
        return _calculationTime_ms.compareTo(el.GetCalculationTime_ms());
    }
}