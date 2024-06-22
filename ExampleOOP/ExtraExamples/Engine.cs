using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraExamples
{
    public class Engine
    {
        #region Properties
        public int Cylinders { get; set; }
        public int SparkPlugs { get; set; }
        public int Valves { get; set; }
        #endregion
        
        public Engine(int cylinders, int sparkPlugs, int valves) 
        {
            Cylinders = cylinders;
            SparkPlugs = sparkPlugs;
            Valves = valves;
        }
    }
}
