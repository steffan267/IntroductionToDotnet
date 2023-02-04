using FluentAssertions;
using NUnit.Framework;

namespace Tests;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.All)]
public class Fun_with_strings
{
    [Test]
    public void Conact1()
    {
        var fullString = "Hello " + "bob";
        fullString.Should().Be("Hello bob");
    }
    [Test]
    public void Conact2()
    {
        var name = "bob";
        var fullString = $"Hello {name}";
        fullString.Should().Be("Hello bob");
    }
    
    [Test]
    public void ComparingStrings()
    {
        ("bob" == "bob").Should().BeTrue();
        ("bob" == "lol").Should().BeFalse();
        (string.IsNullOrEmpty(null)).Should().BeTrue();
        (string.IsNullOrEmpty("")).Should().BeTrue();
        (string.IsNullOrEmpty(" ")).Should().BeFalse();
        (string.IsNullOrWhiteSpace(" ")).Should().BeTrue();
        (string.IsNullOrWhiteSpace("")).Should().BeTrue();
        (string.IsNullOrWhiteSpace(null)).Should().BeTrue();
    }
    [Test]
    public void string_join()
    {
        var strings = new List<string>
        {
            "bob",
            "lol",
            "hans"
        };

        var commaSeparatedString = string.Join(",",strings);
        commaSeparatedString.Should().Be("bob,lol,hans");
    }
}