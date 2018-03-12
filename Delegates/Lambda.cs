using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class Lambda

    {
        static void Main(string[] args)
        {
            MathInvoke((int x, int y) => { return (x + y).ToString(); }, 10, 3);
            MathInvoke((int x, int y) => { return (x - y).ToString(); }, 10, 3);
            EvaluateInvoke((int x) => x > 1000, 500);
            Console.ReadLine();
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
