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
        var testqueens = GenerateInitial();
        var solution = Search.FindThePath(testqueens, 6, 6);
        Assert.Equal(ExpectedSolution(), solution.Queens, new QueenEqualityComparer());
    }

    [Fact]
    public void Throws_WhenTheInputIsIncorrect()
    {
        Assert.Throws<InvalidDataException>(() => Search.GenerateInitialQueens(-1, 2));
    }

    private List<Queen> GenerateInitial() => new()
    {
        new(0, 0),
        new(1, 2),
        new(2, 1),
        new(3, 4),
        new(4, 2),
        new(5, 0),
    };

    private List<Queen> ExpectedSolution() => new()
    {
        new(0,2),
        new(1,5),
        new(2,1),
        new(3,4),
        new(4,0),
        new(5,3),
    };

}
