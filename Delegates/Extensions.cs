using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    public class Animal
    {

    }

    public class Bird : Animal
    {

    }

    class Extensions

    {
        static void Main(string[] args)
        {
            Bird bird = new Bird();
            Console.WriteLine(bird.GetBirdSong());
            Console.ReadLine();
        }
    }
    public static class BirdExtensions
    {
        public static string GetBirdSong(this Bird bird)
        {
            return "Tweet-Tweet";
        }
    }
}
