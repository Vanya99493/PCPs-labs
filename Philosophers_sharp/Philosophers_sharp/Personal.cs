using System;
using System.Threading;

namespace Philosophers_sharp
{
    public class Personal
    {
        private Semaphore _waiters;
        private bool[] _canServesPhilosopher;
        private int _philosophersCount;

        public Personal(int waitersCount, int philosopherCount)
        {
            _waiters = new Semaphore(waitersCount, waitersCount);
            _canServesPhilosopher = new bool[philosopherCount];
            for (int i = 0; i < _canServesPhilosopher.Length; i++)
            {
                _canServesPhilosopher[i] = true;
            }
            _philosophersCount = philosopherCount;
        }

        public bool CallWaiter(int philosopherIndex)
        {
            _waiters.WaitOne();
            if (_canServesPhilosopher[(_philosophersCount + philosopherIndex - 1) % _philosophersCount] &&
                _canServesPhilosopher[(_philosophersCount + philosopherIndex + 1) % _philosophersCount])
            {
                _canServesPhilosopher[philosopherIndex] = false;
                return true;
            }
            _waiters.Release();
            return false;
        }

        public void RecallWaiter(int philosopherIndex)
        {
            _waiters.Release();
            _canServesPhilosopher[philosopherIndex] = true;
        }

        public bool Get(int i) => _canServesPhilosopher[i];
    }
}