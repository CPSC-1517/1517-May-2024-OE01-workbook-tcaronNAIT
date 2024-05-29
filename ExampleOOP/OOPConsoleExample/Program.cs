using ExampleOOP;

List<Employment> employments = CreateCollection();
foreach (var item in employments)
{
    Console.WriteLine($"{item.ToString()}");
}
//Count the number of elements - Use the Count Property not the Count() Method.
Console.WriteLine();
Console.WriteLine($"The number of elements in the collection is {employments.Count}.");

//Use loops with Collections - Find a title that matches a specific value
Console.WriteLine();
Employment employment = null;
foreach(var item in employments)
{
    if(item.Title.Equals("PG II"))
        employment = item;
}
if (employment != null)
    Console.WriteLine($"PG II instance found {employment.ToString()} with a loop!");

//Use Linq to do the same thing :)
Console.WriteLine();
employment = null;
employment = employments.Find(x => x.Title.Equals("PG II"));
if (employment != null)
    Console.WriteLine($"PG II instance found {employment.ToString()} with linq Find()!");
else
    Console.WriteLine("No PG II instance found.");

//Other Linq example - Typically use Find for this class
Console.WriteLine();
employment = null;
employment = employments.FirstOrDefault(x => x.Title.Contains("SP"));
if (employment != null)
    Console.WriteLine($"SP I instance found {employment.ToString()} with linq FirstOrDefault()!");
else
    Console.WriteLine("No SP I instance found.");

//Linq to return a collection from a collection
//Add in Logic to check if there is any matching elements or not!
if (employments.Any(x => x.Title.Contains("GG")))
{
    Console.WriteLine();
    var newEmployments = employments.Where(x => x.Title.Contains("PG")).ToList();
    Console.WriteLine("Instances with PG:");
    newEmployments.ForEach(x => Console.WriteLine(x.Title));
}
else
{
    Console.WriteLine("No instances found with PG.");
}

Console.ReadLine();


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
#endregion