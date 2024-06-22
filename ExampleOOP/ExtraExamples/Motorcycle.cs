using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraExamples
{
    public class Motorcycle : Vehicle
    {
        #region Properties
        public Engine Engine { get; set; }
        #endregion
        public Motorcycle(Make make, string model, string favouriteSong, Engine engine, int wheels = 2)
        {
            Wheels = wheels;
            Make = make;
            Model = model;
            Engine = engine;
        }
    }
}
