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
        private string _title = string.Empty;
        private double _years;
        private SupervisoryLevel _level;
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

        public double Years
        {
            get { return _years; }
            //years cannot be a negative
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException($"Years {value} is less than 0. Years must be positive");
                }
                _years = value;
            }
        }
        public SupervisoryLevel Level
        {
            get { return _level; }
            //a private set meanms that the property
            //can ONLY be set with code within the constructor
            //(when we create an instance of the object class)
            //OR with some other code method or another property
            private set
            {
                //validate if the value is an acceptable integer value
                // syntax: Enum.IsDefined(typeof(xxxx), value)
                //        xxxx is the Enum name
                if (!Enum.IsDefined(typeof(SupervisoryLevel), value))
                {
                    throw new ArgumentException($"Supervisory level is invalid {value}");
                    //throw new ArgumentException(string.Format("Supervisory level is invalid {0}", value));
                }
            }
        }

        //property that is auto-implemented
        //no validation in this property
        //there is NO private member for this property
        //the system will generate storage for the data and
        //handles the setting and getting from that storage

        //auto-implemented properties can have public or privates set
        //the decision to have public or private sets is a design decision
        //the ONLY way to access or set a value from/to the property is 
        //through the property itself
        public DateTime StartDate { get; private set; }
        #endregion

        #region Constructors
        //the purpose of a constructor is to create an instance of your
        //class in a known state (this is important)
        //does every class need a constructor? NO
        // if a class doesn't a constructor then the default constructor is used
            // the system create an instance of the class and assigns default values
            //if you don't constructor the common wording is "using a system constructor"

        //If you code a constructor in your class you are now responsible
        //for any and all constructors in that class

        //default constructor
        //you can apply your own literal values for your data members or auto-implemented properties

        //constructor does not have a return datatype
        //Constructors cannot be called directly by a developer logic.
            //to call a constructor we have to use the "new" command.

        //no variables constructor
        public Employment()
        {
            Title = "Unknown";
            //by default the Level will get the 1st value of the Enum
            Level = SupervisoryLevel.TeamMember;
            StartDate = DateTime.Today;
        }

        //greedy constructor

        //a greedy constructor is one that accepts a parameter list of values
        //if you ever have any default values, you always have to put them last in the parameters list
            // when the user uses your constructor you are allowing them to not provide the parameters
            // with a default and assigning the value yourself if it is not provided.
        public Employment(string title, SupervisoryLevel level, DateTime startDate, double years = 0.0)
        {
            Title = title;
            Level = level;

            //you can add logic to your constructor to ensure the incoming value is valid
            //this is useful for auto-implemented properties
            if(startDate > DateTime.Today)
            {
                throw new ArgumentException($"The start date {startDate} is in the future");
            }
            StartDate = startDate;

            //you can also add additional logic to adjust your starting values
            //ensure the years is appropriate for the entered startDate
            if (years > 0.0)
            {
                Years = years;
            }
            else
            {
                TimeSpan span = DateTime.Now - StartDate;
                Years = Math.Round((span.Days / 365.25), 1);
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
