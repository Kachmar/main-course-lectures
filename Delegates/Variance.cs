
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

    public class Hawk : Bird
    {

    }

    class VarianceAndContraVariance
    {
        static Bird bird1 = new Bird();
        static Bird bird2 = new Bird();

        static void Main(string[] args)
        {

            //Covariance
            object myObj = "string";
            Animal animal = new Bird();
            object obj = new Bird();


            //Contravariance
            Action<object> actObject = SetObject;
            Action<Bird> actString = actObject;
            actString.Invoke(new Bird());

            //Covariance in Generic interfaces
            IComparer<Bird> birdComparer = new AnimalComparer();
            birdComparer.Compare(bird1, bird2);


            ////Contravariance in Generic interfaces
            BirdCreator birdCreator = new BirdCreator();
            ContraVariance(birdCreator);

            Console.ReadLine();
        }


        private static void SetObject(object obj)
        {
            Console.WriteLine(obj);
        }

        public static void ContraVariance(ICreator<Animal> creator)
        {
            var animal = creator.Create();
        }
    }

    // "in" is missing
    public interface IComparer<in T> where T : Animal
    {
        bool Compare(T animal1, T animal2);
    }

    public class AnimalComparer : IComparer<Animal>
    {
        public bool Compare(Animal animal1, Animal animal2)
        {
            throw new NotImplementedException();
        }
    }

    // out missing
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