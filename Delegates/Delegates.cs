using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class Delegates
    {
        public delegate void Printer();
        static void Main(string[] args)
        {
            TimeStampInvoke(JohnPrint);
            TimeStampInvoke( PeterPrint );
            Console.ReadLine( );
        }

        public static void JohnPrint()
        {
            Console.WriteLine("John");
        }

        public static void PeterPrint()
        {
            Console.WriteLine("Peter");
        }

        public static void TimeStampInvoke(Printer printer)
        {
            Console.WriteLine(DateTime.Now);
            printer.Invoke();
        }
    }
}
