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
            BirdCreator birdCreator=new BirdCreator(  );

            ContraVariance( birdCreator );


            Console.ReadLine();
        }
        public static void ContraVariance( ICreator<Animal> creator )
        {
            creator.Create( );
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
}
