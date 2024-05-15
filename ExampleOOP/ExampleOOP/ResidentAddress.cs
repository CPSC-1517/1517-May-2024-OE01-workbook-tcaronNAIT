using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleOOP
{
    public record ResidentAddress(int Number, string Street, string City, string Province, string PostalCode)
    {
        //this new record behave like a class
        //list the fields in the declaration of the record
        //this is basically a read-only version of a class
        //when declare the instance of the record (class) and pass in all of the information
        //But we cannot altewr the record instance once it's created

        //If you need to alter a record data you need to create a new instance of the record.

        //OPTIONALLY
        //you can add a greedy constructor to a record BUT the data values to be stores are the items in the record declaration
        //same parameter list, no other options

        //You can add methods to this record datatype THAT DO NOT alter the data values
        //These methods are returning a data value only

        public override string ToString() => $"{Number},{Street},{City},{Province},{PostalCode}";
    }
}
