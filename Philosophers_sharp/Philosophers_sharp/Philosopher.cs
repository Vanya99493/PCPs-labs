using System;
using System.Threading;

namespace Philosophers_sharp
{
    public class Philosopher
    {
        private int _id;
        private int _leftForkId;
        private int _rightForkId;
        private Table _table;
        private Personal _personal;
        private int _lapsCount;
        private int _thinkingTime_ms;
        private int _eatingTime_ms;

        public Philosopher(int id, int leftForkId, int rightForkId, Table table, Personal personal, int lapsCount, int thinkingTime_ms, int eatingTime_ms)
        {
            _id = id;
            _leftForkId = leftForkId;
            _rightForkId = rightForkId;
            _table = table;
            _personal = personal;
            _lapsCount = lapsCount;
            _thinkingTime_ms = thinkingTime_ms;
            _eatingTime_ms = eatingTime_ms;
        }

        public void Start()
        {
            for (int i = 0; i < _lapsCount; i++)
            {
                //Console.WriteLine($"Philosopher {_id} thinking");
                Thread.Sleep(_thinkingTime_ms);

                while (true)
                {
                    bool canStop = _personal.CallWaiter(_id);
                    if (canStop)
                    {
                        break;
                    }
                }
                _table.WaitOneFork(_leftForkId);
                _table.WaitOneFork(_rightForkId);

                //Console.WriteLine($"Philosopher {_id} eating");
                Thread.Sleep(_eatingTime_ms);
                Counter.Counters[_id]++;

                _personal.RecallWaiter(_id);
                _table.ReleaseFork(_leftForkId);
                _table.ReleaseFork(_rightForkId);
            }
        }
    }
}