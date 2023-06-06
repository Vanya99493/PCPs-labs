using System;

namespace Laba_1
{
    public class BreakerElementInformation : IComparable<BreakerElementInformation>
    {
        public int CalculationTime { get; private set; }
        public int Id { get; private set; }

        public bool IsStop { get; private set; }

        public BreakerElementInformation(int calculationTime, int id, bool isStop = false)
        {
            CalculationTime = calculationTime;
            Id = id;
            IsStop = isStop;
        }

        public void Stop()
        {
            IsStop = true;
        }

        public int CompareTo(BreakerElementInformation other)
        {
            return CalculationTime.CompareTo(other.CalculationTime);
        }
    }
}