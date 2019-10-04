using System;
using System.Threading;

namespace AsynchronouseProgramming
{
    public class LambdaExpressionAndCapturedVars
    {
        static void Main()
        {
            for (int i = 0; i < 10; i++)
            {
                new Thread(
                    () => Console.Write(i+",")
                    ).Start();
            }
        }

        //FIX
        //static void Main()
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        int temp = i;
        //        new Thread(() => Console.Write(temp)).Start();
        //    }
        //}
    }
}
