/// <summary>
/// Async and Await seem to switch context and run on different thread after each await in ConsoleApp
/// However in WPF they run on single thread
/// Simon Binyamin
/// </summary>

using System;
using System.Threading;
using System.Threading.Tasks;

namespace concurrency_single_multi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Task<Car> carTask = DoSomethingAsync();
            Task<Car> carTask2 = DoSomethingAsync2();

            SomeMethod();

            Car car = await carTask;
            Car car2 = await carTask2;
        }

        private static void SomeMethod()
        {
            Task.Delay(6000).Wait();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("method " + Thread.CurrentThread.ManagedThreadId);
            }

        }

        static async Task<Car> DoSomethingAsync()
        {

            await Task.Delay(6000);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("car " + Thread.CurrentThread.ManagedThreadId);
            }

            return new Car { Id = 20 };
        }


        static async Task<Car> DoSomethingAsync2()
        {

            await Task.Delay(6000);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("car2  " + Thread.CurrentThread.ManagedThreadId);
            }

            return new Car { Id = 240 };
        }


    }

    internal class Car
    {
        public int Id { get; set; }
    }
}
