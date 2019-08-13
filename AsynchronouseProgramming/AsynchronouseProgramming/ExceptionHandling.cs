using System;
using System.Threading;

namespace AsynchronouseProgramming
{
    public class ExceptionHandling
    {
        public static void Main()
        {
            try
            {
                new Thread(Go).Start();
            }
            catch (Exception ex)
            {
                // We'll never get here!
                Console.WriteLine("Exception!");
            }
        }
        static void Go() { throw null; }
    }
}
