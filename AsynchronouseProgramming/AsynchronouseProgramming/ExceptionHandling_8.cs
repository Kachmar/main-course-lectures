using System;
using System.Threading;

namespace AsynchronouseProgramming
{
    /// <summary>
    /// As we can see from this example, that exception handling is hard, and returning values from thread is also hard
    ///  so lets switch to Tasks
    /// </summary>
    public class ExceptionHandling
    {
        public static void Main()
        {
            try
            {
                //Go();
                new Thread(Go).Start();
            }
            catch (Exception ex)
            {
                // We'll never get here!
                Console.WriteLine("Exception!");
            }
        }

        static void Go()
        {
            throw new Exception("Shit happens");
        }
    }
}
