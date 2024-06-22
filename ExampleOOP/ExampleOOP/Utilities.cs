using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleOOP
{
    public static class Utilities
    {
        //static class does not need to be instantiated by the outside user (developer/code)
        //we reference static classes by using classname.xxxx
        //any methods in the static class still need to have the static keywords
        //DO NOT save any data to a static class

        const string AppName = "Bob and Son Sushi";

        //public static bool IsPositive(double value)
        //{
        //    //bool valid = true;
        //    //if (value < 0.0)
        //    //{
        //    //    return false;
        //    //    //valid = false;
        //    //}
        //    return value > 0.0;
        //    //return valid;
        //}

        public static bool IsPositive(double value) => value >= 0.0;
        public static bool IsPositive(int value) => value > 0;
        public static bool IsPositive(decimal value) => value > 0.0m;
    }
}