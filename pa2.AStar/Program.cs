using pa2.AStar;

var (queensCount, fieldSize) = ConsoleHelper.GetDataFromConsole();
var generatedQueens = Search.GenerateInitialQueens(queensCount, fieldSize);
var startTime = DateTime.Now;
ConsoleHelper.Draw(generatedQueens, fieldSize);
var solution = Search.FindThePath(queensCount, fieldSize);
Console.WriteLine((DateTime.Now - startTime).TotalMilliseconds);
ConsoleHelper.Draw(solution.Queens, fieldSize);
