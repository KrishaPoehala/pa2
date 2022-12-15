using Xunit;

namespace pa2.Tests;

public class LdfsTests
{

    [Fact]
    public void GivesTheRightPath_WhenTheInputIsCorrect()
    {
        
        var ldfs = new LdfsSearch(6, 6);
        ldfs.GenerateInitialQueens();
        ldfs.Search();

        for (int i = 0; i < ldfs.Queens.Count; i++)
        {
            var (x, y, _) = ldfs.Queens[i];
            Assert.Equal(1, ldfs.PlayField[y][x]);
        }
    }
}
