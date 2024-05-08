using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleOOP
{
    public class Employment
    {
        #region Data Members
        //fields, attributes, holds the pieces of data
        //data is valuable, data access should be restricted (private)
        //access or modification will have to be done by other code (components) of this class
        private string _title;
        private double _years;
        #endregion

        #region Properties
        //portal to the data, access cannot private
        //property is associated with a single piece of data
        //properties DO NOT have any parameters

        //fully implemented property
        //why?
        //it allows for additional logic to be included in the execution of the property

        public string Title
        {
            //access the data inside the instance
            get { return _title; }
            set 
            {
                //business rules
                //Title cannot be empty
                //Validate the incoming data
                if(string.IsNullOrWhiteSpace(value))
                {
                    //NO WRITE COMMAND
                    //use an exception
                    throw new ArgumentNullException("Title must be provided");
                }
                _title = value;
            }
        }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        #endregion
    }
}
