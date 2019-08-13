﻿using System;
using System.Threading;

namespace AsynchronouseProgramming
{
    public class BackgroundThreads
    {
        static void Main(string[] args)
        {
            Thread worker = new Thread(() => Console.ReadLine());
            if (args.Length > 0) worker.IsBackground = true;
            worker.Start();
        }
    }
}
