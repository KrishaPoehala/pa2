using pa2.Ldfs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace pa2.Tests;

public class LdfsTests
{

    [Fact]
    public void GivesTheRightPath_WhenTheInputIsCorrect()
    {
        var ldfs = new LdfsSearch(6, 6);

        ldfs.Search(GetInitial());
        File.WriteAllLines("path.txt", ldfs.Queens.Select(x => x.ToString()));

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
       new(5, 3,0)  ,
       new(4, 0,0)  ,
       new(3, 4,0)    ,
       new(2, 1,0)    ,
       new(1, 5,0)    ,
       new(0, 2,0)   ,
    };


}
