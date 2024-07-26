using FluentAssertions;
using ExampleOOP;

namespace TDDUnitTests
{
    public class Person_Should
    {
        #region Valid Data Tests
        //Attribute Title
        //Fact: one test, test body contains all the setup, execution,
        // and evaluation of the execution of a single test
        //Theory: allow for multiple excutions of the same test
            // Using different data inputs
            //To specify the different inputs we use [InlineData(...)] with the Theory attribute
            //We must have parameters to feed the InlineData into!

        //Testing our Constructors
        [Fact]
        public void Create_Instance_Using_Default_Constructor()
        {
            //Expectations
            string expectedFirstName = "Unknown";
            string expectedLastName = "Unknown";

            //Actions
            //sut: subject under test
            Person sut = new();

            //Assertions (evaluation of the results)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.ResidentAddress.Should().BeNull();
            sut.EmploymentPositions.Count().Should().Be(0);
        }

        [Fact]
        public void Create_Instance_Using_Greedy_Constructor_With_All()
        {
            //Arrange
            string firstName = "Unknown";
            string lastName = "Unknown";
            ResidentAddress residentAddress = new ResidentAddress(12, "Maple St.", "Edmonton", "AB", "T5T5T5");
            List<Employment> positions = CreatePositionsList();

            //Expectations
            string expectedFirstName = "Unknown";
            string expectedLastName = "Unknown";
            ResidentAddress expectedResidentAddress = new ResidentAddress(12, "Maple St.", "Edmonton", "AB", "T5T5T5");

            //Actions
            Person sut = new Person(firstName, lastName, residentAddress, positions);

            //Assertions
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.ResidentAddress.Should().Be(expectedResidentAddress);
            sut.EmploymentPositions.Should().HaveCount(3);
        }

        [Fact]
        public void Create_Instance_With_Greedy_Constructor_Without_Positions()
        {
            //Expectations
            string expectedFirstName = "Unknown";
            string expectedLastName = "Unknown";
            ResidentAddress expectedResidentAddress = new ResidentAddress(12, "Maple St.", "Edmonton", "AB", "T5T5T5");

            //Actions
            Person sut = CreatePersonInstanceWithoutPositions();

            //Assertions
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.ResidentAddress.Should().Be(expectedResidentAddress);
            sut.EmploymentPositions.Should().HaveCount(0);
        }

        //Testing our Properties (with Public Sets)
        [Fact]
        public void Change_FirstName()
        {
            //Arrange
            Person sut = CreatePersonInstanceWithoutPositions();
            string newFirstName = "Tina";

            //Expectations
            string expectedFirstName = "Tina";

            //Actions
            sut.FirstName = newFirstName;

            //Assert
            sut.FirstName.Should().Be(expectedFirstName);
        }

        [Fact]
        public void Change_LastName()
        {
            //Arrange
            Person sut = CreatePersonInstanceWithoutPositions();
            string newLastName = "Caron";

            //Exceptions
            string expectedLastName = "Caron";

            //Action
            sut.LastName = newLastName;

            //Asserts
            sut.LastName.Should().Be(expectedLastName);
        }

        [Fact]
        public void Change_ResidentAddress()
        {
            //Arrange
            Person sut = CreatePersonInstanceWithoutPositions();
            ResidentAddress newResidentAddress = null;

            //Expectation
            ResidentAddress expectedResidentAddress = null;

            //Action
            sut.ResidentAddress = newResidentAddress;

            //Assertion
            sut.ResidentAddress.Should().Be(expectedResidentAddress);
        }

        [Fact]
        public void Change_EmploymentPositions()
        {
            //Arrange
            Person sut = CreatePersonInstanceWithoutPositions();
            List<Employment> positions = CreatePositionsList();

            //Actions
            sut.EmploymentPositions = positions;

            //Assert
            sut.EmploymentPositions.Should().HaveCount(3);
        }

        [Fact]
        public void Return_Full_Name()
        {
            //Arrange
            string firstName = "Tina";
            string lastName = "Caron";
            Person sut = new Person(firstName, lastName, null, null);

            //Expectations
            string expectedFullName = $"{lastName}, {firstName}";

            //Assert
            sut.FullName.Should().Be(expectedFullName);
        }

        //Testing our Methods
        [Fact]
        public void ChangeFullName_Of_Person()
        {
            //Arrange
            Person sut = CreatePersonInstanceWithoutPositions();
            string newFirstName = "Tina";
            string newLastName = "Caron";

            //Exceptations
            string expectedFirstName = "Tina";
            string expectedLastName = "Caron";

            //Action
            sut.ChangeFullName(newFirstName, newLastName);

            //Asserts
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
        }

        [Fact]
        public void Add_Employment()
        {
            //Arrange
            Person sut = CreatePersonInstanceWithoutPositions();
            Employment newPosition = new Employment("Test 1", SupervisoryLevel.Supervisor, DateTime.Parse("2018/07/08"));
            
            //Action
            sut.AddEmployment(newPosition);

            //Assert
            sut.EmploymentPositions.Should().HaveCount(1);
        }
        #endregion

        #region Invalid Data Tests
        #endregion

        #region Utilities
        private static Person CreatePersonInstanceWithoutPositions()
        {
            string firstName = "Unknown";
            string lastName = "Unknown";
            ResidentAddress residentAddress = new ResidentAddress(12, "Maple St.", "Edmonton", "AB", "T5T5T5");
            Person sut = new Person(firstName, lastName, residentAddress, null);
            return sut;
        }
        private static List<Employment> CreatePositionsList()
        {
            Employment emp1 = new Employment("Title 1", SupervisoryLevel.Owner, DateTime.Parse("2015/04/17"));
            Employment emp2 = new Employment("Title 2", SupervisoryLevel.TeamLeader, DateTime.Parse("2016/06/17"));
            Employment emp3 = new Employment("Title 3", SupervisoryLevel.Entry, DateTime.Parse("2024/04/11"));
            List<Employment> positions = new List<Employment>() { emp1, emp2, emp3 };
            return positions;
        }
        #endregion
    }
}
