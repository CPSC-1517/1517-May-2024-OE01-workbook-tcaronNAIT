using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleOOP
{
    public class Person
    {
        #region Data Members
        private string _firstName;
        private string _lastName;
        #endregion

        #region Properties
        public string FirstName
        {
            get { return _firstName; }
            set 
            {
                //first name is required
                if(string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("First name is required");
                }
                _firstName = value.Trim();
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = string.IsNullOrEmpty(value) ? value : value.Trim();
            }
        }
        //AutoImplemented Properties
        public ResidentAddress ResidentAddress { get; set; }
        public List<Employment> EmploymentPositions { get; set; } = []; //[] mean new List<Employment>()

        //readonly property that return the person's fullname in the form last, first
        //public string FullName
        //{
        //    get { return $"_lastName, _firstName"; } 
        //}
        public string FullName => $"{_lastName}, {_firstName}";
        #endregion

        #region constructor
        public Person()
        {
            FirstName = "Unknown";
            LastName = "Unknown";
        }

        public Person(string firstName, string lastName, ResidentAddress address, List<Employment> employmentPositions)
        {
            if(address.Number <= 0)
            {
                throw new ArgumentException("Street Address cannot be 0 or negative.");
            }
            FirstName = firstName;
            LastName = lastName;
            ResidentAddress = address;
            if(employmentPositions != null)
            {
                EmploymentPositions = employmentPositions;
            }
        }
        #endregion

        #region Methods
        public void ChangeFullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void AddEmployment(Employment employment)
        {
            if(employment == null)
            {
                throw new ArgumentNullException("No employment data provided");
            }
            EmploymentPositions.Add(employment);
        }

        public override string ToString() => $"{FirstName},{LastName},{ResidentAddress.Number},{ResidentAddress.Street},{ResidentAddress.City},{ResidentAddress.Province},{ResidentAddress.PostalCode}";

        public static Person Parse(string item)
        {
            string[] parts = item.Split(',');

            if (parts.Length != 7)
            {
                throw new FormatException($"String not in the expected format. Missing or excessive value(s): {item}");
            }
            return new Person(parts[0], parts[1], new ResidentAddress(int.Parse(parts[2]), parts[3], parts[4], parts[5], parts[6]), null);
        }
        #endregion
    }
}
