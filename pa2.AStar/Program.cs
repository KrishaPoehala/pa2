using pa2.AStar;

var (queensCount, fieldSize) = ConsoleHelper.GetDataFromConsole();
var generatedQueens = Search.GenerateInitialQueens(queensCount, fieldSize);

var startTime = DateTime.Now;
ConsoleHelper.Draw(generatedQueens, fieldSize);
var solution = Search.FindThePath(generatedQueens, queensCount, fieldSize);
foreach (var item in solution.Queens)
{
    Console.WriteLine(item);
}
Console.WriteLine((DateTime.Now - startTime).TotalMilliseconds);
ConsoleHelper.Draw(solution.Queens, fieldSize);
