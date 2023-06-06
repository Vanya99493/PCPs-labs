import java.lang.reflect.Array;
import java.util.Arrays;

public class Breaker extends Thread {
    private BreakerElementInformation[] _allBreakerElements;

    public Breaker(CalculationInfo[] calculationInfos){
        _allBreakerElements = new BreakerElementInformation[calculationInfos.length];

        for (Integer i = 0; i < _allBreakerElements.length; i++){
            _allBreakerElements[i] = new BreakerElementInformation(calculationInfos[i].GetCalculationTime_ms(), calculationInfos[i].GetId());
        }

        Arrays.sort(_allBreakerElements);
    }

    @Override
    public void run(){
        Wait();
    }

    public Boolean GetStopper(Integer id){
        for (BreakerElementInformation element : _allBreakerElements) {
            if (element.GetId() == id) {
                return element.GetIsStop();
            }
        }
        return false;
    }

    public void Wait(){
        Integer sleptTime = 0;

        for(Integer i = 0; i < _allBreakerElements.length; i++){
            Integer sleepTime = _allBreakerElements[i].GetCalculationTime_ms() - sleptTime;
            try {
                Thread.sleep(sleepTime);
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
            sleptTime += sleepTime;
            _allBreakerElements[i].Stop();
        }
    }
}