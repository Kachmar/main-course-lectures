using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Car> myCars = new List<Car>() {
                                                       new Car() { VIN="A1", Make = "BMW", Model= "550i", StickerPrice=55000, Year=2009},
                                                       new Car() { VIN="B2", Make="Toyota", Model="4Runner", StickerPrice=35000, Year=2010},
                                                       new Car() { VIN="C3", Make="BMW", Model = "745li", StickerPrice=75000, Year=2008},
                                                       new Car() { VIN="D4", Make="Ford", Model="Escape", StickerPrice=25000, Year=2008},
                                                       new Car() { VIN="E5", Make="BMW", Model="55i", StickerPrice=57000, Year=2010}
                                                   };


            //Simple LINQ
            var sameCars = from car in myCars
                           select car;

            // Filtering example
            //var bmws = from car in myCars
            //           where car.Make == "BMW"
            //           && car.Year == 2010
            //           select car;
            //var bmws = myCars.Where(p => p.Make == "BMW" && p.Year == 2010);

            //Sorting example
            //var orderedCars = from car in myCars
            //                  orderby car.Year descending
            //                  select car;
            //var orderedCars = myCars.OrderByDescending(p => p.Year);

            //foreach (var car in orderedCars)
            //{
            //    Console.WriteLine("{0} {1}", car.Year, car.Model, car.VIN);
            //}

            //complex queries
            var firstBMW = myCars.OrderByDescending(p => p.Year).First(p => p.Make == "BMW");
            Console.WriteLine(firstBMW.VIN);

            //Commonly used 'Any';
            var anyCheapCars = myCars.Any(car => car.StickerPrice < 10000);

            //All
            Console.WriteLine(myCars.All(p => p.Year > 2007));

            //When we want to take every item for read, or modification we use ForEach
            //myCars.ForEach(p => p.StickerPrice -= 3000);
            //myCars.ForEach(p => Console.WriteLine("{0} {1:C}", p.VIN, p.StickerPrice));

            //Console.WriteLine(myCars.Exists(p => p.Model == "745li"));

            //SUM
            //Console.WriteLine(myCars.Sum(p => p.StickerPrice));


            Console.WriteLine(myCars.GetType());
            var orderedCars = myCars.OrderByDescending(p => p.Year);
            Console.WriteLine(orderedCars.GetType());

            var bmws = myCars.Where(p => p.Make == "BMW" && p.Year == 2010);
            Console.WriteLine(bmws.GetType());

            //Anonymous types
            var newCars = from car in myCars
                          where car.Make == "BMW"
                                && car.Year == 2010
                          select new { car.Make, car.Model };

            Console.WriteLine(newCars.GetType());
            //Group By

            var queryByMake =
                from car in myCars
                group car by car.Make into newGroup
                select newGroup;

            foreach (var nameGroup in queryByMake)
            {
                Console.WriteLine("Key: {0}", nameGroup.Key);
                foreach (var car in nameGroup)
                {
                    Console.WriteLine($"\t{car.Make}, {car.Model}");
                }
            }
            //Lazy evaluation
            var bmwCars = myCars.Where(c => c.Make == "BMW");
            removeBrands(myCars);
            var anyBmw = bmwCars.Any(car => car.Make == "BMW");
            Console.WriteLine($"there are BMW: {anyBmw}");
            Console.ReadLine();

        }
        private static void removeBrands(List<Car> cars)
        {
            cars.ForEach(car => car.Make = "Anonymous");
        }
    }

    class Car
    {
        public string VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double StickerPrice { get; set; }
    }
}
