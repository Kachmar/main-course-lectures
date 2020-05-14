using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AsynchronouseProgramming
{

    /// The assignment is to create code that would output 123456789... to console
    /// Two threads should start in parallel calculating each part of the string 1234 and 56789...
    /// The order is important how threads append strings to 'result' variable.
    /// Tips: Use  new ManualResetEvent(false);    manualResetEvent.Set();  manualResetEvent.WaitOne();
    /// Use thread Join methods
    class TaskForStudents
    {
        public static string result = "";

        public static void Main()
        {
            SumCalculator sumCalculator = new SumCalculator();
            var first = new Thread(() => { sumCalculator.Calculate(1, 495, null); });
            var second = new Thread(() => { sumCalculator.Calculate(495, 500, null); });
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }

    public class SumCalculator
    {
        public void Calculate(int from, int to, ManualResetEvent manualResetEvent)
        {
            string result = "";
            for (int i = from; i < to; i++)
            {
                result += i;
            }

            if (manualResetEvent != null)
            {
                manualResetEvent.WaitOne();
            }

            TaskForStudents.result += result;
        }
    }
}