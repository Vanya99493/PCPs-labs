using System;

namespace Laba_1
{
    public class Calculator
    {
        public double Sum { get; private set; }
        public int Count { get; private set; }

        private SingleBreaker _breaker;
        private MultipleBreaker _multipleBreaker;
        private bool _isSingleBreaker;
        private int _step;
        private int _calculateTime;
        private int _id;

        public Calculator(SingleBreaker breaker, CalculateInfo info)
        {
            _breaker = breaker;
            _step = info.Step;
            _calculateTime = info.CalculationTime_ms;
            _id = info.Id;
            _isSingleBreaker = true;
        }

        public Calculator(MultipleBreaker breaker, CalculateInfo info)
        {
            _multipleBreaker = breaker;
            _step = info.Step;
            _calculateTime = info.CalculationTime_ms;
            _id = info.Id;
            _isSingleBreaker = false;
        }

        public void Calculate()
        {
            Sum = 0;
            Count = 0;
            bool condition;

            do
            {
                Sum += _step;
                Count++;

                condition = _isSingleBreaker ? !_breaker.IsStoped : !_multipleBreaker.GetStopper(_id);
            } while (condition);

            Console.WriteLine($"Thread Id: {_id}; Sum: {Sum}; Step: {_step}; Count: {Count}; Calculation time: {_calculateTime} ms");
        }
    }
}