using ExampleOOP;
using System.Text.Json;

//List<Employment> employments = CreateCollection();
//foreach (var item in employments)
//{
//    Console.WriteLine($"{item.ToString()}");
//}
////Count the number of elements - Use the Count Property not the Count() Method.
//Console.WriteLine();
//Console.WriteLine($"The number of elements in the collection is {employments.Count}.");

////Use loops with Collections - Find a title that matches a specific value
//Console.WriteLine();
//Employment employment = null;
//foreach(var item in employments)
//{
//    if(item.Title.Equals("PG II"))
//        employment = item;
//}
//if (employment != null)
//    Console.WriteLine($"PG II instance found {employment.ToString()} with a loop!");

////Use Linq to do the same thing :)
//Console.WriteLine();
//employment = null;
//employment = employments.Find(x => x.Title.Equals("PG II"));
//if (employment != null)
//    Console.WriteLine($"PG II instance found {employment.ToString()} with linq Find()!");
//else
//    Console.WriteLine("No PG II instance found.");

////Other Linq example - Typically use Find for this class
//Console.WriteLine();
//employment = null;
//employment = employments.FirstOrDefault(x => x.Title.Contains("SP"));
//if (employment != null)
//    Console.WriteLine($"SP I instance found {employment.ToString()} with linq FirstOrDefault()!");
//else
//    Console.WriteLine("No SP I instance found.");

////Linq to return a collection from a collection
////Add in Logic to check if there is any matching elements or not!
//if (employments.Any(x => x.Title.Contains("PG")))
//{
//    Console.WriteLine();
//    var newEmployments = employments.Where(x => x.Title.Contains("PG")).ToList();
//    Console.WriteLine("Instances with PG:");
//    newEmployments.ForEach(x => Console.WriteLine(x.Title));
//}
//else
//{
//    Console.WriteLine("No instances found with PG.");
//}

//Console.ReadLine();


#region Methods
List<Employment> CreateCollection()
{
    List<Employment> newCollection = new List<Employment>();
    newCollection.Add(new Employment("PG I", SupervisoryLevel.Entry,
                        DateTime.Parse("May 1, 2010"), 0.5));
    newCollection.Add(new Employment("PG II", SupervisoryLevel.TeamMember,
                        DateTime.Parse("Nov 1, 2010"), 3.2));
    newCollection.Add(new Employment("PG III", SupervisoryLevel.TeamLeader,
                        DateTime.Parse("Jan 6, 2014"), 8.6));
    newCollection.Add(new Employment("SP I", SupervisoryLevel.Supervisor,
                        DateTime.Parse("Jul 22, 2022"), 1.8));
    return newCollection;
}

static void WriteCSV(string FILE_PATH_CSV, string employmentString)
{
    try
    {
        File.WriteAllText(FILE_PATH_CSV, employmentString);
        Console.WriteLine("Employments Saved");
    }
    catch
    {
        Console.WriteLine("Error writting CSV File");
    }
}
static List<Employment> ReadCSV(string FILE_PATH_CSV)
{
    List<Employment> newEmployments = [];
    foreach (var line in File.ReadLines(FILE_PATH_CSV).Where(x => !String.IsNullOrEmpty(x)))
    {
        bool successParse = Employment.TryParse(line, out Employment employmentResult);
        if (successParse)
        {
            newEmployments.Add(employmentResult);
        }
    }

    return newEmployments;
}
void WriteJson(string FILE_PATH_JSON)
{
    JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    List<Person> persons = [];
    ResidentAddress residentAddress = new ResidentAddress(12, "Maple St.", "Edmonton", "AB", "T5T 5T5");
    persons.Add(new Person("Bob", "Ross", residentAddress, CreateCollection()));
    persons.Add(new Person("Fred", "George", residentAddress, CreateCollection()));

    string personsJson = JsonSerializer.Serialize(persons, options);

    File.WriteAllText(FILE_PATH_JSON, personsJson);
}
#endregion

const string FILE_PATH_CSV = "../../../../../EmploymentList.txt";
const string FILE_PATH_JSON = "../../../../../EmploymentJson.json";

//Create Collection
List<Employment> employments = CreateCollection();

//Read and Write to CSV
string employmentString = string.Empty;
foreach (var employment in employments)
{
    employmentString = employmentString + employment.ToString() + "\n";
}

Console.WriteLine(employmentString);

WriteCSV(FILE_PATH_CSV, employmentString);

List<Employment> newEmployments = ReadCSV(FILE_PATH_CSV);

newEmployments.ForEach(x => Console.WriteLine(x.ToString()));

Console.WriteLine();
Console.WriteLine("JSON Example Area");

WriteJson(FILE_PATH_JSON);

List<Person> newPersons = [];
string jsonString = File.ReadAllText(FILE_PATH_JSON);

newPersons = JsonSerializer.Deserialize<List<Person>>(jsonString);


Console.ReadLine();

