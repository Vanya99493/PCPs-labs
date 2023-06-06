using System.Threading;

namespace Laba_1
{
    public class SingleBreaker
    {
        public bool IsStoped { get; private set; }
        public int _timeToWait;

        public SingleBreaker(int timeToWait)
        {
            IsStoped = false;
            _timeToWait = timeToWait;
        }

        public void Wait()
        {
            Thread.Sleep(_timeToWait);
            IsStoped = true;
        }
    }
}