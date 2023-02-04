using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.All)]
public class Fun_with_linq_extensions
{
    private List<int> _list = Enumerable.Range(0, 10).ToList();
    
    [Test]
    public void First()
    {
        var firstElement = _list.ElementAt(0);
        var alsoFirst = _list.First();
        firstElement.Should().Be(alsoFirst);
    }

    [Test]
    public void Last()
    {
        var lastElement = _list.ElementAt(_list.Count - 1);
        var alsoLast = _list.Last();
        lastElement.Should().Be(alsoLast);
    }

    [Test]
    public void FirstOrDefault()
    {
        var first = _list.FirstOrDefault();
        first.Should().Be(_list.First());

        var emptyList = new List<int>();
        var zero = emptyList.FirstOrDefault();
        zero.Should().Be(0);

        var shouldBe4 = _list.First(x => x > 3);
        shouldBe4.Should().Be(4);

        var tooHight = _list.FirstOrDefault(x => x > 10);
        tooHight.Should().Be(0);

        var stringList = new List<string>
        {
            "bob",
            "lol"
        };

        var firstString = stringList.First();
        firstString.Should().Be("bob");

        var doenstExists = stringList.FirstOrDefault(x => x == "Hans");
        doenstExists.Should().BeNull();
    }


    [Test]
    public void Loops()
    {
        TestContext.WriteLine("old scool for loop 1:");
        for (var i = 0; i < _list.Count; i++)
        {
            TestContext.WriteLine(_list.ElementAt(i).ToString());
        }

        TestContext.WriteLine("old scool for loop 2:");

        var asArray = _list.ToArray();
        for (var i = asArray.Length - 1; i >= 0; i--)
        {
            TestContext.WriteLine(asArray[i].ToString());
        }

        TestContext.WriteLine("newer for loop 1:");
        foreach (var i in _list)
        {
            TestContext.WriteLine(i.ToString());
        }

        TestContext.WriteLine("Fancy for loop:");
        _list.ForEach(x => TestContext.WriteLine(x.ToString()));
    }

    [Test]
    public void Flatmap()
    {
        var list = new List<List<int>>();
        list.Add(new List<int> { 1, 2, 3, 4 });
        list.Add(new List<int> { 5, 6, 7, 8 });
        list.Add(new List<int> { 9, 10 });

        var oneList = list.SelectMany(x => x);
        TestContext.WriteLine(String.Join(",", oneList));
    }

    [Test]
    public void ToDictionary()
    {
        var oneList = _list.ToDictionary(x => x, x => x * 2);
        TestContext.WriteLine(String.Join(",", oneList));
    }

    [Test]
    public void GroupBy()
    {
        var oneList = _list.GroupBy(x => x % 2 == 1);
        TestContext.WriteLine(JsonConvert.SerializeObject(oneList, Formatting.Indented));
    }

    [Test]
    public void Sorting()
    {
        var randomNumbers = Enumerable.Range(0, 10).Select(x => Random.Shared.Next(0, 10)).ToList();
        TestContext.WriteLine(String.Join(",", randomNumbers));
        var orderedNumbers = randomNumbers.OrderByDescending(x => x);
        TestContext.WriteLine(String.Join(",", orderedNumbers));
        var orderedNumbersAsc = randomNumbers.OrderBy(x => x);
        TestContext.WriteLine(String.Join(",", orderedNumbersAsc));
    }

    [Test]
    public void Filtering()
    {
        var list = _list.Where(x => x > 5).ToList();
        TestContext.WriteLine(String.Join(",", list));
    }
    
    [Test]
    public void Projections()
    {
        // run a function on every element in the list
        // in this case we multiply every integer by 2 and return the result
        var list = _list.Select(x => x * 2).ToList();
        TestContext.WriteLine(String.Join(",", list));
    }

    [Test]
    public void Chaining()
    {
        // list of lists
        var lists = Enumerable.Range(0, 4).Select(x => Enumerable.Range(0, 4).ToList()).ToList();

        var groupBy = lists
            .SelectMany(x => x) // flatten multiple lists into 1
            .Where(x => x > 1)// filter out the elements we dont want
            .GroupBy(x => x % 2 == 1); // group them by odd and even
        TestContext.WriteLine(JsonConvert.SerializeObject(groupBy, Formatting.Indented));
    }
}