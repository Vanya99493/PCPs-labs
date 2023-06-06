import java.util.Random;

public class Main {
    private static final Integer THREADS_COUNT = 8;
    private static final Integer ARRAY_LENGTH = 10000;
    private static final Integer MIN_VALUE = 0;
    private static final Integer MAX_VALUE = 100;

    private static Integer _minimalValue = Integer.MAX_VALUE;
    private static Integer _minimalValueIndex = -1;
    private static Integer _threadsEnded = 0;
    private static Integer[] _array;

    public static void main(String[] args) {
        _array = fillArray();
        Main main = new Main();

        Integer step = ARRAY_LENGTH / THREADS_COUNT,
                lastIndex = 0;

        for (Integer i = 0; i < THREADS_COUNT - 1; i++){
            Searcher searcher = new Searcher(lastIndex, lastIndex + step, main);
            searcher.start();
            lastIndex += step;
        }
        Searcher searcher = new Searcher(lastIndex, _array.length, main);
        searcher.start();

        main.waitForFindingMinimalElement();

        System.out.println("Minimal element: " + _minimalValue + "; minimal element`s index: " + _minimalValueIndex);
    }

    private static Integer[] fillArray(){
        Integer[] array = new Integer[ARRAY_LENGTH];
        Random rand = new Random();
        for (Integer i = 0; i < array.length; i++){
            array[i] = rand.nextInt(MAX_VALUE - MIN_VALUE) + MIN_VALUE;
        }

        array[rand.nextInt(array.length)] = Integer.MIN_VALUE;

        return array;
    }

    public static Integer getArrayElement(Integer index){
        return _array[index];
    }

    synchronized public void changeMinimalValue(Integer newMinimalValue, Integer minimalElementIndex){
        if(_minimalValue > newMinimalValue){
            _minimalValue = newMinimalValue;
            _minimalValueIndex = minimalElementIndex;
        }
        _threadsEnded++;
        notify();
    }

    synchronized public void waitForFindingMinimalElement(){
        while(_threadsEnded < THREADS_COUNT){
            try {
                wait();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }
}