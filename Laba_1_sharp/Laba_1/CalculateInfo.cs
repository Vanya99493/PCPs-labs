using System;

namespace Laba_1
{
    public class CalculateInfo
    {
        public int Step { get; private set; }
        public int CalculationTime_ms { get; private set; }
        public int Id { get; private set; }

        public CalculateInfo(int step, int calculationTime, int id)
        {
            Step = step;
            CalculationTime_ms = calculationTime;
            Id = id;
        }
    }
}