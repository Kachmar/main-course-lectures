using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class FuncPredicateAction

    {
        static void Main(string[] args)
        {
            MathInvoke(Add, 3, 5);
            MathInvoke(Substract, 10, 3);
            EvaluateInvoke(IsGreaterEnough, 500);
            Console.ReadLine();
        }

        public static string Add(int x, int y)
        {
            return (x + y).ToString();
        }

        public static string Substract(int x, int y)
        {
            return (x + y).ToString();
        }

        public static bool IsGreaterEnough(int x)
        {
            return x > 1000;
        }

        public static void MathInvoke(Func<int, int, string> mathFunc, int x, int y)
        {
            Console.WriteLine(mathFunc.Invoke(x, y));
        }

        public static void EvaluateInvoke(Predicate<int> predicateFunc, int x)
        {
            Console.WriteLine($"is great enough: {predicateFunc.Invoke(x)}");
        }
    }
}
