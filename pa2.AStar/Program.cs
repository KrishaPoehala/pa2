using pa2.AStar;
using pa2.Common;

var helper = new ConsoleHelper();
var (queensCount, fieldSize) = helper.GetDataFromConsole();
var astar = new AStarSearch(queensCount, fieldSize);
var generatedQueens = astar.GenerateInitialQueens();

var startTime = DateTime.Now;
helper.Draw(generatedQueens.Select(x => x as QueenBase).ToList(), fieldSize);
Console.WriteLine("=============================");
var solution = astar.FindThePath(generatedQueens);
helper.Draw(solution.Queens.Select(x => x as QueenBase).ToList(), fieldSize);
Console.Write("The path was found in: ");
Console.WriteLine((DateTime.Now - startTime).TotalMilliseconds);
