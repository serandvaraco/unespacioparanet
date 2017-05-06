using System;
using System.Collections.Generic;

namespace NetReflection
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                IDictionary<string, object> elements = new Dictionary<string, object>();
                elements.Add(new KeyValuePair<string, object>("number", 12));
                elements.Add(new KeyValuePair<string, object>("name", "serandvaraco"));

                var obj = elements.DictionaryToObject();  ;

                Console.WriteLine($"number is {obj.number}");
                Console.WriteLine($"name is {obj.name}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }

            Console.ReadKey(); 

        }
    }
}
