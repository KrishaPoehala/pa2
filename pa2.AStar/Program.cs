using pa2.AStar;
using pa2.Common;
using pa2.Ldfs;

var helper = new ConsoleHelper();
var (queensCount, fieldSize) = helper.GetDataFromConsole();
var astar = new AStarSearch(queensCount, fieldSize);
var generatedQueens = astar.GenerateInitialQueens();

var startTime = DateTime.Now;
Console.WriteLine("A star search: ");
Console.WriteLine("Generated board: ");
helper.Draw(generatedQueens.Select(x => x as QueenBase).ToList(), fieldSize);
Console.WriteLine("=============================");
var solution = astar.FindThePath(generatedQueens);
helper.Draw(solution.Queens.Select(x => x as QueenBase).ToList(), fieldSize);
Console.Write("The path was found in: ");
Console.WriteLine((DateTime.Now - startTime).TotalMilliseconds);

startTime = DateTime.Now;
Console.WriteLine("\n Ldfs search: ");
var ldfs = new LdfsSearch(fieldSize, queensCount);
var ldfsSolution = ldfs.Search(ldfs.GenerateInitialQueens());
Console.WriteLine($"The ldfs found the solution in: {(DateTime.Now - startTime).TotalMilliseconds}");
helper.Draw(solution.Queens.Select(x => x as QueenBase).ToList(), fieldSize);