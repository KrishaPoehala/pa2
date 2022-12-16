
using pa2.Ldfs;

var consoleHelper = new ConsoleHelper();
var (queensCount, fieldSize) = consoleHelper.GetDataFromConsole();
var ldfs = new LdfsSearch(queensCount, fieldSize);
var queens = ldfs.GenerateInitialQueens();
Console.WriteLine("The generated board: ");
consoleHelper.Draw(queens, 6);
var startTime = DateTime.Now;
var solution = ldfs.Search(queens);
Console.WriteLine("The found solution");
consoleHelper.Draw(solution, 6);
Console.Write("The solution was found in: ");
Console.WriteLine((DateTime.Now - startTime).TotalMilliseconds);
