using System.Globalization;
using NUnit.Framework;

namespace Tests;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.All)]
public class Fun_with_dates
{

    [Test]
    public void Local()
    {
        var rightNow = DateTime.Now;
        TestContext.WriteLine(rightNow.ToString(CultureInfo.InvariantCulture));
        TestContext.WriteLine(rightNow.ToString());
        TestContext.WriteLine(rightNow.ToString("o"));
        TestContext.WriteLine(rightNow.Date.ToString(""));
        TestContext.WriteLine(rightNow.TimeOfDay.ToString());
    }
    [Test]
    public void Utc()
    {
        var rightNow = DateTime.UtcNow;
        TestContext.WriteLine(rightNow.ToString(CultureInfo.InvariantCulture));
        TestContext.WriteLine(rightNow.ToString());
        TestContext.WriteLine(rightNow.ToString("o"));
        TestContext.WriteLine(rightNow.Date.ToString(""));
        TestContext.WriteLine(rightNow.TimeOfDay.ToString());
    }
    
    [Test]
    public void dateWithTimezone()
    {
        var rightNow = DateTimeOffset.Now;
        TestContext.WriteLine(rightNow.ToString(CultureInfo.InvariantCulture));
        TestContext.WriteLine(rightNow.ToString());
        TestContext.WriteLine(rightNow.ToString("o"));
        TestContext.WriteLine(rightNow.Date.ToString(""));
        TestContext.WriteLine(rightNow.TimeOfDay.ToString());
    }
}