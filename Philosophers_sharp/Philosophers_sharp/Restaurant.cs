using System;
using System.Threading;

namespace Philosophers_sharp
{
    public class Restaurant
    {
        private Philosopher[] _philosophers;
        private Table _table;
        private Personal _personal;

        public Restaurant(int philosophersCount, int waitersCount)
        {
            _philosophers = new Philosopher[philosophersCount];
            _table = new Table(philosophersCount);
            _personal = new Personal(waitersCount, philosophersCount);
        }

        public void Start(int lapsCount, int thinkingTime_ms, int eatingTime_ms)
        {
            for (int i = 0; i < _philosophers.Length; i++)
            {
                _philosophers[i] = new Philosopher(
                    i, 
                    (i + 1) % _philosophers.Length, 
                    i, 
                    _table, 
                    _personal,
                    lapsCount, 
                    thinkingTime_ms, 
                    eatingTime_ms
                    );

                new Thread(_philosophers[i].Start).Start();
            }

            Print();
        }

        private void Print()
        {
            while (true)
            {
                Console.Clear();
                for (int i = 0; i < Counter.Counters.Length; i++)
                {
                    if (_personal.Get(i))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("T\t");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("F\t");
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Counter.Print();

                Thread.Sleep(110);
            }
        }
    }
}