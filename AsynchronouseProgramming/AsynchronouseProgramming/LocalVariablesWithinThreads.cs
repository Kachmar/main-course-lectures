using System;
using System.Threading;

namespace AsynchronouseProgramming
{
    public class LocalVariablesWithinThreads
    {
        bool _done;
        static void Main()
        {
            LocalVariablesWithinThreads localVariablesWithinThreads = new LocalVariablesWithinThreads();
            new Thread(localVariablesWithinThreads.Go).Start();
            localVariablesWithinThreads.Go();
        }

        // Create a common instance
        void Go() // Note that this is an instance method
        {
            if (!_done)
            {
                _done = true;
                Console.WriteLine("Done");
            }
        }
    }
}