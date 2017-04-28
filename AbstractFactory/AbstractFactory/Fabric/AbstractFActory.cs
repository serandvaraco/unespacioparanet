using AbstractFactory.BI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Fabric
{
    public abstract class ContinentFactory
    {
        public abstract Herbivore CreateHerbivore();
        public abstract Carnivore CreateCarnivore();

    }

    public class AfricaFactory : ContinentFactory {

        public override Carnivore CreateCarnivore()
        {
            return new Lion(); 
        }

        public override Herbivore CreateHerbivore()
        {
            return new Deer();
        }
    }

    class AmericaFactory : ContinentFactory
    {
        public override Herbivore CreateHerbivore()
        {
            return new Bison();
        }
        public override Carnivore CreateCarnivore()
        {
            return new Wolf();
        }
    }


}
