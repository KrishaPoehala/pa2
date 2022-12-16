using pa2.AStar;
using System.IO;
using Xunit;

namespace pa2.Tests
{
    public class AStartTests
    {
        [Fact]
        public void GivesASolution_WhenTheInputIsCorrect()
        {
            var _ = Search.GenerateInitialQueens(6, 6);
            var solution = Search.FindThePath(6, 6);

            for (int i = 0; i < solution.Queens.Count; i++)
            {
                var x = solution.Queens[i].X;
                var y = solution.Queens[i].Y;
                Assert.Equal(1, solution.Map[x + y * 8]);
            }
        }

        [Fact]
        public void Throws_WhenTheInputIsIncorrect()
        {
            Assert.Throws<InvalidDataException>(() => Search.GenerateInitialQueens(-1,2));
        }
    }
}