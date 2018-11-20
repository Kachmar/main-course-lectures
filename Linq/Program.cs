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
            Owner ownerMax = new Owner() { Country = "Usa", Name = "Max" };
            Owner ownerPetro = new Owner() { Country = "Ukraine", Name = "Petro" };
            Owner ownerSuzy = new Owner() { Country = "Belarus", Name = "Suzy" };
            List<Car> myCars = new List<Car>() {
                                                       new Car()
                                                           {
                                                               VIN="A1", Make = "BMW", Model= "550i", StickerPrice=55000, Year=2009,
                                                               Owner = ownerSuzy,
                                                           },
                                                       new Car()
                                                           {
                                                               VIN="B2", Make="Toyota", Model="4Runner", StickerPrice=35000, Year=2010,
                                                               Owner = ownerSuzy
                                                           },
                                                       new Car()
                                                           {
                                                               VIN="C3", Make="BMW", Model = "745li", StickerPrice=75000, Year=2008,
                                                               Owner = ownerMax
                                                           },
                                                       new Car()
                                                           {
                                                               VIN="D4", Make="Ford", Model="Escape", StickerPrice=25000, Year=2008,
                                                               Owner = ownerPetro
                                                           },
                                                       new Car()
                                                           {
                                                               VIN="E5", Make="BMW", Model="55i", StickerPrice=57000, Year=2010,
                                                               Owner = ownerMax
                                                           }
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
            var orderedCars = from car in myCars
                              orderby car.Year descending
                              select car;
            //var orderedCars = myCars.OrderByDescending(p => p.Year);

            //foreach (var car in orderedCars)
            //{
            //    Console.WriteLine("{0} {1}", car.Year, car.Model, car.VIN);
            //}

            //complex queries
            var firstBMW = myCars.OrderByDescending(p => p.Year).First(p => p.Make == "BMW");
            Console.WriteLine(firstBMW.VIN);

            //I want to merge all the string from the array to single string with some transformation 
            //Aggregate  
            string[] MySkills = {
                                        "C#.net",
                                        "Asp.net",
                                        "MVC",
                                        "Linq",
                                        "EntityFramework",
                                        "Swagger",
                                        "Web-Api",
                                        "OrchardCMS",
                                        "Jquery",
                                        "Sqlserver",
                                        "Docusign"
                                    };
            var commaSeperatedString = MySkills.Aggregate((current, next) => current + ", " + next);


            //Split
            string[] splitValues = commaSeperatedString.Split(',');

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
            //var orderedCars = myCars.OrderByDescending(p => p.Year);
            Console.WriteLine(orderedCars.GetType());

            var bmws = myCars.Where(p => p.Make == "BMW" && p.Year == 2010);
            Console.WriteLine(bmws.GetType());

            //Anonymous types
            var newCars = from car in myCars
                          where car.Make == "BMW"
                                && car.Year == 2010
                          select new { car.Make, car.Model, IsNew = true };

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

            //I have a queue of car buyers from Ukraine and they should become new owners of the cars
            //ZIP
            Owner[] newOwner = new Owner[] { new Owner() { Country = "Ukraine", Name = "Andrii" },
                                                   new Owner() { Country = "Ukraine", Name = "Vasyl"},
                                                   new Owner() { Country = "Ukraine", Name = "Mykola" }
                                               };
            //var result = newOwner.Zip(
            //    myCars,
            //    (owner, car) =>
            //    {
            //        car.Owner = owner;
            //        return car;
            //    });
            //Lets display new car and their owners


            //Enumerable.Range
            foreach (var i in Enumerable.Range(1, 3))
            {
                Console.WriteLine(myCars[i].Model);
            }

            //I want to get a list of all car owners given the myCars collection
            //SelectMany
            var allOwners = myCars.SelectMany((car, i) => car.AllOwners);


            //Single car
            var singleCar = myCars.Single(car => car.Model == "BMW");
            var firstCar = myCars.First(car => car.Make == "BMW");
            Console.ReadLine();
        }

        private static void removeBrands(List<Car> cars)
        {
            cars.ForEach(car => car.Make = "Anonymous");
        }
    }

    class Car
    {
        private Owner owner;
        public Car()
        {
            AllOwners = new List<Owner>();
        }
        public string VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public double StickerPrice { get; set; }

        public Owner Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
                AllOwners.Add(value);
            }
        }

        public List<Owner> AllOwners { get; set; }
    }

    class Owner
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
