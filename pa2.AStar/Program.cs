using pa2.AStar;

var helper = new ConsoleHelper();
var (queensCount, fieldSize) = helper.GetDataFromConsole();
var astar = new Search(queensCount, fieldSize);
var generatedQueens = astar.GenerateInitialQueens();

var startTime = DateTime.Now;
helper.Draw(generatedQueens, fieldSize);
Console.WriteLine("=============================");
var solution = astar.FindThePath(generatedQueens);
helper.Draw(solution.Queens, fieldSize);
Console.Write("The path was found in: ");
Console.WriteLine((DateTime.Now - startTime).TotalMilliseconds);
