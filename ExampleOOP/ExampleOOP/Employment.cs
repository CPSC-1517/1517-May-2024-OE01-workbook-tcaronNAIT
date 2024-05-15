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
                if (!Utilities.IsPositive(value))
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

        //method syntax: accesslevel [override][static] rdt methodName([list of parameters])

        //you might need to dump the content of your instance into a string
        //there is a default method for every class but you might need to override it!

        //public override string ToString()
        //{
        //    //we mioght want to return a csv list of the data
        //    //we might want to return a certain format
        //    //ex: Project Leader II, TeamLeader, Sep 11, 2010, 13.8 (the comma in the date might be an issue)
        //        //correct it to not return the comma
        //        //ex: Project Leader II,TeamLeader,Sep 11 2010,13.8
        //    return $"{Title},{Level},{StartDate.ToString("MMM dd yyyy")},{Years.ToString()}";
        //}

        //Value = Value + 1;
        //Value += 1;

        public override string ToString() => $"{Title},{Level},{StartDate.ToString("MMM dd yyyy")},{Years.ToString()}";

        //Validation as well can be done in multiple places
        //Can validate in the property
        //Can validate in the constructor
        //Can validate in a method
        public void SetEmploymentResponsibilityLevel(SupervisoryLevel level)
        {
            //already validated in the property
            Level = level;
        }

        public void CorrectStartDate(DateTime startDate)
        {
            //since the startDate is only validated in the constructor (auto-implemented property)
            //Anytime we are setting it in a method we want to validate it

            if (startDate > DateTime.Today)
            {
                throw new ArgumentException($"The start date {startDate} is in the future");
            }
            StartDate = startDate;
        }

        public double UpdateCurrentEmploymentYearsExperience()
        {
            TimeSpan span = DateTime.Now - StartDate;
            Years = Math.Round((span.Days / 365.25), 1);
            return Years;
        }

        //Parsing(string)
        //attempot to change the content of the provided string value (csv) to another datatype

        //parsing for this class will assume the passed string value will resemble the greedy constructor
        //parsing methods contain basic validation on the number fields
            //for example: string 55 --> int.Parse(string); <-- successful
            // string bob --> int.Parse(string); <-- failure -- abort the parse with a message

        //If we can parse with ints why can't we parse our Employment class

        //this method will need to be called without an instance of the class existing!
        //this means the method must be static
        public static Employment Parse(string item)
        {
            //we have to make sure we break apart the item string
            //You could use a different delimator but we are using a comma as this is mimicking csv
            string[] parts = item.Split(',');

            //want to check if the right amount of parts are provided
            if(parts.Length != 4)
            {
                throw new FormatException($"String not in the expected format. Missing or excessive value(s): {item}");
            }

            //return an instance of the Employment class
            //take the separates parts and use them in the Constructor
            return new Employment(parts[0],
                                    (SupervisoryLevel)Enum.Parse(typeof(SupervisoryLevel), parts[1]),
                                    DateTime.Parse(parts[2]),
                                    double.Parse(parts[3]));
        }

        //we can also make a TryParse
        //the TryParse method will take in a string and return a bool value to say if the Parse was a success or not
        //the TryParse also outputs an instance of the class
            // Example xxxx.TryParse(string, out datatype parameterValue)

        public static bool TryParse(string item, out Employment result)
        {
            result = null;
            try
            {
                //use code I've already written
                result = Parse(item);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
