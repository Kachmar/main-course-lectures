using System;
using System.Threading;

class ThreadTest
{
    //static void Main()
    //{
    //    Thread t = new Thread(WriteY);
    //    t.Start();
    //    // Kick off a new thread
    //    // running WriteY()
    //    // Simultaneously, do something on the main thread.
    //    for (int i = 0; i < 1000; i++)
    //    {
    //        Console.Write("x");
    //    }
    //}
    //static void WriteY()
    //{
    //    for (int i = 0; i < 1000; i++)
    //    {
    //        Console.Write("y");
    //    }
    //}

    static void Main()
    {
        Thread t = new Thread(Go);
        t.Name = "TestName";
        t.Start();
        //try comment next line and see what happens
        t.Join();
        Console.WriteLine("Thread t has ended!");
    }
    static string Go()
    {
        return "Some result";
    }
}