using System;
using System.Threading;

namespace AsynchronouseProgramming
{
    public class LocalVariablesWithinThreads
    {
        bool _done;
        static void Main()
        {
            LocalVariablesWithinThreads tt = new LocalVariablesWithinThreads();
            new Thread(tt.Go).Start();
            tt.Go();
        }

        // Create a common instance
        void Go() // Note that this is an instance method
        {
            if (!_done)
            {
                _done = true; Console.WriteLine("Done");
            }
        }
    }
}