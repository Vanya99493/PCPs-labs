public class CalculationInfo {
    public Integer _step;
    public Integer _calculationTime_ms;
    public Integer _id;

    public CalculationInfo(Integer step, Integer calculationTime_ms, Integer id){
        _step = step;
        _calculationTime_ms = calculationTime_ms;
        _id = id;
    }

    public Integer GetStep() {
        return _step;
    }

    public Integer GetId() {
        return _id;
    }

    public Integer GetCalculationTime_ms() {
        return _calculationTime_ms;
    }
}