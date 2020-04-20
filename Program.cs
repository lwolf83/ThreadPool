using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
namespace dojoExo2MultiThreading
{
    class Program
    {
        private static CountdownEvent _countdown;
        private static Int32 _threadsCount;
        public static ConcurrentQueue<int> numberList = new ConcurrentQueue<int>();
        public static Random random = new Random();
        public static int totalSum = 0;
        public static void Main(string[] args)
        {
            while(numberList.Count() < 1000000)
            {
                var program = new Program(3);
                program.Run();
                _countdown.Wait();
                Console.WriteLine(numberList.Count());
            }
            
            Console.ReadLine();

        }
        public Program(Int32 threadsCount)
        {
            _threadsCount = threadsCount;
            _countdown = new CountdownEvent(threadsCount); // Set the counter to the number of threads executing
        }
        public void Run()
        {

                for (int i = 0; i < _threadsCount; i++)
                {
                    ThreadPool.QueueUserWorkItem(x => AddNumber());
                }


        }
        public void AddNumber()
        {

            int randomNumber = random.Next(1, 50);
            numberList.Enqueue(randomNumber);
            _countdown.Signal();

        }
    }
}
