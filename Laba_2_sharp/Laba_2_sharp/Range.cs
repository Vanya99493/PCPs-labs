namespace Laba_2_sharp
{
    public class Range
    {
        public int LowerBorder { get; private set; }
        public int UpperBorder { get; private set; }

        public Range(int lowerBorder, int upperBorder)
        {
            LowerBorder = lowerBorder;
            UpperBorder = upperBorder;
        }
    }
}