using pa2.AStar;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace pa2.Tests;

public class AStartTests
{
    [Fact]
    public void GivesASolution_WhenTheInputIsCorrect()
    {
        var astar = new AStarSearch(6, 6);
        var testqueens = GetInitial();
        var solution = astar.FindThePath(testqueens);
        Assert.Equal(GetExpected(), solution.Queens, new QueenEqualityComparer());
    }       

    [Fact]
    public void Throws_WhenTheInputIsIncorrect()
    {
        var astar = new AStarSearch(-1, 6);
        Assert.Throws<InvalidDataException>(() => astar.GenerateInitialQueens());
    }

    private List<Queen> GetInitial() => new()
    {
        new(0, 0),
        new(1, 2),
        new(2, 1),
        new(3, 4),
        new(4, 2),
        new(5, 0),
    };

    private List<Queen> GetExpected() => new()
    {
        new(0,2),
        new(1,5),
        new(2,1),
        new(3,4),
        new(4,0),
        new(5,3),
    };

}
