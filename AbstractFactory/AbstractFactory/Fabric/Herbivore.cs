using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.BI
{
    public abstract class Herbivore { }
    public abstract class Carnivore {
        public abstract void Eat(Herbivore h); 
    }

    /*Venado*/
    public class Deer : Herbivore      { }


    class Lion : Carnivore
    {
        public override void Eat(Herbivore h)
        {
            // Eat Wildebeest
            Console.WriteLine(this.GetType().Name +
      " eats " + h.GetType().Name);
        }
    }


    class Bison : Herbivore
    {
    }

    /// <summary>
      /// The 'ProductB2' class
      /// </summary>
    class Wolf : Carnivore
    {
        public override void Eat(Herbivore h)
        {
            // Eat Bison
            Console.WriteLine(this.GetType().Name +
      " eats " + h.GetType().Name);
        }
    }
}
