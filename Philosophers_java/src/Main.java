public class Main {
    public static Integer[] Counter;

    private static Integer _philosophersCount = 5;
    private static Integer _lapsCount = 10;
    private static Integer _thinkingTime = 1000;
    private static Integer _eatingTime = 1000;

    public static void main(String[] args) {
        Counter = new Integer[_philosophersCount];
        for (Integer i = 0; i < Counter.length; i++) {
            Counter[i] = 0;
        }

        Restaurant restaurant = new Restaurant(_philosophersCount);
        restaurant.start(_lapsCount, _thinkingTime, _eatingTime);
    }
}