using pa2.Ldfs;
using System.Collections.Generic;
using Xunit;

namespace pa2.Tests;

public class LdfsTests
{

    [Fact]
    public void GivesTheRightPath_WhenTheInputIsCorrect()
    {
        var ldfs = new LdfsSearch(6, 6);

        ldfs.Search(GetInitial());

        Assert.Equal(GetExpected(), ldfs.Queens, new QueenEqualityComparer());
    }

    private List<Queen> GetInitial() => new()
    {
        new(0, 0,0),
        new(1, 2,0),
        new(2, 1,0),
        new(3, 4,0),
        new(4, 2,0),
        new(5, 0,0),
    };

    private List<Queen> GetExpected() => new()
    {
        new(0, 2, 0),
        new(1, 5, 0),
        new(2, 1, 0),
        new(3, 4, 0),
        new(4, 0, 0),
        new(5, 3, 0),
    };


}
