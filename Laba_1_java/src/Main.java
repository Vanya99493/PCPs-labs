public class Main {
    private static CalculationInfo[] _calculationInfo1 = new CalculationInfo[]{
        new CalculationInfo(2, 3000, 1),
        new CalculationInfo(1, 2000, 2),
        new CalculationInfo(3, 4000, 3),
        new CalculationInfo(10, 1500, 4)
    };
    private static CalculationInfo[] _calculationInfo = new CalculationInfo[]{
        new CalculationInfo(2, 3000, 1),
        new CalculationInfo(1, 2000, 2)
    };

    public static void main(String[] args) {
        Breaker breaker = new Breaker(_calculationInfo);
        Calculator[] calculators = new Calculator[_calculationInfo.length];
        for (Integer i = 0; i < calculators.length; i++){
            calculators[i] = new Calculator(breaker, _calculationInfo[i]);
            calculators[i].start();
        }
        breaker.start();
    }
}