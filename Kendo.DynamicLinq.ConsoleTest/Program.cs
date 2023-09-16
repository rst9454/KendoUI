using System;
using System.Linq;

namespace Kendo.DynamicLinq.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var people = new[] {
                new Person { Age = 50,Name="Sunil Pandey" },
                new Person { Age = 20,Name="Nikita Pandey" },
                new Person { Age = 33,Name="Shahin Khatoon" }
            };
            var result = people.AsQueryable().ToDataSourceResult(1, 2, null, null, new[] { new Aggregator {
                    Aggregate = "max",
                    Field = "Age"
                } }, null);

            Console.WriteLine(result.Aggregates);
            Console.ReadKey();
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
