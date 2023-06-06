using System;
using System.Threading;

namespace Laba_1 
{
    public class Program
    {
        private static CalculateInfo[] calculateInfo = new CalculateInfo[]
        {
            new CalculateInfo(3, 5000, IdManager.GetId()),
            new CalculateInfo(1, 3000, IdManager.GetId()),
            new CalculateInfo(2, 4000, IdManager.GetId()),
            new CalculateInfo(2, 2000, IdManager.GetId()),
            new CalculateInfo(3, 5000, IdManager.GetId()),
            new CalculateInfo(1, 3000, IdManager.GetId()),
            new CalculateInfo(2, 4000, IdManager.GetId()),
            new CalculateInfo(2, 2000, IdManager.GetId())
        };
        private static CalculateInfo[] calculateInfo1 = new CalculateInfo[]
        {
            new CalculateInfo(3, 5000, IdManager.GetId()),
            new CalculateInfo(1, 3000, IdManager.GetId()),
            new CalculateInfo(2, 4000, IdManager.GetId()),
            new CalculateInfo(2, 2000, IdManager.GetId())
        };
        private static CalculateInfo[] calculateInfo2 = new CalculateInfo[]
        {
            new CalculateInfo(2, 4000, IdManager.GetId()),
            new CalculateInfo(2, 2000, IdManager.GetId())
        };

        private static void Main(string[] args)
        {
            Console.Write("1 - many breakers for many calculators;\n2 - one breaker for many calculators\nINPUT:\t");
            string command = Console.ReadLine();

            switch (command)
            {
                case "1":
                    SingleBreakerStart();
                    break;
                case "2":
                    MultipleBreakerStart();
                    break;
            }

            Console.ReadKey();
        }

        private static void SingleBreakerStart()
        {
            Calculator[] calculators = new Calculator[calculateInfo.Length];
            Thread[] threads = new Thread[calculateInfo.Length];
            for (int i = 0; i < calculateInfo.Length; i++)
            {
                SingleBreaker breaker = new SingleBreaker(calculateInfo[i].CalculationTime_ms);
                calculators[i] = new Calculator(breaker, calculateInfo[i]);
                threads[i] = new Thread(calculators[i].Calculate);
                threads[i].Start();
                new Thread(breaker.Wait).Start();
            }
        }

        private static void MultipleBreakerStart()
        {
            MultipleBreaker breaker = new MultipleBreaker(calculateInfo);

            Calculator[] calculators = new Calculator[calculateInfo.Length];
            Thread[] threads = new Thread[calculateInfo.Length];
            for (int i = 0; i < calculateInfo.Length; i++)
            {
                calculators[i] = new Calculator(breaker, calculateInfo[i]);
                threads[i] = new Thread(calculators[i].Calculate);
                threads[i].Start();
            }
            new Thread(breaker.Wait).Start();
        }
    }
}