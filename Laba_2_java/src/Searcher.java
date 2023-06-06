public class Searcher extends Thread{
    private Integer _lowerBorder;
    private Integer _upperBorder;
    private Main _main;

    public Searcher(Integer lowerBorder, Integer upperBorder, Main main){
        _lowerBorder = lowerBorder;
        _upperBorder = upperBorder;
        _main = main;
    }

    @Override
    synchronized public void run(){
        Integer localMinimalElement = Integer.MAX_VALUE;
        Integer localMinimalElementIndex = -1;

        for (Integer i = _lowerBorder; i < _upperBorder; i++){
            if(Main.getArrayElement(i) < localMinimalElement){
                localMinimalElement = Main.getArrayElement(i);
                localMinimalElementIndex = i;
            }
        }

        _main.changeMinimalValue(localMinimalElement, localMinimalElementIndex);
    }
}
