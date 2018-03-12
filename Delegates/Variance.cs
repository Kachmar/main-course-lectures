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
    class Variance

    {
        static void Main(string[] args)
        {
            var comparer = new AnimalComparer();
            CompareBirds(comparer);

            Animal animal = Create(new BirdCreator());
            Console.ReadLine();
        }

        static void CompareBirds(ICustomComparer<Bird> comparer)
        {
            var bird1 = new Bird();
            var bird2 = new Bird();
            if (comparer.Compare(bird1, bird2) > 0)
                Console.WriteLine("first bird wins!");
        }

        static Animal Create(ICreator<Bird> creator)
        {
            return creator.Create();
        }
    }
    public interface ICreator<out T>
    {
        T Create();
    }

    class BirdCreator : ICreator<Bird>
    {
        public Bird Create()
        {
            return new Bird();
        }
    }

    public interface IIllegal<in T>
    {
        T SomeMethod(T param);
    }

    public interface ICustomComparer<in T>
    {
        int Compare(T x, T y);
    }

    public class AnimalComparer : ICustomComparer<Animal>
    {

        public int Compare(Animal x, Animal y)
        {
            return 1;
        }

    }
}
