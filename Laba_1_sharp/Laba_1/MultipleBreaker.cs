using System;
using System.Threading;

namespace Laba_1
{
    public class MultipleBreaker
    {
        private BreakerElementInformation[] _allBreakerElements;

        public MultipleBreaker(CalculateInfo[] calculateInfo)
        {
            _allBreakerElements = new BreakerElementInformation[calculateInfo.Length];

            for (int i = 0; i < _allBreakerElements.Length; i++)
            {
                _allBreakerElements[i] = new BreakerElementInformation(calculateInfo[i].CalculationTime_ms, calculateInfo[i].Id);
            }

            Array.Sort(_allBreakerElements);
        }

        public bool GetStopper(int threadId) 
        {
            foreach (BreakerElementInformation element in _allBreakerElements)
            {
                if(element.Id == threadId)
                {
                    return element.IsStop;
                }
            }

            return false;
        }

        public void Wait()
        {
            int sleptTime = 0;

            for (int i = 0; i < _allBreakerElements.Length; i++)
            {
                int sleepTime = _allBreakerElements[i].CalculationTime - sleptTime;
                Thread.Sleep(sleepTime);
                sleptTime += sleepTime;
                _allBreakerElements[i].Stop();
            }
        }
    }
}