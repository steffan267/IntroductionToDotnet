using FluentAssertions;
using NUnit.Framework;

namespace Tests;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.All)]
public class Fun_with_timespan
{
    [Test]
    public void timebetween()
    {
        var now = DateTime.Now;
        var twoHoursInTheFuture = now.AddHours(2);
        var diff = twoHoursInTheFuture-now;
        diff.Should().Be(TimeSpan.FromHours(2));
        
        diff = now-twoHoursInTheFuture;
        diff.Should().Be(TimeSpan.FromHours(-2));
    }
    
    [Test]
    public void timebetween_in_the_past()
    {
        var now = DateTime.Now;
        var twoHoursInTheFuture = now.AddDays(2);
        var diff = twoHoursInTheFuture-now;
        diff.Should().Be(TimeSpan.FromDays(2));
    }
}