using System.Threading;

namespace Philosophers_sharp
{
    public class Table
    {
        private Semaphore[] _forks;

        public Table(int forksCount)
        {
            _forks = new Semaphore[forksCount];
            for (int i = 0; i < _forks.Length; i++)
            {
                _forks[i] = new Semaphore(1, 1);
            }
        }

        public void WaitOneFork(int index)
        {
            _forks[index].WaitOne();
        }

        public void ReleaseFork(int index)
        {
            _forks[index].Release();
        }
    }
}